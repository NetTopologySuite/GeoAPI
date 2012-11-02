using System;

namespace GeoAPI
{
    /// <summary>
    /// Static class that provides access to a  <see cref="IGeometryServices"/> class.
    /// </summary>
    public static class GeometryServiceProvider
    {
        private static IGeometryServices _instance;
        private static readonly object LockObject = new object();

        /// <summary>
        /// Gets or sets the <see cref="IGeometryServices"/> instance.
        /// </summary>
        public static IGeometryServices Instance
        {
            get
            {
                lock (LockObject)
                {
                    return _instance ?? (_instance = ReflectInstance());
                }
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

        private static IGeometryServices ReflectInstance()
        {
#if !SILVERLIGHT
            var a = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in a)
            {
                foreach (var t in assembly.GetExportedTypes())
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
#endif
            throw new InvalidOperationException("Cannot use GeometryServiceProvider without an assigned IGeometryServices class");
        }
    }
}