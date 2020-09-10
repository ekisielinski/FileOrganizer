using System;
using System.IO;

namespace FileOrganizer.CommonUtils
{
    public static class FileUtils
    {
        public static string GetRandomFileNameWithTimestamp( DateTime timestamp, string? filePathForExtension )
        {
            string datePart = timestamp.ToString( "yyyy-MM-dd");

            string randomName = Path.GetFileNameWithoutExtension( Path.GetRandomFileName() );
            string extensionWithDot = Path.GetExtension( filePathForExtension ?? string.Empty );

            return $"{datePart}_{randomName}{extensionWithDot}";
        }
    }
}
