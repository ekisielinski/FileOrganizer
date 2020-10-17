using FileOrganizer.Domain;

namespace FileOrganizer.Core.FakeDatabase
{
    public sealed class ActivityLogger : IActivityLogger
    {
        readonly FakeDatabaseSingleton database;

        //====== ctors

        public ActivityLogger( FakeDatabaseSingleton database)
        {
            this.database = database;
        }

        //====== IActivityLogger

        public void Add( string message )
        {
            var entry = new ActivityLogEntry( database.CurrentUser.Name, database.UtcNow, message );

            database.Logs.Add( entry );
        }
    }
}
