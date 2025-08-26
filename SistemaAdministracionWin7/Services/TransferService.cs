using DTO.BusinessEntities;
using Helper.LogService;
using Repository.Repositories;
using Repository.Repositories.CajaRepository;
using Repository.Repositories.ValeRepository;
using Services.JsonSerializationService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Services
{

    public interface ITransferService
    {
        bool SendRemitoToLocal(RemitoData remito);
        bool ValidateTransferRemito(RemitoData remito);
        void CleanDirectory(DirectoryType dir);
    }

    public enum DirectoryType
    {
        TransferenciaRemito=1,
        Transferenciaventas=2,
    }

    public class TransferService : ITransferService
    {
        protected readonly ITransferRepository _repo;
        private readonly IJsonSerializationService _jsonService;
        private const string LOGGER_ID = "PdfService";
        private const string REMITO_FILENAME_FORMAT = "remito_{0}_{1:yyyyMMddHHmmss}.json";
        private const string REMITO_FILENAME_PATTERN = @"remito_([a-fA-F0-9\-]+)_(\d{14})\.json";

        private const string LOCALES_Remitos_Pending_FOLDER = "Pending";
        private const string LOCALES_Remitos_RECIBIDOS_FOLDER = "Processed";
        private const string LOCALES_Remitos_PROCESADOS_FOLDER = "Done";
        private const string LOCALES_Remitos_Error_FOLDER = "Error";

        private const string BUCKET = "file-transfer-mell";
        private const string BUCKET_FOLDER_DELIVEYNOTES = "Deliverynotes";

        private string CENTRAL_REMITOS_FOLDER = string.Format("{0}/Deliverynotes/", ConfigurationManager.AppSettings["FTPBase"]);
        //private string LOCALES_MY_REMITOS_A_PROCESAR_FOLDER = string.Format("{0}/{1}/", ConfigurationManager.AppSettings["FTPBase"], LOCALES_Remitos_Pending_FOLDER);
        //private string LOCALES_MY_REMITOS_PROCESADOS_FOLDER = string.Format("{0}/{1}/", ConfigurationManager.AppSettings["FTPBase"], LOCALES_PROCESADOS_FOLDER);

        //static FtpService()
        //{
        //    // Initialize logger if not already exists
        //    if (!Log.Exists(LOGGER_ID))
        //    {
        //        string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "PdfService.log");
        //        Log.New(LOGGER_ID, logPath, LogLevel.Debug);
        //    }
        //}

        public TransferService()
        {
            if (!Log.Exists(LOGGER_ID))
            {
                string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "TransferService.log");
                Log.New(LOGGER_ID, logPath, LogLevel.Debug);
            }

            _repo = new FileTransferRepository() ;
            _jsonService = new JsonSerializationService.JsonSerializationService();
        }
        public TransferService(ITransferRepository repo)
        {   // Initialize logger if not already exists
            if (!Log.Exists(LOGGER_ID))
            {
                string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "TransferService.log");
                Log.New(LOGGER_ID, logPath, LogLevel.Debug);
            }

            _repo = repo;
            _jsonService = new JsonSerializationService.JsonSerializationService();
        }



        //public static string GetSHA256Checksum(string filePath)
        //{
        //    using (var sha256 = SHA256.Create())
        //    {
        //        using (var stream = File.OpenRead(filePath))
        //        {
        //            byte[] hash = sha256.ComputeHash(stream);
        //            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        //        }
        //    }
        //}

        public void CleanDirectory(DirectoryType dir)
        {
            throw new NotImplementedException();
        }

        public bool UploadToS3(string filePath, string bucket, string folder)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "C:\\Ftp\\Central\\RemitosEnviar\\Scripts\\sendRemitos.bat",
                Arguments = $"\"{filePath}\" \"{bucket}\" \"{folder}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(startInfo))
            {
                string output = process.StandardOutput.ReadToEnd();
                string errors = process.StandardError.ReadToEnd();
                process.WaitForExit();

                // Check exit code
                if (process.ExitCode == 0)
                {
                    Console.WriteLine("Upload successful!");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Upload failed with code: {process.ExitCode}");
                    Console.WriteLine($"Error: {errors}");
                    return false;
                }
            }
        }


        //this should be used by pventa for now.
        //public List<string> CheckNewRemitos(List<string> currentRemitosId)
        //{
        //    List<string> newRemitosfilenames = new List<string>();


        //    foreach (string file in Directory.EnumerateFiles(LOCALES_MY_REMITOS_A_PROCESAR_FOLDER, "remito_*.json"))
        //    {
        //        var match = Regex.Match(file, REMITO_FILENAME_PATTERN);
        //        if (match.Success)
        //        {
        //            Guid remitoId = Guid.Parse(match.Groups[1].Value);
        //            if (!currentRemitosId.Contains(remitoId.ToString()))
        //                newRemitosfilenames.Add(remitoId.ToString());

        //            //DateTime timestamp = DateTime.ParseExact(match.Groups[2].Value, "yyyyMMddHHmmss", null);
        //        }
        //    }

           


        //    return newRemitosfilenames;
        //}

        public bool SendRemitoToLocal(RemitoData remito)
        {
            try
            { 
                FileTransferData datita = new FileTransferData();
                datita.ID = Guid.NewGuid();
                datita.Enable = true;
                datita.ToLocalID = remito.LocalDestino.ID.ToString();
                datita.FromLocalID = remito.Local.ID.ToString();
                datita.Completed = false;

                //first generate the file.
                var pathNewFile = RemitoToFile(remito);

                if (pathNewFile == string.Empty)
                    throw new Exception("Path Empty cant continue");
                
                
                datita.LocalFileName = pathNewFile;
                datita.Description = GetDescription(remito);
                datita.Date = DateTime.Now;

                //then call the bat to upload to s3
                var uploadResult = UploadToS3(pathNewFile, BUCKET, string.Format("{0}/{1}/", BUCKET_FOLDER_DELIVEYNOTES, remito.LocalDestino.ID.ToString(), LOCALES_Remitos_Pending_FOLDER));

                if(uploadResult)
                {

                    datita.Completed = true;
                        //move the file in local to proccessed?
                        //where I should display that ?
                }
                else
                {
                    datita.Completed = false;
                    datita.Error = "Error!";
                    //move the file to error status ?
                    //where I should display that ?
                }


                _repo.Insert(datita);
            }
            catch (Exception ex)
            {
                
                Log.WriteError(LOGGER_ID, $"Validation error in SendRemitoToLocal: {ex.Message}", ex);
                return false;
            }







            return true;
        }

        public List<FileTransferData> GetAll()
        {


            return _repo.GetAll();
        }


        private string GetDescription(RemitoData remito)
        {
            return string.Format("Remito nro {2} enviado desde {0} hacia {1}", remito.Local.Description, remito.LocalDestino.Description, remito.NumeroCompleto);
        }

        private string RemitoToFile(RemitoData remito)
        {
            try
            {
                Log.WriteDebug(LOGGER_ID, $"Starting SendRemitoToLocal for remito ID: {remito?.ID}");

                // Validate input
                if (remito == null)
                {
                    Log.WriteError(LOGGER_ID, "SendRemitoToLocal called with null remito");
                    throw new ArgumentNullException(nameof(remito), "Remito cannot be null");
                }

                if (remito.LocalDestino == null || remito.LocalDestino.ID == Guid.Empty)
                {
                    Log.WriteError(LOGGER_ID, $"Remito {remito.ID} has invalid LocalDestino");
                    throw new ArgumentException("LocalDestino must be specified with a valid ID", nameof(remito));
                }

                Log.WriteInfo(LOGGER_ID, $"Processing remito {remito.ID} from Local {remito.Local?.ID} to LocalDestino {remito.LocalDestino.ID}");

                // Create directory structure: LocalDestinoGuid/remitos/
                string localDestinoFolder = remito.LocalDestino.ID.ToString();


                // Generate filename: remito_[RemitoID]_[DateTime].json

                string fileName = string.Format(REMITO_FILENAME_FORMAT, remito.ID, DateTime.Now);
                // Build the full relative path
                string relativePath = Path.Combine(CENTRAL_REMITOS_FOLDER, localDestinoFolder, fileName);

                Log.WriteDebug(LOGGER_ID, $"Saving remito to path: {relativePath}");

                // Save the remito to file using the JsonSerializationService
                _jsonService.JsonToFile(remito, relativePath);

                Log.WriteInfo(LOGGER_ID, $"Remito {remito.ID} successfully saved to {relativePath}. Estado: {remito.estado}, CantidadTotal: {remito.CantidadTotal}");

                return relativePath;
            }
            catch (ArgumentNullException ex)
            {
                Log.WriteError(LOGGER_ID, $"Validation error in SendRemitoToLocal: {ex.Message}", ex);
                return string.Empty;
            }
            catch (ArgumentException ex)
            {
                Log.WriteError(LOGGER_ID, $"Invalid argument in SendRemitoToLocal: {ex.Message}", ex);
                return string.Empty;
            }
            catch (Exception ex)
            {
                Log.WriteError(LOGGER_ID, $"Unexpected error sending remito {remito?.ID} to local: {ex.Message}", ex);
                return string.Empty;
            }
        }



        //quiza esto es para hacernos los lindos.
        public bool ValidateTransferRemito(RemitoData remito)
        {
            var checksum1 = "";
            var checksum2 = "";

            //chequeo en absoultepdfPath/LocalGuid?/remitosRecibidos/idremito.txt y lo abro
            //checkeo hash de ese file vs hash de objeto
            return (checksum1 == checksum2);
        }
    }
}
