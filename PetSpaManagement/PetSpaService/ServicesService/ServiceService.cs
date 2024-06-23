using PetSpaBussinessObject;
using PetSpaDAO;
using PetSpaRepo.AdminServiceRepo;
using PetSpaService.AdminServiceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaService.ServicesService
{
    public class ServiceService : IServiceService
    {
        private IServiceRepo _serviceRepo;
        public ServiceService()
        {
            _serviceRepo = new ServiceRepo();
        }

        public void AddService(Service service) => ServiceDAO.Instance.AddService(service);

        public void DeleteService(int serviceID) => ServiceDAO.Instance.DeleteService(serviceID);

        public Service GetService(int serviceID) => ServiceDAO.Instance.GetService(serviceID);

        public List<Service> GetServiceList() => ServiceDAO.Instance.GetServiceList();

        public void UpdateService(Service service) => ServiceDAO.Instance.UpdateService(service);

    }
}
