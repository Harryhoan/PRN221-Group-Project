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

		public void AddService(Service service)
		{
			if (service == null || service.Id != default)
				throw new Exception("Invalid service cannot be added");
			_serviceRepo.AddService(service);
		}

		public void DeleteService(int serviceId)
		{
			if (!(serviceId > 0))
				throw new Exception("Invalid service id");
			_serviceRepo.DeleteService(serviceId);
		}

		public Service GetService(int serviceId)
		{
			return _serviceRepo.GetService(serviceId);
		}

		public List<Service> GetServiceList() => ServiceDAO.Instance.GetServiceList();

		public void UpdateService(Service service)
		{
			if (service == null || service.Id == default)
				throw new Exception("Invalid new service");
			_serviceRepo.UpdateService(service.Id, service);
		}
		public int NumberOfService() => ServiceDAO.Instance.NumberOfService();
    }
}
