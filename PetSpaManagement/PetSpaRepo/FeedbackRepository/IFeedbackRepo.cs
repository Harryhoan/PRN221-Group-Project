using PetSpaBussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaRepo.FeedbackRepository
{
    public interface IFeedbackRepo
    {
        public void NewFeedback(Feedback feedback);
    }
}
