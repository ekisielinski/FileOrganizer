﻿using FileOrganizer.CommonUtils;

namespace FileOrganizer.Domain
{
    public sealed class DatabaseFiles
    {
        public DatabaseFiles( FileName source, FileName? thumbnail )
        {
            Source    = Guard.NotNull( source, nameof( source ) );
            Thumbnail = thumbnail;
        }

        //====== public properties

        public FileName  Source    { get; }
        public FileName? Thumbnail { get; } // TODO: move thumbnail to image details
    }
}
