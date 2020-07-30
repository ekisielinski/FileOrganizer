namespace FileOrganizer.Services.FileDatabase
{
    public interface IFileDatabase : IFileDatabaseReader
    {
        IFileStorage GetStorage( FileDatabaseFolder folder );
    }
}
