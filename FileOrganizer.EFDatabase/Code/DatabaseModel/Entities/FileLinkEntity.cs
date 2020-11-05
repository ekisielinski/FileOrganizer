using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileOrganizer.EFDatabase
{
    public sealed class FileLinkEntity : LinkEntity
    {
        public int FileId { get; set; }

        //--- navigation

        // FileId
        public FileEntity? File { get; set; }
    }



    public sealed class FileLinkEntityConfiguration : IEntityTypeConfiguration<FileLinkEntity>
    {
        public void Configure( EntityTypeBuilder<FileLinkEntity> builder )
        {
            builder
                .Property( x => x.FileId )
                .IsRequired();

            //---

            builder
                .HasOne<FileEntity>( fileLink => fileLink.File! )
                .WithMany( file => file.Links )
                .HasPrincipalKey( file => file.Id )
                .HasForeignKey( fileLink => fileLink.FileId )
                .IsRequired();
        }
    }
}
