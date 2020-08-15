using FileOrganizer.Core.Services;
using System.Collections.Generic;

namespace FileOrganizer.Core
{
    public interface IFileSearcher
    {
        IReadOnlyList<FileDetails> GetFiles( PagingParameters pagingParameters ); // TODO: add search options
    }
}
