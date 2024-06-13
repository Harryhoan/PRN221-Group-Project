using PetSpaBussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaRepo
{
    public interface IAccountRepo
    {
        public Account GetAccountByEmail(string Email);
        public List<Account> GetAllAccount();
    }
}
