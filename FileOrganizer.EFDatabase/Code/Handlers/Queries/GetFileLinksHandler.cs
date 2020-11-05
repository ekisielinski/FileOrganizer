using FileOrganizer.CommonUtils;
using FileOrganizer.Domain;
using FileOrganizer.EFDatabase;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace FileOrganizer.Core.FakeDatabase
{
    public sealed class GetFileLinksHandler : IRequestHandler<GetFileLinksQuery, IReadOnlyList<FileLink>>
    {
        readonly EFAppContext context;

        //====== ctors

        public GetFileLinksHandler( EFAppContext context )
        {
            this.context = Guard.NotNull( context, nameof( context ) );
        }

        //====== IRequestHandler

        public async Task<IReadOnlyList<FileLink>> Handle( GetFileLinksQuery request, CancellationToken cancellationToken )
        {
            var file = await context.Entities
                .Files
                .Include( "Links.Issuer" ) // todo: nameof
                .FirstOrDefaultAsync( x => x.Id == request.FileId.Value );

            return file.Links.Select( x => MappingUtils.ToFileLink( x ) ).ToList();
        }
    }
}
