using PetSpaBussinessObject;
using PetSpaDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaRepo.AvailableRepository
{
    public class AvailableRepo : IAvailableRepo
    {
        public void AddAvailable(Available available) => AvailableDAO.Instance.AddAvailable(available);

        public void DeleteAvailable(int availableId) => AvailableDAO.Instance.DeleteAvailable(availableId);

        public List<Available> GetAvailableList() => AvailableDAO.Instance.GetAllAvailable();

        public void UpdateAvailable(Available available) => AvailableDAO.Instance.UpdateAvailable(available);

        public Available GetAvailable(int availableId) => AvailableDAO.Instance.GetAvailable(availableId);

		public List<Available> GetAvailableListBySpot(int spotId) => AvailableDAO.Instance.GetAllAvailableBySpot(spotId);

	}
}
