using System.Collections.Generic;
using FileOrganizer.Core;
using FileOrganizer.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileOrganizer.WebUI.Pages
{
    public class SearchModel : PageModel
    {
        [BindProperty( SupportsGet = true )]
        public int PageSize { get; set; } = 25;

        [BindProperty( SupportsGet = true )]
        public int PageIndex { get; set; } = 0;

        public IReadOnlyList<FileDetails> Files { get; set; } = new List<FileDetails>();

        //====== actions

        public void OnGet( [FromServices] IFileSearcher searcher )
        {
            // TODO: min max validation...

            var paging = new PagingParameters( PageSize, PageIndex );

            Files = searcher.GetFiles( paging );
        }
    }
}
