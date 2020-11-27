using FileOrganizer.Domain;

namespace FileOrganizer.WebUI.Pages
{
    public sealed class CreateUserForm
    {
        public string? UserName        { get; init; }
        public string? UserDisplayName { get; init; }
        public string? Password        { get; init; }

        //====== public methods

        public UserPassword    ToUserPassword()    => new( Password );
        public UserName        ToUserName()        => new( UserName );
        public UserDisplayName ToUserDisplayName() => new( UserDisplayName ?? string.Empty );
    }
}
