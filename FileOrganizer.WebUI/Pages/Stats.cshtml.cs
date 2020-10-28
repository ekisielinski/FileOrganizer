using System.Threading.Tasks;
using FileOrganizer.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileOrganizer.WebUI.Pages
{
    public class StatsModel : PageModel
    {
        public Statistics Stats { get; private set; } = Statistics.Empty;

        public async Task OnGetAsync( [FromServices] IMediator mediator )
        {
            Stats = await mediator.Send( new GetStatisticsQuery() );
        }
    }
}
