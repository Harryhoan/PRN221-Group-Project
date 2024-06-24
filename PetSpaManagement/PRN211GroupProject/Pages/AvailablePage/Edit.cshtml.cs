using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetSpaBussinessObject;
using PetSpaService.AdminServiceService;
using PetSpaService.AvailableService;
using PetSpaService.SpotService;
using PetSpaService.SpotService.SpotService;

namespace PRN211GroupProject.Pages.AvailablePage
{
	public class EditModel : PageModel
	{
		private readonly IAvailableService _availableService;
		private readonly IServiceService _serviceService;
		private readonly ISpotService _spotService;

		public EditModel(IAvailableService availableService, IServiceService serviceService, ISpotService spotService)
		{
			_availableService = availableService;
			_serviceService = serviceService;
			_spotService = spotService;
		}

		[BindProperty]
		public Available? Available { get; set; }

		public IList<Service>? Services { get; set; }
		public IList<Spot>? Spots { get; set; }
		public IActionResult OnGet(int availableId)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest();
				}

				if (_availableService.GetAvailableList() == null || _availableService.GetAvailableList().Count == 0)
				{
					return NotFound();
				}

				Services = _serviceService.GetServiceList();
				if (Services == null || !(Services.Count > 0))
				{
					return NotFound();
				}

				var existingAvailable = _availableService.GetAvailable(availableId);
				if (existingAvailable == null)
				{
					return NotFound();
				}
				this.Available = existingAvailable;

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
					_availableService.UpdateAvailable(Available);
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
