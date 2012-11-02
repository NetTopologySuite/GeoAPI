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
using System.Threading;

namespace GeoAPI.Diagnostics
{
    public class Trace
    {
        private static Object _logger;
        private static readonly Object _loggerSync = new Object();

        public static ILogger Logger
        {
            get
            {
                ILogger logger = Thread.VolatileRead(ref _logger) as ILogger;

                if (logger == null)
                {
                    lock (_loggerSync)
                    {
                        logger = Thread.VolatileRead(ref _logger) as ILogger;

                        if (logger == null)
                        {
                            logger = new DefaultLogger();
                            Thread.VolatileWrite(ref _logger, logger);
                        }
                    }
                }

                return logger;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("value"); 

                lock (_loggerSync)
                {
                    Thread.VolatileWrite(ref _logger, value);
                }
            }
        }

        public static void Debug(String message)
        {
            Logger.Debug(message);
        }

        public static void Debug(String category, String message)
        {
            Logger.Debug(category, message);
        }

        public static void Info(String message)
        {
            Logger.Info(message);
        }

        public static void Info(String category, String message)
        {
            Logger.Info(category, message);
        }

        public static void Warning(String message)
        {
            Logger.Warning(message);
        }

        public static void Warning(String category, String message)
        {
            Logger.Warning(category, message);
        }

        public static void Error(String message)
        {
            Logger.Error(message);
        }

        public static void Error(String category, String message)
        {
            Logger.Error(category, message);
        }

        public static void Fatal(String message)
        {
            Logger.Fatal(message);
        }

        public static void Fatal(String category, String message)
        {
            Logger.Fatal(category, message);
        }

        public static void StartLogicalOperation(String operation)
        {
            Logger.StartLogicalOperation(operation);
        }

        public static void StopLogicalOperation()
        {
            Logger.StopLogicalOperation();
        }
    }
}
