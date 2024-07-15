using System;
using System.Collections.Generic;

namespace PetSpaBussinessObject
{
    public partial class Voucher
    {
        public Voucher()
        {
            Accounts = new HashSet<Account>();
            Bills = new HashSet<Bill>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Discount { get; set; }
        public DateTime Expired { get; set; }
        public int Reach { get; set; }
        public DateTime Created { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
    }
}
