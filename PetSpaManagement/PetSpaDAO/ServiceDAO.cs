using Microsoft.EntityFrameworkCore;
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
                service.Status = false;
                _context.SaveChanges();
            }
        }
        public void UpdateService(Service newService)
        {
            if (newService == null)
            {
                throw new ArgumentNullException(nameof(newService), "Service cannot be null");
            }

            var existingService = _context.Services.FirstOrDefault(s => s.Id == newService.Id);
            if (existingService == null)
            {
                throw new Exception("Service does not exist");
            }

            _context.Entry(existingService).CurrentValues.SetValues(newService);
            _context.SaveChanges();
        }
        public void AddService(Service service)
        {
            Service newService = GetService(service.Id);
            if (newService == null)
            {
                service.Created = DateTime.Now;
                _context.Services.Add(service);
                _context.SaveChanges();
            }
        }
        public int NumberOfService() => _context.Services.AsNoTracking().Count();
    }
}
