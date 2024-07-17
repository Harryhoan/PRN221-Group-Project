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
                feedback.Created = DateTime.Now;
                feedback.Updated = feedback.Created;
                context.Feedbacks.Add(feedback);
                context.SaveChanges();
            }
        }
        public List<Feedback> GetAllFeedback()
        {
            var feedback = context.Feedbacks.Include(f => f.Acc).Include(f => f.Service).ToList();
            return context.Feedbacks.ToList();
        }
        public List<Feedback> GetAllAccountFeedBack(int id)
        {
            return context.Feedbacks.Include(a => a.Acc).Include(a => a.Service).Where(f => f.AccId == id).ToList();
        }
        public void UpdateFeedback(Feedback feedback)
        {
            if(feedback == null)
            {
                throw new ArgumentNullException(nameof(feedback), "Feedback cannot be null");
            }

            var existingFeedback = context.Feedbacks.FirstOrDefault(f => f.Id == feedback.Id);
            if(existingFeedback == null)
            {
                throw new Exception("Feedback does not exist.");
            }
            feedback.Updated = DateTime.Now;
            context.Entry(existingFeedback).CurrentValues.SetValues(feedback);
            context.SaveChanges();
        }
        public void DeleteFeedback(int feedbackID)
        {
            Feedback feedback = GetFeedback(feedbackID);
            if (feedback != null)
            {
                context.Feedbacks.Remove(feedback);
                context.SaveChanges();
            }
        }
        public int NumberOfFeedback() => context.Feedbacks.Count();
    }
}
