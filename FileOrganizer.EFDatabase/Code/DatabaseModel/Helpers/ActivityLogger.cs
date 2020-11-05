using FileOrganizer.CommonUtils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace FileOrganizer.EFDatabase
{
    // TODO: move to MediatR pipeline, should be in same transaction/commit?
    public sealed class ActivityLogger : IActivityLogger
    {
        readonly EFAppContext context;

        //====== ctors

        public ActivityLogger( EFAppContext context )
        {
            this.context = Guard.NotNull( context, nameof( context ) );
        }

        //====== IActivityLogger

        public void Add( string message )
        {
            if (string.IsNullOrEmpty( message )) throw new ArgumentException( "Message cannot be null or empty.", nameof( message ) );

            var entity = new ActivityLogEntity
            {
                Message      = message,
                UtcTimestamp = context.UtcNow.Value,
            };

            context.Entities
                .AppUsers
                .Include( x => x.ActivityLog )
                .First( x => x.UserName == context.Requestor.UserName.Value )
                .ActivityLog.Add( entity );
                         
            context.Entities.SaveChanges();
        }
    }
}
