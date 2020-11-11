using FileOrganizer.CommonUtils;
using FileOrganizer.Core.Helpers;
using FileOrganizer.Domain;
using FileOrganizer.Services.FileDatabase;
using MediatR;
using Microsoft.Extensions.FileProviders;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FileOrganizer.EFDatabase.Handlers
{
    public sealed class GetMetadataFromFileContentHandler : IRequestHandler<GetMetadataFromFileContentQuery, FileMetadataContainer>
    {
        readonly EFAppContext context;
        readonly IFileDatabaseReader fileReader;
        readonly IMetadataReader metadataReader;

        //====== ctors

        public GetMetadataFromFileContentHandler( EFAppContext context, IFileDatabaseReader fileReader, IMetadataReader metadataReader )
        {
            this.context        = Guard.NotNull( context,        nameof( context ) );
            this.fileReader     = Guard.NotNull( fileReader,     nameof( fileReader ) );
            this.metadataReader = Guard.NotNull( metadataReader, nameof( metadataReader ) );
        }

        //====== IRequestHandler

        public async Task<FileMetadataContainer> Handle( GetMetadataFromFileContentQuery request, CancellationToken cancellationToken )
        {
            FileEntity? entity = await context.Entities.Files.FindAsync( request.FileId.Value );

            if (entity is null) throw new Exception( "File not found: " + request.FileId ); // TODO: custom exception

            IFileInfo fileInfo = fileReader.SourceFilesReader.Get( new FileName( entity.SourceFileName ) );
            if (fileInfo.Exists == false) throw new Exception( "File not found: " + request.FileId ); // TODO: custom exception

            using var stream = fileInfo.CreateReadStream();
            FileMetadataContainer container = metadataReader.GetMetadata( stream );

            return container;
        }
    }
}
