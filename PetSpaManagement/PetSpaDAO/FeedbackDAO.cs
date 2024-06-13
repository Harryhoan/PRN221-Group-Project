using PetSpaDaos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaDAO
{
    public class FeedbackDAO
    {
        private readonly PetSpaManagementContext context = null;
        private static FeedbackDAO instance = null;

        public FeedbackDAO()
        {
            context = new PetSpaManagementContext();
        }

        public static FeedbackDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FeedbackDAO();
                }
                return instance;
            }
        }
    }
}
