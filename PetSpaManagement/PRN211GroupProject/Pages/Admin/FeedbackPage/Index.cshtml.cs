using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetSpaBussinessObject;
using PetSpaDaos;
using PetSpaService.FeedbacksService;

namespace PRN211GroupProject.Pages.Admin.FeedbackPage
{
    public class IndexModel : PageModel
    {
        private readonly IFeedbackService _feedback;

        public IndexModel(IFeedbackService feedback)
        {
            _feedback = feedback;
        }

        public IList<Feedback> Feedback { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var roleClaim = User.FindFirst(ClaimTypes.Role);
            if (User.Identity == null || !User.Identity.IsAuthenticated || roleClaim == null || roleClaim.Value.ToString() != "Admin")
            {
                return Unauthorized();
            }
            if (_feedback.GetAllFeedback != null)
            {
                Feedback = _feedback.GetAllFeedback();
            }
            return Page();
        }
    }
}
