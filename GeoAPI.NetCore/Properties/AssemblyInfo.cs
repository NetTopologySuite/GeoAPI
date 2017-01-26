using System;
using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle(Consts.Title)]
[assembly: AssemblyDescription(Consts.Description)]
[assembly: AssemblyCopyright(Consts.Copyright)]
[assembly: AssemblyCulture("")]
[assembly: AssemblyVersion(Consts.Version)]
[assembly: AssemblyConfiguration(Consts.Configuration)]
[assembly: AssemblyCompany(Consts.Company)]
[assembly: AssemblyProduct(Consts.Product)]
[assembly: AssemblyTrademark("")]

#if !NET_CORE
[assembly: CLSCompliant(Consts.CLSCompliant)]
#endif

#if !PCL && !NET_CORE
// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(Consts.ComVisible)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid(Consts.Guid)]
#endif