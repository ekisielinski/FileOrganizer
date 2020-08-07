using FileOrganizer.CommonUtils;
using FileOrganizer.Services.FileDatabase;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace FileOrganizer.Core.Services
{
    // TODO: this class is temporary

    public sealed class InMemoryFileUploader : IFileUploader, IFileDetailsReader
    {
        readonly IFileDatabase fileDatabase;
        readonly ITimestampGenerator timestampGenerator;
        readonly IThumbnailsMaker thumbnailsMaker;

        readonly List<Entry> items = new List<Entry>();
        
        int uploadId = -1;
        int fileId = -1;

        //====== ctors

        public InMemoryFileUploader( IFileDatabase fileDatabase, ITimestampGenerator timestampGenerator, IThumbnailsMaker thumbnailsMaker )
        {
            this.fileDatabase = Guard.NotNull( fileDatabase, nameof( fileDatabase ) );
            this.timestampGenerator = Guard.NotNull( timestampGenerator, nameof( timestampGenerator ) );
            this.thumbnailsMaker = Guard.NotNull( thumbnailsMaker, nameof( thumbnailsMaker ) );
        }

        //====== IFileUploader

        public UploadResult Upload( UploadInfo[] uploads )
        {
            uploadId++;

            foreach (UploadInfo upload in uploads)
            {
                UtcTimestamp timestamp = timestampGenerator.UtcNow;

                string newFileName = MakeRandomFileName( upload.FileName, timestamp );

                IFileInfo fileInfo = fileDatabase.GetStorage( FileDatabaseFolder.Files )
                                                 .Create( upload.Content, new FileName( newFileName ) );

                string thumbFileName = CreateThumbnail( fileInfo, timestamp );

                fileId++;

                items.Add( new Entry
                {
                    Id            = fileId,
                    UploadId      = uploadId,
                    MimeType      = upload.MimeType,
                    FileName      = upload.FileName,
                    WhenAdded     = timestamp,
                    DatabaseFiles = new DatabaseFiles( new FileName( newFileName ), new FileName( thumbFileName ) )
                } );
            }

            IEnumerable<FileId> fileIds = items.Where( x => x.UploadId == uploadId ).Select( x => new FileId( x.Id ) );

            return new UploadResult( new UploadId( uploadId ), fileIds );
        }

        //====== IFileDetailsReader

        public FileDetails? GetFileDetailsById( FileId fileId )
        {
            Entry entry = items.FirstOrDefault( x => x.Id == fileId.Value );

            if (entry is null) return null;

            return new FileDetails
            {
                FileId = fileId,
                DatabaseFiles = entry.DatabaseFiles
            };
        }

        public IReadOnlyList<FileDetails> GetFileDetailsByUploadId( UploadId uploadId )
        {
            IEnumerable<FileId> fileIds = items.Where( x => x.UploadId == uploadId.Value ).Select( x => new FileId( x.Id ) );

            return fileIds.Select( GetFileDetailsById ).ToList();
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

        //====== helper class

        private sealed class Entry
        {
            public int           Id            { get; set; } = -1;
            public int           UploadId      { get; set; } = -1;
            public MimeType      MimeType      { get; set; } = MimeType.Unknown;
            public string?       FileName      { get; set; } = null;
            public UtcTimestamp  WhenAdded     { get; set; } = new UtcTimestamp( DateTime.UtcNow );
            public DatabaseFiles DatabaseFiles { get; set; }
        }
    }
}
