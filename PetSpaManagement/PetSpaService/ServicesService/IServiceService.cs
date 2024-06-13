using PetSpaBussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaService.AdminServiceService
{
    public interface IServiceService
    {
        Service GetService(int serviceID);
        List<Service> GetServiceList();
        public void DeleteService(int serviceID);
        public void UpdateService(Service service);
        public void AddService(Service service);

    }
}
