using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetSpaBussinessObject;
using PetSpaService.SpotService.SpotService;

namespace PRN211GroupProject.Pages.Admin.SpotPage
{
    public class IndexModel : PageModel
    {
        private readonly ISpotService _spotService;

        public IndexModel(ISpotService spotService)
        {
            _spotService = spotService;
        }
        public IList<Spot>? Spots { get; set; } = default;

        public IActionResult OnGet()
        {
            try
            {
                if (_spotService == null)
                {
                    return BadRequest();
                }
                Spots = _spotService.GetSpotList();
                return Page();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
