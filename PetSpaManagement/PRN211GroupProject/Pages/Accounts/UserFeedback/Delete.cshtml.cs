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

namespace PRN211GroupProject.Pages.Accounts.UserFeedback
{
    public class DeleteModel : PageModel
    {
        private readonly IFeedbackService _feedbackService;

        public DeleteModel(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [BindProperty]
      public Feedback Feedback { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _feedbackService.GetAllAccountFeedBack((int)id) == null)
            {
                return NotFound();
            }

            var feedback = _feedbackService.GetFeedback((int)id);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            try
            {
                if (id == null || _feedbackService.GetAllAccountFeedBack((int)id) == null)
                {
                    return Page();
                }
                _feedbackService.DeleteFeedback((int)id);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return RedirectToPage("./Index");
        }
    }
}
