@echo off
set version=0.1.0-unstable
set zip="packages\7-Zip.CommandLine.9.20.0\tools\7za.exe"
set msbuildcmd="C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\Tools\VsMSBuildCmd.bat"

if not exist %msbuildcmd% goto error
call %msbuildcmd%

:cleanup
if not exist build mkdir build
del /s /f /q build\*

if not exist build\bin mkdir build\bin
if not exist build\dist mkdir build\dist

:build
nuget.exe restore
msbuild Kyrodan.HiDriveSdk.sln /p:Configuration=Release /t:Clean,Build /fl /flp:logfile=build\build.log
if %errorlevel% NEQ 0 goto error

:package
xcopy src\Kyrodan.HiDrive\bin\Release\*.* build\bin
rem del build\bin\*.pdb build\bin\*.xml 
%zip% a -tzip build\dist\Kyrodan.HiDrive-%version%.zip .\build\bin\*



:final
goto end

:error
echo !
echo !
echo !
echo Fehler im Buildvorgang
echo !
echo !
echo !


:end
pause
