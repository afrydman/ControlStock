@echo off
setlocal enabledelayedexpansion	

REM ===================================
REM S3 Upload Script with Timestamped Logging
REM ===================================

REM Generate timestamp for log filename
for /f "tokens=2 delims==" %%I in ('wmic os get localdatetime /value') do set "dt=%%I"
set "TIMESTAMP=%dt:~0,4%%dt:~4,2%%dt:~6,2%_%dt:~8,2%%dt:~10,2%%dt:~12,2%"

REM Set log file path with timestamp (same directory as batch file)
set "LOG_FILE=%~dp0upload-log_%TIMESTAMP%.txt"

REM Alternative: If you want daily logs instead of per-execution
REM set "TIMESTAMP=%dt:~0,4%%dt:~4,2%%dt:~6,2%"
REM set "LOG_FILE=%~dp0upload-log_%TIMESTAMP%.txt"

REM Check parameters
if "%~3"=="" (
    echo ERROR: Missing parameters > "%LOG_FILE%"
    echo Usage: %0 ^<fileToUpload^> ^<bucketName^> ^<folder^> >> "%LOG_FILE%"
    exit /b 1
)

REM Assign parameters
set "FILE_TO_UPLOAD=%~1"
set "BUCKET_NAME=%~2"
set "FOLDER_NAME=%~3"

REM Remove trailing slash
if "%FOLDER_NAME:~-1%"=="/" set "FOLDER_NAME=%FOLDER_NAME:~0,-1%"

REM Check file exists
if not exist "%FILE_TO_UPLOAD%" (
    echo ERROR: File not found: %FILE_TO_UPLOAD% > "%LOG_FILE%"
    exit /b 2
)

REM Get filename
for %%F in ("%FILE_TO_UPLOAD%") do set "FILENAME=%%~nxF"

REM Build S3 path
set "S3_PATH=s3://%BUCKET_NAME%/%FOLDER_NAME%/%FILENAME%"

REM Log start
echo ======================================= > "%LOG_FILE%"
echo S3 Upload Log >> "%LOG_FILE%"
echo ======================================= >> "%LOG_FILE%"
echo Start Time: %date% %time% >> "%LOG_FILE%"
echo Source: %FILE_TO_UPLOAD% >> "%LOG_FILE%"
echo Destination: %S3_PATH% >> "%LOG_FILE%"
echo --------------------------------------- >> "%LOG_FILE%"

REM Execute upload (also capture output)
"C:\Program Files\Amazon\AWSCLIV2\aws.exe" s3 cp "%FILE_TO_UPLOAD%" "%S3_PATH%" >> "%LOG_FILE%" 2>&1

REM Capture exit code
set "EXIT_CODE=%ERRORLEVEL%"

REM Log result
echo --------------------------------------- >> "%LOG_FILE%"
if %EXIT_CODE% EQU 0 (
    echo End Time: %date% %time% >> "%LOG_FILE%"
    echo Status: SUCCESS - Upload completed >> "%LOG_FILE%"
    echo Exit Code: %EXIT_CODE% >> "%LOG_FILE%"
    echo Upload successful
    
    REM Optional: Return log filename for the calling program
    echo Log file: %LOG_FILE%
    
    exit /b 0
) else (
    echo End Time: %date% %time% >> "%LOG_FILE%"
    echo Status: ERROR - Upload failed >> "%LOG_FILE%"
    echo Exit Code: %EXIT_CODE% >> "%LOG_FILE%"
    echo Upload failed
    
    REM Optional: Return log filename for the calling program
    echo Log file: %LOG_FILE%
    
    exit /b %EXIT_CODE%
)