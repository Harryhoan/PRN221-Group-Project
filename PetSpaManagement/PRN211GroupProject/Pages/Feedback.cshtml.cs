using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetSpaBussinessObject;
using PetSpaDaos;
using PetSpaService.FeedbacksService;

namespace PRN211GroupProject.Pages
{
    public class FeedbackModel : PageModel
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackModel(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Feedback Feedback { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            _feedbackService.NewFeedback(Feedback);

            return RedirectToPage("./Index");
        }
    }
}
