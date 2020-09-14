namespace FileOrganizer.Core.FakeDatabase
{
    public interface IRequestorAccessor
    {
        // todo: should it always return user name?
        UserName? UserName { get; }

        UserRoles Roles { get; }
    }
}
