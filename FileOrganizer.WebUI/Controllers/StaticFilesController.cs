using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileOrganizer.WebUI.Controllers
{
    [Route( "static/{action}/{id}" )]
    [Authorize]
    public class StaticFilesController : Controller
    {
        public string File( int id )
        {
            return "file id: " + id;
        }

        public string Thumb( int id )
        {
            return "thumb id: " + id;
        }
    }
}
