using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetSpaBussinessObject;
using PetSpaDaos;
using PetSpaService.AdminServiceService;
using PRN211GroupProject.Utilities;

namespace PRN211GroupProject.Pages.ServicePage
{
    public class CreateModel : PageModel
    {
        private readonly IServiceService _serviceService;

        public CreateModel(IServiceService serviceService)
        {
            _serviceService = serviceService;
            Service = new();
        }

        public IActionResult OnGet()
        {
            try
            {
                return Page();
            }
            catch
            {
                return BadRequest();
            }
        }

        [BindProperty]
        public Service Service { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var roleClaim = User.FindFirst(ClaimTypes.Role);
                if (User.Identity == null || !User.Identity.IsAuthenticated || roleClaim == null || roleClaim.Value.ToString() != "Admin")
                {
                    return Unauthorized();
                }
                if (_serviceService == null)
                {
                    return BadRequest();
                }
                if (Service == null || Service.Id <= 0)
                {
                    return Page();
                }

                if (String.IsNullOrEmpty(Service.Name))
                {
                    return BadRequest();
                }
				if (String.IsNullOrEmpty(Service.Description))
				{
					return BadRequest();
				}
				if (Service.Price <= 0)
                {
                    return BadRequest();
                }
                if (Service.Duration <= 1)
                {
                    return BadRequest();
                }
                Service.Name = FormatUtilities.TrimSpacesPreserveSingle(Service.Name);
				Service.Description = FormatUtilities.TrimSpacesPreserveSingle(Service.Description);
				_serviceService.AddService(Service);

                return RedirectToPage();
            }
            catch
            {
                return BadRequest();
            }
            
        }
    }
}
