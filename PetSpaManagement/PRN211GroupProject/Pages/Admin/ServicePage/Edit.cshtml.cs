using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetSpaBussinessObject;
using PetSpaDaos;
using PetSpaService.AdminServiceService;

namespace PRN211GroupProject.Pages.ServicePage
{
    public class EditModel : PageModel
    {
        private readonly IServiceService _serviceService;

        public EditModel(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [BindProperty]
        public Service Service { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null || _serviceService.GetServiceList() == null)
            {
                return NotFound();
            }

            var service =  _serviceService.GetService(id);
            if (service == null)
            {
                return NotFound();
            }
            Service = service;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                _serviceService.UpdateService(Service);
            }
            catch (Exception ex)
            {
                //if (!ServiceExists(Service.Id))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
            }

            return RedirectToPage("./Index");
        }

        //private bool ServiceExists(int id)
        //{
        //  return (_context.Services?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
