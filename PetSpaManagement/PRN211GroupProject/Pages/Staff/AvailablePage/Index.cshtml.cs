using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetSpaBussinessObject;
using PetSpaService.AdminServiceService;
using PetSpaService.AvailableService;
using System.Security.Claims;

namespace PRN211GroupProject.Pages.AvailablePage
{
	public class IndexModel : PageModel
	{
		private readonly IAvailableService _availableService;

		public IndexModel(IAvailableService availableService)
		{
			_availableService = availableService;
		}
		public IList<Available>? Availables { get; set; } = default;

		public IActionResult OnGet()
		{
            try
			{
				if (_availableService == null)
				{
					return BadRequest();
				}
				Availables = _availableService.GetAvailableList();
				return Page();
			}
			catch
			{
				return BadRequest();
			}
		}
	}
}
