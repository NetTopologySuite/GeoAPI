echo off

set SolutionDir=%~dp0
set NuGetOutDir=%SolutionDir%Release
set NuGetCommand=%SolutionDir%.nuget\NuGet.exe
set VersionInfoCommand=%SolutionDir%.nuget\VersionInfo.vbs

for /f %%i in ('cscript //nologo %VersionInfoCommand% %NuGetOutDir%\v4.0\AnyCPU\GeoAPI.dll') do set GeoAPIVersion=%%i
set AsmFileVersion=%GeoAPIVersion%
set NuGetVersion=%AsmFileVersion%

%NuGetCommand% update -self
%NuGetCommand% pack GeoAPI.nuspec -Version %NuGetVersion% -outputdirectory %NuGetOutDir% -symbols
