using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace FileOrganizer.EFDatabase
{
    public sealed class AppUserEntity
    {
        public int      Id             { get; set; }
                                       
        public string   UserName       { get; set; } = string.Empty;
        public string?  DisplayName    { get; set; }
        public string?  EmailAddress   { get; set; }
        public string   PasswordHash   { get; set; } = string.Empty;
        public DateTime UtcWhenCreated { get; set; }

        //---

        public ICollection<ActivityLogEntryEntity> ActivityLog { get; set; }
        public ICollection<UserRolesEntity>        UserRoles   { get; set; }
        public ICollection<UploadEntity>           Uploads     { get; set; }
        public ICollection<FileEntity>             Files       { get; set; }

        //====== public static methods

        public static void Configure( EntityTypeBuilder<AppUserEntity> builder )
        {
            builder
                .HasMany( x => x.Files )
                .WithOne( x => x.Uploader );


            builder
                .HasMany( x => x.UserRoles )
                .WithOne( x => x.AssignedTo );

            builder
                .HasMany( x => x.ActivityLog )
                .WithOne( x => x.Issuer );

            //--- app users

            builder
                .HasIndex( x => x.UserName )
                .IsUnique( true );

            builder
                .Property( x => x.UserName )
                .IsRequired();

            builder
                .Property( x => x.PasswordHash )
                .IsRequired();
        }
    }
}
