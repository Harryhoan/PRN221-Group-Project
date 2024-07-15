using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetSpaBussinessObject;
using PetSpaDaos;
using PetSpaService.AccountService;
using PetSpaService.AdminServiceService;
using PetSpaService.FeedbacksService;

namespace PRN211GroupProject.Pages
{
    public class FeedbackModel : PageModel
    {
        private readonly IFeedbackService _feedbackService;
        private readonly IServiceService _serviceService;
        private readonly IAccountService _accountService;

        public FeedbackModel(IFeedbackService feedbackService, IServiceService serviceService, IAccountService accountService)
        {
            _feedbackService = feedbackService;
            _serviceService = serviceService;
            _accountService = accountService;
        }

        public IActionResult OnGet()
        {
            ViewData["AccId"] = new SelectList(_accountService.GetAllAccount(), "Id", "Email");
            ViewData["ServiceId"] = new SelectList(_serviceService.GetServiceList(), "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Feedback Feedback { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (Feedback == null)
                {
                    return BadRequest();
                }
                try
                {
                    Feedback.Created = DateTime.Now;
                    Feedback.Updated = DateTime.Now;
                    _feedbackService.NewFeedback(Feedback);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return RedirectToPage("./Index");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}

