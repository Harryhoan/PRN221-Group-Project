﻿using PetSpaBussinessObject;
using PetSpaDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaRepo.FeedbackRepository
{
    public class FeedbackRepo : IFeedbackRepo
    {
        public List<Feedback> GetAllFeedback() => FeedbackDAO.Instance.GetAllFeedback();

        public Feedback GetFeedback(int feedbackId) => FeedbackDAO.Instance.GetFeedback(feedbackId);

        public void NewFeedback(Feedback feedback) => FeedbackDAO.Instance.NewFeedback(feedback);
        public int NumberOfFeedback() => FeedbackDAO.Instance.NumberOfFeedback();
        public List<Feedback> GetAllAccountFeedBack(int id) => FeedbackDAO.Instance.GetAllAccountFeedBack(id);
        public void UpdateFeedback(Feedback feedback) => FeedbackDAO.Instance.UpdateFeedback(feedback);
        public void DeleteFeedback(int accountID) => FeedbackDAO.Instance.DeleteFeedback(accountID);
    }
}
