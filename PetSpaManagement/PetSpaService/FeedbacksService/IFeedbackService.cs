﻿using PetSpaBussinessObject;
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
        Feedback GetFeedback(int feedbackId);
        public List<Feedback> GetAllFeedback();
        public int NumberOfFeedback();
        public List<Feedback> GetAllAccountFeedBack(int id);
        public void UpdateFeedback(Feedback feedback);
        public void DeleteFeedback(int accountID);
    }
}
