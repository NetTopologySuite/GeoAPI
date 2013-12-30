using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoAPI
{
    /// <summary>
    /// IClonable interface.
    /// </summary>
    public interface ICloneable
    {
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns></returns>
        object Clone();
    }
}
