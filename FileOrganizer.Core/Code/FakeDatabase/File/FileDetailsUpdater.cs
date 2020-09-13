using System.Linq;

namespace FileOrganizer.Core.FakeDatabase
{
    public sealed class FileDetailsUpdater : IFileDetailsUpdater
    {
        readonly FakeDatabaseSingleton database;

        //====== ctors

        public FileDetailsUpdater( FakeDatabaseSingleton database )
        {
            this.database = database;
        }

        //====== IFileDetailsUpdater

        public void UpdateDescription( FileId fileId, FileDescription description )
        {
            database.Files.FirstOrDefault( x => x.Id == fileId.Value ).Description = description;
        }

        public void UpdateTitle( FileId fileId, FileTitle title )
        {
            database.Files.FirstOrDefault( x => x.Id == fileId.Value ).Title = title;
        }
    }
}
