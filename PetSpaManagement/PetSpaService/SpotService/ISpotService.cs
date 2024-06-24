using PetSpaBussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaService.SpotService.SpotService
{
    public interface ISpotService
    {
        public Spot GetSpot(int spotId);

        public List<Spot> GetSpotList();

        public List<Spot> GetActiveSpotList();

		public void AddSpot(Spot spot);

        public void DeleteSpot(int spotId);

        public void UpdateSpot(Spot spot);
    }
}
