using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FileOrganizer.EFDatabase
{
    public sealed class TagEntity
    {
        public string  Name        { get; set; } = string.Empty;
        public string? DisplayName { get; set; }
        public string? Description { get; set; }

        public DateTime UtcWhenAdded { get; set; }

        public int CreatedById { get; set; }

        //--- navigation

        // IssuerId
        public AppUserEntity? CreatedBy { get; set; }
    }



    public sealed class TagEntityConfiguration : IEntityTypeConfiguration<TagEntity>
    {
        public void Configure( EntityTypeBuilder<TagEntity> builder )
        {
            builder
                .HasKey( x => x.Name );

            builder
                .Property( x => x.DisplayName )
                .IsUnicode()
                .HasMaxLength( 80 )
                .IsRequired( false );

            builder
                .Property( x => x.Description )
                .IsUnicode()
                .HasMaxLength( 1000 )
                .IsRequired( false );

            builder
                .Property( x => x.UtcWhenAdded )
                .IsRequired();

            builder
                .Property( x => x.CreatedById )
                .IsRequired();

            //---

            builder
                .HasOne<AppUserEntity>( tag => tag.CreatedBy! )
                .WithMany( appUser => appUser.Tags )
                .HasPrincipalKey( appUser => appUser.Id )
                .HasForeignKey( tag => tag.CreatedById )
                //.OnDelete( DeleteBehavior.ClientCascade ) // todo: cascade dont work
                .IsRequired();
        }
    }
}
