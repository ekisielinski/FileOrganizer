using System.IO;

namespace FileOrganizer.Core
{
    public interface ISha256Generator
    {
        Sha256Hash GenerateHash( Stream stream );
    }
}
