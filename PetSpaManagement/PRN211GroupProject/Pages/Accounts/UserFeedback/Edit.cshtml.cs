using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetSpaBussinessObject;
using PetSpaDaos;
using PetSpaService.AccountService;
using PetSpaService.AdminServiceService;
using PetSpaService.FeedbacksService;
using PRN211GroupProject.Utilities;

namespace PRN211GroupProject.Pages.Accounts.UserFeedback
{
    public class EditModel : PageModel
    {
        private readonly IFeedbackService _feedbackService;
        private readonly IServiceService _serviceService;
        private readonly IAccountService _accountService;
        public EditModel(IFeedbackService feedbackService, IServiceService serviceService, IAccountService accountService)
        {
            _feedbackService = feedbackService;
            _serviceService = serviceService;
            _accountService = accountService;
        }

        [BindProperty]
        public Feedback Feedback { get; set; } = default!;
        public Account? Account { get; set; }
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
            Feedback = feedback;
            ViewData["ServiceId"] = new SelectList(_serviceService.GetServiceList(), "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                Account = AccountUtilities.Instance.GetAccount(HttpContext, _accountService);

                Feedback.Status = true;
                Feedback.AccId = Account.Id;
                _feedbackService.UpdateFeedback(Feedback);
                return RedirectToPage("/Accounts/UserFeedback/Index");
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
