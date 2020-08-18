namespace FileOrganizer.Core.Services
{
    public interface IFileDetailsUpdater
    {
        void UpdateTitle( FileId fileId, FileTitle title );

        void UpdateDescription( FileId fileId, FileDescription description );
    }
}
