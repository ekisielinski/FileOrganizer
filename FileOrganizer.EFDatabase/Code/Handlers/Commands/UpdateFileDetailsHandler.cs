using FileOrganizer.CommonUtils;
using FileOrganizer.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FileOrganizer.EFDatabase.Handlers
{
    public sealed class UpdateFileDetailsHandler : IRequestHandler<UpdateFileDetailsCommand>
    {
        readonly EFAppContext context;
        readonly IActivityLogger logger;

        //====== ctors

        public UpdateFileDetailsHandler( EFAppContext context, IActivityLogger logger )
        {
            this.context = Guard.NotNull( context, nameof( context ) );
            this.logger  = Guard.NotNull( logger,  nameof( logger  ) );
        }

        //====== IRequestHandler

        public async Task<Unit> Handle( UpdateFileDetailsCommand request, CancellationToken cancellationToken )
        {
            if (request.CanSkipExecution) return Unit.Value;

            var entity = context.Entities.Files.Find( request.FileId.Value );
            if (entity is null) throw new Exception( "File not found." );

            var messages = new List<string>();

            if (request.FileTitle != null)
            {
                entity.Title = request.FileTitle.Value;
                messages.Add( $"Description updated for file #{request.FileId}. New value: {request.Description}" );
            }

            if (request.Description != null)
            {
                entity.Description = request.Description.Value;
                messages.Add( $"Title updated for file #{request.FileId}. New value: {request.FileTitle.Value}" );
            }
            if (request.PrimaryDateTime != null)
            {
                entity.PrimaryDateTime = request.PrimaryDateTime.ToSpecialString();
                messages.Add( $"Primary date time updated for file #{request.FileId}. New value: {request.PrimaryDateTime}" );
            }

            await context.Entities.SaveChangesAsync();

            logger.Add( string.Join( "\r\n", messages ) );

            return Unit.Value;
        }
    }
}
