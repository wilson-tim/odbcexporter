' -----------------------------------------------------------------
' File:         exportandarchive.vbs
' Author:       Richard Williams (rwilliams@exodus.co.uk)
' Created:      25th November 2008
' Modified:     
' Version:      1.0
' Description:  runs two C# applications
'               travelinkexporter which exports certain tables
'               to a csv files
'               filearchiver - archives files to an archive
'
' ******************************************
' *** WARNING - REQUIRES 2 FILES OR MORE ***
' ******************************************
' -----------------------------------------------------------------


'******  Configuration  ******

Exporterprog = "E:\ExtractFiles\IEST\V2TExport\TravelinkExporter.exe autostart"
filearchiveprog = "E:\ExtractFiles\IEST\farchive\Archiver.exe autostart"
logfile = "E:\ExtractFiles\IEST\Logs\export.log"
tidy = "E:\ExtractFiles\IEST\TidyUp.cmd"
Const ForReading = 1, ForWriting = 2, ForAppending = 8


'****** End Configuration  ******



'****** Start the process  ******

Set objShell = WScript.CreateObject("WScript.Shell")

syslog "",0
syslog "Starting export",1

exportobj = objShell.run(Exporterprog,1,true)

' check for successful closure
if exportobj = 0 then
  syslog "Finished export",1
else
  syslog "Problem with export",1
end if

syslog "Starting archive",1

archobj = objShell.run(filearchiveprog,1,true)

' check for successful archive
if archobj = 0 then
  syslog "Finished archive",1
else
  syslog "Problem with archive",1
end if

syslog "Starting Tidy-up",1

tidyobj = objShell.run(tidy,1,true)

' check for successful Tidy-up
if tidyobj = 0 then
  syslog "Finished Tidy-up",1
else
  syslog "Problem with Tidy-up",1
end if

if exportobj = 0 And archobj = 0 And tidyobj = 0 then
  syslog "Finished job",1
else
  syslog "job finished with problems",1
end if

syslog "",0

Set objShell = nothing
Set exportobj = nothing	
Set archobj = nothing
Set tidyobj = nothing


'****** End of process  ******



'****** Process routines  ******


Sub syslog (sText,timestamp)

   'Write LogFile Events
   'Timestamp 1 for yes 0  for no

    Dim lfso, lf
    Set lfso = CreateObject("Scripting.FileSystemObject")
    Set lf = lfso.OpenTextFile(logfile, ForAppending, true)
    if timestamp = 1 then
    lf.Write now & " " & sText & vbcrlf
    else
       lf.Write sText & vbcrlf
    end if
    lf.close
    set lf = nothing

End Sub



'****** END  ******