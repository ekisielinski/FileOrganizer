using FileOrganizer.CommonUtils;
using FileOrganizer.Domain;
using FileOrganizer.EFDatabase;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileOrganizer.Core.FakeDatabase
{
    public sealed class GetFileLinksHandler : IRequestHandler<GetFileLinksQuery, IReadOnlyList<FileLink>>
    {
        readonly EFAppContext context;

        //====== ctors

        public GetFileLinksHandler( EFAppContext context )
            => this.context = Guard.NotNull( context, nameof( context ) );

        //====== IRequestHandler

        public async Task<IReadOnlyList<FileLink>> Handle( GetFileLinksQuery request, CancellationToken cancellationToken )
        {
            FileEntity? file = await context.Entities
                .Files
                .AsNoTracking()
                .Include( nameof( FileEntity.Links ) + "." + nameof( LinkEntity.Issuer ) )
                .FirstOrDefaultAsync( x => x.Id == request.FileId.Value );

            if (file is null) throw new Exception( "File not found." ); // TODO: custom ex

            return file.Links!.Select( MappingUtils.ToFileLink ).ToList();
        }
    }
}
