using PetSpaBussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaRepo.SpotRepository
{
    public interface ISpotRepo
    {
        public void AddSpot(Spot spot);

        public void SoftRemoveSpot(int spotId);

        public List<Spot> GetSpotList();

		public List<Spot> GetActiveSpotList();

		public void UpdateSpot(Spot spot);

        public Spot GetSpot(int spotId);

	}
}
