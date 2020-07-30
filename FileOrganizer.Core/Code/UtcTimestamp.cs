using System;

namespace FileOrganizer.Core
{
    public sealed class UtcTimestamp
    {
        public UtcTimestamp( DateTime value )
        {
            Value = value;
        }

        //====== public properties

        public DateTime Value { get; }

        //====== override: Object

        public override string ToString() => Value.ToString();
    }
}
