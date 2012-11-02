using System;
using System.Diagnostics;

namespace GeoAPI.Diagnostics
{
/**
 * Utility functions to report memory usage.
 * 
 * @author mbdavis
 *
 */
public class Memory 
{
	
	public static long Total
	{
		get { return GC.GetTotalMemory(true);}
	}
	
	public static String TotalString
	{
		get { return format(Total); }
	}
	

	public const double KB = 1024;
	public const double MB = 1048576;
	public const double GB = 1073741824;

	public static String format(long mem)
	{
		if (mem < 2 * KB)
			return mem + " bytes";
		if (mem < 2 * MB)
			return round(mem / KB) + " KB";
		if (mem < 2 * GB)
			return round(mem / MB) + " MB";
		return round(mem / GB) + " GB";
	}
	
	public static double round(double d)
	{
		return Math.Ceiling(d * 100) / 100;
	}
}
}
