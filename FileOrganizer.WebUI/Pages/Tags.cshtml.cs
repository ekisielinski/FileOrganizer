using System.Collections.Generic;
using System.Threading.Tasks;
using FileOrganizer.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileOrganizer.WebUI.Pages
{
    public class TagsModel : PageModel
    {
        public IReadOnlyList<Tag> Tags = new List<Tag>();

        public async Task OnGet( [FromServices] IMediator mediator )
        {
            Tags = await mediator.Send( new GetTagsQuery() );
        }
    }
}
