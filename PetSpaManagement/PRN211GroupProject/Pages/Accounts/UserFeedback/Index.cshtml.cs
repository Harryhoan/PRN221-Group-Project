using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetSpaBussinessObject;
using PetSpaDaos;
using PetSpaService.AccountService;
using PetSpaService.FeedbacksService;
using PRN211GroupProject.Utilities;

namespace PRN211GroupProject.Pages.Accounts.UserFeedback
{
    public class IndexModel : PageModel
    {
        private readonly IFeedbackService _feedbackService;
        private readonly IAccountService _accountService;

        public IndexModel(IFeedbackService feedbackService, IAccountService accountService)
        {
            _feedbackService = feedbackService;
            _accountService = accountService;
        }
        public string errorMessage { get; set; }

        public IList<Feedback> Feedback { get;set; } = default!;
        public Account? Account { get; set; }

        public IActionResult OnGet()
        {
            Account = AccountUtilities.Instance.GetAccount(HttpContext, _accountService);
            if (Account != null)
            {
                Feedback = _feedbackService.GetAllAccountFeedBack(Account.Id);
                if (Feedback == null || Feedback.Count == 0)
                {
                    errorMessage = "No Feedback was found for this account.";
                }
                return Page();
            }
            else
            {
                errorMessage = "You must login first";
                return RedirectToPage("/Accounts/Login");
            }
        }
    }
}
