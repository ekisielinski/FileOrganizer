using FileOrganizer.CommonUtils;
using FileOrganizer.Core;
using FileOrganizer.WebUI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FileOrganizer.WebUI.Services
{
    public sealed class StaticFilesLinkGenerator : IStaticFilesLinkGenerator
    {
        readonly LinkGenerator linkGenerator;
        readonly IHttpContextAccessor accessor;

        //====== ctors

        public StaticFilesLinkGenerator( LinkGenerator linkGenerator, IHttpContextAccessor accessor )
        {
            this.linkGenerator = Guard.NotNull( linkGenerator, nameof( linkGenerator ) );
            this.accessor = Guard.NotNull( accessor, nameof( accessor ) );
        }

        //====== IStaticFilesLinkGenerator

        public string GetSourceFilePath( FileName fileName )
            => GetPath( fileName, nameof( StaticFilesController.File ) );

        public string GetThumbnailPath( FileName fileName )
            => GetPath( fileName, nameof( StaticFilesController.Thumb ) );

        //====== private methods

        private string GetPath( FileName fileName, string actionName )
        {
            Guard.NotNull( fileName, nameof( fileName ) );
            Guard.NotNull( actionName, nameof( actionName ) );
         
            return linkGenerator.GetPathByAction(
                httpContext: accessor.HttpContext,
                action: actionName,
                controller: "StaticFiles", // TODO: controller name utils...
                values: new { fileName = fileName.Value }
                );
        }
    }
}
