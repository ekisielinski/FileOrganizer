using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FileOrganizer.EFDatabase
{
    public abstract class LinkEntity
    {
        public int      Id           { get; set; }
                        
        public string   Address      { get; set; } = string.Empty;
        public string?  Title        { get; set; }
        public string?  Comment      { get; set; }
        public DateTime UtcWhenAdded { get; set; }

        public int      IssuerId     { get; set; }

        //--- navigation

        // IssuerId
        public AppUserEntity? Issuer { get; set; }
    }



    public sealed class LinkEntityConfiguration : IEntityTypeConfiguration<LinkEntity>
    {
        public void Configure( EntityTypeBuilder<LinkEntity> builder )
        {
            builder
                .HasKey( x => x.Id );

            builder
                .Property( x => x.Address )
                .HasMaxLength( 2000 )
                .IsRequired();

            builder
                .Property( x => x.Title )
                .IsUnicode()
                .HasMaxLength( 500 )
                .IsRequired( false );

            builder
                .Property( x => x.Comment )
                .IsUnicode()
                .HasMaxLength( 2000 )
                .IsRequired( false );

            builder
                .Property( x => x.UtcWhenAdded )
                .IsRequired();

            builder
                .Property( x => x.IssuerId )
                .IsRequired();

            //---

            builder
                .HasOne<AppUserEntity>( link => link.Issuer! )
                .WithMany( appUser => appUser.FileLinks )
                .HasPrincipalKey( appUser => appUser.Id )
                .HasForeignKey( link => link.IssuerId )
                .OnDelete( DeleteBehavior.ClientCascade ) // todo: cascade dont work
                .IsRequired();
        }
    }
}
