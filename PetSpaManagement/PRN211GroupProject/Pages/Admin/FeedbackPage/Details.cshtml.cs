using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetSpaBussinessObject;
using PetSpaDaos;
using PetSpaService.FeedbacksService;

namespace PRN211GroupProject.Pages.Admin.FeedbackPage
{
    public class DetailsModel : PageModel
    {
        private readonly IFeedbackService _feedback;

        public DetailsModel(IFeedbackService feedback)
        {
            _feedback = feedback;
        }

      public Feedback Feedback { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null || _feedback.GetAllFeedback() == null)
            {
                return NotFound();
            }

            var feedback = _feedback.GetFeedback(id);
            if (feedback == null)
            {
                return NotFound();
            }
            else 
            {
                Feedback = feedback;
            }
            return Page();
        }
    }
}
