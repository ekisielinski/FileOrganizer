using Microsoft.EntityFrameworkCore;
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
        
        //--- navigation

        // UploaderId
        public AppUserEntity? Uploader { get; set; }

        public ICollection<FileEntity>? Files { get; set; }
        
        //public IReadOnlyList<string> RejectedDuplicates { get; set; }
    }



    public sealed class UploadEntityConfiguration : IEntityTypeConfiguration<UploadEntity>
    {
        public void Configure( EntityTypeBuilder<UploadEntity> builder )
        {
            builder
                .HasKey( x => x.Id );

            builder
                .Property( x => x.Description )
                .IsUnicode()
                .HasMaxLength( 5000 )
                .IsRequired( false );

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
                .Property( x => x.UploaderId )
                .IsRequired();

            //---

            builder
                .HasOne<AppUserEntity>( upload => upload.Uploader! )
                .WithMany( appUser => appUser.Uploads )
                .HasPrincipalKey( appUser => appUser.Id )
                .HasForeignKey( upload => upload.UploaderId )
                .IsRequired();
        }
    }
}
