using PetSpaBussinessObject;
using PetSpaDaos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaDAO
{
    public class SpotDAO
    {
        private readonly PetSpaManagementContext context = null;
        private static SpotDAO instance = null;

        public SpotDAO()
        {
            context = new PetSpaManagementContext();
        }

        public static SpotDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SpotDAO();
                }
                return instance;
            }
        }

        public List<Spot> GetAllSpot()
        {
            var spots = context.Spots.ToList();
            if (spots == null)
                throw new Exception("All spots cannot be retrieved");
            return spots;

        }

        public List<Spot> GetActiveSpot() 
        {
			var spots = context.Spots.Where(s => s.Status == true).ToList();
			if (spots == null)
				throw new Exception("Active spots cannot be retrieved");
			return spots;
		}
        public Spot GetSpot(int spotId)
        {
            var spot = context.Spots.FirstOrDefault(s => s.Id.Equals(spotId));
            if (spot == null)
                throw new Exception("Spot cannot be retrieved");
            return spot;
        }
        public void AddSpot(Spot spot)
        {
            try
            {
                Spot existingSpot = GetSpot(spot.Id);
                if (existingSpot == null && spot != null)
                {
                    context.Spots.Add(spot);
                    context.SaveChanges();
                }
            }
            catch
            {
                Console.WriteLine("Spot cannot be added");
            }
        }
        public void UpdateSpot(Spot newSpot)
        {
            try
            {
                if (newSpot == null)
                {
                    throw new ArgumentNullException(nameof(newSpot), "Spot cannot be null");
                }

                var existingSpot = context.Spots.FirstOrDefault(s => s.Id == newSpot.Id);
                if (existingSpot == null)
                {
                    throw new Exception("Spot does not exist");
                }
                context.Entry(existingSpot).CurrentValues.SetValues(newSpot);
                context.SaveChanges();
            }
            catch
            {
                Console.WriteLine("Spot cannot be updated");
            }
        }

        public void SoftRemoveSpot(int spotId)
        {
            try
            {
                var spot = GetSpot(spotId);
                if (spot == null) 
                    throw new Exception("Spot cannot be found");
                spot.Status = false;
                context.Update(spot);
                context.SaveChanges();
            }
            catch
            {
                Console.WriteLine("Spot cannot be removed");
            }
        }
    }
}
