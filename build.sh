#!/usr/bin/env bash

#exit if any command fails
set -e

dotnet --version
dotnet restore
dotnet build ./GeoAPI

xbuild /p:Configuration=CIBuild GeoAPI.sln

 
