using System;

namespace FileOrganizer.CommonUtils
{
    public static class Guard
    {
        public static T NotNull<T>( T value, string? paramName = null ) where T : class
        {
            return value ?? throw new ArgumentNullException( paramName );
        }

        public static int NotNegative( int value, string? paramName = null )
        {
            if (value >= 0) return value;

            throw new ArgumentOutOfRangeException( paramName ?? nameof( value ), value, "Negative values are forbidden." );
        }

        public static int MinValue( int value, int min, string? paramName = null )
        {
            if (value >= min) return value;

            throw new ArgumentOutOfRangeException( paramName ?? nameof( value ), value, "Value is too small. Minimum allowed value: " + min );
        }
    }
}
