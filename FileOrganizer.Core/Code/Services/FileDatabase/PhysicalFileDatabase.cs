using FileOrganizer.CommonUtils;
using FileOrganizer.Core;
using FileOrganizer.Core.Helpers;
using System.IO;

namespace FileOrganizer.Services.FileDatabase
{
    public sealed class PhysicalFileDatabase : IFileDatabase, IFileDatabaseCleaner
    {
        readonly string rootPath;

        PhysicalFileContainer? sourceFiles = null;
        PhysicalFileContainer? thumbnails = null;

        //====== ctors

        public PhysicalFileDatabase( string rootPath )
        {
            Guard.NotNull( rootPath, nameof( rootPath ) );

            this.rootPath = Path.GetFullPath( rootPath );
        }

        //====== IFileDatabase

        public IFileContainer SourceFiles => sourceFiles ??= CreateContainer( "Files" );
        public IFileContainer Thumbnails  => thumbnails  ??= CreateContainer( "Thumbs" );

        //====== IFileDatabaseReader

        public IFileContainerReader SourceFilesReader => SourceFiles;
        public IFileContainerReader ThumbnailsReader  => Thumbnails;

        //====== IFileDatabaseCleaner

        public void DeleteAllFiles()
        {
            _ = SourceFiles;
            _ = Thumbnails;

            sourceFiles!.DeleteAllFiles();
            thumbnails!.DeleteAllFiles();
        }

        //====== private methods

        private PhysicalFileContainer CreateContainer( string dirName )
        {
            var dirPath = new DirectoryFullPath( Path.Combine( rootPath, dirName ) );

            return new PhysicalFileContainer( dirPath );
        }
    }
}
