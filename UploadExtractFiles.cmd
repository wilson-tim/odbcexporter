set source=G:\DWExtracts\CMS

cd %source%

for /F "tokens=1,2,3,4 delims=/ " %%A in ('date /t') do set today=%%D%%B%%C

echo %today% > %source%\Archive\FTP_Date.txt

cd %source%\Archive
ftp -i -s:%source%\UploadExtractFiles.ftp > %source%\FTP_Log.log 




