using FileOrganizer.Domain;
using System.Threading.Tasks;

namespace FileOrganizer.WebUI.Services.Auth
{
    public interface IAuthService : IAuthUserAccessor
    {
        Task<bool> LoginAsync( UserName userName, string password );

        Task LogoutAsync();
    }
}
