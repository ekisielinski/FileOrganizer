using FileOrganizer.Domain;
using System;

namespace FileOrganizer.Core.Helpers
{
    public sealed class SystemTimestampGenerator : ITimestampGenerator
    {
        public UtcTimestamp UtcNow => new UtcTimestamp( DateTime.UtcNow );
    }
}
