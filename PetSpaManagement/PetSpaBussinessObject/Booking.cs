using System;
using System.Collections.Generic;

namespace PetSpaBussinessObject
{
    public partial class Booking
    {
        public Booking()
        {
            BillDetaileds = new HashSet<BillDetailed>();
        }

        public int Id { get; set; }
        public DateTime Started { get; set; }
        public DateTime Ended { get; set; }
        public DateTime Created { get; set; }
        public int AvailableId { get; set; }
        public int AccountId { get; set; }
        public bool Status { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual Available Available { get; set; } = null!;
        public virtual ICollection<BillDetailed> BillDetaileds { get; set; }
    }
}
