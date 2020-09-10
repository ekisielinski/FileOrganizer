using FileOrganizer.Core.Services;

namespace FileOrganizer.Core
{
    public interface IFileDetailsReader : IDomainCommand
    {
        FileDetails GetFileDetailsById( FileId fileId );
    }
}
