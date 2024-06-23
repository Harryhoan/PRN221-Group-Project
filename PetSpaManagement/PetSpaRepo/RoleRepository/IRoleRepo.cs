using PetSpaBussinessObject;
using PetSpaDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaRepo.RoleRepository
{
    public interface IRoleRepo
    {
        public List<Role> GetAllRole();
    }
}
