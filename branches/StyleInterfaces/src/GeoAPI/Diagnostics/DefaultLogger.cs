// Portions copyright 2005 - 2007: Diego Guidi
// Portions copyright 2006 - 2008: Rory Plaire (codekaizen@gmail.com)
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
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA 

using System;
using System.Collections.Generic;
using System.Diagnostics;
using SystemTrace = System.Diagnostics.Trace;

namespace GeoAPI.Diagnostics
{
    class DefaultLogger : ILogger
    {
        private readonly Dictionary<String, TraceSwitch> _switchCache 
            = new Dictionary<String, TraceSwitch>(StringComparer.InvariantCultureIgnoreCase);

        #region ILogger Members
        public void Debug(String message)
        {
            SystemTrace.Write(message);
        }

        public void Debug(String category, String message)
        {
            TraceSwitch traceSwitch = getSwitch(category);
            SystemTrace.WriteIf(traceSwitch.TraceVerbose, category, message);
        }

        public void Info(String message)
        {
            SystemTrace.TraceInformation(message);
        }

        public void Info(String category, String message)
        {
            TraceSwitch traceSwitch = getSwitch(category);
            SystemTrace.WriteIf(traceSwitch.TraceInfo, category, message);
        }

        public void Warning(String message)
        {
            SystemTrace.TraceWarning(message);
        }

        public void Warning(String category, String message)
        {
            TraceSwitch traceSwitch = getSwitch(category);
            SystemTrace.WriteIf(traceSwitch.TraceWarning, category, message);
        }

        public void Error(String message)
        {
            SystemTrace.TraceError(message);
        }

        public void Error(String category, String message)
        {
            TraceSwitch traceSwitch = getSwitch(category);
            SystemTrace.WriteIf(traceSwitch.TraceError, category, message);
        }

        public void Fatal(String message)
        {
            SystemTrace.TraceError(message);
        }

        public void Fatal(String category, String message)
        {
            TraceSwitch traceSwitch = getSwitch(category);
            SystemTrace.WriteIf(traceSwitch.TraceError, category, message);
        }

        public void StartLogicalOperation(String operation)
        {
            SystemTrace.CorrelationManager.StartLogicalOperation(operation);
        }

        public void StopLogicalOperation()
        {
            SystemTrace.CorrelationManager.StopLogicalOperation();
        }

        #endregion

        private TraceSwitch getSwitch(String category)
        {
            TraceSwitch traceSwitch;

            if (!_switchCache.TryGetValue(category, out traceSwitch))
            {
                traceSwitch = new TraceSwitch(category, category);
                _switchCache[category] = traceSwitch;
            }
            return traceSwitch;
        }
    }
}
