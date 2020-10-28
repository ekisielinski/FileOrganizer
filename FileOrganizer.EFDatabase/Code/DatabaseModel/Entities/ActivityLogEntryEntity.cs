using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
namespace FileOrganizer.EFDatabase
{
    public sealed class ActivityLogEntryEntity
    {
        public int      Id           { get; set; }

        public string   Message      { get; set; } = string.Empty;
        public DateTime UtcTimestamp { get; set; }

        public int      IssuerId     { get; set; }

        //---

        public AppUserEntity? Issuer { get; set; }

        //====== public static methods

        public static void Configure( EntityTypeBuilder<ActivityLogEntryEntity> builder )
        {
            builder
                .Property( x => x.Message )
                .IsRequired()
                .IsUnicode()
                .HasMaxLength( 2000 );

            builder
                .Property( x => x.IssuerId )
                .IsRequired();
        }
    }
}
