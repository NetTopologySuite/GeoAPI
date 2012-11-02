using System;
using System.Collections.Generic;
using System.Text;
using GeoAPI.Indexing;
#if DOTNET35
using sl = System.Linq;
#else
using sl = GeoAPI.DataStructures;
#endif

namespace GeoAPI.DataStructures
{
    public class IntervalFactory : IBoundsFactory<Interval>
    {
        #region IBoundsFactory<Interval> Members

        public Interval CreateNullBounds()
        {
            return Interval.Zero;
        }

        public Interval CreateMinimumSpanningBounds(IEnumerable<Interval> bounds)
        {
            Interval interval = sl.Enumerable.FirstOrDefault(bounds);
            foreach (Interval i in sl.Enumerable.Skip(bounds, 1))
                interval = interval.ExpandToInclude(i);
            return interval;
        }

        #endregion
    }
}
