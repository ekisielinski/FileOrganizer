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

    public sealed class InMemoryFileUploader : IFileUploader, IFileDetailsReader, IAppUserFinder, IFileSearcher
    {
        readonly IFileDatabase fileDatabase;
        readonly ITimestampGenerator timestampGenerator;
        readonly IThumbnailsMaker thumbnailsMaker;
        
        readonly List<UploadEntry> uploads = new List<UploadEntry>();
        readonly List<FileEntry> files = new List<FileEntry>();

        readonly List<AppUser> appUsers = new List<AppUser>( FakeData.CreateAppUsers() );

        int uploadId = -1;
        int fileId = -1;

        //====== ctors

        public InMemoryFileUploader( IFileDatabase fileDatabase, ITimestampGenerator timestampGenerator, IThumbnailsMaker thumbnailsMaker )
        {
            this.fileDatabase       = Guard.NotNull( fileDatabase, nameof( fileDatabase ) );
            this.timestampGenerator = Guard.NotNull( timestampGenerator, nameof( timestampGenerator ) );
            this.thumbnailsMaker    = Guard.NotNull( thumbnailsMaker, nameof( thumbnailsMaker ) );
        }

        //====== IFileUploader

        public UploadId Upload( UploadParameters parameters )
        {
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

            var fileDetailsList = tempFiles.Select( x => GetFileDetailsById( new FileId( x.Id ) ) );

            this.uploads.Add( new UploadEntry
            {
                Id = new UploadId( uploadId ),
                Description = parameters.Description
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

        //====== helper class

        private sealed class FileEntry
        {
            public int           Id            { get; set; } = -1;
            public UploadId      UploadId      { get; set; }
            public MimeType      MimeType      { get; set; } = MimeType.Unknown;
            public string?       FileName      { get; set; } = null;
            public UtcTimestamp  WhenAdded     { get; set; } = new UtcTimestamp( DateTime.UtcNow );
            public DatabaseFiles DatabaseFiles { get; set; }
            public DataSize      Size          { get; set; }
        }

        public sealed class UploadEntry
        {
            public UploadId Id { get; set; }
            public UploadDescription Description { get; set; } = UploadDescription.None;
        }
    }
}
