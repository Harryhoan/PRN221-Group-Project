using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using PetSpaBussinessObject;
using PetSpaService.SpotService;
using PetSpaService.AvailableService;
using PetSpaService.AdminServiceService;
using PetSpaService.SpotService.SpotService;
using System.Security.Claims;
namespace PRN211GroupProject.Pages.AvailablePage
{
    public class CreateModel : PageModel
	{
		private readonly IAvailableService _availableService;
		private readonly IServiceService _serviceService;
		private readonly ISpotService _spotService;
		public CreateModel(IAvailableService availableService, IServiceService serviceService, ISpotService spotService)
		{
			_availableService = availableService;
			_serviceService = serviceService;
			_spotService = spotService;
		}

		[BindProperty]
		public Available? Available { get; set; }
		public IList<Service>? Services { get; set; }
		public IList<Spot>? Spots { get; set; }
		public IActionResult OnGet()
		{
            try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest();
				}

				Services = _serviceService.GetServiceList();
				if (Services == null || !(Services.Count > 0))
				{
					return NotFound();
				}

				Spots = _spotService.GetSpotList();
				if (Services == null || !(Services.Count > 0))
				{
					return NotFound();
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
				if (!ModelState.IsValid)
				{
					return Page();
				}

				if (Available == null)
				{
					return BadRequest();
				}

				try
				{
					_availableService.AddAvailable(Available);
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
