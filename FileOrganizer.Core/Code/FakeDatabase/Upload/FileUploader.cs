using FileOrganizer.CommonUtils;
using FileOrganizer.Core.Helpers;
using FileOrganizer.Core.Services;
using FileOrganizer.Services.FileDatabase;
using Microsoft.Extensions.FileProviders;
using System.Collections.Generic;
using System.Linq;

namespace FileOrganizer.Core.FakeDatabase
{
    public sealed class FileUploader : IFileUploader
    {
        readonly FakeDatabaseSingleton database;
        readonly IFileDatabase fileDatabase;
        readonly IThumbnailMaker thumbnailMaker;
        readonly ISha256Generator sha256Generator;

        //====== ctors

        public FileUploader(
            FakeDatabaseSingleton database,
            IFileDatabase fileDatabase,
            IThumbnailMaker thumbnailMaker,
            ISha256Generator sha256Generator )
        {
            this.database = database;
            this.fileDatabase = fileDatabase;
            this.thumbnailMaker = thumbnailMaker;
            this.sha256Generator = sha256Generator;
        }

        //====== IFileUploader

        // todo: handle duplicates

        UploadId IFileUploader.Upload( UploadParameters parameters )
        {
            UtcTimestamp startTimestamp = database.UtcNow;

            database.uploadId++;

            var tempFiles = new List<FileEntry>();

            foreach (SourceFile sourceFile in parameters.SourceFiles)
            {
                UtcTimestamp timestamp = database.UtcNow;

                Sha256Hash hash = sha256Generator.GenerateHash( sourceFile.Content );

                string newFileName = FileUtils.GetRandomFileNameWithTimestamp( timestamp.Value, sourceFile.OrginalFileName );

                IFileInfo fileInfo = fileDatabase.GetContainer( FileDatabaseFolder.SourceFiles )
                                                 .Create( sourceFile.Content, new FileName( newFileName ) );

                ThumbnailMakerResult? thumbResult = thumbnailMaker.TryMakeAndSaveThumbnail( fileInfo, timestamp );

                database.fileId++;

                tempFiles.Add( new FileEntry
                {
                    Id            = database.fileId,
                    UploadId      = new UploadId( database.uploadId ),
                    MimeType      = sourceFile.MimeType,
                    FileName      = sourceFile.OrginalFileName,
                    WhenAdded     = timestamp,
                    DatabaseFiles = new DatabaseFiles( new FileName( newFileName ), thumbResult?.ThumbnailFileName ),
                    Size          = new DataSize( sourceFile.Content.Length ),
                    ImageDetails  = new ImageDetails { Size = thumbResult?.OrginalImageSize },
                    Hash          = hash
                } );
            }

            database.Files.AddRange( tempFiles );

            database.Uploads.Add( new UploadEntry
            {
                Id          = new UploadId( database.uploadId ),
                Description = parameters.Description,
                WhenAdded   = startTimestamp,
                FileCount   = tempFiles.Count,
                Size        = new DataSize( parameters.SourceFiles.Sum( x => x.Content.Length ) ),
                UserName    = database.Requestor.UserName
            } );

            return new UploadId( database.uploadId );
        }
    }
}
