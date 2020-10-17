using FileOrganizer.Domain;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileOrganizer.Core.FakeDatabase
{
    public sealed class GetUploadDetailsHandler : IRequestHandler<GetUploadDetailsQuery, UploadDetails>
    {
        readonly FakeDatabaseSingleton database;
        readonly IMediator mediator;

        //====== ctors

        public GetUploadDetailsHandler( FakeDatabaseSingleton database, IMediator mediator )
        {
            this.database = database;
            this.mediator = mediator;
        }

        public Task<UploadDetails> Handle( GetUploadDetailsQuery request, CancellationToken cancellationToken )
        {
            UploadEntry upload = database.Uploads.Single( x => x.Id.Value == request.UploadId.Value);

            IEnumerable<FileDetails> fileDetailsList = database.Files
                .Where( x => x.UploadId.Value == request.UploadId.Value)
                .Select( x => GetFileDetails( new FileId( x.Id ))!);

            var result = new UploadDetails( request.UploadId, fileDetailsList, upload.Description, upload.RejectedDuplicates );

            return Task.FromResult( result );
        }

        private FileDetails GetFileDetails( FileId fileId )
        {
            return mediator.Send( new GetFileDetailsQuery( fileId ) ).Result; // todo: result, remove calling command
        }
    }
}
