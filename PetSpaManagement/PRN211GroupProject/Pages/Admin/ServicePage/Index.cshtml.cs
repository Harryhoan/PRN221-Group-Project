using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetSpaBussinessObject;
using PetSpaDaos;
using PetSpaRepo.AdminServiceRepo;
using PetSpaService.AdminServiceService;

namespace PRN211GroupProject.Pages.ServicePage
{
    public class IndexModel : PageModel
    {
        private readonly IServiceService _service;

        public IndexModel(IServiceService service)
        {
            _service = service;
        }

        public IList<Service> Service { get; set; } = default!;
        [BindProperty]
        public Service NewService { get; set; } = default!;
        public async Task OnGetAsync()
        {
            if (_service.GetServiceList() != null)
            {
                Service = _service.GetServiceList();
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (NewService != null)
            {
                _service.AddService(NewService);
            }
            return RedirectToPage("./Index");
        }
    }
}
