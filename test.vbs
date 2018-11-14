' -----------------------------------------------------------------
' File:         test.vbs
' Author:       Steve Martin
' Created:      24th May 2017
' Modified:     24th May 2017
' Version:      1.0
' Description:  Simply Tests that the Schedule can run this
'               
' ******************************************
' *** WARNING - REQUIRES 2 FILES OR MORE ***
' ******************************************
' -----------------------------------------------------------------

counter             = 0
'******  Configuration  ******

logfile         = "G:\DWExtracts\CMS\Logs\test.log"

Const ForReading = 1, ForWriting = 2, ForAppending = 8

'****** End Configuration  ******

'****** Start the process  ******
DO  
' value < expected 
   syslog "--------------------",0
   syslog "Testing Schedue Task",1
   syslog "Check Value - " & counter ,1
   syslog "--------------------",0
   counter=counter+1
LOOP UNTIL counter =5

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