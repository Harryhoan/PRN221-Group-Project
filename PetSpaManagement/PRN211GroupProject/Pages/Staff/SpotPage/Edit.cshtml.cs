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
using PetSpaService.SpotService.SpotService;

namespace PRN211GroupProject.Pages.Staff.SpotPage
{
    public class EditModel : PageModel
    {
        private readonly ISpotService _spotService;
        public EditModel(ISpotService spotService)
        {
            _spotService = spotService;
        }

        [BindProperty]
        public Spot Spot { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var roleClaim = User.FindFirst(ClaimTypes.Role);
            if (User.Identity == null || !User.Identity.IsAuthenticated || roleClaim == null || roleClaim.Value.ToString() != "Staff")
            {
                return Unauthorized();
            }
            if (id == null || _spotService.GetSpotList() == null)
            {
                return NotFound();
            }

            var spot = _spotService.GetSpot((int)id);
            if (spot == null)
            {
                return NotFound();
            }
            Spot = spot;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                 _spotService.UpdateSpot(Spot);
            }
            catch (DbUpdateConcurrencyException)
            {
            }

            return RedirectToPage("./Index");
        }
    }
}
