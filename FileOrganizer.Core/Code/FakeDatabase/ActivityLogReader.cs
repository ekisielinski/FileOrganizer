using System.Collections.Generic;
using System.Linq;

namespace FileOrganizer.Core.FakeDatabase
{
    public sealed class ActivityLogReader : IActivityLogReader
    {
        readonly FakeDatabaseSingleton database;

        //====== ctors

        public ActivityLogReader( FakeDatabaseSingleton database )
        {
            this.database = database;
        }

        //====== IActivityLogReader

        public IReadOnlyList<ActivityLogEntry> GetAll()
        {
            return database.Logs.OrderByDescending( x=> x.Timestamp.Value ).ToList();
        }
    }
}
