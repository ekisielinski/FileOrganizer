namespace FileOrganizer.Core.FakeDatabase
{
    internal sealed class UserEntry
    {
        public AppUserDetails AppUserDetails { get; set; }

        public string PasswordHash { get; set; }
    }
}
