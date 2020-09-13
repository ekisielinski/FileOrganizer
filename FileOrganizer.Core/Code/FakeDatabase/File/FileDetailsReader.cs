﻿using FileOrganizer.Core.Services;
using System.Linq;

namespace FileOrganizer.Core.FakeDatabase
{
    public sealed class FileDetailsReader : IFileDetailsReader
    {
        readonly FakeDatabaseSingleton database;

        //====== ctors

        public FileDetailsReader( FakeDatabaseSingleton database )
        {
            this.database = database;
        }

        //====== IFileDetailsReader

        public FileDetails GetFileDetailsById( FileId fileId )
        {
            FileEntry entry = database.Files.Single( x => x.Id == fileId.Value );

            return new FileDetails
            {
                FileId        = fileId,
                DatabaseFiles = entry.DatabaseFiles,
                FileSize      = entry.Size,
                Description   = entry.Description,
                Title         = entry.Title,
                ImageDetails  = entry.ImageDetails
            };
        }
    }
}
