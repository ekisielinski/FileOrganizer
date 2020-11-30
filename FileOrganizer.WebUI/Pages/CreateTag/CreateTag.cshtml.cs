using FileOrganizer.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace FileOrganizer.WebUI.Pages.CreateTag
{
    public class CreateTagModel : PageModel
    {
        [BindProperty] public CreateTagForm Form { get; init; } = new CreateTagForm();

        //====== actions

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync( [FromServices] ISender sender )
        {
            if (ModelState.IsValid == false) return Page();

            try
            {
                await sender.Send( new CreateTagCommand(
                    new( Form.Name! ),
                    new( Form.DisplayName ?? string.Empty ),
                    new( Form.Description ?? string.Empty ) ) );
            }
            catch (Exception ex)
            {
                ModelState.AddModelError( "", "Unknown error: " + ex.InnerException?.Message );
                return Page();
            }

            return RedirectToPage( "/Tags" );
        }
    }
}
