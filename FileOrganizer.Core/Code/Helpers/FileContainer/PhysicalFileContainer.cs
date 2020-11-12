using FileOrganizer.CommonUtils;
using FileOrganizer.Domain;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;
using System.IO;

namespace FileOrganizer.Core.Helpers
{
    public sealed class PhysicalFileContainer : IFileContainer
    {
        readonly PhysicalFileProvider provider;

        //====== ctors

        public PhysicalFileContainer( DirectoryFullPath root )
        {
            Guard.NotNull( root, nameof( root ) );

            provider = new PhysicalFileProvider( root.Value );
        }

        //====== IFileStorage

        public IFileInfo Create( Stream stream, FileName fileName )
        {
            Guard.NotNull( stream, nameof( stream ) );
            Guard.NotNull( fileName, nameof( fileName ) );

            Directory.CreateDirectory( provider.Root );

            string filePath = Path.Combine( provider.Root, fileName.Value );

            using (FileStream fileStream = File.Create( filePath ))
            {
                stream.Position = 0;
                stream.CopyTo( fileStream );
            }

            return new PhysicalFileInfo( new FileInfo( filePath ) );
        }

        public IFileInfo Get( FileName fileName ) => provider.GetFileInfo( fileName.Value );

        //====== public methods

        public void DeleteAllFiles()
        {
            var dir = new DirectoryInfo( provider.Root );

            foreach (FileInfo file in dir.EnumerateFiles())
            {
                file.Delete();
            }
        }
    }
}
