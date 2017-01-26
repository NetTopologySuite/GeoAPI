echo off

set msbuild="C:\Program Files (x86)\MSBuild\14.0\bin\msbuild"
if not exist %msbuild% (
	set msbuild="C:\Program Files (x86)\MSBuild\12.0\bin\msbuild"
	if not exist %msbuild% (
		echo "Error trying to find MSBuild executable"
		exit 1
	)
)
set SolutionDir=%~dp0

echo building all projects in the solution (Release)

rmdir /s/q "%SolutionDir%Release"
mkdir "%SolutionDir%Release"

CALL :Build GeoAPI v2.0 "" v2.0 "TRACE;NET20"
CALL :Build GeoAPI v3.5 "" v3.5 "TRACE;NET20;NET35"
CALL :Build GeoAPI v4.0 "" v4.0 "TRACE;NET20;NET35;NET40"
CALL :Build GeoAPI v4.0.3 "" v4.0.3 "TRACE;NET20;NET35;NET40"
CALL :Build GeoAPI v4.5 "" v4.5 "TRACE;NET20;NET35;NET40"
CALL :Build GeoAPI_PCL v4.0 Profile336 PCL "TRACE;PCL"

echo building .NET Core
rmdir /s/q "%SolutionDir%GeoAPI.NetCore\bin\Release\netstandard1.0"
dotnet build -c Release %SolutionDir%GeoAPI.NetCore
mkdir "%SolutionDir%Release\netstandard1.0"
copy "%SolutionDir%GeoAPI.NetCore\bin\Release\netstandard1.0\GeoAPI.NetCore.*" "%SolutionDir%Release\netstandard1.0\*.*"

echo building for Windows CE
REM check this: https://gist.github.com/skarllot/4953ddb6e23d8a6f0816029c4155997a
set msbuild35="C:\Windows\Microsoft.NET\Framework\v3.5\MSBuild"
if not exist %msbuild% (
	echo "Error trying to find MSBuild 3.5 executable, cannot build for Windows CF"
	exit 1
)
%msbuild35% GeoAPI.vs2008.sln /target:GeoAPI_CF /p:Configuration=Release

echo build complete.

EXIT /B %ERRORLEVEL%

:Build
set Project=%~1
set TargetFX=%~2
set Profile=%~3
set TargetDir=%~4
set Constants=%~5

set OutputPath="%SolutionDir%Release\%TargetDir%\AnyCPU"
set ObjOutputPath="%SolutionDir%GeoAPI\obj\%TargetDir%\"
echo building for .NET Framework %Target% %Profile%
rmdir /s/q "%OutputPath%" 2> nul
%msbuild% %SolutionDir%GeoAPI.sln /target:%Project% /property:Configuration=Release;TargetFrameworkVersion=%TargetFX%;TargetFrameworkProfile=%Profile%;OutputPath=%OutputPath%\;DefineConstants="%Constants%;BaseIntermediateOutputPath=%ObjOutputPath%" /verbosity:minimal

REM Clean variables
set Project=
set TargetFX=
set Profile=
set TargetDir=
set Constants=
set OutputPath=
set ObjOutputPath=
EXIT /B 0