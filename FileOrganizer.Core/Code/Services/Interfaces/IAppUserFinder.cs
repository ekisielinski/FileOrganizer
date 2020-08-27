using System.Collections.Generic;

namespace FileOrganizer.Core
{
    public interface IAppUserFinder
    {
        AppUser? Find( UserName userName );

        // TODO: it needs separate interface?
        IReadOnlyList<AppUser> GetAllAppUsers();
    }
}
