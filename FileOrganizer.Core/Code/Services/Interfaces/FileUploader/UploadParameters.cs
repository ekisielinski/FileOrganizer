using FileOrganizer.CommonUtils;
using System.Collections.Generic;
using System.Linq;

namespace FileOrganizer.Core.Services
{
    public sealed class UploadParameters
    {
        public UploadParameters( IEnumerable<SourceFile> sourceFiles, UploadDescription description )
        {
            Guard.NotNull( sourceFiles, nameof( sourceFiles ) );

            Description = Guard.NotNull( description, nameof( description ) );

            SourceFiles = sourceFiles.ToList();
        }

        //====== public properties

        public IReadOnlyList<SourceFile> SourceFiles { get; }

        public UploadDescription Description { get; }
    }
}
