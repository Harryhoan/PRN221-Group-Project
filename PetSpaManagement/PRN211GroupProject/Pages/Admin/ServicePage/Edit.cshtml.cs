using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetSpaBussinessObject;
using PetSpaDaos;
using PetSpaService.AdminServiceService;
using PRN211GroupProject.Utilities;

namespace PRN211GroupProject.Pages.ServicePage
{
	public class EditModel : PageModel
	{
		private readonly IServiceService _serviceService;

		public EditModel(IServiceService serviceService)
		{
			_serviceService = serviceService;
			Service = new();
		}

		[BindProperty]
		public Service Service { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			try
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

				var service = _serviceService.GetService((int)id);
				if (service == null)
				{
					return NotFound();
				}
				Service = service;
				return Page();
			}
			catch
			{
				return BadRequest();
			}
		}

		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see https://aka.ms/RazorPagesCRUD.
		public async Task<IActionResult> OnPostAsync()
		{
			try
			{
				var roleClaim = User.FindFirst(ClaimTypes.Role);
				if (User.Identity == null || !User.Identity.IsAuthenticated || roleClaim == null || roleClaim.Value.ToString() != "Admin")
				{
					return Unauthorized();
				}
				if (Service != null && Service.Id > 0 && !String.IsNullOrEmpty(Service.Description) && !String.IsNullOrEmpty(Service.Description) && Service.Price > 0 && Service.Duration >= 1)
				{
					Service.Description = FormatUtilities.TrimSpacesPreserveSingle(Service.Description);
					Service.Name = FormatUtilities.TrimSpacesPreserveSingle(Service.Name);
					_serviceService.UpdateService(Service);
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
