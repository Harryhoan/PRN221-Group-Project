using PetSpaBussinessObject;
using PetSpaDaos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaDAO
{
    public class AccountDAO
    {
        private readonly PetSpaManagementContext context = null;
        private static AccountDAO instance = null;

        public AccountDAO()
        {
            context = new PetSpaManagementContext();
        }

        public static AccountDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AccountDAO();
                }
                return instance;
            }
        }
        public Account GetAccountByEmail(string Email)
        {
            return context.Accounts.SingleOrDefault(m => m.Email.Equals(Email));
        }
        public List<Account> GetAllAccount()
        {
            return context.Accounts.ToList();

        }

    }
}
