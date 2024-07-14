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
    public class AvailableDAO
    {
        private readonly PetSpaManagementContext context = null;
        private static AvailableDAO instance = null;

        public AvailableDAO()
        {
            context = new PetSpaManagementContext();
        }

        public static AvailableDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AvailableDAO();
                }
                return instance;
            }
        }

        public List<Available> GetAllAvailable()
        {
            var availables = context.Availables.Include(a => a.Service).Include(a => a.Spot).Where(a => a.Service.Status == true && a.Spot.Status == true).ToList();
            if (availables == null)
                throw new Exception("All availables cannot be retrieved");
            return availables;

        }
		public List<Available> GetAllAvailableBySpot(int spotId)
		{
			var availables = context.Availables.Include(a => a.Service).Include(a => a.Spot).Where(a => a.SpotId == spotId).ToList();
			if (availables == null)
				throw new Exception("All availables cannot be retrieved");
			return availables;

		}

		public Available GetAvailable(int availableId)
        {
            var available = context.Availables.Include(a => a.Service).Include(a => a.Spot).FirstOrDefault(a => a.Id.Equals(availableId));
            if (available == null)
                throw new Exception("Available cannot be retrieved");
            return available;   
        }
        public void AddAvailable(Available available)
        {
            try
            {
                if (available != null)
                {
                    Available existingAvailable = GetAvailable(available.Id);
                    if (existingAvailable == null)
                    {
                        context.Availables.Add(available);
                        context.SaveChanges();
                    }
                }
            }
            catch
            {
                Console.WriteLine("Available cannot be added");
            }
        }
        public void UpdateAvailable(Available newAvaiable)
        {
            try
            {
                if (newAvaiable == null)
                {
                    throw new ArgumentNullException(nameof(newAvaiable), "Available cannot be null");
                }

                var existingAvailable = context.Availables.FirstOrDefault(a => a.Id == newAvaiable.Id);
                if (existingAvailable == null)
                {
                    throw new Exception("Available does not exist");
                }
                context.Entry(existingAvailable).CurrentValues.SetValues(newAvaiable);
                context.SaveChanges();
            }
            catch
            {
                Console.WriteLine("Available cannot be updated");
            }
        }

        public void DeleteAvailable(int availableId)
        {
            try
            {
                var existingAvailable = GetAvailable(availableId) ?? throw new Exception("Available cannot be found");
                context.Remove(availableId);
                context.SaveChanges();
            } 
            catch
            {
                Console.WriteLine("Available cannot be deleted");
            }
        }
    }
}
