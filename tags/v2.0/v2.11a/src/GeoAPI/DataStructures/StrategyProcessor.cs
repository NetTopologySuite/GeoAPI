//
// This file is part of GeoAPI.Net.
// GeoAPI.Net is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// GeoAPI.Net is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.

// You should have received a copy of the GNU Lesser General Public License
// along with GeoAPI.Net; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA using System;

// This file Copyright Newgrove Consultants Ltd 2008
// Author: John Diss

using System;
using System.Collections.Generic;

namespace GeoAPI.DataStructures
{
    public class StrategyProcessor<TInput, TOutput> : IStrategyProcessor<TInput, TOutput>
    {
        private readonly List<IStrategy<TInput, TOutput>> _strategies = new List<IStrategy<TInput, TOutput>>();

        public StrategyProcessor(IEnumerable<IStrategy<TInput, TOutput>> links)
        {
            foreach (IStrategy<TInput, TOutput> v in links)
                AddStrategy(v, false);
            Sort();
        }

        public IEnumerable<IStrategy<TInput, TOutput>> Strategies
        {
            get
            {
                foreach (IStrategy<TInput, TOutput> v in _strategies)
                    yield return v;
            }
        }

        #region IStrategyProcessor<TInput,TOutput> Members

        public TOutput Process(TInput input, TOutput defaultOutput)
        {
            foreach (IStrategy<TInput, TOutput> link in _strategies)
            {
                TOutput output;
                if (link.Process(input, out output))
                    return output;
            }

            return defaultOutput;
        }

        #endregion

        public void AddStrategy(IStrategy<TInput, TOutput> link)
        {
            AddStrategy(link, true);
        }

        private void AddStrategy(IStrategy<TInput, TOutput> link, bool resort)
        {
            _strategies.Add(link);
            if (resort)
                Sort();
        }

        private void Sort()
        {
            _strategies.Sort(
                new Comparison<IStrategy<TInput, TOutput>>(
                    delegate(IStrategy<TInput, TOutput> a, IStrategy<TInput, TOutput> b) { return Comparer<int>.Default.Compare(a.Priority, b.Priority); }));
        }

        public void RemoveStrategy(IStrategy<TInput, TOutput> link)
        {
            _strategies.Remove(link);
        }
    }

    public interface IStrategyProcessor<TInput, TOutput>
    {
        TOutput Process(TInput input, TOutput defaultOutput);
    }


    public interface IStrategy<TInput, TOutput>
    {
        int Priority { get; }
        bool Process(TInput input, out TOutput output);
    }
}