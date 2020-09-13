using FileOrganizer.Core.Services;
using System.Collections.Generic;
using System.Linq;

namespace FileOrganizer.Core.FakeDatabase
{
    public sealed class FileSearcher : IFileSearcher
    {
        readonly FakeDatabaseSingleton database;
        readonly IFileDetailsReader fileDetailsReader;

        //====== ctors

        public FileSearcher( FakeDatabaseSingleton database, IFileDetailsReader fileDetailsReader )
        {
            this.database = database;
            this.fileDetailsReader = fileDetailsReader;
        }

        //====== IFileSearcher

        public IReadOnlyList<FileDetails> GetFiles( PagingParameters pagingParameters )
        {
            return database.Files
                .Skip( pagingParameters.SkipCount )
                .Take( pagingParameters.PageSize )
                .Select( x => fileDetailsReader.GetFileDetailsById( new FileId( x.Id ) ) ).ToList();
        }

        public int CountFiles() => database.Files.Count;
    }
}
