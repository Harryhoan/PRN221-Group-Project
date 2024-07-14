using PetSpaBussinessObject;
using PetSpaDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaRepo.AdminServiceRepo
{
    public class ServiceRepo : IServiceRepo
    {
        public void AddService(Service service) => ServiceDAO.Instance.AddService(service);

        public void DeleteService(int serviceID) => ServiceDAO.Instance.DeleteService(serviceID);

        public List<Service> GetServiceList() => ServiceDAO.Instance.GetServiceList();

        public void UpdateService(int serviceID, Service service) => ServiceDAO.Instance.UpdateService(service);

        public Service GetService(int serviceID) => ServiceDAO.Instance.GetService(serviceID);
        public int NumberOfService() => ServiceDAO.Instance.NumberOfService();
    }
}
