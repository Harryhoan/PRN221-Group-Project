using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetSpaBussinessObject;
using PetSpaService.AdminServiceService;
using PetSpaService.SpotService.SpotService;
using System.Linq.Expressions;
using System.Security.Claims;

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
		public Spot Spot { get; set; }

		public IActionResult OnGet()
		{
            var roleClaim = User.FindFirst(ClaimTypes.Role);
            if (User.Identity == null || !User.Identity.IsAuthenticated || roleClaim == null || roleClaim.Value.ToString() != "Staff")
            {
                return Unauthorized();
            }
			return Page();
        }

		public IActionResult OnPost()
		{
            if (!ModelState.IsValid || _spotService.GetSpotList() == null || Spot == null)
            {
                return Page();
            }
            _spotService.AddSpot(Spot);
            return RedirectToPage();
        }
	}
}
