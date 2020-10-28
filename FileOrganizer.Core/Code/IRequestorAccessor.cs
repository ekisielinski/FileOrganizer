using FileOrganizer.Domain;

namespace FileOrganizer.Core
{
    public interface IRequestorAccessor
    {
        UserName UserName { get; }

        UserRoles Roles { get; }
    }
}
