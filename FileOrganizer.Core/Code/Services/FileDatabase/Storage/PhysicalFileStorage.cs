using FileOrganizer.CommonUtils;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;
using System.IO;

namespace FileOrganizer.Services.FileDatabase
{
    public sealed class PhysicalFileStorage : IFileStorage
    {
        readonly PhysicalFileProvider provider;

        //====== ctors

        public PhysicalFileStorage( string rootDirPath )
        {
            Guard.NotNull( rootDirPath, nameof( rootDirPath ) );

            provider = new PhysicalFileProvider( rootDirPath );
        }

        //====== IFileStorage

        public IFileInfo Create( Stream stream, FileName fileName )
        {
            Guard.NotNull( fileName, nameof( fileName ) );

            Directory.CreateDirectory( Path.GetDirectoryName( provider.Root ) );

            string filePath = Path.Combine( provider.Root, fileName.Value );

            using (FileStream fileStream = File.Create( filePath ))
            {
                stream.Position = 0;
                stream.CopyTo( fileStream );
            }

            return new PhysicalFileInfo( new FileInfo( filePath ) );
        }

        public IFileInfo Get( FileName fileName ) => provider.GetFileInfo( fileName.Value );
    }
}
