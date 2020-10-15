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

        public static int InRange( int value, int min, int max, string? paramName = null )
        {
            if (min > max) throw new ArgumentOutOfRangeException( nameof( min ),
                $"The {nameof( min )} parameter cannot be greater than the {nameof( max )} parameter" );

            if (value >= min && value <= max) return value;

            throw new ArgumentOutOfRangeException( paramName ?? nameof( value ), $"Value is out of range [{min}..{max}]." );
        }

        public static int? InRangeNullable( int? value, int min, int max, string? paramName = null )
        {
            if (min > max) throw new ArgumentOutOfRangeException( nameof( min ),
                $"The {nameof( min )} parameter cannot be greater than the {nameof( max )} parameter" );

            if (value is null) return null;

            return InRange( value.Value, min, max, paramName );
        }
    }
}
