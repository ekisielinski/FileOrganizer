using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileOrganizer.Core.Services
{
    public sealed class InMemoryFileUploader : IFileUploader
    {
        readonly List<Entry> items = new List<Entry>();

        //====== IFileUploader

        public FileId Upload( Stream content, MimeType mimeType, string? fileName )
        {
            using var ms = new MemoryStream();
            content.CopyTo( ms );

            int id = items.LastOrDefault()?.Id ?? 1;

            items.Add( new Entry
            {
                Id       = id,
                Content  = ms.ToArray(),
                MimeType = mimeType,
                FileName = fileName
            } );

            return new FileId( id );
        }

        //====== helper class

        private sealed class Entry
        {
            public int      Id       { get; set; } = -1;
            public byte[]   Content  { get; set; } = Array.Empty<byte>();
            public MimeType MimeType { get; set; } = MimeType.Unknown;
            public string?  FileName { get; set; } = null;
        }
    }
}
