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
using PetSpaService.AdminServiceService;

namespace PRN211GroupProject.Pages.ServicePage
{
    public class DetailsModel : PageModel
    {
        private readonly IServiceService _serviceService;

        public DetailsModel(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

      public Service Service { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var roleClaim = User.FindFirst(ClaimTypes.Role);
            if (User.Identity == null || !User.Identity.IsAuthenticated || roleClaim == null || roleClaim.Value.ToString() != "Admin")
            {
                return Unauthorized();
            }
            if (id == null || _serviceService.GetServiceList() == null)
            {
                return NotFound();
            }

            var service = _serviceService.GetService(id);
            if (service == null)
            {
                return NotFound();
            }
            else 
            {
                Service = service;
            }
            return Page();
        }
    }
}
