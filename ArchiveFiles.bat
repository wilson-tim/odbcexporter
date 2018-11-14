cd G:\DWExtracts\CMS\Data\
for /f %%f in ('dir /b G:\DWExtracts\CMS\Data\') do "C:\Program Files\7-Zip\7z.exe" a -tzip -sdel -bd -y -mx=1 -mmt=off G:\DWExtracts\CMS\Archive\%%~nf.gz %%~nf.txt > G:\DWExtracts\CMS\Logs\Archive.log
