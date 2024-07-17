using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetSpaBussinessObject;
using PetSpaService.AdminServiceService;
using PetSpaService.SpotService.SpotService;
using PRN211GroupProject.Utilities;
using System.Linq.Expressions;
using System.Security.Claims;

namespace PRN211GroupProject.Pages.Admin.SpotPage
{
	public class CreateModel : PageModel
	{
		private readonly ISpotService _spotService;
		public CreateModel(ISpotService spotService)
		{
			_spotService = spotService;
			Spot = new();
		}

		[BindProperty]
		public Spot Spot { get; set; }

		public IActionResult OnGet()
		{
			try
			{
				var roleClaim = User.FindFirst(ClaimTypes.Role);
				if (User.Identity == null || !User.Identity.IsAuthenticated || roleClaim == null || roleClaim.Value.ToString() != "Staff")
				{
					return Unauthorized();
				}
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
				var roleClaim = User.FindFirst(ClaimTypes.Role);
				if (User.Identity == null || !User.Identity.IsAuthenticated || roleClaim == null || roleClaim.Value.ToString() != "Staff")
				{
					return Unauthorized();
				}
				if (Spot == null)
				{
					return Page();
				}
				if (String.IsNullOrEmpty(Spot.Name))
				{
					return Page();
				}
				Spot.Name = FormatUtilities.TrimSpacesPreserveSingle(Spot.Name);
				_spotService.AddSpot(Spot);
				return RedirectToPage();
			}
			catch
			{
				return BadRequest();
			}
		}
	}
}
