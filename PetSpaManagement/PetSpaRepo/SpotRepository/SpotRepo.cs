using PetSpaBussinessObject;
using PetSpaDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaRepo.SpotRepository
{
    public class SpotRepo : ISpotRepo
    {
        public void AddSpot(Spot spot) => SpotDAO.Instance.AddSpot(spot);

        public Spot GetSpot(int spotId) => SpotDAO.Instance.GetSpot(spotId);

        public List<Spot> GetSpotList() => SpotDAO.Instance.GetAllSpot();

        public List<Spot> GetActiveSpotList() => SpotDAO.Instance.GetActiveSpot();

        public void SoftRemoveSpot(int spotId) => SpotDAO.Instance.SoftRemoveSpot(spotId);

        public void UpdateSpot(Spot spot) => SpotDAO.Instance.UpdateSpot(spot);
    }
}
