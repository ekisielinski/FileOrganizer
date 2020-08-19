using System.Collections.Generic;

namespace FileOrganizer.Core
{
    public interface IUploadInfoReader
    {
        IReadOnlyList<UploadInfo> GetAll();
    }
}
