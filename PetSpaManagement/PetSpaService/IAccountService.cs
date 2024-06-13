using PetSpaBussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaService
{
    public interface IAccountService
    {
        Account GetAccountByEmail(string Email, string password);
        List<Account> GetAllAccount();

    }
}
