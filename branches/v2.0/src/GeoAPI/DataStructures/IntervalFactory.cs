using System;
using System.Collections.Generic;
using System.Text;
using GeoAPI.Indexing;

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
            Interval interval = Enumerable.FirstOrDefault(bounds);
            foreach (Interval i in Enumerable.Skip(bounds, 1))
                interval = interval.ExpandToInclude(i);
            return interval;
        }

        #endregion
    }
}
