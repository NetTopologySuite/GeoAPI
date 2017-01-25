#!/usr/bin/env bash

#exit if any command fails
set -e

xbuild /p:Configuration=CIBuild GeoAPI.sln

dotnet build ./GeoAPI.NetCore
