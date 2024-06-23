using PetSpaBussinessObject;
using PetSpaDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaService.RolesService
{
    public class RoleService : IRoleService
    {
        public List<Role> GetAllRole() => RoleDAO.Instance.GetAllRole();
    }
}
