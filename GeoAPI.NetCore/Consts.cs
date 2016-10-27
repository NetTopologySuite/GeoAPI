internal static class Consts
{
    public const string Version = "1.7.5";
    public const string Title = "GeoAPI";
    public const string Description = "GeoAPI Version that matches NTS v.1.14";
    public const string Company = "NetTopologySuite - Team";
    public const string Copyright = "Copyright ©  2007-2016";
    public const bool ComVisible = false;
    public const bool CLSCompliant = true;

#if DEBUG
    public const string Configuration = "Debug";
#else
    public const string Configuration = "Stable";
#endif

#if NET40
    public const string Product = "GeoAPI";
    public const string Guid = "b6726fc4-0319-4a6d-84f5-aafc6ba530e3";
#elif NET35
    public const string Product = "GeoAPI.Net35";
    public const string Guid = "8de32941-f422-41e6-a7e5-964bb4ab2d3b";
#elif NET20
    public const string Product = "GeoAPI.Net20";
    public const string Guid = "2ac6149a-cb66-48d3-9c2b-6a5c19dcf203";
#elif NET_CORE
    public const string Product = "GeoAPI.NetCore";
    public const string Guid = "eef37cab-4f83-4366-8533-3d7c16b449c7";
#elif WindowsCE
    public const string Product = "GeoAPI.Net35CF";
    public const string Guid = "8ce966f8-d4fd-4437-a79c-314d9632384a";
#elif PCL
    public const string Product = "GeoAPI.PCL";
#endif
}