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


// NOTE:
// These types are direct replacements for the types in System.Core.dll assembly,
// which is part of the .Net 3.5 framework. Since we are trying to move to 3.5
// expression styles, these are here to ease the transition while still being
// able to run on .Net 2.0.

#if !DOTNET35

namespace GeoAPI.SystemCoreReplacement
{
    /// <summary>
    /// An action
    /// </summary>
    public delegate void Action();

    /// <summary>
    /// An action taking one argument.
    /// </summary>
    /// <param name="arg1">The first argument.</param>
    public delegate void Action<T1>(T1 arg1);

    /// <summary>
    /// An action taking two argument.
    /// </summary>
    /// <param name="arg1">The first argument.</param>
    /// <param name="arg2">The second argument.</param>
    public delegate void Action<T1, T2>(T1 arg1, T2 arg2);

    /// <summary>
    /// An action taking one argument.
    /// </summary>
    /// <param name="arg1">The first argument.</param>
    /// <param name="arg2">The second argument.</param>
    /// <param name="arg3">The third argument.</param>
    public delegate void Action<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3);

    /// <summary>
    /// An action taking one argument.
    /// </summary>
    /// <param name="arg1">The first argument.</param>
    /// <param name="arg2">The second argument.</param>
    /// <param name="arg3">The third argument.</param>
    /// <param name="arg4">The fourth argument.</param>
    public delegate void Action<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);

    /// <summary>
    /// A function returning a <typeparamref name="TResult"/>
    /// </summary>
    /// <typeparam name="TResult">The type of the function's result.</typeparam>
    /// <returns>A <typeparamref name="TResult"/></returns>
    public delegate TResult Func<TResult>();

    /// <summary>
    /// A function returning a <typeparamref name="TResult"/>
    /// </summary>
    /// <typeparam name="TResult">The type of the function's result.</typeparam>
    /// <param name="arg1">The first argument</param>
    /// <returns>A <typeparamref name="TResult"/></returns>
    public delegate TResult Func<T, TResult>(T arg1);

    /// <summary>
    /// A function returning a <typeparamref name="TResult"/>
    /// </summary>
    /// <typeparam name="TResult">The type of the function's result.</typeparam>
    /// <param name="arg1">The first argument</param>
    /// <param name="arg2">The second argument</param>
    /// <returns>A <typeparamref name="TResult"/></returns>
    public delegate TResult Func<T1, T2, TResult>(T1 arg1, T2 arg2);

    /// <summary>
    /// A function returning a <typeparamref name="TResult"/>
    /// </summary>
    /// <typeparam name="TResult">The type of the function's result.</typeparam>
    /// <param name="arg1">The first argument</param>
    /// <param name="arg2">The second argument</param>
    /// <param name="arg3">The third argument</param>
    /// <returns>A <typeparamref name="TResult"/></returns>
    public delegate TResult Func<T1, T2, T3, TResult>(T1 arg1, T2 arg2, T3 arg3);

    /// <summary>
    /// A function returning a <typeparamref name="TResult"/>
    /// </summary>
    /// <typeparam name="TResult">The type of the function's result.</typeparam>
    /// <param name="arg1">The first argument</param>
    /// <param name="arg2">The second argument</param>
    /// <param name="arg3">The third argument</param>
    /// <param name="arg4">The fourth argument</param>
    /// <returns>A <typeparamref name="TResult"/></returns>
    public delegate TResult Func<T1, T2, T3, T4, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
}

#endif