internal static class Consts
{
    public const string Version = "1.7.5";
    public const string Title = "GeoAPI";
    public const string Description = "GeoAPI Version that matches NTS v.1.15";
    public const string Company = "NetTopologySuite - Team";
    public const string Copyright = "Copyright © NetTopologySuite - Team 2007-2017";
    public const bool ComVisible = false;
    public const bool CLSCompliant = true;

    private const string FullFrameworkGuid = "b6726fc4-0319-4a6d-84f5-aafc6ba530e3";

#if DEBUG
    public const string Configuration = "Debug";
#else
    public const string Configuration = "Stable";
#endif

#if WindowsCE
    public const string Product = "GeoAPI.Net35CF";
    public const string Guid = "8ce966f8-d4fd-4437-a79c-314d9632384a";
#elif NET45
    public const string Product = "GeoAPI";
    public const string Guid = FullFrameworkGuid;
#elif NET403
    public const string Product = "GeoAPI.Net403";
    public const string Guid = FullFrameworkGuid;
#elif NET40
    public const string Product = "GeoAPI.Net40";
    public const string Guid = FullFrameworkGuid;
#elif NET35
    public const string Product = "GeoAPI.Net35";
    public const string Guid = FullFrameworkGuid;
#elif NET20
    public const string Product = "GeoAPI.Net20";
    public const string Guid = FullFrameworkGuid;
#elif NETSTANDARD1_0
    public const string Product = "GeoAPI.NetStandard10";
#elif PCL
    public const string Product = "GeoAPI.PCL";
#endif
}
