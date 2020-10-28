using FileOrganizer.CommonUtils;
using FileOrganizer.Core;
using FileOrganizer.Core.Helpers;
using FileOrganizer.Domain;

namespace FileOrganizer.EFDatabase
{
    public sealed class EFAppContext
    {
        readonly ITimestampGenerator timestamp;

        //====== ctors

        public EFAppContext( FileOrganizerEntities entities, IRequestorAccessor requestor, ITimestampGenerator timestamp )
        {
            Entities  = Guard.NotNull( entities,  nameof( entities  ) );
            Requestor = Guard.NotNull( requestor, nameof( requestor ) );

            this.timestamp = Guard.NotNull( timestamp, nameof( timestamp ) );
        }

        //====== public properties

        public FileOrganizerEntities Entities { get; }

        public IRequestorAccessor Requestor { get; }

        public UtcTimestamp UtcNow => timestamp.UtcNow;
    }
}
