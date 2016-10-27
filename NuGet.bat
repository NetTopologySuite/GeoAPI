echo off

set SolutionDir=%~dp0
set GeoAPIVersion=1.7.5
set AsmFileVersion=%GeoAPIVersion%
set NuGetOutDir=%SolutionDir%Release
set NuGetVersion=%AsmFileVersion%
set NuGetCommand=%SolutionDir%.nuget\NuGet.exe

%NuGetCommand% update -self
%NuGetCommand% pack GeoAPI.nuspec -Version %NuGetVersion% -outputdirectory %NuGetOutDir% -symbols
