using PetSpaBussinessObject;
using PetSpaDaos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaDAO
{
    public class ServiceDAO
    {
        private readonly PetSpaManagementContext _context = null;
        private static ServiceDAO instance = null;
        public ServiceDAO()
        {
            _context = new PetSpaManagementContext();
        }
        public static ServiceDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ServiceDAO();
                }
                return instance;
            }
        }
        public Service GetService(int serviceID)
        {
            return _context.Services.FirstOrDefault(m => m.Id.Equals(serviceID));
        }
        public List<Service> GetServiceList()
        {
            return _context.Services.ToList();
        }
        public void DeleteService(int serviceID)
        {
            Service service = GetService(serviceID);
            if (service != null)
            {
                _context.Services.Remove(service);
                _context.SaveChanges();
            }
        }
        public void UpdateService(int serviceID, Service newService)
        {
            Service service = GetService(serviceID);
            if (service != null)
            {
                service.Name = service.Name;
                service.Price = service.Price;
                service.Duration = service.Duration;
                service.Status = service.Status;
                _context.Services.Update(service);
                _context.SaveChanges();
            }
        }
        public void AddService(Service service)
        {
            Service newService = GetService(service.Id);
            if (newService != null)
            {
                _context.Add(newService);
                _context.SaveChanges();
            }
        }
    }
}
