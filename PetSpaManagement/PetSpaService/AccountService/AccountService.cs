using PetSpaBussinessObject;
using PetSpaDAO;
using PetSpaRepo.AccountRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaService.AccountService
{
    public class AccountService : IAccountService
    {
        private IAccountRepo repo;
        public AccountService()
        {
            repo = new AccountRepo();
        }

        public void AddAccount(Account account) => AccountDAO.Instance.AddAccount(account);

        public Account GetAccount(int accountID) => AccountDAO.Instance.GetAccount(accountID);

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

        public void UpdateAccount(Account account) => AccountDAO.Instance.UpdateAccount(account);
    }
}

