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
                Owner       = new AppUser(
                    x.UserName,
                    database.Users.FirstOrDefault( appUser => appUser.AppUserDetails.User.Name.Value == x.UserName.Value )
                                  .AppUserDetails.User.DisplayName,
                    UserRoles.Empty ),
                TotalSize   = x.Size
            } );

            return infos.ToList();
        }
    }
}
