using PetSpaBussinessObject;
using PetSpaRepo.AvailableRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaService.AvailableService
{
    public class AvailableService : IAvailableService
    {
        private IAvailableRepo availableRepo;
        public AvailableService()
        {
            availableRepo = new AvailableRepo();
        }

        public Available GetAvailable(int availableId)
        {
            return availableRepo.GetAvailable(availableId);
        }

        public List<Available> GetAvailableList()
        {
            return availableRepo.GetAvailableList();
        }

        public void AddAvailable(Available available)
        {
            if (!(available.ServiceId > 0 && available.SpotId > 0))
                throw new Exception("Invalid available");
            availableRepo.AddAvailable(available);
        }

        public void DeleteAvailable(int availableId)
        {
            if (!(availableId > 0))
                throw new Exception("Invalid available id");
            availableRepo.DeleteAvailable(availableId);
        }

        public void UpdateAvailable(Available available)
        {
            if (available == null || !(available.Id > 0))
                throw new Exception("Invalid new available");
            availableRepo.UpdateAvailable(available);
        }
    }

}
