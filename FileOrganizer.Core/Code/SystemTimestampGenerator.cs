using System;

namespace FileOrganizer.Core
{
    public sealed class SystemTimestampGenerator : ITimestampGenerator
    {
        public UtcTimestamp UtcNow => new UtcTimestamp( DateTime.UtcNow );
    }
}
