using FileOrganizer.CommonUtils;
using FileOrganizer.Core.Helpers;
using FileOrganizer.Domain;
using FileOrganizer.Services.FileDatabase;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileOrganizer.EFDatabase.Handlers
{
    // todo: move thumbnail genweration to separate command
    public sealed class UploadFilesHandler : IRequestHandler<UploadFilesCommand, UploadId>
    {
        readonly EFAppContext context;
        readonly IFileDatabase fileDatabase;
        readonly IThumbnailMaker thumbnailMaker;
        readonly ISha256Generator sha256Generator;
        readonly IActivityLogger logger;

        public UploadFilesHandler(
            EFAppContext context,
            IFileDatabase fileDatabase,
            IThumbnailMaker thumbnailMaker,
            ISha256Generator sha256Generator,
            IActivityLogger logger )
        {
            this.context         = context;
            this.fileDatabase    = fileDatabase;
            this.thumbnailMaker  = thumbnailMaker;
            this.sha256Generator = sha256Generator;
            this.logger          = logger;
        }

        public async Task<UploadId> Handle( UploadFilesCommand request, CancellationToken cancellationToken )
        {
            AppUserEntity? uploader = await context.Entities.AppUsers.FirstOrDefaultAsync( x => x.UserName == context.Requestor.UserName.Value );

            if (uploader is null) throw new ArgumentException( "Invalid requestor." ); // todo: custom exception

            UtcTimestamp startTimestamp = context.UtcNow;

            var tempFiles  = new List<FileEntity>();
            var duplicates = new List<string>();

            foreach (SourceFile sourceFile in request.Parameters.SourceFiles)
            {
                UtcTimestamp timestamp = context.UtcNow;

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

                    var fileEntity = new FileEntity
                    {
                        MimeType        = sourceFile.MimeType.Value,
                        FileName        = sourceFile.OrginalFileName,
                        UtcWhenAdded    = timestamp.Value,
                        SourceFileName  = newFileName,
                        SourceFileSize  = sourceFile.Content.Length,
                        Description     = null,
                        Title           = null,
                        Hash            = hash.ToHexString(),
                        PrimaryDateTime = null,

                        Image = new ImageEntity()
                        {
                            Width  = thumbResult?.OrginalImageSize.Width,
                            Height = thumbResult?.OrginalImageSize.Height,
                            ThumbnailFileName = thumbResult?.ThumbnailFileName.Value,
                        },
                        
                        Uploader = uploader
                    };

                    tempFiles.Add( fileEntity );
                }
            }

            var uploadEntity = new UploadEntity
            {
                Description  = request.Parameters.Description.Value,
                UtcWhenAdded = startTimestamp.Value,
                FileCount    = tempFiles.Count,
                TotalSize    = tempFiles.Select( x => x.SourceFileSize ).Aggregate( 0L, (a, b) => a + b ),
                Uploader     = uploader
            };

            foreach (var file in tempFiles)
            {
                file.Upload = uploadEntity;
                // todo handle 0 files as error
            }

            context.Entities.Files.AddRange( tempFiles );

            await context.Entities.SaveChangesAsync();

            logger.Add( $"New upload #{uploadEntity.Id}. Files added (except duplicates): {uploadEntity.FileCount}" );

            return new UploadId( uploadEntity.Id );
        }

        //====== private methods

        private bool IsDuplicate( Sha256Hash hash )
        {
            // todo: performance?
            return context.Entities.Files.AsNoTracking().Any( x => x.Hash == hash.ToHexString() );
        }
    }
}
