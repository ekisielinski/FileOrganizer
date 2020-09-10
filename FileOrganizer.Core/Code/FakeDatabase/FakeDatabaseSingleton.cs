using FileOrganizer.CommonUtils;
using FileOrganizer.Core.Helpers;
using FileOrganizer.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileOrganizer.Core
{
    // TODO: this class is temporary

    public sealed partial class FakeDatabaseSingleton :
        IFileDetailsReader,
        IAppUserFinder,
        IFileSearcher,
        IFileDetailsUpdater,
        IAppUserCreator,
        ICredentialsValidator,
        IAppUserReader,
        IAppUserUpdater
    {
        readonly ITimestampGenerator timestampGenerator;
        readonly IPasswordHasher passwordHasher;
        
        internal int fileId = -1;
        internal int uploadId = -1;

        internal List<UploadEntry> Uploads { get; } = new List<UploadEntry>();
        internal List<FileEntry>   Files   { get; } = new List<FileEntry>();
        internal List<UserEntry>   Users   { get; } = new List<UserEntry>();

        //====== ctors

        public FakeDatabaseSingleton( ITimestampGenerator timestampGenerator, IPasswordHasher passwordHasher )
        {
            this.timestampGenerator = Guard.NotNull( timestampGenerator, nameof( timestampGenerator ) );
            this.passwordHasher     = passwordHasher;
        }

        //====== IFileDetailsReader

        public FileDetails GetFileDetailsById( FileId fileId )
        {
            FileEntry entry = Files.Single( x => x.Id == fileId.Value );

            return new FileDetails
            {
                FileId        = fileId,
                DatabaseFiles = entry.DatabaseFiles,
                FileSize      = entry.Size,
                Description   = entry.Description,
                Title         = entry.Title,
                ImageDetails  = entry.ImageDetails
            };
        }

        AppUser IAppUserReader.GetUser( UserName userName )
            => Users.Single( x => x.AppUserDetails.User.Name.Value == userName.Value ).AppUserDetails.User;

        AppUserDetails IAppUserReader.GetUserDetails( UserName userName )
            => Users.Single( x => x.AppUserDetails.User.Name.Value == userName.Value ).AppUserDetails;

        AppUser? ICredentialsValidator.TryGetUser( UserName name, UserPassword password )
        {
            var user = Users.FirstOrDefault( x => x.AppUserDetails.User.Name.Value == name.Value );
            if (user is null) return null;

            bool ok = passwordHasher.VerifyHash( user.PasswordHash, password );

            return ok ? user.AppUserDetails.User : null;
        }

        void IAppUserCreator.Create( AppUser appUser, UserPassword password )
        {
            if (Users.Any( x => x.AppUserDetails.User.Name.Value == appUser.Name.Value )) throw new Exception( "User already exists." );

            string hash = passwordHasher.HashPassword( password );
            var appUserDetails = new AppUserDetails( appUser, null, timestampGenerator.UtcNow );

            Users.Add( new UserEntry { AppUserDetails = appUserDetails, PasswordHash = hash } );
        }

        public IReadOnlyList<AppUser> GetAllAppUsers()
            => Users.Select( x => x.AppUserDetails.User ).ToList();


        void IAppUserUpdater.SetEmail( UserName userName, EmailAddress? email )
        {
            UserEntry entry = Users.Single( x => x.AppUserDetails.User.Name.Value == userName.Value );
            Users.Remove( entry );

            var newEntry = new UserEntry
            {
                AppUserDetails = new AppUserDetails( entry.AppUserDetails.User, email, entry.AppUserDetails.WhenCreated )
            };

            Users.Add( newEntry );
        }

        void IAppUserUpdater.SetDisplayName( UserName userName, UserDisplayName displayName )
        {
            UserEntry entry = Users.Single( x => x.AppUserDetails.User.Name.Value == userName.Value );
            Users.Remove( entry );

            var newUser = new AppUser( entry.AppUserDetails.User.Name, displayName, entry.AppUserDetails.User.Roles );
            var newDetails = new AppUserDetails( newUser, entry.AppUserDetails.Email, entry.AppUserDetails.WhenCreated );

            Users.Add( new UserEntry { AppUserDetails = newDetails } );
        }

        public IReadOnlyList<FileDetails> GetFiles( PagingParameters pagingParameters )
        {
            return Files
                .Skip( pagingParameters.SkipCount )
                .Take( pagingParameters.PageSize )
                .Select( x => GetFileDetailsById( new FileId( x.Id ) ) ).ToList();
        }

        public int CountFiles()
        {
            return Files.Count;
        }

        public void UpdateDescription( FileId fileId, FileDescription description )
        {
            Files.FirstOrDefault( x => x.Id == fileId.Value ).Description = description;
        }

        public void UpdateTitle( FileId fileId, FileTitle title )
        {
            Files.FirstOrDefault( x => x.Id == fileId.Value ).Title = title;
        }
    }
}
