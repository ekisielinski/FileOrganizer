using FileOrganizer.CommonUtils;
using System.Collections.Generic;

namespace FileOrganizer.Core.FakeDatabase
{
    public sealed class UploadEntry
    {
        public UploadId          Id          { get; set; }
        public UploadDescription Description { get; set; } = UploadDescription.None;
        public UtcTimestamp      WhenAdded   { get; set; }
        public int               FileCount   { get; set; }
        public DataSize          Size        { get; set; }
        public UserName          UserName    { get; set; }

        public IReadOnlyList<string> RejectedDuplicates { get; set; }
    }
}
