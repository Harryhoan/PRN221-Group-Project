using PetSpaBussinessObject;
using PetSpaDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaRepo.AvailableRepository
{
    public interface IAvailableRepo
    {
        public void AddAvailable(Available available);

        public void DeleteAvailable(int availableId);

        public List<Available> GetAvailableList();

        public void UpdateAvailable(Available available);

        public Available GetAvailable(int availableId);

        public List<Available> GetAvailableListBySpot(int spotId);

	}
}
