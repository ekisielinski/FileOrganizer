using System;

namespace FileOrganizer.Domain
{
    public sealed class UtcTimestamp
    {
        public UtcTimestamp( DateTime value )
        {
            if (value.Kind != DateTimeKind.Utc) throw new ArgumentException( "Invalid DateTime format (only UTC is allowed).", nameof( value ) );

            Value = value;
        }

        //====== public properties

        public DateTime Value { get; }

        //====== override: Object

        public override string ToString() => Value.ToString();
    }
}
