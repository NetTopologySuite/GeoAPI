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

set Target=v2.0
set OutputPath="%SolutionDir%Release\%Target%\AnyCPU"
set ObjOutputPath="%SolutionDir%GeoAPI\obj\%Target%\"
set Constants=TRACE;NET20
echo building for .NET Framework %Target%
rmdir /s/q "%OutputPath%" 2> nul
%msbuild% %SolutionDir%GeoAPI.sln /target:GeoAPI /property:Configuration=Release;TargetFrameworkVersion=%Target%;TargetFrameworkProfile=;OutputPath=%OutputPath%\;DefineConstants="%Constants%;BaseIntermediateOutputPath=%ObjOutputPath%" /verbosity:minimal

set Target=v3.5
set OutputPath="%SolutionDir%Release\%Target%\AnyCPU"
set ObjOutputPath="%SolutionDir%GeoAPI\obj\%Target%\"
set Constants=%Constants%;NET35
echo building for .NET Framework %Target%
rmdir /s/q "%OutputPath%" 2> nul
%msbuild% %SolutionDir%GeoAPI.sln /target:GeoAPI /property:Configuration=Release;TargetFrameworkVersion=%Target%;TargetFrameworkProfile=;OutputPath=%OutputPath%\;DefineConstants="%Constants%;BaseIntermediateOutputPath=%ObjOutputPath%" /verbosity:minimal

set Target=v4.0
set OutputPath="%SolutionDir%Release\%Target%\AnyCPU"
set ObjOutputPath="%SolutionDir%GeoAPI\obj\%Target%\"
set Constants=%Constants%;NET40
echo building for .NET Framework %Target%
rmdir /s/q "%OutputPath%" 2> nul
%msbuild% %SolutionDir%GeoAPI.sln /target:GeoAPI /property:Configuration=Release;TargetFrameworkVersion=%Target%;TargetFrameworkProfile=;OutputPath=%OutputPath%\;DefineConstants="%Constants%;BaseIntermediateOutputPath=%ObjOutputPath%" /verbosity:minimal

set Target=v4.0.3
set OutputPath="%SolutionDir%Release\%Target%\AnyCPU"
set ObjOutputPath="%SolutionDir%GeoAPI\obj\%Target%\"
set Constants=%Constants%
echo building for .NET Framework %Target%
rmdir /s/q "%OutputPath%" 2> nul
%msbuild% %SolutionDir%GeoAPI.sln /target:GeoAPI /property:Configuration=Release;TargetFrameworkVersion=%Target%;TargetFrameworkProfile=;OutputPath=%OutputPath%\;DefineConstants="%Constants%;BaseIntermediateOutputPath=%ObjOutputPath%" /verbosity:minimal

set Target=v4.5
set OutputPath="%SolutionDir%Release\%Target%\AnyCPU"
set ObjOutputPath="%SolutionDir%GeoAPI\obj\%Target%\"
set Constants=%Constants%
echo building for .NET Framework %Target%
rmdir /s/q "%OutputPath%" 2> nul
%msbuild% %SolutionDir%GeoAPI.sln /target:GeoAPI /property:Configuration=Release;TargetFrameworkVersion=%Target%;TargetFrameworkProfile=;OutputPath=%OutputPath%\;DefineConstants="%Constants%;BaseIntermediateOutputPath=%ObjOutputPath%" /verbosity:minimal

set Target=v4.0
set Profile=Profile336
set OutputPath="%SolutionDir%Release\PCL\AnyCPU"
set ObjOutputPath="%SolutionDir%GeoAPI\obj\PCL\"
set Constants=TRACE;PCL
echo building for .NET Framework %Target% %Profile%
rmdir /s/q "%OutputPath%" 2> nul
%msbuild% %SolutionDir%GeoAPI.sln /target:GeoAPI_PCL /property:Configuration=Release;TargetFrameworkVersion=%Target%;TargetFrameworkProfile=%Profile%;OutputPath=%OutputPath%\;DefineConstants="%Constants%;BaseIntermediateOutputPath=%ObjOutputPath%" /verbosity:minimal

REM echo building for .NET Core
REM %msbuild% %SolutionDir%GeoAPI.sln /target:GeoAPI_NetCore /verbosity:minimal

echo build complete.
