using PetSpaDaos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaDAO
{
    public class BillDetailedDAO
    {
        private readonly PetSpaManagementContext context = null;
        private static BillDetailedDAO instance = null;

        public BillDetailedDAO()
        {
            context = new PetSpaManagementContext();
        }

        public static BillDetailedDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BillDetailedDAO();
                }
                return instance;
            }
        }
    }
}
