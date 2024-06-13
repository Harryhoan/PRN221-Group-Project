using PetSpaDaos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaDAO
{
    public class BillDAO
    {
        private readonly PetSpaManagementContext context = null;
        private static BillDAO instance = null;

        public BillDAO()
        {
            context = new PetSpaManagementContext();
        }

        public static BillDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BillDAO();
                }
                return instance;
            }
        }
    }
}
