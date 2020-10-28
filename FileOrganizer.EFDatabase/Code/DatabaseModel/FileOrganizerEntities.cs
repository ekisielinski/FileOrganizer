using Microsoft.EntityFrameworkCore;

namespace FileOrganizer.EFDatabase
{
    public sealed class FileOrganizerEntities : DbContext
    {
        public FileOrganizerEntities( DbContextOptions options ) : base( options )
        {
            // empty
        }

        //====== public proprties

        public DbSet<AppUserEntity>          AppUsers    { get; set; }
        public DbSet<UserRolesEntity>        UserRoles   { get; set; }
        public DbSet<ActivityLogEntryEntity> ActivityLog { get; set; }
        public DbSet<FileEntity>             Files       { get; set; }
        public DbSet<ImageEntity>            ImageInfos  { get; set; }
        public DbSet<UploadEntity>           Uploads     { get; set; }

        //====== protected virtual methods

        protected override void OnModelCreating( ModelBuilder mb )
        {
            ActivityLogEntryEntity.Configure( mb.Entity<ActivityLogEntryEntity>() );
            UserRolesEntity       .Configure( mb.Entity<UserRolesEntity>()        );
            AppUserEntity         .Configure( mb.Entity<AppUserEntity>()          );
            ImageEntity           .Configure( mb.Entity<ImageEntity>()            );
            FileEntity            .Configure( mb.Entity<FileEntity>()             );
            UploadEntity          .Configure( mb.Entity<UploadEntity>()           );

            base.OnModelCreating( mb );
        }
    }
}
