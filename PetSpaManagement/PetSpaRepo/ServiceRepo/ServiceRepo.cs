using PetSpaBussinessObject;
using PetSpaDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaRepo.ServiceRepo
{
    public class ServiceRepo : IServiceRepo
    {
        public Service GetService(int ServiceID) => ServiceDAO.Instance.GetService(ServiceID);
        public List<Service> GetServiceList() => ServiceDAO.Instance.GetServiceList();
        public void DeleteService(int ServiceID) => ServiceDAO.Instance.DeleteService(ServiceID);
        public void UpdateService(int ServiceID, Service service) => ServiceDAO.Instance.UpdateService(ServiceID, service);
        public void AddService(Service service) => ServiceDAO.Instance.AddService(service);

    }
}
