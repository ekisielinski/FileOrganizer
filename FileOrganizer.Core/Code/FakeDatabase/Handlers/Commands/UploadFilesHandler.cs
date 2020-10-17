using FileOrganizer.CommonUtils;
using FileOrganizer.Core.Helpers;
using FileOrganizer.Domain;
using FileOrganizer.Services.FileDatabase;
using MediatR;
using Microsoft.Extensions.FileProviders;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileOrganizer.Core.FakeDatabase
{
    public sealed class UploadFilesHandler : IRequestHandler<UploadFilesCommand, UploadId>
    {
        readonly FakeDatabaseSingleton database;
        readonly IFileDatabase fileDatabase;
        readonly IThumbnailMaker thumbnailMaker;
        readonly ISha256Generator sha256Generator;
        readonly IActivityLogger logger;

        public UploadFilesHandler(
            FakeDatabaseSingleton database,
            IFileDatabase fileDatabase,
            IThumbnailMaker thumbnailMaker,
            ISha256Generator sha256Generator,
            IActivityLogger logger )
        {
            this.database = database;
            this.fileDatabase = fileDatabase;
            this.thumbnailMaker = thumbnailMaker;
            this.sha256Generator = sha256Generator;
            this.logger = logger;
        }

        public Task<UploadId> Handle( UploadFilesCommand request, CancellationToken cancellationToken )
        {
             UtcTimestamp startTimestamp = database.UtcNow;

            database.uploadId++;

            var tempFiles = new List<FileEntry>();
            var duplicates = new List<string>();

            foreach (SourceFile sourceFile in request.Parameters.SourceFiles)
            {
                UtcTimestamp timestamp = database.UtcNow;

                Sha256Hash hash = sha256Generator.GenerateHash( sourceFile.Content );

                if (IsDuplicate( hash ))
                {
                    duplicates.Add( sourceFile.OrginalFileName ?? "???" );
                }
                else
                {
                    string newFileName = FileUtils.GetRandomFileNameWithTimestamp( timestamp.Value, sourceFile.OrginalFileName );

                    IFileInfo fileInfo = fileDatabase.SourceFiles.Create( sourceFile.Content, new FileName( newFileName ) );

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
                        ImageDetails  = new ImageDetails { Dimensions = thumbResult?.OrginalImageSize },
                        Hash          = hash,
                        Uploader      = database.CurrentUser.Name
                    } );
                }
            }

            database.Files.AddRange( tempFiles );

            database.Uploads.Add( new UploadEntry
            {
                Id                 = new UploadId( database.uploadId ),
                Description        = request.Parameters.Description,
                WhenAdded          = startTimestamp,
                FileCount          = tempFiles.Count,
                Size               = tempFiles.Select( x => x.Size ).Aggregate( DataSize.Zero, DataSize.Sum ),
                UserName           = database.CurrentUser.Name,
                RejectedDuplicates = duplicates
            } );

            logger.Add( $"New upload #{database.uploadId}. Files: {tempFiles.Count}" );

            return Task.FromResult( new UploadId( database.uploadId ) );
        }

        //====== private methods

        private bool IsDuplicate( Sha256Hash hash )
        {
            return database.Files.Any( x => x.Hash.Equals( hash ) );
        }
    }
}
