using System.ComponentModel.DataAnnotations;

namespace FileOrganizer.WebUI.Areas.Auth.Models
{
    public sealed class LoginForm
    {
        public string? UserName { get; init; }

        [DataType( DataType.Password )]
        public string? Password { get; init; }

        // TODO: return url?
    }
}
