using PetSpaDaos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaDAO
{
    public class SpotDAO
    {
        private readonly PetSpaManagementContext context = null;
        private static SpotDAO instance = null;

        public SpotDAO()
        {
            context = new PetSpaManagementContext();
        }

        public static SpotDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SpotDAO();
                }
                return instance;
            }
        }
    }
}
