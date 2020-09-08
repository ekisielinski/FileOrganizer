using FileOrganizer.CommonUtils;
using System.IO;
using System.Security.Cryptography;

namespace FileOrganizer.Core.Helpers
{
    public sealed class Sha256Generator : ISha256Generator
    {
        public Sha256Hash GenerateHash( Stream stream )
        {
            Guard.NotNull( stream, nameof( stream ) );

            using var sha = SHA256.Create();

            stream.Position = 0;
            byte[] data = sha.ComputeHash( stream );

            return new Sha256Hash( data );
        }
    }
}
