using Microsoft.EntityFrameworkCore;
using PetSpaBussinessObject;
using PetSpaDaos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaDAO
{
    public class FeedbackDAO
    {
        private readonly PetSpaManagementContext context = null;
        private static FeedbackDAO instance = null;

        public FeedbackDAO()
        {
            context = new PetSpaManagementContext();
        }

        public static FeedbackDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FeedbackDAO();
                }
                return instance;
            }
        }
        public Feedback GetFeedback(int feedbackId)
        {
            return context.Feedbacks.FirstOrDefault(m => m.Id.Equals(feedbackId));
        }
        public void NewFeedback(Feedback feedback)
        {
            Feedback newFeedback = GetFeedback(feedback.Id);
            if (newFeedback == null)
            {
                context.Feedbacks.Add(feedback);
                context.SaveChanges();
            }
        }
        public List<Feedback> GetAllFeedback()
        {
            var feedback = context.Feedbacks.Include(f => f.Acc).Include(f => f.Service).ToList();
            return context.Feedbacks.ToList();
        }
    }
}
