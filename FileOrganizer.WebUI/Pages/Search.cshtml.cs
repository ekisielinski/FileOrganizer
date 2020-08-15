using System.Collections.Generic;
using FileOrganizer.Core;
using FileOrganizer.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileOrganizer.WebUI.Pages
{
    public class SearchModel : PageModel
    {
        public IReadOnlyList<FileDetails> Files { get; set; } = new List<FileDetails>();

        //====== actions

        public void OnGet( [FromServices] IFileSearcher searcher )
        {
            Files = searcher.GetAll();
        }
    }
}
