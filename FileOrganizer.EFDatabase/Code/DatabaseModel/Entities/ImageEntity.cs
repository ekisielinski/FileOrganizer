using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileOrganizer.EFDatabase
{
    // todo: owned entity?
    public class ImageEntity
    {
        public int     Id                { get; set; }
                       
        public int?    Width             { get; set; }
        public int?    Height            { get; set; }
        public string? ThumbnailFileName { get; set; }

        public int     FileId            { get; set; }
        
        //--- navigation

        // FileId
        public FileEntity? File { get; set; }
    }



    public sealed class ImageEntityConfiguration : IEntityTypeConfiguration<ImageEntity>
    { 
        public void Configure( EntityTypeBuilder<ImageEntity> builder )
        {
            builder
                .HasKey( x => x.Id );

            builder
                .Property( x => x.Width )
                .IsRequired( false );

            builder
                .Property( x => x.Height )
                .IsRequired( false );

            builder
                .Property( x => x.ThumbnailFileName )
                .HasMaxLength( 100 )
                .IsRequired( false );

            builder
                .Property( x => x.FileId )
                .IsRequired();

            //---

            builder
                .HasOne<FileEntity>( image => image.File! )
                .WithOne( file => file.Image! )
                .HasPrincipalKey<FileEntity>( file => file.Id )
                .HasForeignKey<ImageEntity>( image => image.FileId );
        }
    }
}
