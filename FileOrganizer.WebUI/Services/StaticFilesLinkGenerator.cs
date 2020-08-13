using FileOrganizer.CommonUtils;
using FileOrganizer.Core;
using FileOrganizer.Services.FileDatabase;
using FileOrganizer.WebUI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;

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

        public string GetDatabaseFilePath( FileName fileName, FileDatabaseFolder folder )
        {
            Guard.NotNull( fileName, nameof( fileName ) );

            string actionName = folder switch
            {
                FileDatabaseFolder.Files  => nameof( StaticFilesController.File ),
                FileDatabaseFolder.Thumbs => nameof( StaticFilesController.Thumb ),

                _ => throw new ArgumentException( "Invalid enum value.", nameof( folder ) )
            };

            return linkGenerator.GetPathByAction(
                httpContext: accessor.HttpContext,
                action:      actionName,
                controller:  "StaticFiles", // TODO: controller name utils...
                values:      new { fileName = fileName.Value }
                );
        }
    }
}
