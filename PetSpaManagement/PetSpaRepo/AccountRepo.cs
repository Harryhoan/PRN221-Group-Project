using PetSpaBussinessObject;
using PetSpaDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaRepo
{
    public class AccountRepo : IAccountRepo
    {
        public Account GetAccountByEmail(string Email) => AccountDAO.Instance.GetAccountByEmail(Email);
        public List<Account> GetAllAccount() => AccountDAO.Instance.GetAllAccount();
    }
}
