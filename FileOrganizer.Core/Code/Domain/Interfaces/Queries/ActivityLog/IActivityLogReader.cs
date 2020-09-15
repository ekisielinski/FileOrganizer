using System.Collections.Generic;

namespace FileOrganizer.Core
{
    public interface IActivityLogReader : IDomainQuery
    {
        IReadOnlyList<ActivityLogEntry> GetAll();
    }
}
