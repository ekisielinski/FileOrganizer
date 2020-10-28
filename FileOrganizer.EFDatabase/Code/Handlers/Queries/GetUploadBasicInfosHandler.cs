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
    public sealed class GetUploadBasicInfosHandler : IRequestHandler<GetUploadBasicInfosQuery, IReadOnlyList<UploadBasicInfo>>
    {
        readonly EFAppContext context;

        //====== ctors

        public GetUploadBasicInfosHandler( EFAppContext context )
        {
            this.context = Guard.NotNull( context, nameof( context ) );
        }

        //====== IRequestHandler

        public async Task<IReadOnlyList<UploadBasicInfo>> Handle( GetUploadBasicInfosQuery request, CancellationToken cancellationToken )
        {
            List<UploadEntity>? entities = await context.Entities
                .Uploads
                .Include( x => x.Uploader )
                .OrderByDescending( x => x.UtcWhenAdded )
                .AsNoTracking()
                .ToListAsync();

            return entities.Select( MappingUtils.ToUploadBasicInfo ).ToList();
        }
    }
}
