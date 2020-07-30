namespace FileOrganizer.Core
{
    public interface ITimestampGenerator
    {
        UtcTimestamp UtcNow { get; }
    }
}
