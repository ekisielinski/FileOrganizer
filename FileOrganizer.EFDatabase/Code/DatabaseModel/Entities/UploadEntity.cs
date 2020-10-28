using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace FileOrganizer.EFDatabase
{
    public sealed class UploadEntity
    {
        public int           Id           { get; set; }
                            
        public string?       Description  { get; set; }
        public DateTime      UtcWhenAdded { get; set; }
        public int           FileCount    { get; set; } // todo: read about computed columns
        public long          TotalSize    { get; set; } // todo: read about computed columns

        public int           UploaderId   { get; set; }
        public AppUserEntity Uploader     { get; set; }

        public List<FileEntity> Files { get; set; }

        //public IReadOnlyList<string> RejectedDuplicates { get; set; }

        public static void Configure( EntityTypeBuilder<UploadEntity> builder )
        {
            builder
                .HasKey( x => x.Id );

            builder
                .Property( x => x.Description )
                .IsRequired( false )
                .IsUnicode()
                .HasMaxLength( 5000 );

            builder
                .Property( x => x.UtcWhenAdded )
                .IsRequired();

            builder
                .Property( x => x.FileCount )
                .IsRequired();

            builder
                .Property( x => x.TotalSize )
                .IsRequired();

            builder
                .HasOne( x => x.Uploader )
                .WithMany( x => x.Uploads )
                .IsRequired();
        }
    }
}
