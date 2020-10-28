using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileOrganizer.EFDatabase
{
    public class ImageEntity
    {
        public int  Id     { get; set; }
                    
        public int? Width  { get; set; }
        public int? Height { get; set; }

        public string? ThumbnailFileName { get; set; }

        public int  FileId { get; set; }
        
        //---

        public FileEntity? File { get; set; }


        public static void Configure( EntityTypeBuilder<ImageEntity> builder )
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
                .IsRequired( false );
        }
    }
}
