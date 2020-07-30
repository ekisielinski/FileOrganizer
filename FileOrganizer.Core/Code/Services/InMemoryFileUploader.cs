using FileOrganizer.CommonUtils;
using FileOrganizer.Services.FileDatabase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileOrganizer.Core.Services
{
    public sealed class InMemoryFileUploader : IFileUploader, IFileDetailsReader
    {
        readonly IFileDatabase fileDatabase;
        readonly ITimestampGenerator timestampGenerator;

        readonly List<Entry> items = new List<Entry>();

        //====== ctors

        public InMemoryFileUploader( IFileDatabase fileDatabase, ITimestampGenerator timestampGenerator )
        {
            this.fileDatabase = Guard.NotNull( fileDatabase, nameof( fileDatabase ) );
            this.timestampGenerator = Guard.NotNull( timestampGenerator, nameof( timestampGenerator ) ); ;
        }

        //====== IFileUploader

        public FileId Upload( Stream content, MimeType mimeType, string? fileName )
        {
            UtcTimestamp timestamp = timestampGenerator.UtcNow;

            string newFileName = MakeRandomFileName( fileName, timestamp );

            fileDatabase.GetStorage( FileDatabaseFolder.Files )
                        .Create( content, new FileName( newFileName ) );

            int id = (items.LastOrDefault()?.Id ?? 0) + 1;

            items.Add( new Entry
            {
                Id                 = id,
                MimeType           = mimeType,
                FileName           = fileName,
                WhenAdded          = timestamp,
                FileNameInDatabase = newFileName
            } );

            return new FileId( id );
        }

        //====== IFileDetailsReader

        public FileDetails? GetFileDetailsById( FileId fileId )
        {
            Entry entry = items.FirstOrDefault( x => x.Id == fileId.Value );

            if (entry is null) return null;

            return new FileDetails
            {
                FileId = fileId,
                FileNameInDatabase = entry.FileNameInDatabase
            };
        }

        //====== private methods

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
            public int          Id                 { get; set; } = -1;
            public MimeType     MimeType           { get; set; } = MimeType.Unknown;
            public string?      FileName           { get; set; } = null;
            public UtcTimestamp WhenAdded          { get; set; } = new UtcTimestamp( DateTime.UtcNow );
            public string       FileNameInDatabase { get; set; } = string.Empty;
        }
    }
}
