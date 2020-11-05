using FileOrganizer.CommonUtils;
using FileOrganizer.Domain;
using System;
using System.Drawing;
using System.Linq;

namespace FileOrganizer.EFDatabase
{
    internal static class MappingUtils
    {
        public static Size? GetImageDimensions( ImageEntity entity )
        {
            Guard.NotNull( entity, nameof( entity ) );

            if (entity.Width is null && entity.Height is null) return null;

            return new Size( entity.Width!.Value, entity.Height!.Value );
        }

        public static UploadBasicInfo ToUploadBasicInfo( UploadEntity entity )
        {
            Guard.NotNull( entity, nameof( entity ) );

            if (entity.Uploader is null) throw new ArgumentException( $"The {nameof( entity.Uploader )} property must be included." );

            return new UploadBasicInfo
            {
                Id           = new UploadId( entity.Id ),

                Uploader     = ToAppUserNames( entity.Uploader ),
                WhenUploaded = new UtcTimestamp( entity.UtcWhenAdded.ToUniversalTime() ),

                Description  = new UploadDescription( entity.Description ?? string.Empty ),

                TotalSize    = new DataSize( entity.TotalSize ),
                FileCount    = entity.FileCount
            };
        }

        public static AppUserDetails ToAppUserDetails( AppUserEntity entity )
        {
            Guard.NotNull( entity, nameof( entity ) );

            return new AppUserDetails(
                ToAppUser( entity ),
                entity.EmailAddress is null ? null : new EmailAddress(entity.EmailAddress),
                new UtcTimestamp( entity.UtcWhenCreated.ToUniversalTime() )
            );
        }

        public static AppUser ToAppUser( AppUserEntity entity )
        {
            Guard.NotNull( entity, nameof( entity ) );

            return new AppUser(
              new UserName( entity.UserName ),
              new UserDisplayName( entity.DisplayName ?? string.Empty ),
              new UserRoles( entity.UserRoles.Select( ToUserRole ) ) );
        }

        public static AppUserNames ToAppUserNames( AppUserEntity entity )
        {
            Guard.NotNull( entity, nameof( entity ) );

            return new AppUserNames( new UserName( entity.UserName ), new UserDisplayName( entity.DisplayName ?? string.Empty ) );
        }

        public static ActivityLogEntry ToActivityLogEntry( ActivityLogEntity entity )
        {
            Guard.NotNull( entity, nameof( entity ) );

            if (entity.Issuer is null) throw new InvalidOperationException( $"The {nameof( entity.Issuer )} property must be included." );

            return new ActivityLogEntry(
                new UserName( entity.Issuer.UserName ),
                new UtcTimestamp( entity.UtcTimestamp.ToUniversalTime() ),
                entity.Message );
        }

        public static FileDetails ToFileDetails( FileEntity entity )
        {
            Guard.NotNull( entity, nameof( entity ) );

            if (entity.Image is null) throw new Exception( "Image property must be included." );

            return new FileDetails
            {
                FileId          = new FileId( entity.Id ),

                Uploader        = ToAppUserNames( entity.Uploader ),
                DatabaseFiles   = new DatabaseFiles( new Domain.FileName( entity.SourceFileName ), entity.Image.ThumbnailFileName is null ? null : new FileName( entity.Image.ThumbnailFileName ) ),

                Title           = new FileTitle( entity.Title ?? "" ),
                Description     = new FileDescription( entity.Description ?? "" ),
                PrimaryDateTime = ToPrimaryDateTime( entity.PrimaryDateTime ),

                FileSize        = new DataSize( entity.SourceFileSize ),

                ImageDetails = new ImageDetails()
                {
                    Dimensions = GetImageDimensions( entity.Image )
                }
            };
        }

        public static PartialDateTime ToPrimaryDateTime( string? value )
            => value is null ? PartialDateTime.Empty : PartialDateTime.FromSpecialString( value );

        public static UserRole ToUserRole( UserRolesEntity entity )
        {
            Guard.NotNull( entity, nameof( entity ) );

            return new UserRole( entity.RoleName );
        }

        public static FileLink ToFileLink( FileLinkEntity entity )
        {
            if (entity.Issuer is null) throw new ArgumentException( "The Issuer property must be included." );

            return new FileLink
            {
                //FileId    = new FileId( entity.FileId ),
                FileId = new FileId( entity.File.Id ),

                Id        = new LinkId( entity.Id ),
                Address   = new LinkUrl( entity.Address ),
                Title     = new LinkTitle( entity.Title ),
                Comment   = new LinkComment( entity.Comment ),
                WhenAdded = new UtcTimestamp( entity.UtcWhenAdded.ToUniversalTime() ),
                AddedBy   = ToAppUserNames( entity.Issuer )
            };
        }
    }
}
