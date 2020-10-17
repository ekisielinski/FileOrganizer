using FileOrganizer.Domain;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileOrganizer.Core.FakeDatabase
{
    public sealed class UpdateFileDetailsHandler : IRequestHandler<UpdateFileDetailsCommand>
    {
        readonly FakeDatabaseSingleton database;
        readonly IActivityLogger logger;

        //====== ctors

        public UpdateFileDetailsHandler( FakeDatabaseSingleton database, IActivityLogger logger )
        {
            this.database = database;
            this.logger = logger;
        }

        public Task<Unit> Handle( UpdateFileDetailsCommand request, CancellationToken cancellationToken )
        {
            if (request.CanSkipExecution) return Unit.Task;

            if (request.FileTitle != null) UpdateTitle( request.FileId, request.FileTitle );
            if (request.Description != null) UpdateDescription( request.FileId, request.Description );
            if (request.PrimaryDateTime != null) UpdatePrimaryDateTime( request.FileId, request.PrimaryDateTime );

            return Unit.Task;
        }

        public void UpdateDescription( FileId fileId, FileDescription description )
        {
            database.Files.FirstOrDefault( x => x.Id == fileId.Value ).Description = description;

            logger.Add( $"Description updated for file #{fileId}. New value: {description}" );
        }

        public void UpdateTitle( FileId fileId, FileTitle title )
        {
            database.Files.FirstOrDefault( x => x.Id == fileId.Value ).Title = title;

            logger.Add( $"Title updated for file #{fileId}. New value: {title}" );
        }

        public void UpdatePrimaryDateTime( FileId fileId, PartialDateTime primaryDateTime )
        {
            database.Files.FirstOrDefault( x => x.Id == fileId.Value ).PrimaryDateTime = primaryDateTime;

            logger.Add( $"Primary date time updated for file #{fileId}. New value: {primaryDateTime}" );
        }
    }
}
