using PetSpaBussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaRepo.AccountRepository
{
    public interface IAccountRepo
    {
        public Account GetAccountByEmail(string Email);
        public List<Account> GetAllAccount();
        public Account GetAccount(int accountID);
        public void AddAccount(Account account);
        public void UpdateAccount(int accountID, Account account);
        public void DeleteAccount(int accountID);
        public int NumberOfUser();
    }
}
