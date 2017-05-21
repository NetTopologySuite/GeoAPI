echo off

set SolutionDir=%~dp0
set NuGetOutDir=%SolutionDir%Release\AnyCPU
set NuGetCommand=%SolutionDir%.nuget\NuGet.exe
set VersionInfoCommand=%SolutionDir%.nuget\VersionInfo.vbs

for /f %%i in ('cscript //nologo %VersionInfoCommand% %NuGetOutDir%\net45\GeoAPI.dll') do set GeoAPIVersion=%%i
set AsmFileVersion=%GeoAPIVersion%
set NuGetVersion=%AsmFileVersion%
if not "%~1"=="" SET NuGetVersion=%NuGetVersion%-%~1

%NuGetCommand% update -self
%NuGetCommand% pack GeoAPI.nuspec -Version %NuGetVersion% -outputdirectory %NuGetOutDir% -symbols
