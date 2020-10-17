using FileOrganizer.Domain;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileOrganizer.Core.FakeDatabase
{
    public sealed class GetUploadBasicInfosHandler : IRequestHandler<GetUploadBasicInfosQuery, IReadOnlyList<UploadBasicInfo>>
    {
        private readonly FakeDatabaseSingleton database;

        public GetUploadBasicInfosHandler( FakeDatabaseSingleton database )
        {
            this.database = database;
        }

        public Task<IReadOnlyList<UploadBasicInfo>> Handle( GetUploadBasicInfosQuery request, CancellationToken cancellationToken )
        {
            var infos = database.Uploads.Select( x => new UploadBasicInfo()
            {
                Id           = x.Id,
                Description  = x.Description,
                WhenUploaded = x.WhenAdded,
                FileCount    = x.FileCount,
                Uploader     = GetUserNames( x.UserName ),
                TotalSize    = x.Size
            } );

            IReadOnlyList<UploadBasicInfo> res = infos.OrderByDescending( x => x.WhenUploaded.Value ).ToList();
            return Task.FromResult( res );
        }

        //====== private methods

        private AppUserNames GetUserNames( UserName userName )
        {
            UserDisplayName displayName = database.Users
                .First( appUser => appUser.AppUserDetails.User.Name.Value == userName.Value )
                .AppUserDetails.User.DisplayName;

            return new AppUserNames( userName, displayName );
        }
    }
}
