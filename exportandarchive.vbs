' -----------------------------------------------------------------
' File:         exportandarchive.vbs
' Author:       Richard Williams (rwilliams@exodus.co.uk)
' Created:      25th November 2008
' Modified:     28th April 2017
' Version:      1.0
' Description:  runs two C# applications
'               travelinkexporter which exports certain tables
'               to a csv files
'               filearchiver - archives files to an archive
'             
' 28/04/2017    tthsm4   Modified to Loop 5 Times if the extract has problems.
'
' ******************************************
' *** WARNING - REQUIRES 2 FILES OR MORE ***
' ******************************************
' -----------------------------------------------------------------


'******  Configuration  ******

Exporterprog    = "G:\DWExtracts\CMS\V2TExport\OdbcExporter.exe autostart"
filearchiveprog = "G:\DWExtracts\CMS\farchive\Archiver.exe autostart"
logfile         = "G:\DWExtracts\CMS\Logs\export.log"
Prep            = "G:\DWExtracts\CMS\Prep.cmd"
copytoboxi      = "G:\DWExtracts\CMS\UploadExtractFiles.cmd"
tidy            = "G:\DWExtracts\CMS\TidyUp.cmd"

Const ForReading = 1, ForWriting = 2, ForAppending = 8

strFolder           = "G:\DWExtracts\CMS\Data"
set objFS           = CreateObject("Scripting.FileSystemObject")
set objFolder       = objFS.GetFolder(strFolder)
expected            = 5
value               = 0
counter             = 0
exportok            = "N"

'****** End Configuration  ******

'****** Start the process  ******

Set objShell = WScript.CreateObject("WScript.Shell")

syslog "",0
syslog "Clear Workspace",1

Prepobj = objShell.run(Prep,1,true)

' check for successful Workspace Clearance
if Prepobj = 0 then
  syslog "Clear Workspace",1
else
  syslog "Problem with Clearing Workspace",1
end if

syslog "Starting export",1

DO  
' value < expected 
   exportobj = objShell.run(Exporterprog,1,true)

   ' check for successful closure
   if exportobj = 0 then
      syslog "Finished export",1
      exportok = "Y"
   else
     syslog "Problem with export",1
   end if

   syslog "Check Extract",1

   value=0

   Go(objFolder)

   syslog "Check Value - " & Value ,1

REM
REM If the Value returned from the Function does not match the expected number of files
REM Then Sleep for 5 minutes, before looping but 5 times only
REM

   IF NOT value = expected THEN 

      WScript.Sleep(1000 * 60 * 5) 

      counter=counter+1

      IF counter = 3 THEN
         exportobj = counter
      END IF

   ELSE 
      counter=3
   END IF

LOOP UNTIL counter =3

' Archive
   IF exportok = "Y" then

      syslog "Starting archive",1

      SET archShell = WScript.CreateObject("WScript.Shell")
      archobj = archShell.run("G:\DWExtracts\CMS\ArchiveFiles.bat",1,true)

   ELSE
      syslog "___________________________",1
      syslog "Extract Problem please investigate!",1
      syslog "___________________________",1
   END IF

' check for successful archive
if archobj = 0 then
  syslog "Finished archive",1
else
  syslog "Problem with archive",1
end if

syslog "Copy to BOXI",1


SET copyShell = WScript.CreateObject("WScript.Shell")
copytoboxiobj = 0
'copyShell.run(copytoboxi,1,true)

' check for successful Tidy-up
if copytoboxiobj = 0 then
  syslog "Copy to BOXI",1
else
  syslog "Problem with Copy to BOXI",1
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
REM
REM Subroutine to count the nunmber of files in the Object Directory
REM
Sub Go(objDIR)
  If objDIR <> "\System Volume Information" Then
    For Each eFolder in objDIR.SubFolders
        Go eFolder
    Next
    For Each strFiles In objDIR.Files
        strFileName = strFiles.Name
        strFilePath = strFiles.Path         
            If DateDiff("d",strFiles.DateLastModified,Now) = 0 Then
                value = value+1
        End If 
    Next  
  End If  
End Sub


'****** END  ******