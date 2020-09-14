using System.Linq;

namespace FileOrganizer.Core.FakeDatabase
{
    public sealed class FileDetailsUpdater : IFileDetailsUpdater
    {
        readonly FakeDatabaseSingleton database;
        readonly IActivityLogger logger;

        //====== ctors

        public FileDetailsUpdater( FakeDatabaseSingleton database, IActivityLogger logger )
        {
            this.database = database;
            this.logger = logger;
        }

        //====== IFileDetailsUpdater

        public void UpdateDescription( FileId fileId, FileDescription description )
        {
            database.Files.FirstOrDefault( x => x.Id == fileId.Value ).Description = description;

            logger.Add( $"Description updated for file #{fileId}. New value: {description}" );
        }

        public void UpdateTitle( FileId fileId, FileTitle title )
        {
            database.Files.FirstOrDefault( x => x.Id == fileId.Value ).Title = title;

            logger.Add( $"Title updated for file #{fileId}. New value: {title}" );
        }
    }
}
