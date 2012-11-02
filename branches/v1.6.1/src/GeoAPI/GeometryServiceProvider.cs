using System;
using System.Collections.Generic;
using System.Reflection;

namespace GeoAPI
{
    /// <summary>
    /// Static class that provides access to a  <see cref="IGeometryServices"/> class.
    /// </summary>
    public static class GeometryServiceProvider
    {
        private static volatile IGeometryServices _instance;
        private static readonly object LockObject = new object();

        /// <summary>
        /// Gets or sets the <see cref="IGeometryServices"/> instance.
        /// </summary>
        public static IGeometryServices Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (LockObject)
                    {
                        if (_instance == null)
                            _instance = ReflectInstance();
                    }
                }
                return _instance;
            }

            set
            {
                if (value == null)
                    throw new ArgumentNullException("value", "You must not assign null to Instance!");

                lock (LockObject)
                {
                    _instance = value;
                }
            }
        }

        private static IEnumerable<Type> GetLoadableTypes(Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            try
            {
                return assembly.GetExportedTypes();
            }
            catch (TypeLoadException)
            {
                return new Type[0];
            }
            catch (ReflectionTypeLoadException ex)
            {
                var types = ex.Types;
                IList<Type> list = new List<Type>(types.Length);
                foreach (var t in types)
                    if (t != null && t.IsPublic)
                        list.Add(t);
                return list;
            }
        }

        private static IGeometryServices ReflectInstance()
        {
            var dom = AppDomain.CurrentDomain;
#if !SILVERLIGHT
            var a = dom.GetAssemblies();
#elif SILVERLIGHT
            var a = new [] { Assembly.GetCallingAssembly(), Assembly.GetExecutingAssembly() };
#endif
            foreach (var assembly in a)
            {
                // Take a look at issue 114: http://code.google.com/p/nettopologysuite/issues/detail?id=114
#if (!WINDOWS_PHONE)
                if (assembly is System.Reflection.Emit.AssemblyBuilder) continue;
#endif
                if (assembly.GetType().FullName == "System.Reflection.Emit.InternalAssemblyBuilder") continue;
#if !SILVERLIGHT
                if (assembly.GlobalAssemblyCache && assembly.CodeBase == Assembly.GetExecutingAssembly().CodeBase) continue;
#endif

                foreach (var t in GetLoadableTypes(assembly))
                {
                    if (t.IsInterface) continue;
                    if (t.IsAbstract) continue;
                    if (t.IsNotPublic) continue;
                    if (!typeof(IGeometryServices).IsAssignableFrom(t)) continue;

                    var constuctors = t.GetConstructors();
                    foreach (var constructorInfo in constuctors)
                    {
                        if (constructorInfo.IsPublic && constructorInfo.GetParameters().Length == 0)
                            return (IGeometryServices)Activator.CreateInstance(t);
                    }
                }
            }
            throw new InvalidOperationException("Cannot use GeometryServiceProvider without an assigned IGeometryServices class");
        }

#if SILVERLIGHT
        private static IEnumerable<Assembly> GetAssemblies(IEnumerable<System.Windows.AssemblyPart> parts)
        {
            yield return Assembly.GetCallingAssembly();
            yield return Assembly.GetExecutingAssembly();
#if !WINDOWS_PHONE
            foreach (var part in System.Windows.Deployment.Current.Parts)
            {
                var info = System.Windows.Application.GetResourceStream(new System.Uri(part.Source, System.UriKind.Relative));
                var asm = new System.Windows.AssemblyPart().Load(info.Stream);
                yield return asm;
            }
#endif
        }
#endif
    }
}
