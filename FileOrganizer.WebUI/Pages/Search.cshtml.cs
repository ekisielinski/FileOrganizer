using System.Collections.Generic;
using System.Linq;
using FileOrganizer.Core;
using FileOrganizer.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FileOrganizer.WebUI.Pages
{
    public class SearchModel : PageModel
    {
        [BindProperty( SupportsGet = true )]
        public int PageSize { get; set; } = 25;

        [BindProperty( SupportsGet = true )]
        public int PageIndex { get; set; } = 0;

        public IReadOnlyList<FileDetails> Files { get; set; } = new List<FileDetails>();

        public int FileCount { get; set; }
        public int PageCount { get; set; }

        public List<SelectListItem> PageSizeSelectList = new List<SelectListItem>
        {
            new SelectListItem { Value = "10",  Text = "10" },
            new SelectListItem { Value = "25",  Text = "25" },
            new SelectListItem { Value = "50",  Text = "50" },
            new SelectListItem { Value = "100", Text = "100" }
        };

        //====== actions

        public void OnGet( [FromServices] IFileSearcher searcher )
        {
            FixPageSize();

            FileCount = searcher.CountFiles();

            PageCount = FileCount / PageSize;
            if (FileCount % PageSize > 0) PageCount++;

            // TODO: min max validation...
            var paging = new PagingParameters( PageSize, PageIndex );

            Files = searcher.GetFiles( paging );
        }

        private void FixPageSize()
        {
            int[] validValues = PageSizeSelectList.Select( x => int.Parse( x.Value ) ).ToArray();

            if (validValues.Contains( PageSize )) return;

            PageSize = 25;
        }
    }
}
