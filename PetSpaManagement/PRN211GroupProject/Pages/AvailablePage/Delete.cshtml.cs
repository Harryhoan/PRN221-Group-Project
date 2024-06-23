using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetSpaBussinessObject;
using PetSpaService.AdminServiceService;
using PetSpaService.AvailableService;

namespace PRN211GroupProject.Pages.AvailablePage
{
    public class DeleteModel : PageModel
    {
        private readonly IAvailableService _availableService;
        public DeleteModel(IAvailableService availableService)
        {
            _availableService = availableService;
        }

        [BindProperty]
        public Available? Available { get; set; }
        public IActionResult OnGet(int availableId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (_availableService.GetAvailableList() == null || _availableService.GetAvailableList().Count == 0)
            {
                return NotFound();
            }

            var existingAvailable = _availableService.GetAvailable(availableId);
            if (existingAvailable == null)
            {
                return NotFound();
            }
            this.Available = existingAvailable;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Available == null)
            {
                return BadRequest();
            }

            try
            {
                _availableService.DeleteAvailable(Available.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToPage("./Index");
        }
    }
}
