using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FileOrganizer.EFDatabase
{

    public sealed class FileEntity
    {
        public int           Id                { get; set; }

        public string        MimeType          { get; set; }
        public string?       FileName          { get; set; }
        public DateTime      UtcWhenAdded      { get; set; }
        public string        SourceFileName    { get; set; }
        public long          SourceFileSize    { get; set; }
        public string?       Description       { get; set; }
        public string?       Title             { get; set; }
        public string        Hash              { get; set; }
        public string?       PrimaryDateTime   { get; set; }

        public int           UploadId          { get; set; }
        public UploadEntity  Upload            { get; set; }

        public ImageEntity   Image             { get; set; }

        public int           UploaderId        { get; set; }
        public AppUserEntity Uploader          { get; set; }
        
        public static void Configure( EntityTypeBuilder<FileEntity> builder )
        {
            builder
                .Property( x => x.UploadId )
                .IsRequired();

            builder
                .Property( x => x.SourceFileName )
                .IsRequired();

            builder
                .Property( x => x.Hash )
                .IsRequired();

            builder
                .HasOne( x => x.Image )
                .WithOne( x => x.File );

            builder
                .HasOne( src => src.Uploader )
                .WithMany( dst => dst.Files )
                .HasForeignKey( file => file.UploaderId )
                .OnDelete( DeleteBehavior.Restrict ) // todo: read about it
                .IsRequired();
        }
    }
}
