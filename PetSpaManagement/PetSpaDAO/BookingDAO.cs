using PetSpaDaos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaDAO
{
    public class BookingDAO
    {
        private readonly PetSpaManagementContext context = null;
        private static BookingDAO instance = null;

        public BookingDAO()
        {
            context = new PetSpaManagementContext();
        }

        public static BookingDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BookingDAO();
                }
                return instance;
            }
        }
    }
}
