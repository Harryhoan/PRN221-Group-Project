using PetSpaBussinessObject;
using PetSpaDaos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaDAO
{
    public class RoleDAO
    {
        private readonly PetSpaManagementContext context = null;
        private static RoleDAO instance = null;

        public RoleDAO()
        {
            context = new PetSpaManagementContext();
        }

        public static RoleDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RoleDAO();
                }
                return instance;
            }
        }
        public List<Role> GetAllRole()
        {
            return context.Roles.ToList();
        }
    }
}
