using FileOrganizer.CommonUtils;
using FileOrganizer.Core.Helpers;
using FileOrganizer.Core.Services;
using FileOrganizer.Services.FileDatabase;
using Microsoft.Extensions.FileProviders;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace FileOrganizer.Core
{
    public sealed class FakeDatabaseUploadApi :
        IFileUploader,
        IUploadDetailsReader,
        IUploadInfoReader
    {
        readonly FakeDatabaseSingleton database;
        readonly ITimestampGenerator timestampGenerator;
        readonly ISha256Generator sha256Generator;
        readonly IFileDatabase fileDatabase;
        readonly IThumbnailMaker thumbnailsMaker;

        //====== ctors

        public FakeDatabaseUploadApi(
            FakeDatabaseSingleton database,
            ITimestampGenerator timestampGenerator,
            ISha256Generator sha256Generator,
            IFileDatabase fileDatabase,
            IThumbnailMaker thumbnailsMaker )
        {
            this.database = database;
            this.timestampGenerator = timestampGenerator;
            this.sha256Generator = sha256Generator;
            this.fileDatabase = fileDatabase;
            this.thumbnailsMaker = thumbnailsMaker;
        }

        IReadOnlyList<UploadInfo> IUploadInfoReader.GetAll()
        {
            var infos = database.Uploads.Select( x => new UploadInfo()
            {
                Id          = x.Id,
                Description = x.Description,
                WhenAdded   = x.WhenAdded,
                FileCount   = x.FileCount,
                Owner       = new AppUser(
                    x.UserName,
                    database.Users.FirstOrDefault( appUser => appUser.AppUserDetails.User.Name.Value == x.UserName.Value ).AppUserDetails.User.DisplayName,
                    UserRoles.Empty ),
                TotalSize   = x.Size
            } );

            return infos.ToList();
        }

        UploadDetails IUploadDetailsReader.GetUploadDetails( UploadId uploadId )
        {
            UploadEntry upload = database.Uploads.Single( x => x.Id.Value == uploadId.Value);

            IEnumerable<FileDetails> fileDetailsList = database.Files
                .Where( x => x.UploadId.Value == uploadId.Value)
                .Select( x => database.GetFileDetailsById( new FileId( x.Id ))!);

            return new UploadDetails( uploadId, fileDetailsList, upload.Description );
        }

        // todo: handle duplicates

        UploadId IFileUploader.Upload( UploadParameters parameters )
        {
            UtcTimestamp startTimestamp = timestampGenerator.UtcNow;

            database.uploadId++;

            var tempFiles = new List<FileEntry>();

            foreach (SourceFile sourceFile in parameters.SourceFiles)
            {
                UtcTimestamp timestamp = timestampGenerator.UtcNow;

                Sha256Hash hash = sha256Generator.GenerateHash( sourceFile.Content );

                string newFileName = MakeRandomFileName( sourceFile.OrginalFileName, timestamp );

                IFileInfo fileInfo = fileDatabase.GetContainer( FileDatabaseFolder.SourceFiles )
                                                 .Create( sourceFile.Content, new FileName( newFileName ) );

                ThumbnailMakerResult thumbResult = null;
                string thumbFileName = string.Empty;

                try
                {
                    thumbResult = thumbnailsMaker.MakeThumb( fileInfo, new Size( 300, 300 ) );
                    thumbFileName = SaveThumbnail( thumbResult.Thumbnail, timestamp );
                }
                catch
                {
                    // TODO: log errors
                }
                finally
                {
                    thumbResult?.Thumbnail?.Dispose();
                }

                database.fileId++;

                tempFiles.Add( new FileEntry
                {
                    Id            = database.fileId,
                    UploadId      = new UploadId( database.uploadId ),
                    MimeType      = sourceFile.MimeType,
                    FileName      = sourceFile.OrginalFileName,
                    WhenAdded     = timestamp,
                    DatabaseFiles = new DatabaseFiles( new FileName( newFileName ), new FileName( thumbFileName ) ),
                    Size          = new DataSize( sourceFile.Content.Length ),
                    ImageDetails  = new ImageDetails { Size = thumbResult?.FullSize },
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
                UserName    = new UserName( "admin" ) // temp
            } );

            return new UploadId( database.uploadId );
        }

        private string MakeRandomFileName( string? fileName, UtcTimestamp timestamp )
        {
            string datePart = timestamp.Value.ToString( "yyyy-MM-dd");

            string randomName = Path.GetFileNameWithoutExtension( Path.GetRandomFileName() );
            string extensionWithDot = Path.GetExtension( fileName ?? string.Empty );

            return $"{datePart}_{randomName}{extensionWithDot}";
        }

        private string SaveThumbnail( Image thumbnail, UtcTimestamp timestamp )
        {
            using var memoryStream = new MemoryStream( 50 * 1024 );

            thumbnail.Save( memoryStream, ImageFormat.Jpeg );

            string newFileName = MakeRandomFileName( "any-name.jpg", timestamp );

            var thumbFile = fileDatabase.GetContainer( FileDatabaseFolder.Thumbnails )
                                        .Create( memoryStream, new FileName( newFileName ) );

            return thumbFile.Name;
        }
    }
}
