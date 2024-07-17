using PetSpaBussinessObject;
using PetSpaDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaRepo.AccountRepository
{
    public class AccountRepo : IAccountRepo
    {
        public void AddAccount(Account account) => AccountDAO.Instance.AddAccount(account);

        public Account GetAccount(int accountID) => AccountDAO.Instance.GetAccount(accountID);

        public Account GetAccountByEmail(string Email) => AccountDAO.Instance.GetAccountByEmail(Email);
        public List<Account> GetAllAccount() => AccountDAO.Instance.GetAllAccount();
        public List<Account> GetAllAccountCreatedThisYear() => AccountDAO.Instance.GetAllAccountCreatedThisYear();

        public void UpdateAccount(int accountID, Account account) => AccountDAO.Instance.UpdateAccount(account);
        public void DeleteAccount(int accountID) => AccountDAO.Instance.DeleteAccount(accountID);

        public int NumberOfUser() => AccountDAO.Instance.NumberOfUser();
    }
}
