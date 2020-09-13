﻿using FileOrganizer.Core.Helpers;
using System.Collections.Generic;

namespace FileOrganizer.Core.FakeDatabase
{
    public sealed partial class FakeDatabaseSingleton
    {
        readonly ITimestampGenerator timestampGenerator;

        internal int uploadId = -1;
        internal int fileId = -1;

        internal List<UploadEntry> Uploads { get; } = new List<UploadEntry>();
        internal List<FileEntry>   Files   { get; } = new List<FileEntry>();
        internal List<UserEntry>   Users   { get; } = new List<UserEntry>();

        //====== ctors

        public FakeDatabaseSingleton( ITimestampGenerator timestampGenerator )
        {
            this.timestampGenerator = timestampGenerator;
        }

        //====== public properties

        public UtcTimestamp UtcNow => timestampGenerator.UtcNow;
    }
}
