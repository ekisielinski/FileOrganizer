using FileOrganizer.Core.Services;
using System.Collections.Generic;
using System.Linq;

namespace FileOrganizer.Core.FakeDatabase
{
    public sealed class UploadDetailsReader : IUploadDetailsReader
    {
        readonly FakeDatabaseSingleton database;
        readonly IFileDetailsReader fileDetailsReader;

        //====== ctors

        public UploadDetailsReader( FakeDatabaseSingleton database, IFileDetailsReader fileDetailsReader )
        {
            this.database = database;
            this.fileDetailsReader = fileDetailsReader;
        }

        //====== IUploadDetailsReader

        public UploadDetails GetUploadDetails( UploadId uploadId )
        {
            UploadEntry upload = database.Uploads.Single( x => x.Id.Value == uploadId.Value);

            IEnumerable<FileDetails> fileDetailsList = database.Files
                .Where( x => x.UploadId.Value == uploadId.Value)
                .Select( x => fileDetailsReader.GetFileDetailsById( new FileId( x.Id ))!);

            return new UploadDetails( uploadId, fileDetailsList, upload.Description );
        }
    }
}
