using PetSpaBussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaRepo.ServiceRepo
{
    public interface IServiceRepo
    {
        public Service GetService(int serviceID);
        public List<Service> GetServiceList();
        public void DeleteService(int serviceID);
        public void UpdateService(int serviceID, Service service);
        public void AddService(Service service);
    }
}
