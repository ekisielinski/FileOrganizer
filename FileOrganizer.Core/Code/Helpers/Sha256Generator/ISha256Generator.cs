using System.IO;

namespace FileOrganizer.Core.Helpers
{
    public interface ISha256Generator
    {
        Sha256Hash GenerateHash( Stream stream );
    }
}
