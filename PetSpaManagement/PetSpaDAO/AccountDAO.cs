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
        public Account GetAccount(int accountID) 
        {
            return context.Accounts.FirstOrDefault(m => m.Id.Equals(accountID));
        }
        public void AddAccount(Account account)
        {
            Account newAccount = GetAccount(account.Id);
            if (newAccount == null)
            {
                context.Accounts.Add(account);
                context.SaveChanges();
            }
        }
        public void UpdateAccount(Account newAccount)
        { 
            if (newAccount == null)
            {
                throw new ArgumentNullException(nameof(newAccount), "Account cannot be null");
            }

            var existingAccount = context.Accounts.FirstOrDefault(s => s.Id == newAccount.Id);
            if (existingAccount == null)
            {
                throw new Exception("Account does not exist");
            }
            context.Entry(existingAccount).CurrentValues.SetValues(newAccount);
            context.SaveChanges();
        }
        //public void DeleteAccount(int accountID)
        //{
        //    Account
        //}

    }
}
