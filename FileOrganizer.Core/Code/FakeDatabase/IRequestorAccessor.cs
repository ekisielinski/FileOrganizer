namespace FileOrganizer.Core.FakeDatabase
{
    public interface IRequestorAccessor
    {
        UserName? UserName { get; }

        UserRoles Roles { get; }
    }
}
