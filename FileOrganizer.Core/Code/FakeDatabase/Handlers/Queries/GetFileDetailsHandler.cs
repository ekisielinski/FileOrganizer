using FileOrganizer.Domain;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileOrganizer.Core.FakeDatabase
{
    public sealed class GetFileDetailsHandler : IRequestHandler<GetFileDetailsQuery, FileDetails>
    {
        readonly FakeDatabaseSingleton database;

        //====== ctors

        public GetFileDetailsHandler( FakeDatabaseSingleton database )
        {
            this.database = database;
        }

        public Task<FileDetails> Handle( GetFileDetailsQuery request, CancellationToken cancellationToken )
        {
            FileEntry entry = database.Files.Single( x => x.Id == request.FileId.Value );

            var result = new FileDetails
            {
                FileId          = request.FileId,
                DatabaseFiles   = entry.DatabaseFiles,
                FileSize        = entry.Size,
                Description     = entry.Description,
                Title           = entry.Title,
                ImageDetails    = entry.ImageDetails,
                Uploader        = database.Users.First( x => x.AppUserDetails.User.Name.Value == entry.Uploader.Value ).AppUserDetails.User.ToAppUserNames(),
                PrimaryDateTime = entry.PrimaryDateTime
            };

            return Task.FromResult( result );
        }
    }
}
