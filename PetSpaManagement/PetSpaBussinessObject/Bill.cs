using System;
using System.Collections.Generic;

namespace PetSpaBussinessObject
{
    public partial class Bill
    {
        public Bill()
        {
            BillDetaileds = new HashSet<BillDetailed>();
        }

        public int Id { get; set; }
        public DateTime Started { get; set; }
        public double Total { get; set; }
        public int AccId { get; set; }
        public int? VoucherId { get; set; }

        public virtual Account Acc { get; set; } = null!;
        public virtual Voucher? Voucher { get; set; }
        public virtual ICollection<BillDetailed> BillDetaileds { get; set; }
    }
}
