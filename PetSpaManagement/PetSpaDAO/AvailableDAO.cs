using PetSpaDaos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaDAO
{
    public class AvailableDAO
    {
        private readonly PetSpaManagementContext context = null;
        private static AvailableDAO instance = null;

        public AvailableDAO()
        {
            context = new PetSpaManagementContext();
        }

        public static AvailableDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AvailableDAO();
                }
                return instance;
            }
        }
    }
}
