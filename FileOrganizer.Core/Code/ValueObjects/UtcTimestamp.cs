using System;

namespace FileOrganizer.Core
{
    public sealed class UtcTimestamp
    {
        public UtcTimestamp( DateTime value )
        {
            if (value.Kind != DateTimeKind.Utc) throw new ArgumentException( "Invalid DateTime format.", nameof( value ) );

            Value = value;
        }

        //====== public properties

        public DateTime Value { get; }

        //====== override: Object

        public override string ToString() => Value.ToString();
    }
}
