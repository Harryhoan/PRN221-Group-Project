using PetSpaBussinessObject;
using PetSpaDAO;
using PetSpaRepo.BookingRepository;
using PetSpaRepo.FeedbackRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaService.FeedbacksService
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepo feedbackRepo;

        public FeedbackService()
        {
            feedbackRepo = new FeedbackRepo();
        }


        public List<Feedback> GetAllFeedback() => feedbackRepo.GetAllFeedback();
        public Feedback GetFeedback(int feedbackId) => feedbackRepo.GetFeedback(feedbackId);
        public void NewFeedback(Feedback feedback) => feedbackRepo.NewFeedback(feedback);
        public int NumberOfFeedback() => feedbackRepo.NumberOfFeedback();
        public List<Feedback> GetAllAccountFeedBack(int id) =>feedbackRepo.GetAllAccountFeedBack(id);
        public void UpdateFeedback(Feedback feedback)
        {
            if (feedback == null || feedback.Id == default)
                throw new Exception("Invalid Feedback");
            feedbackRepo.UpdateFeedback(feedback);
        }
        public void DeleteFeedback(int accountID) => feedbackRepo.DeleteFeedback(accountID);
    }
}
