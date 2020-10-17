using System.Collections.Generic;
using System.Threading.Tasks;
using FileOrganizer.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileOrganizer.WebUI.Pages
{
    public class LogsModel : PageModel
    {
        public IReadOnlyList<ActivityLogEntry> Logs { get; private set; } = new List<ActivityLogEntry>();

        //====== actions

        public async Task OnGet( [FromServices] IMediator mediator )
        {
            var query = new GetActivityLogEntriesQuery( null );

            Logs = await mediator.Send( query );
        }
    }
}
