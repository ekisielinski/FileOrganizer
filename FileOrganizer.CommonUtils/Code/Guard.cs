using System;

namespace FileOrganizer.CommonUtils
{
    public static class Guard
    {
        public static T NotNull<T>( T value, string? paramName = null ) where T : class
        {
            return value ?? throw new ArgumentNullException( paramName );
        }
    }
}
