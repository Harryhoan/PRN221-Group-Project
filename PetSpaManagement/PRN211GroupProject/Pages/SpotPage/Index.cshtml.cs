using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetSpaBussinessObject;
using PetSpaService.SpotService.SpotService;

namespace PRN211GroupProject.Pages.SpotPage
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
            if (_spotService == null)
            {
                return BadRequest();
            }
            Spots = _spotService.GetSpotList();
            return Page();
        }
    }
}
