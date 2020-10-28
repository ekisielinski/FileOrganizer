using FileOrganizer.CommonUtils;
using FileOrganizer.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FileOrganizer.EFDatabase.Handlers
{
    public sealed class GetFileDetailsHandler : IRequestHandler<GetFileDetailsQuery, FileDetails>
    {
        readonly EFAppContext context;

        //====== ctors

        public GetFileDetailsHandler( EFAppContext context )
        {
            this.context = Guard.NotNull( context, nameof( context ) );
        }

        //====== IRequestHandler

        public async Task<FileDetails> Handle( GetFileDetailsQuery request, CancellationToken cancellationToken )
        {
            FileEntity? entity = await context.Entities
                .Files
                .Include( x => x.Uploader )
                .Include( x => x.Image )
                .AsNoTracking()
                .FirstOrDefaultAsync( x => x.Id == request.FileId.Value );

            if (entity is null) throw new System.Exception( "File not found." ); // todo: custom ex

            return MappingUtils.ToFileDetails( entity );
        }
    }
}
