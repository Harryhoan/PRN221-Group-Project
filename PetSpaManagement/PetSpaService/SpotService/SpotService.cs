using PetSpaBussinessObject;
using PetSpaRepo;
using PetSpaRepo.SpotRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
namespace PetSpaService.SpotService.SpotService
{
    public class SpotService : ISpotService
    {
        private ISpotRepo spotRepo;

        public SpotService()
        {
            spotRepo = new SpotRepo();
        }

        public void AddSpot(Spot spot)
        {
            if (spot.Id != default(int))
                throw new Exception("Invalid spot cannot be added");
            spotRepo.AddSpot(spot);
        }

        public void DeleteSpot(int spotId)
        {
            if (!(spotId > 0))
                throw new Exception("Invalid spot id");
            spotRepo.SoftRemoveSpot(spotId);
        }

        public Spot GetSpot(int spotId)
        {
            return spotRepo.GetSpot(spotId);
        }

        public List<Spot> GetSpotList()
        {
            return spotRepo.GetSpotList();
        }

        public List<Spot> GetActiveSpotList()
        {
            return spotRepo.GetActiveSpotList();
        }

        public void UpdateSpot(Spot spot)
        {
            if (spot == null || !(spot.Id > 0))
                throw new Exception("Invalid new spot");
            spotRepo.UpdateSpot(spot);
        }
    }
}
