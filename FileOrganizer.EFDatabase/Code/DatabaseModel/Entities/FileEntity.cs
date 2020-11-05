using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace FileOrganizer.EFDatabase
{
    public sealed class FileEntity
    {
        public int      Id              { get; set; }

        public string   MimeType        { get; set; }
        public string?  FileName        { get; set; } // orginal file name
        public string   SourceFileName  { get; set; }
        public long     SourceFileSize  { get; set; }
        public string?  Description     { get; set; }
        public string?  Title           { get; set; }
        public string   Hash            { get; set; }
        public string?  PrimaryDateTime { get; set; }
        public DateTime UtcWhenAdded    { get; set; }

        public int      UploadId        { get; set; }
        public int      UploaderId      { get; set; }

        //--- navigation

        // UploadId
        public UploadEntity?  Upload    { get; set; }
        // UploaderId
        public AppUserEntity? Uploader  { get; set; }

        // none
        public ImageEntity? Image { get; set; }

        public ICollection<FileLinkEntity>? Links { get; set; }
    }



    public sealed class FileEntityConfiguration : IEntityTypeConfiguration<FileEntity>
    {
        public void Configure( EntityTypeBuilder<FileEntity> builder )
        {
            builder
                .HasKey( x => x.Id );

            builder
                .Property( x => x.MimeType )
                .HasMaxLength( 128 )
                .IsRequired();

            builder
                .Property( x => x.FileName )
                .HasMaxLength( 2000 )
                .IsRequired( false );

            builder
                .Property( x => x.SourceFileName )
                .HasMaxLength( 100 )
                .IsRequired();

            builder
                .Property( x => x.SourceFileSize )
                .IsRequired();

            builder
                .Property( x => x.Description )
                .HasMaxLength( 2000 )
                .IsRequired( false );

            builder
                .Property( x => x.Title )
                .HasMaxLength( 200 )
                .IsRequired( false );

            builder
                .Property( x => x.Hash ) // todo: unique?
                .IsRequired();


            builder
                .Property( x => x.PrimaryDateTime )
                .HasMaxLength( 30 )
                .IsRequired( false );

            builder
                .Property( x => x.UtcWhenAdded )
                .IsRequired();

            builder
                .Property( x => x.UploadId )
                .IsRequired();

            builder
                .Property( x => x.UploaderId )
                .IsRequired();

            //---

            builder
                .HasOne<UploadEntity>( file => file.Upload! )
                .WithMany( upload => upload.Files )
                .HasPrincipalKey( upload => upload.Id )
                .HasForeignKey( file => file.UploadId )
                .IsRequired();

            builder
                .HasOne<AppUserEntity>( file => file.Uploader! )
                .WithMany( appUser => appUser.Files )
                .HasPrincipalKey( appUser => appUser.Id )
                .HasForeignKey( file => file.UploaderId )
                .OnDelete( DeleteBehavior.ClientCascade ) // todo: cascade dont work
                .IsRequired();
        }
    }
}
