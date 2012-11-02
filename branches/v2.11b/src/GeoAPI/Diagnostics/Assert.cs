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

namespace GeoAPI.Diagnostics
{
    /// <summary>
    /// A utility for making programming assertions.
    /// </summary>
    public static class Assert
    {
        [DebuggerStepThrough]
        [AssertionMethod]
        public static void IsTrue([AssertionCondition(AssertionConditionType.IS_TRUE)]Boolean assertion)
        {
            IsTrue(assertion, null);
        }
        [DebuggerStepThrough]
        [AssertionMethod]
        public static void IsTrue([AssertionCondition(AssertionConditionType.IS_TRUE)]Boolean assertion, String message)
        {
            if (assertion)
            {
                return;
            }

            throw new AssertionFailedException(message);
        }
        [DebuggerStepThrough]
        [AssertionMethod]
        public static void IsEquals<TValue>(TValue expectedValue, TValue actualValue)
        {
            IsEquals(expectedValue, actualValue, null);
        }

        [DebuggerStepThrough]
        [AssertionMethod]
        public static void IsEquals<TValue>(TValue expectedValue, TValue actualValue, String message)
        {
            if (!EqualityComparer<TValue>.Default.Equals(expectedValue, actualValue))
            {
                throw new AssertionFailedException("Expected " + expectedValue +
                                                   " but encountered " + actualValue +
                                                   (message != null
                                                        ? ": " + message
                                                        : String.Empty));
            }
        }

        [DebuggerStepThrough]
        [AssertionMethod]
        public static void IsNotEquals<TValue>(TValue expectedValue, TValue actualValue)
        {
            IsNotEquals(expectedValue, actualValue, null);
        }

        [DebuggerStepThrough]
        [AssertionMethod]
        public static void IsNotEquals<TValue>(TValue unExpectedValue, TValue actualValue, String message)
        {
            if (EqualityComparer<TValue>.Default.Equals(unExpectedValue, actualValue))
            {
                throw new AssertionFailedException("Expected value not to be " + unExpectedValue +
                                                   " but encountered " + actualValue +
                                                   (message != null
                                                        ? ": " + message
                                                        : String.Empty));
            }
        }

        [DebuggerStepThrough]
        [AssertionMethod]
        public static void ShouldNeverReachHere()
        {
            ShouldNeverReachHere(null);
        }

        [DebuggerStepThrough]
        [AssertionMethod]
        public static void ShouldNeverReachHere(String message)
        {
            throw new AssertionFailedException("Should never reach here"
                                               + (message != null
                                                    ? ": " + message
                                                    : String.Empty));
        }

        [DebuggerStepThrough]
        [AssertionMethod]
        public static Exception ShouldNeverReachHereException()
        {
            return ShouldNeverReachHereException(null);
        }

        [DebuggerStepThrough]
        [AssertionMethod]
        public static Exception ShouldNeverReachHereException(String message)
        {
            return new AssertionFailedException("Should never reach here"
                                               + (message != null
                                                    ? ": " + message
                                                    : String.Empty));
        }

        [DebuggerStepThrough]
        [AssertionMethod]
        public static void IsNotNull([AssertionCondition(AssertionConditionType.IS_NOT_NULL)]Object o)
        {
            if (ReferenceEquals(o, null))
            {
                throw new AssertionFailedException("Object is null");
            }
        }
    }
}