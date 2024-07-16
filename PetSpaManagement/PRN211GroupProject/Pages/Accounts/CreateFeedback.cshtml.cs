using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using PetSpaBussinessObject;
using PetSpaDaos;
using PetSpaService.AccountService;
using PetSpaService.AdminServiceService;
using PetSpaService.FeedbacksService;
using PRN211GroupProject.Utilities;

namespace PRN211GroupProject.Pages.Accounts
{
    public class CreateFeedbackModel : PageModel
    {
        private readonly IFeedbackService _feedbackService;
        private readonly IAccountService _accountService;
        private readonly IServiceService _serviceService;

        public CreateFeedbackModel(IFeedbackService feedbackService, IAccountService accountService, IServiceService serviceService)
        {
            _feedbackService = feedbackService;
            _accountService = accountService;
            _serviceService = serviceService;
        }

        public IActionResult OnGet()
        {
            ViewData["AccId"] = new SelectList(_accountService.GetAllAccount(), "Id", "Name");
            ViewData["ServiceId"] = new SelectList(_serviceService.GetServiceList(), "Id", "Name");
            Account = AccountUtilities.Instance.GetAccount(HttpContext, _accountService);
            return Page();
        }

        [BindProperty]
        public Feedback Feedback { get; set; } = default!;

		[BindProperty]
		public Account? Account { get; set; }
		// To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
		public async Task<IActionResult> OnPostAsync()
        {
            try
            {
				Account = AccountUtilities.Instance.GetAccount(HttpContext, _accountService);
                if (Account == null)
                {
                    return RedirectToPage("/Accounts/Login");
                }
                if (Feedback == null || String.IsNullOrEmpty(Feedback.Information) || Feedback.ServiceId == default || Feedback.Rating < 0)
                {
                    return BadRequest();
                }
                try
                {
                    Feedback.AccId = Account.Id;
                    Feedback.Status = true;
                    Feedback.Created = DateTime.Now;
                    Feedback.Updated = Feedback.Created;
                    _feedbackService.NewFeedback(Feedback);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return RedirectToPage("/Index");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
