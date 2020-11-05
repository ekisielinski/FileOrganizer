using Microsoft.EntityFrameworkCore;
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

        //--- navigation

        public ICollection<ActivityLogEntity>? ActivityLog { get; set; }
        public ICollection<UserRolesEntity>?   UserRoles   { get; set; }
        public ICollection<UploadEntity>?      Uploads     { get; set; }
        public ICollection<FileEntity>?        Files       { get; set; }
        public ICollection<LinkEntity>?        FileLinks   { get; set; }
    }



    public sealed class AppUserEntityConfiguration : IEntityTypeConfiguration<AppUserEntity>
    {
        public void Configure( EntityTypeBuilder<AppUserEntity> builder )
        {
            builder
                .HasKey( x => x.Id );

            builder
                .HasIndex( x => x.UserName )
                .IsUnique( true );

            builder
                .Property( x => x.UserName )
                .IsRequired();

            builder
                .Property( x => x.DisplayName )
                .HasMaxLength( 80 )
                .IsUnicode()
                .IsRequired( false );

            builder
                .Property( x => x.EmailAddress )
                .HasMaxLength( 320 )
                .IsRequired( false );

            builder
                .Property( x => x.PasswordHash )
                .HasMaxLength( 100 )
                .IsRequired();

            builder
                .Property( x => x.UtcWhenCreated )
                .IsRequired();
        }
    }
}
