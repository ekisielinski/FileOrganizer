using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileOrganizer.Domain;
using FileOrganizer.WebUI.Pages.Shared.Components;
using MediatR;
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

        public FileSearchResult SearchResult { get; set; }

        public List<SelectListItem> PageSizeSelectList = new List<SelectListItem>
        {
            new() { Value = "10",  Text = "10"  },
            new() { Value = "25",  Text = "25"  },
            new() { Value = "50",  Text = "50"  },
            new() { Value = "100", Text = "100" }
        };

        public PagerModel PagerModel { get; set; } = new PagerModel();

        //====== actions

        public async Task OnGet( [FromServices] IMediator mediator )
        {
            FixPageSize();

            // TODO: min max validation...
            var cmd = new SearchForFilesQuery( new PagingParameters( PageSize, PageIndex ) );

            SearchResult = await mediator.Send( cmd );

            PagerModel = new PagerModel
            {
                PageCount   = SearchResult.PageCount,
                CurrentPage = PageIndex,
                UrlFactory  = (pindex) => $"?pageIndex={pindex}&pageSize={PageSize}"
            };
        }

        private void FixPageSize()
        {
            int[] validValues = PageSizeSelectList.Select( x => int.Parse( x.Value ) ).ToArray();

            if (validValues.Contains( PageSize )) return;

            PageSize = 25;
        }
    }
}
