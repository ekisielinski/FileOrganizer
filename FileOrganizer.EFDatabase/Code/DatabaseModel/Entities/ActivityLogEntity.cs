using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FileOrganizer.EFDatabase
{
    public sealed class ActivityLogEntity
    {
        public int      Id           { get; set; }

        public string   Message      { get; set; } = string.Empty;
        public DateTime UtcTimestamp { get; set; }

        public int      IssuerId     { get; set; }

        //--- navigation

        // IssuerId
        public AppUserEntity? Issuer { get; set; }
    }



    public sealed class ActivityLogEntityConfiguration : IEntityTypeConfiguration<ActivityLogEntity>
    {
        public void Configure( EntityTypeBuilder<ActivityLogEntity> builder )
        {
            builder
                .HasKey( x => x.Id );

            builder
                .Property( x => x.Message )
                .IsUnicode()
                .HasMaxLength( 2000 )
                .IsRequired();

            builder
                .Property( x => x.UtcTimestamp )
                .IsRequired();

            builder
                .Property( x => x.IssuerId )
                .IsRequired();

            //---

            builder
                .HasOne<AppUserEntity>( activityLog => activityLog.Issuer! )
                .WithMany( x => x.ActivityLog )
                .HasPrincipalKey( appUser => appUser.Id )
                .HasForeignKey( activityLog => activityLog.IssuerId )
                .IsRequired();
        }
    }
}
