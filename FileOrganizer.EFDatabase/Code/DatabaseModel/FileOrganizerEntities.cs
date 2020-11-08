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

        public DbSet<AppUserEntity>      AppUsers    { get; set; }
        public DbSet<UserRolesEntity>    UserRoles   { get; set; }
        public DbSet<ActivityLogEntity>  ActivityLog { get; set; }
        public DbSet<FileEntity>         Files       { get; set; }
        public DbSet<FileLinkEntity>     FileLinks   { get; set; }
        public DbSet<ImageEntity>        ImageInfos  { get; set; }
        public DbSet<UploadEntity>       Uploads     { get; set; }
        public DbSet<TagEntity>          Tags        { get; set; }

        //====== protected virtual methods

        protected override void OnModelCreating( ModelBuilder mb )
        {
            mb.ApplyConfigurationsFromAssembly( typeof( FileOrganizerEntities ).Assembly );
        }
    }
}
