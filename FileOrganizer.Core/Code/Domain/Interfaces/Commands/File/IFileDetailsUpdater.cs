namespace FileOrganizer.Core
{
    public interface IFileDetailsUpdater : IDomainCommand
    {
        void UpdateTitle( FileId fileId, FileTitle title );

        void UpdateDescription( FileId fileId, FileDescription description );
    }
}
