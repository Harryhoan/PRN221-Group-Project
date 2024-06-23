using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetSpaBussinessObject;
using PetSpaService.AdminServiceService;
using PetSpaService.SpotService.SpotService;

namespace PRN211GroupProject.Pages.SpotPage
{
    public class CreateModel : PageModel
    {
		private readonly ISpotService _spotService;
		public CreateModel(ISpotService spotService)
		{
			_spotService = spotService;
		}

		[BindProperty]
		public Spot? Spot { get; set; }

		public IActionResult OnGet()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			return Page();
		}

		public IActionResult OnPost()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			if (Spot == null)
			{
				return BadRequest();
			}

			try
			{
				_spotService.AddSpot(Spot);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			return RedirectToPage("./Index");
		}
	}
}
