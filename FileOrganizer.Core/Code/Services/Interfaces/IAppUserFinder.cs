using System.Collections.Generic;

namespace FileOrganizer.Core
{
    public interface IAppUserFinder
    {
        IReadOnlyList<AppUser> GetAllAppUsers();
    }
}
