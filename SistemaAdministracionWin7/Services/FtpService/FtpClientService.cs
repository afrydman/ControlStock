using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using Persistence.LogService;

namespace Services.FtpService
{
    public class FtpClientService
    {
        private readonly string _ftpServer;
        private readonly string _username;
        private readonly string _password;
        private readonly FtpLogger _logger;

        public FtpClientService(string ftpServer, string username, string password)
        {
            _ftpServer = ftpServer.TrimEnd('/');
            _username = username;
            _password = password;
            _logger = new FtpLogger("FtpClient");
        }

        public bool UploadFile(string localFilePath, string remoteFilePath)
        {
            var stopwatch = Stopwatch.StartNew();
            var fileInfo = new FileInfo(localFilePath);
            var fileSize = fileInfo.Exists ? fileInfo.Length : 0;
            
            _logger.LogUploadStart(localFilePath, remoteFilePath, fileSize);
            
            try
            {
                string ftpUrl = $"{_ftpServer}/{remoteFilePath.TrimStart('/')}";
                
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(_username, _password);
                request.UseBinary = true;

                // Read local file
                byte[] fileContents = File.ReadAllBytes(localFilePath);
                request.ContentLength = fileContents.Length;

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(fileContents, 0, fileContents.Length);
                }

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    stopwatch.Stop();
                    _logger.LogUploadComplete(Path.GetFileName(localFilePath), fileContents.Length, stopwatch.ElapsedMilliseconds, true);
                    return response.StatusCode == FtpStatusCode.ClosingData;
                }
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogUploadComplete(Path.GetFileName(localFilePath), fileSize, stopwatch.ElapsedMilliseconds, false);
                _logger.LogError("UPLOAD", $"Failed to upload {Path.GetFileName(localFilePath)}", ex);
                return false;
            }
        }

        public bool DownloadFile(string remoteFilePath, string localFilePath)
        {
            var stopwatch = Stopwatch.StartNew();
            _logger.LogDownloadStart(remoteFilePath, localFilePath);
            
            try
            {
                string ftpUrl = $"{_ftpServer}/{remoteFilePath.TrimStart('/')}";
                
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(_username, _password);
                request.UseBinary = true;

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (FileStream fileStream = File.Create(localFilePath))
                {
                    responseStream.CopyTo(fileStream);
                }

                stopwatch.Stop();
                var fileInfo = new FileInfo(localFilePath);
                var fileSize = fileInfo.Exists ? fileInfo.Length : 0;
                _logger.LogDownloadComplete(Path.GetFileName(localFilePath), fileSize, stopwatch.ElapsedMilliseconds, true);
                return true;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogDownloadComplete(Path.GetFileName(localFilePath), 0, stopwatch.ElapsedMilliseconds, false);
                _logger.LogError("DOWNLOAD", $"Failed to download {Path.GetFileName(remoteFilePath)}", ex);
                return false;
            }
        }

        public string[] ListFiles(string remotePath)
        {
            try
            {
                Log($"Listing files in: {remotePath}");

                string ftpUrl = $"{_ftpServer}/{remotePath.TrimStart('/')}";
                
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(_username, _password);

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    string fileList = reader.ReadToEnd();
                    return fileList.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                }
            }
            catch (Exception ex)
            {
                Log($"List files failed: {ex.Message}");
                return new string[0];
            }
        }

        public bool CreateDirectory(string remotePath)
        {
            try
            {
                Log($"Creating directory: {remotePath}");

                string ftpUrl = $"{_ftpServer}/{remotePath.TrimStart('/')}";
                
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.Credentials = new NetworkCredential(_username, _password);

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    Log($"Directory created: {response.StatusDescription}");
                    return response.StatusCode == FtpStatusCode.PathnameCreated;
                }
            }
            catch (WebException webEx)
            {
                // Check if directory already exists
                var response = webEx.Response as FtpWebResponse;
                if (response != null && response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    Log($"Directory already exists: {remotePath}");
                    return true; // Directory already exists, that's OK
                }
                Log($"Create directory failed: {webEx.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Log($"Create directory failed: {ex.Message}");
                return false;
            }
        }

        public bool CreateDirectoryRecursive(string remotePath)
        {
            try
            {
                var pathParts = remotePath.TrimStart('/').Split('/');
                var currentPath = "";

                foreach (var part in pathParts)
                {
                    if (string.IsNullOrWhiteSpace(part)) continue;
                    
                    currentPath = string.IsNullOrEmpty(currentPath) ? part : $"{currentPath}/{part}";
                    
                    if (!DirectoryExists(currentPath))
                    {
                        CreateDirectory(currentPath);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Log($"Create directory recursive failed: {ex.Message}");
                return false;
            }
        }

        public bool DirectoryExists(string remotePath)
        {
            try
            {
                var files = ListFiles(remotePath);
                return true; // If we can list files, directory exists
            }
            catch
            {
                return false;
            }
        }

        public bool FileExists(string remoteFilePath)
        {
            try
            {
                string ftpUrl = $"{_ftpServer}/{remoteFilePath.TrimStart('/')}";
                
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
                request.Method = WebRequestMethods.Ftp.GetFileSize;
                request.Credentials = new NetworkCredential(_username, _password);

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    return true;
                }
            }
            catch (WebException)
            {
                return false;
            }
        }

        public bool DeleteFile(string remoteFilePath)
        {
            try
            {
                Log($"Deleting file: {remoteFilePath}");

                string ftpUrl = $"{_ftpServer}/{remoteFilePath.TrimStart('/')}";
                
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                request.Credentials = new NetworkCredential(_username, _password);

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    Log($"File deleted: {response.StatusDescription}");
                    return response.StatusCode == FtpStatusCode.FileActionOK;
                }
            }
            catch (Exception ex)
            {
                Log($"Delete file failed: {ex.Message}");
                return false;
            }
        }
    }
}