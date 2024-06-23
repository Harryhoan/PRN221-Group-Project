using PetSpaBussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaService.FeedbacksService
{
    public interface IFeedbackService
    {
        public void NewFeedback(Feedback feedback);
    }
}
