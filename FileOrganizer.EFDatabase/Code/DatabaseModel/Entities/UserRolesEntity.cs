using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileOrganizer.EFDatabase
{
    public sealed class UserRolesEntity
    {
        public int    Id           { get; set; }

        public string RoleName     { get; set; } = string.Empty; // e.g. "admin", "moderator"
        public int    AssignedToId { get; set; }

        //--- navigation

        // AssignedToId
        public AppUserEntity? AssignedTo { get; set; }
    }



    public sealed class UserRolesEntityConfiguration : IEntityTypeConfiguration<UserRolesEntity>
    {
        public void Configure( EntityTypeBuilder<UserRolesEntity> builder )
        {
            builder
                .HasKey( x => x.Id );

            builder
                .Property( x => x.RoleName )
                .HasMaxLength( 200 )
                .IsRequired();

            builder
                .Property( x => x.AssignedToId )
                .IsRequired();

            //---

            builder
                .HasOne<AppUserEntity>( userRoles => userRoles.AssignedTo! )
                .WithMany( appUser => appUser.UserRoles )
                .HasPrincipalKey( appUser => appUser.Id )
                .HasForeignKey( userRoles => userRoles.AssignedToId )
                .IsRequired();
        }
    }
}
