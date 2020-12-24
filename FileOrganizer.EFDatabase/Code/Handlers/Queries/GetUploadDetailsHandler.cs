using FileOrganizer.CommonUtils;
using FileOrganizer.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileOrganizer.EFDatabase.Handlers
{
    public sealed class GetUploadDetailsHandler : IRequestHandler<GetUploadDetailsQuery, UploadDetails>
    {
        readonly EFAppContext context;

        //====== ctors

        public GetUploadDetailsHandler( EFAppContext context )
            => this.context = Guard.NotNull( context, nameof( context ) );

        //====== IRequestHandler

        public async Task<UploadDetails> Handle( GetUploadDetailsQuery request, CancellationToken cancellationToken )
        {
            UploadEntity? entity = await context.Entities
                .Uploads
                .AsNoTracking()
                .Include( "Files.Image" ) // todo nameof
                .Include( "Files.Uploader")
                .FirstOrDefaultAsync( x => x.Id == request.UploadId.Value);

            if (entity is null) throw new Exception( "Upload not found." ); // TODO: custom exception

            IEnumerable<FileDetails> fileDetailsList = entity.Files.Select( MappingUtils.ToFileDetails );

            return new UploadDetails(
                request.UploadId,
                fileDetailsList,
                new UploadDescription( entity.Description ?? string.Empty ),
                new string[] { }  ); // todo: add rejected to dastabase
        }
    }
}
