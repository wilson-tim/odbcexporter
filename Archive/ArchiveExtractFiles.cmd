@echo off
REM **********************************************************
REM ArchiveExtractFiles.cmd
REM **********************************************************
REM Archives extracted .gz files from a folder older than $foo days
REM
REM Parameters:
REM -pPATH - Path to check for files
REM -dDAYS - How many days worth of files to retain (default to 5)
REM -TEST - TEST mode. Displays the files rather than DELETES them
SET _path=.\
SET _days=5
SET _ext=gz
SET _mode=DELETE


IF [%1]==[] goto do_work

REM Process parameters
:process_parameters
SET _currentparam=%1%

ECHO %_currentparam%
IF %_currentparam:~0,2%==-p set _path=%_currentparam:~2%
IF %_currentparam:~0,2%==-d set _days=%_currentparam:~2%
IF %_currentparam:~0,2%==-x set _ext=%_currentparam:~2%
IF %_currentparam%==-TEST set _mode=TEST

IF [%_currentparam%]==[] GOTO check_parameters

SHIFT
IF [%1]==[] GOTO check_parameters
GOTO process_parameters

:check_parameters
ECHO Parameters
ECHO Path is %_path%
ECHO Days are %_days%
ECHO Extension is %_ext%
ECHO Mode is %_mode%
IF [%_path%]==[] GOTO invalid_parameters

:do_work

IF %_mode%==DELETE forfiles -p %_path% -m *.%_ext% -d -%_days% -c "cmd /c del @PATH"
IF %_mode%==TEST forfiles -p %_path% -m *.%_ext% -d -%_days% -c "cmd /c echo @PATH"

goto end

:invalid_parameters
ECHO Invalid Parameters.
ECHO.
ECHO Usage is:
ECHO     ArchiveExtractFiles.cmd -p "PATH" [-xEXT] [-dDAYS] [-TEST]
ECHO Where:
ECHO     -p Denotes the path to extract
ECHO     -x Denotes the file extension to delete (default gz)
ECHO     -d Denotes the number of days to keep files (default 7)
ECHO     -TEST will run in test mode (will display the files that will be deleted)
ECHO.

:end
