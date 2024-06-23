using PetSpaBussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaService.AvailableService
{
    public interface IAvailableService
    {
        public Available GetAvailable(int availableId);

        public List<Available> GetAvailableList();

        public void AddAvailable(Available available);

        public void DeleteAvailable(int availableId);

        public void UpdateAvailable(Available available);

    }
}
