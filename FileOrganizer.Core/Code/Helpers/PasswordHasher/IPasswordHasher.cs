namespace FileOrganizer.Core
{
    public interface IPasswordHasher
    {
        string HashPassword( UserPassword password );

        bool VerifyHash( string hash, UserPassword password );
    }
}
