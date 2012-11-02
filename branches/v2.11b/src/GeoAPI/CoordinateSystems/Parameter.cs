// Portions copyright 2005 - 2006: Morten Nielsen (www.iter.dk)
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

namespace GeoAPI.CoordinateSystems
{
	/// <summary>
	/// A named parameter value.
	/// </summary>
	public class Parameter : IEquatable<Parameter>
    {
        private readonly String _name;
        private readonly Double _value;

		/// <summary>
		/// Creates an instance of a parameter
		/// </summary>
		/// <remarks>Units are always either meters or degrees.</remarks>
		/// <param name="name">Name of parameter.</param>
		/// <param name="value">Parameter value.</param>
		public Parameter(String name, Double value)
		{
            if (String.IsNullOrEmpty(name))
		    {
                throw new ArgumentException("Parameter name cannot be null or empty.", "name");
		    }

		    _name = name;
			_value = value;
        }

        public override string ToString()
        {
            return String.Format("{0} = {1:N6}", Name, Value);
        }

		#region IParameter Members

		/// <summary>
		/// Gets the parameter name.
		/// </summary>
		public String Name
		{
			get { return _name; }
		}

		/// <summary>
		/// Gets the parameter value.
		/// </summary>
		public Double Value
		{
			get { return _value; }
		}

	    public String Xml
	    {
	        get { throw new NotImplementedException(); }
	    }

	    public String Wkt
	    {
	        get { throw new NotImplementedException(); }
	    }

	    #endregion

        public override Boolean Equals(Object obj)
        {
            return Equals(obj as Parameter);
        }

        public override Int32 GetHashCode()
        {
            return _name.GetHashCode() ^ _value.GetHashCode();
        }

        #region IEquatable<Parameter> Members

        public Boolean Equals(Parameter other)
        {
            if(ReferenceEquals(other, null))
            {
                return false;
            }

            // TODO: factor tolerance into Parameter equality
            if(other._name == _name && other._value == _value)
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}
