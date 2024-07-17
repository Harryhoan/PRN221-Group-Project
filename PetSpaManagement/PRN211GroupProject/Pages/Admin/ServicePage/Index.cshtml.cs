using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetSpaBussinessObject;
using PetSpaDaos;
using PetSpaRepo.AdminServiceRepo;
using PetSpaService.AdminServiceService;
using PetSpaService.ServicesService;
using PRN211GroupProject.Utilities;

namespace PRN211GroupProject.Pages.ServicePage
{
    public class IndexModel : PageModel
    {
        private readonly IServiceService _service;

        public IndexModel(IServiceService service)
        {
            _service = service;
			Service = new List<Service>();
			NewService = new();
        }

        public IList<Service> Service { get; set; }
        [BindProperty]
        public Service NewService { get; set; }
        public async Task OnGetAsync()
        {
            if (_service.GetServiceList() != null)
            {
                Service = _service.GetServiceList();
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
			try
			{
				var roleClaim = User.FindFirst(ClaimTypes.Role);
				if (User.Identity == null || !User.Identity.IsAuthenticated || roleClaim == null || roleClaim.Value.ToString() != "Admin")
				{
					return Unauthorized();
				}
				if (NewService != null && NewService.Id == default && !String.IsNullOrEmpty(NewService.Description) && !String.IsNullOrEmpty(NewService.Description) && NewService.Price > 0 && NewService.Duration >= 1)
				{
					NewService.Description = FormatUtilities.TrimSpacesPreserveSingle(NewService.Description);
					NewService.Name = FormatUtilities.TrimSpacesPreserveSingle(NewService.Name);
					_service.AddService(NewService);
					return RedirectToPage("/Admin/ServicePage/Index");
				}
				return Page();
			}
			catch
			{
				return BadRequest();
			}
		}
    }
}
