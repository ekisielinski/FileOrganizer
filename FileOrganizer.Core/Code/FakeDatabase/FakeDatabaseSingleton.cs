using FileOrganizer.CommonUtils;
using FileOrganizer.Core.Helpers;
using FileOrganizer.Core.Services;
using FileOrganizer.Services.FileDatabase;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace FileOrganizer.Core
{
    // TODO: this class is temporary

    public sealed partial class FakeDatabaseSingleton :
        IFileUploader,
        IFileDetailsReader,
        IAppUserFinder,
        IFileSearcher,
        IFileDetailsUpdater,
        IUploadInfoReader,
        IAppUserCreator,
        ICredentialsValidator,
        IAppUserReader,
        IAppUserUpdater,
        IUploadDetailsReader
    {
        readonly IFileDatabase fileDatabase;
        readonly ITimestampGenerator timestampGenerator;
        readonly IThumbnailsMaker thumbnailsMaker;
        readonly ISha256Generator sha256Generator;
        readonly IPasswordHasher passwordHasher;

        readonly List<UploadEntry> uploads = new List<UploadEntry>();
        readonly List<FileEntry> files = new List<FileEntry>();
        readonly List<UserEntry> users = new List<UserEntry>();
        
        int uploadId = -1;
        int fileId = -1;

        //====== ctors

        public FakeDatabaseSingleton(
            IFileDatabase fileDatabase,
            ITimestampGenerator timestampGenerator,
            IThumbnailsMaker thumbnailsMaker,
            ISha256Generator sha256Generator,
            IPasswordHasher passwordHasher )
        {
            this.fileDatabase       = Guard.NotNull( fileDatabase, nameof( fileDatabase ) );
            this.timestampGenerator = Guard.NotNull( timestampGenerator, nameof( timestampGenerator ) );
            this.thumbnailsMaker    = Guard.NotNull( thumbnailsMaker, nameof( thumbnailsMaker ) );
            this.sha256Generator    = Guard.NotNull( sha256Generator, nameof( sha256Generator ) ); ;
            this.passwordHasher     = passwordHasher;
        }

        //====== IFileUploader

        public UploadId Upload( UploadParameters parameters )
        {
            UtcTimestamp startTimestamp = timestampGenerator.UtcNow;

            uploadId++;

            var tempFiles = new List<FileEntry>();

            foreach (SourceFile sourceFile in parameters.SourceFiles)
            {
                UtcTimestamp timestamp = timestampGenerator.UtcNow;

                Sha256Hash hash = sha256Generator.GenerateHash( sourceFile.Content );

                string newFileName = MakeRandomFileName( sourceFile.OrginalFileName, timestamp );

                IFileInfo fileInfo = fileDatabase.GetContainer( FileDatabaseFolder.SourceFiles )
                                                 .Create( sourceFile.Content, new FileName( newFileName ) );

                string thumbFileName = CreateThumbnail( fileInfo, timestamp );

                fileId++;

                tempFiles.Add( new FileEntry
                {
                    Id            = fileId,
                    UploadId      = new UploadId( uploadId ),
                    MimeType      = sourceFile.MimeType,
                    FileName      = sourceFile.OrginalFileName,
                    WhenAdded     = timestamp,
                    DatabaseFiles = new DatabaseFiles( new FileName( newFileName ), new FileName( thumbFileName ) ),
                    Size          = new DataSize( sourceFile.Content.Length ),
                    ImageDetails  = new ImageDetails { Size = GetImageDimension( fileInfo ) },
                    Hash          = hash
                } );
            }

            files.AddRange( tempFiles );

            uploads.Add( new UploadEntry
            {
                Id          = new UploadId( uploadId ),
                Description = parameters.Description,
                WhenAdded   = startTimestamp,
                FileCount   = tempFiles.Count,
                Size        = new DataSize( parameters.SourceFiles.Sum( x => x.Content.Length ) ),
                UserName    = new UserName( "admin" ) // temp
            } );

            return new UploadId( uploadId );
        }

        //====== IFileDetailsReader

        public FileDetails? GetFileDetailsById( FileId fileId )
        {
            FileEntry entry = files.FirstOrDefault( x => x.Id == fileId.Value );

            if (entry is null) return null;

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

        public UploadDetails? GetUploadDetails( UploadId uploadId )
        {
            var upload = uploads.FirstOrDefault( x => x.Id.Value == uploadId.Value);
            
            if (upload is null) return null;

            IEnumerable<FileDetails> fileDetailsList = files
                .Where( x => x.UploadId.Value == uploadId.Value)
                .Select( x => GetFileDetailsById( new FileId( x.Id ))!);

            return new UploadDetails( uploadId, fileDetailsList, upload.Description );
        }

        //====== private methods

        private string CreateThumbnail( IFileInfo sourceFile, UtcTimestamp timestamp )
        {
            using Image? thumbnail = thumbnailsMaker.MakeThumb( sourceFile, new Size( 300, 300 ) );

            if (thumbnail is null) return string.Empty;

            using var memoryStream = new MemoryStream( 50 * 1024 );

            thumbnail.Save( memoryStream, ImageFormat.Jpeg );

            string newFileName = MakeRandomFileName( "any-name.jpg", timestamp );

            var thumbFile = fileDatabase.GetContainer( FileDatabaseFolder.Thumbnails )
                                        .Create( memoryStream, new FileName( newFileName ) );

            return thumbFile.Name;
        }

        private string MakeRandomFileName( string? fileName, UtcTimestamp timestamp )
        {
            string datePart = timestamp.Value.ToString( "yyyy-MM-dd");

            string randomName = Path.GetFileNameWithoutExtension( Path.GetRandomFileName() );
            string extensionWithDot = Path.GetExtension( fileName ?? string.Empty );

            return $"{datePart}_{randomName}{extensionWithDot}";
        }

        #region App Users

        AppUser IAppUserReader.GetUser( UserName userName )
            => users.Single( x => x.AppUserDetails.User.Name.Value == userName.Value ).AppUserDetails.User;

        AppUserDetails IAppUserReader.GetUserDetails( UserName userName )
            => users.Single( x => x.AppUserDetails.User.Name.Value == userName.Value ).AppUserDetails;

        AppUser? ICredentialsValidator.TryGetUser( UserName name, UserPassword password )
        {
            var user = users.FirstOrDefault( x => x.AppUserDetails.User.Name.Value == name.Value );
            if (user is null) return null;

            bool ok = passwordHasher.VerifyHash( user.PasswordHash, password );

            return ok ? user.AppUserDetails.User : null;
        }

        void IAppUserCreator.Create( AppUser appUser, UserPassword password )
        {
            if (users.Any( x => x.AppUserDetails.User.Name.Value == appUser.Name.Value )) throw new Exception( "User already exists." );

            string hash = passwordHasher.HashPassword( password );
            var appUserDetails = new AppUserDetails( appUser, null, timestampGenerator.UtcNow );

            users.Add( new UserEntry { AppUserDetails = appUserDetails, PasswordHash = hash } );
        }

        public IReadOnlyList<AppUser> GetAllAppUsers()
            => users.Select( x => x.AppUserDetails.User ).ToList();


        void IAppUserUpdater.SetEmail( UserName userName, EmailAddress? email )
        {
            UserEntry entry = users.Single( x => x.AppUserDetails.User.Name.Value == userName.Value );
            users.Remove( entry );

            var newEntry = new UserEntry
            {
                AppUserDetails = new AppUserDetails( entry.AppUserDetails.User, email, entry.AppUserDetails.WhenCreated )
            };

            users.Add( newEntry );
        }

        void IAppUserUpdater.SetDisplayName( UserName userName, UserDisplayName displayName )
        {
            UserEntry entry = users.Single( x => x.AppUserDetails.User.Name.Value == userName.Value );
            users.Remove( entry );

            var newUser = new AppUser( entry.AppUserDetails.User.Name, displayName, entry.AppUserDetails.User.Roles );
            var newDetails = new AppUserDetails( newUser, entry.AppUserDetails.Email, entry.AppUserDetails.WhenCreated );

            users.Add( new UserEntry { AppUserDetails = newDetails } );
        }

        #endregion

        public IReadOnlyList<FileDetails> GetFiles( PagingParameters pagingParameters )
        {
            return files
                .Skip( pagingParameters.SkipCount )
                .Take( pagingParameters.PageSize )
                .Select( x => GetFileDetailsById( new FileId( x.Id ) ) ).ToList();
        }

        public int CountFiles()
        {
            return files.Count;
        }

        public void UpdateDescription( FileId fileId, FileDescription description )
        {
            files.FirstOrDefault( x => x.Id == fileId.Value ).Description = description;
        }

        public void UpdateTitle( FileId fileId, FileTitle title )
        {
            files.FirstOrDefault( x => x.Id == fileId.Value ).Title = title;
        }

        public IReadOnlyList<UploadInfo> GetAll()
        {
            var infos = uploads.Select( x => new UploadInfo()
            {
                Id          = x.Id,
                Description = x.Description,
                WhenAdded   = x.WhenAdded,
                FileCount   = x.FileCount,
                Owner       = new AppUser(
                    x.UserName,
                    users.FirstOrDefault( appUser => appUser.AppUserDetails.User.Name.Value == x.UserName.Value ).AppUserDetails.User.DisplayName,
                    UserRoles.Empty ),
                TotalSize   = x.Size
            } );

            return infos.ToList();
        }

       
        private static Size? GetImageDimension( IFileInfo fileInfo )
        {
            try
            {
                using var stream = fileInfo.CreateReadStream();
                using Image srcImage = Image.FromStream( stream );

                return srcImage.Size;
            }
            catch
            {
                return null;
            }
        }
    }
}
