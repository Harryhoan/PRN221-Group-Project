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
            account.Pass = HashPassword(account.Pass);
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
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
        public Account Login(string Email, string password)
        {
            Account account = GetAccountByEmail(Email);
            var valid = VerifyPassword(password, account.Pass);
            if (account != null && valid == true)
            {
                return account;

            }
            return null;
        }
        public List<Account> GetAllAccount() => repo.GetAllAccount();
        public void UpdateAccount(Account account)
        {
            if (account == null || account.Id == default)
                throw new Exception("Invalid account");
            account.Pass = HashPassword(account.Pass);
            repo.UpdateAccount(account.Id, account);
        }
        public void DeleteAccount(int accountId)
        {
            if (!(accountId > 0))
                throw new Exception("Invalid accountid");
             repo.DeleteAccount(accountId);
        }

        public bool IsAdmin(int accountID) => AccountDAO.Instance.IsAdmin(accountID);

    }
}

