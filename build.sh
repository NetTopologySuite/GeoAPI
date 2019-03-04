#!/usr/bin/env bash

#exit if any command fails
set -e

# Build the project
#
dotnet msbuild GeoAPI.sln /m "/t:Restore;Build" /p:Configuration=Release /v:minimal /p:WarningLevel=3

# Run unit tests
#
dotnet test test/GeoAPI.Tests --no-build --no-restore -c Release
