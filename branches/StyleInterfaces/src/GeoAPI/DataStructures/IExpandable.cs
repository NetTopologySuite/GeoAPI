using System;
using System.Collections.Generic;
using System.Text;

namespace GeoAPI.DataStructures
{
    public interface IExpandable<T>
    {
        T ExpandToInclude(T item);
    }
}
