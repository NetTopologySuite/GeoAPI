using System;

namespace GeoAPI.Diagnostics
{
    public static class ExceptionFilter
    {
        public static Boolean IsExceptionCritical(Exception ex)
        {
            return ex is OutOfMemoryException ||
                   ex is StackOverflowException ||
                   ex is ExecutionEngineException;
        }
    }
}
