using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileOrganizer.EFDatabase
{
    public sealed class UserRolesEntity
    {
        public int    Id           { get; set; }

        public string RoleName     { get; set; } = string.Empty; // e.g. "admin", "moderator"
        public int    AssignedToId { get; set; }

        //---

        public AppUserEntity? AssignedTo { get; set; }

        //====== public static methods

        public static void Configure( EntityTypeBuilder<UserRolesEntity> builder )
        {
            builder
                .Property( x => x.RoleName )
                .HasMaxLength( 200 )
                .IsRequired();

            builder
                .Property( x => x.AssignedToId )
                .IsRequired();
        }
    }
}
