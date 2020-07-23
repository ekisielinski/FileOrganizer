namespace FileOrganizer.WebUI.Services.Auth
{
    public interface IAuthUserAccessor
    {
        AuthUser? CurrentUser { get; }
    }
}
