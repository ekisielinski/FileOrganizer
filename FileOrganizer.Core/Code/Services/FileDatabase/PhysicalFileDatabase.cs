using FileOrganizer.CommonUtils;
using FileOrganizer.Core;
using FileOrganizer.Core.Helpers;
using System;
using System.IO;

namespace FileOrganizer.Services.FileDatabase
{
    public sealed class PhysicalFileDatabase : IFileDatabase, IFileDatabaseCleaner
    {
        readonly string rootPath;

        //====== ctors

        public PhysicalFileDatabase( string rootPath )
        {
            Guard.NotNull( rootPath, nameof( rootPath ) );

            this.rootPath = Path.GetFullPath( rootPath );
        }

        //===== IFileDatabase

        public IFileContainer GetContainer( FileDatabaseFolder folder )
            => new PhysicalFileContainer( GetFolderFullPath( folder ) );

        public IFileContainerReader GetContainerReader( FileDatabaseFolder folder )
            => new PhysicalFileContainer( GetFolderFullPath( folder ) );

        //====== IFileDatabaseCleaner

        public void DeleteAllFiles()
        {
            var sourceFiles = GetContainer( FileDatabaseFolder.SourceFiles) as PhysicalFileContainer;
            sourceFiles.DeleteAllFiles();

            var thumbFiles = GetContainer( FileDatabaseFolder.Thumbnails) as PhysicalFileContainer;
            thumbFiles.DeleteAllFiles();
        }

        //====== private methods

        private DirectoryFullPath GetFolderFullPath( FileDatabaseFolder folder )
        {
            string dirPath = Path.Combine( rootPath, folder switch
            {
                FileDatabaseFolder.SourceFiles => "Files",
                FileDatabaseFolder.Thumbnails  => "Thumbs",

                _ => throw new ArgumentException( "Invalid enum value.", nameof( folder ) )
            } );

            return new DirectoryFullPath( dirPath );
        }
    }
}
