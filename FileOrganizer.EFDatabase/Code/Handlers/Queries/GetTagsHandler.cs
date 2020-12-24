using FileOrganizer.CommonUtils;
using FileOrganizer.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileOrganizer.EFDatabase.Handlers
{
    public sealed class GetTagsHandler : IRequestHandler<GetTagsQuery, IReadOnlyList<Tag>>
    {
        readonly EFAppContext context;

        //====== ctors

        public GetTagsHandler( EFAppContext context )
            => this.context = Guard.NotNull( context, nameof( context ) );

        //====== IRequestHandler

        public async Task<IReadOnlyList<Tag>> Handle( GetTagsQuery request, CancellationToken cancellationToken )
        {
            List<TagEntity> tags = await context.Entities.Tags.ToListAsync();

            return tags.Select( MappingUtils.ToTag ).ToList();
        }
    }
}
