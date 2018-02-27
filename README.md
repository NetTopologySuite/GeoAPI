# GeoAPI

GeoAPI project provides a common framework based on OGC/ISO standards to improve interoperability among .NET GIS projects. GeoAPI is used mostly as a base for [NTS](https://github.com/NetTopologySuite/NetTopologySuite/)

### Project status
Package | License | Release | Develop | NuGet (release) | NuGet (pre-release) 
------- | ------- | ------- | ------- | --------------- | ----------------------
GeoAPI  | [![LGPL licensed](https://img.shields.io/badge/license-LGPL-blue.svg)](https://github.com/NetTopologySuite/GeoAPI/blob/develop/LICENSE.md) | [![Travis](https://travis-ci.org/NetTopologySuite/GeoAPI.svg?branch=master)](https://travis-ci.org/NetTopologySuite/GeoAPI) | [![Travis](https://travis-ci.org/NetTopologySuite/GeoAPI.svg?branch=develop)](https://travis-ci.org/NetTopologySuite/GeoAPI) |  [![NuGet](https://img.shields.io/nuget/v/GeoAPI.svg?style=flat)](https://www.nuget.org/packages/GeoAPI/) | [![NuGet](https://img.shields.io/nuget/vpre/GeoAPI.svg?style=flat)](https://www.nuget.org/packages/GeoAPI/)
GeoAPI.Core | [![LGPL licensed](https://img.shields.io/badge/license-LGPL-blue.svg)](https://github.com/NetTopologySuite/GeoAPI/blob/develop/LICENSE.md) |  |  |  [![NuGet](https://img.shields.io/nuget/v/GeoAPI.Core.svg?style=flat)](https://www.nuget.org/packages/GeoAPI.Core/) | [![NuGet](https://img.shields.io/nuget/vpre/GeoAPI.Core.svg?style=flat)](https://www.nuget.org/packages/GeoAPI.Core/)
GeoAPI.CoordinateSystems | [![LGPL licensed](https://img.shields.io/badge/license-LGPL-blue.svg)](https://github.com/NetTopologySuite/GeoAPI/blob/develop/LICENSE.md) |  |  |  [![NuGet](https://img.shields.io/nuget/v/GeoAPI.CoordinateSystems.svg?style=flat)](https://www.nuget.org/packages/GeoAPI.CoordinateSystems/) | [![NuGet](https://img.shields.io/nuget/vpre/GeoAPI.CoordinateSystems.svg?style=flat)](https://www.nuget.org/packages/GeoAPI.CoordinateSystems/)

### Supported platforms

GeoAPI supports a wide range of .NET versions:

- net20
- net35-client
- net35-cf
- net40-client
- net403-client
- net45
- netstandard1.0
- netstandard2.0
- portable40-net40+sl5+win8+wp8+wpa81
- portable40-net403+sl5+win8+wp8+wpa81

### Development & building

Because of the wide support of platforms, building GeoAPI can be a pain. Make sure you install all the targeting packs. We provided a few scripts for this in the root folder of this repo.
