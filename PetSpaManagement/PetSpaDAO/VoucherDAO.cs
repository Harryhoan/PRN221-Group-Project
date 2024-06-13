using PetSpaDaos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaDAO
{
    public class VoucherDAO
    {
        private readonly PetSpaManagementContext context = null;
        private static VoucherDAO instance = null;

        public VoucherDAO()
        {
            context = new PetSpaManagementContext();
        }

        public static VoucherDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new VoucherDAO();
                }
                return instance;
            }
        }
    }
}
