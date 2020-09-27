using FileOrganizer.Core;
using System.Collections.Generic;

namespace FileOrganizer.WebUI.Models
{
    public sealed class CreateNewUserRequest
    {
        public string? UserName        { get; set; }
        public string? UserDisplayName { get; set; }
        public string? Password        { get; set; }
        public bool    IsModerator     { get; set; }

        //====== public methods

        public UserPassword ToUserPassword() => new UserPassword( Password );

        public AppUser ToAppUser()
        {
            var userName = new UserName( UserName! );
            var displayName = new UserDisplayName( UserDisplayName ?? string.Empty );

            var rolesList = new List<UserRole>();
            if (IsModerator) rolesList.Add( UserRole.Moderator );
            var roles = new UserRoles( rolesList );

            return new AppUser( userName, displayName, roles );
        }
    }
}
