using FileOrganizer.Domain;

namespace FileOrganizer.WebUI.Models
{
    public sealed class CreateNewUserRequest
    {
        public string? UserName        { get; set; }
        public string? UserDisplayName { get; set; }
        public string? Password        { get; set; }

        //====== public methods

        public UserPassword ToUserPassword() => new UserPassword( Password );
        public UserName ToUserName() => new UserName( UserName );
        public UserDisplayName ToUserDisplayName() => new UserDisplayName( UserDisplayName );
    }
}
