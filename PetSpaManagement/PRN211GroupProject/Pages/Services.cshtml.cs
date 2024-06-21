using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetSpaBussinessObject;
using PetSpaService;
using PetSpaService.AdminServiceService;

namespace PRN211GroupProject.Pages
{
    public class ServicesModel : PageModel
    {
        public List<Service> services { get; set; }

        private readonly IServiceService serviceService;
        public ServicesModel(IServiceService serviceSer)
        {
            serviceService = serviceSer;
            services = new List<Service>();
        }
        public void OnGet()
        {

        }

    }
}
