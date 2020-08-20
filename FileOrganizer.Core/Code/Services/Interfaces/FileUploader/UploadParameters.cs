using FileOrganizer.CommonUtils;
using System.Collections.Generic;

namespace FileOrganizer.Core.Services
{
    public sealed class UploadParameters
    {
        public UploadParameters( IEnumerable<SourceFile> sourceFiles, UploadDescription description )
        {
            SourceFiles = ArgUtils.ToRoList( sourceFiles, nameof( sourceFiles ) );
            Description = Guard.NotNull( description, nameof( description ) );
        }

        //====== public properties

        public IReadOnlyList<SourceFile> SourceFiles { get; }

        public UploadDescription Description { get; }
    }
}
