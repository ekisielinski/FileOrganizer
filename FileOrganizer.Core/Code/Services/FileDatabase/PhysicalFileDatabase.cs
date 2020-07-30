using FileOrganizer.CommonUtils;
using System;
using System.IO;

namespace FileOrganizer.Services.FileDatabase
{
    public sealed class PhysicalFileDatabase : IFileDatabase
    {
        readonly string rootPath;

        //====== ctors

        public PhysicalFileDatabase( string rootPath )
        {
            Guard.NotNull( rootPath, nameof( rootPath ) );

            this.rootPath = Path.GetFullPath( rootPath );
        }

        //===== IFileDatabase

        public IFileStorage GetStorage( FileDatabaseFolder folder )
            => new PhysicalFileStorage( GetFolderFullPath( folder ) );

        public IFileStorageReader GetStorageReader( FileDatabaseFolder folder )
            => new PhysicalFileStorage( GetFolderFullPath( folder ) );

        //====== private methods

        private string GetFolderFullPath( FileDatabaseFolder folder )
        {
            return Path.Combine( rootPath, folder switch
            {
                FileDatabaseFolder.Files  => "Files",
                FileDatabaseFolder.Thumbs => "Thumbs",

                _ => throw new ArgumentException( "Invalid enum value.", nameof( folder ) )
            } );
        }
    }
}
