using System.Collections.Generic;
using System.Linq;

namespace FileOrganizer.Core.FakeDatabase
{
    public sealed class UploadInfoReader : IUploadInfoReader
    {
        readonly FakeDatabaseSingleton database;

        //====== ctors

        public UploadInfoReader( FakeDatabaseSingleton database )
        {
            this.database = database;
        }

        //====== IUploadInfoReader

        public IReadOnlyList<UploadInfo> GetAll()
        {
            var infos = database.Uploads.Select( x => new UploadInfo()
            {
                Id          = x.Id,
                Description = x.Description,
                WhenAdded   = x.WhenAdded,
                FileCount   = x.FileCount,
                Owner       = GetUserNames( x.UserName ),
                TotalSize   = x.Size
            } );

            return infos.ToList();
        }

        //====== private methods

        private IAppUserNames GetUserNames( UserName userName )
        {
            UserDisplayName displayName = database.Users
                .First( appUser => appUser.AppUserDetails.User.Name.Value == userName.Value )
                .AppUserDetails.User.DisplayName;

            return new AppUser( userName, displayName, UserRoles.Empty );
        }
    }
}
