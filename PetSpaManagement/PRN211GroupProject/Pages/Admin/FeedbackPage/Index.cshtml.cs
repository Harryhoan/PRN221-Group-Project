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
    public class IndexModel : PageModel
    {
        private readonly IFeedbackService _feedback;

        public IndexModel(IFeedbackService feedback)
        {
            _feedback = feedback;
        }

        public IList<Feedback> Feedback { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_feedback.GetAllFeedback != null)
            {
                Feedback = _feedback.GetAllFeedback();
            }
        }
    }
}
