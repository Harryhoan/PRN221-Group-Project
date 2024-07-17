using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetSpaBussinessObject;
using PetSpaService.SpotService.SpotService;

namespace PRN211GroupProject.Pages.Admin.SpotPage
{
	public class DeleteModel : PageModel
	{
		private readonly ISpotService _spotService;
		public DeleteModel(ISpotService spotService)
		{
			_spotService = spotService;
		}

		[BindProperty]
		public Spot? Spot { get; set; }

		public IActionResult OnGet(int spotId)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest();
				}

				if (_spotService.GetSpotList() == null || _spotService.GetSpotList().Count == 0)
				{
					return BadRequest();
				}

				var existingSpot = _spotService.GetSpot(spotId);
				if (existingSpot == null)
				{
					return NotFound();
				}
				this.Spot = existingSpot;

				return Page();
			}
			catch
			{
				return BadRequest();
			}
		}

		public IActionResult OnPost()
		{
			try
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
					_spotService.DeleteSpot(Spot.Id);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}

				return RedirectToPage("./Index");
			}
			catch
			{
				return BadRequest();
			}
		}
	}
}
