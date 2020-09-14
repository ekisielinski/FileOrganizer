using System.Collections.Generic;

namespace FileOrganizer.Core
{
    public interface IActivityLogReader
    {
        IReadOnlyList<ActivityLogEntry> GetAll();
    }
}
