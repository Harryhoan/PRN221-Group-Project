using PetSpaBussinessObject;
using PetSpaDAO;
using PetSpaRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaService
{
    public class AccountService : IAccountService
    {
        private IAccountRepo repo;
        public AccountService()
        {
            repo = new AccountRepo();
        }
        public Account GetAccountByEmail(string Email, string password)
        {
            Account account = repo.GetAccountByEmail(Email);
            if (account != null && account.Pass.Equals(password))
            {
                return account;

            }
            return null;
        }
        public List<Account> GetAllAccount() => AccountDAO.Instance.GetAllAccount();
    }
}

