cd G:\DWExtracts\CMS\Data_Mar2018\
for /f %%f in ('dir /b G:\DWExtracts\CMS\Data_Mar2018\') do "C:\Program Files\7-Zip\7z.exe" a -tzip -sdel -bd -y -mx=1 -mmt=off G:\DWExtracts\CMS\Data_Mar2018\%%~nf.gz %%~nf.txt > G:\DWExtracts\CMS\Logs\Archive_Mar2018.log
