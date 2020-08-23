using FileOrganizer.CommonUtils;
using FileOrganizer.Core.Helpers;
using FileOrganizer.Core.Services;
using FileOrganizer.Services.FileDatabase;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace FileOrganizer.Core
{
    // TODO: this class is temporary

    public sealed partial class FakeDatabaseSingleton :
        IFileUploader,
        IFileDetailsReader,
        IAppUserFinder,
        IFileSearcher,
        IFileDetailsUpdater,
        IUploadInfoReader,
        IAppUserCreator
    {
        readonly IFileDatabase fileDatabase;
        readonly ITimestampGenerator timestampGenerator;
        readonly IThumbnailsMaker thumbnailsMaker;
        
        readonly List<UploadEntry> uploads = new List<UploadEntry>();
        readonly List<FileEntry> files = new List<FileEntry>();

        readonly List<AppUser> appUsers = new List<AppUser>();

        int uploadId = -1;
        int fileId = -1;

        //====== ctors

        public FakeDatabaseSingleton( IFileDatabase fileDatabase, ITimestampGenerator timestampGenerator, IThumbnailsMaker thumbnailsMaker )
        {
            this.fileDatabase       = Guard.NotNull( fileDatabase, nameof( fileDatabase ) );
            this.timestampGenerator = Guard.NotNull( timestampGenerator, nameof( timestampGenerator ) );
            this.thumbnailsMaker    = Guard.NotNull( thumbnailsMaker, nameof( thumbnailsMaker ) );
        }

        //====== IFileUploader

        public UploadId Upload( UploadParameters parameters )
        {
            UtcTimestamp startTimestamp = timestampGenerator.UtcNow;

            uploadId++;

            var tempFiles = new List<FileEntry>();

            foreach (SourceFile sourceFile in parameters.SourceFiles)
            {
                UtcTimestamp timestamp = timestampGenerator.UtcNow;

                string newFileName = MakeRandomFileName( sourceFile.OrginalFileName, timestamp );

                IFileInfo fileInfo = fileDatabase.GetStorage( FileDatabaseFolder.Files )
                                                 .Create( sourceFile.Content, new FileName( newFileName ) );

                string thumbFileName = CreateThumbnail( fileInfo, timestamp );

                fileId++;

                tempFiles.Add( new FileEntry
                {
                    Id            = fileId,
                    UploadId      = new UploadId( uploadId ),
                    MimeType      = sourceFile.MimeType,
                    FileName      = sourceFile.OrginalFileName,
                    WhenAdded     = timestamp,
                    DatabaseFiles = new DatabaseFiles( new FileName( newFileName ), new FileName( thumbFileName ) ),
                    Size          = new DataSize( sourceFile.Content.Length ),
                } );
            }

            files.AddRange( tempFiles );

            uploads.Add( new UploadEntry
            {
                Id          = new UploadId( uploadId ),
                Description = parameters.Description,
                WhenAdded   = startTimestamp,
                FileCount   = tempFiles.Count,
                Size        = new DataSize( parameters.SourceFiles.Sum( x => x.Content.Length ) ),
                UserName    = new UserName( "admin" ) // temp
            } );

            return new UploadId( uploadId );
        }

        //====== IFileDetailsReader

        public FileDetails? GetFileDetailsById( FileId fileId )
        {
            FileEntry entry = files.FirstOrDefault( x => x.Id == fileId.Value );

            if (entry is null) return null;

            return new FileDetails
            {
                FileId        = fileId,
                DatabaseFiles = entry.DatabaseFiles,
                FileSize      = entry.Size,
                Description   = entry.Description,
                Title         = entry.Title
            };
        }

        public UploadDetails? GetUploadDetails( UploadId uploadId )
        {
            var upload = uploads.FirstOrDefault( x => x.Id.Value == uploadId.Value);
            
            if (upload is null) return null;

            IEnumerable<FileDetails> fileDetailsList = files
                .Where( x => x.UploadId.Value == uploadId.Value)
                .Select( x => GetFileDetailsById( new FileId( x.Id ))!);

            return new UploadDetails( uploadId, fileDetailsList, upload.Description );
        }

        //====== private methods

        private string CreateThumbnail( IFileInfo sourceFile, UtcTimestamp timestamp )
        {
            using Image? thumbnail = thumbnailsMaker.MakeThumb( sourceFile, new Size( 300, 300 ) );

            if (thumbnail is null) return string.Empty;

            using var memoryStream = new MemoryStream( 50 * 1024 );

            thumbnail.Save( memoryStream, ImageFormat.Jpeg );

            string newFileName = MakeRandomFileName( "any-name.jpg", timestamp );

            var thumbFile = fileDatabase.GetStorage( FileDatabaseFolder.Thumbs )
                                        .Create( memoryStream, new FileName( newFileName ) );

            return thumbFile.Name;
        }

        private string MakeRandomFileName( string? fileName, UtcTimestamp timestamp )
        {
            string datePart = timestamp.Value.ToString( "yyyy-MM-dd");

            string randomName = Path.GetFileNameWithoutExtension( Path.GetRandomFileName() );
            string extensionWithDot = Path.GetExtension( fileName ?? string.Empty );

            return $"{datePart}_{randomName}{extensionWithDot}";
        }

        public AppUser? Find( UserName userName, string password )
        {
            return appUsers.FirstOrDefault( x => x.Name.Value == userName.Value ); // we ignore password for now
        }

        public AppUser? Find( UserName userName )
        {
            return Find( userName, "any-password" );
        }

        public IReadOnlyList<AppUser> GetAllAppUsers()
        {
            return appUsers.ToList();
        }

        public IReadOnlyList<FileDetails> GetFiles( PagingParameters pagingParameters )
        {
            return files
                .Skip( pagingParameters.SkipCount )
                .Take( pagingParameters.PageSize )
                .Select( x => GetFileDetailsById( new FileId( x.Id ) ) ).ToList();
        }

        public int CountFiles()
        {
            return files.Count;
        }

        public void UpdateDescription( FileId fileId, FileDescription description )
        {
            files.FirstOrDefault( x => x.Id == fileId.Value ).Description = description;
        }

        public void UpdateTitle( FileId fileId, FileTitle title )
        {
            files.FirstOrDefault( x => x.Id == fileId.Value ).Title = title;
        }

        public IReadOnlyList<UploadInfo> GetAll()
        {
            var infos = uploads.Select( x => new UploadInfo()
            {
                Id          = x.Id,
                Description = x.Description,
                WhenAdded   = x.WhenAdded,
                FileCount   = x.FileCount,
                User        = new UserInfo(
                    x.UserName,
                    appUsers.FirstOrDefault( appUser => appUser.Name.Value == x.UserName.Value ).DisplayName ),
                TotalSize   = x.Size
            } );

            return infos.ToList();
        }

        public void Create( AppUser appUser, UserPassword password )
        {
            if (appUsers.Any( x => x.Name.Value == appUser.Name.Value )) throw new Exception( "User already exists." );

            appUsers.Add( appUser );
        }
    }
}
