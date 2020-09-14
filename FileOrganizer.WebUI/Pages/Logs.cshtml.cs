using System.Collections.Generic;
using FileOrganizer.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileOrganizer.WebUI.Pages
{
    public class LogsModel : PageModel
    {
        public IReadOnlyList<ActivityLogEntry> Logs { get; private set; }

        //====== actions

        public void OnGet( [FromServices] IActivityLogReader reader )
        {
            Logs = reader.GetAll();
        }
    }
}
