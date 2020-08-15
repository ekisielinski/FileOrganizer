using FileOrganizer.Core.Services;
using System.Collections.Generic;

namespace FileOrganizer.Core
{
    public interface IFileSearcher
    {
        IReadOnlyList<FileDetails> GetAll(); // TODO: add search options and paging
    }
}
