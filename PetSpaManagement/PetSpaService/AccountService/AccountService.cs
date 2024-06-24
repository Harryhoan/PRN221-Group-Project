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

        public void AddAccount(Account account)
        {
            if (account == null || account.Id != default)
                throw new Exception("Invalid account cannot be added");
            repo.AddAccount(account);
        }

        public Account GetAccount(int accountId)
        {
            return repo.GetAccount(accountId);
        }
        public Account GetAccountByEmail(string email)
        {
            return repo.GetAccountByEmail(email);
        }

        public Account Login(string Email, string password)
        {
            Account account = GetAccountByEmail(Email);
            if (account != null && account.Pass.Equals(password))
            {
                return account;

            }
            return null;
        }
        public List<Account> GetAllAccount() => repo.GetAllAccount();

        public void UpdateAccount(Account account)
        {
            if (account == null || account.Id == default)
                throw new Exception("Invalid new account");
            repo.UpdateAccount(account.Id, account);
        }
    }
}

