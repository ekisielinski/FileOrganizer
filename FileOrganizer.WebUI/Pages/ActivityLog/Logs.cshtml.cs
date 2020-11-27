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

        public async Task OnGetAsync( [FromServices] ISender sender )
        {
            GetActivityLogEntriesQuery query = new();

            Logs = await sender.Send( query );
        }
    }
}
