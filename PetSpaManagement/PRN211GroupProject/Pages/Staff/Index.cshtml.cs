using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace PRN211GroupProject.Pages.Staff
{
    public class IndexModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            var roleClaim = User.FindFirst(ClaimTypes.Role);
            if (User.Identity == null || !User.Identity.IsAuthenticated || roleClaim == null || roleClaim.Value.ToString() != "Staff")
            {
                return Unauthorized();
            }
            return Page();

        }
    }
}
