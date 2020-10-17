using FileOrganizer.Domain;

namespace FileOrganizer.Core.Helpers
{
    public interface ITimestampGenerator
    {
        UtcTimestamp UtcNow { get; }
    }
}
