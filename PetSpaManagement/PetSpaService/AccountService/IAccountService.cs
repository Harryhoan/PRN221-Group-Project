﻿using PetSpaBussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaService.AccountService
{
    public interface IAccountService
    {
        Account GetAccountByEmail(string Email, string password);
        List<Account> GetAllAccount();
        Account GetAccount(int accountID);
        public void AddAccount(Account account);
        public void UpdateAccount(Account account);
    }
}