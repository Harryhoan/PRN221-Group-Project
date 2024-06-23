using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetSpaBussinessObject;
using PetSpaService.AdminServiceService;

namespace PRN211GroupProject.Pages.Services
{
    public class IndexModel : PageModel
    {
        private readonly IServiceService serviceService;
        public List<Service> Services { get; set; }

        public IndexModel(IServiceService serviceSer)
        {
            serviceService = serviceSer;
        }

        public IActionResult OnGet()
        {
            Services = serviceService.GetServiceList(); // Replace with your actual method to fetch services from the database
            return Page();
        }
    }
}
