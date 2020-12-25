using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileOrganizer.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FileOrganizer.WebUI.Pages
{
    public class LogsModel : PageModel
    {
        public IReadOnlyList<ActivityLogEntry> Logs      { get; private set; } = new List<ActivityLogEntry>();
        public SelectList                      UsersList { get; private set; } = new SelectList( Enumerable.Empty<object>() );

        public string? ReloadUriBase { get; private set; }

        //====== actions

        public async Task OnGetAsync( [FromQuery] string? userNameFilter, [FromServices] ISender sender )
        {
            IReadOnlyList<AppUser> appUsers = await sender.Send( new GetAppUsersQuery() );

            AppUser? currentAppUser = null;

            if (userNameFilter is not null)
            {
                currentAppUser = appUsers.FirstOrDefault( x => x.Name.Value == userNameFilter );

                if (currentAppUser is null) throw new( "User not found." ); // TODO: custom ex
            }

            UsersList = new SelectList( appUsers, nameof( AppUser.Name ), nameof( AppUser.Name ), currentAppUser?.Name?.Value );

            GetActivityLogEntriesQuery query = new()
            {
                UserNameFilter = userNameFilter is null ? null : new UserName( userNameFilter )
            };

            Logs = await sender.Send( query );

            ReloadUriBase = Url.PageLink( "/Logs" ) + "?" + nameof( userNameFilter ) + "=";
        }
    }
}
