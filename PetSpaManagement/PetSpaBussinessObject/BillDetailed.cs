using System;
using System.Collections.Generic;

namespace PetSpaBussinessObject
{
    public partial class BillDetailed
    {
        public int Id { get; set; }
        public int BillId { get; set; }
        public double Cost { get; set; }
        public int BookingId { get; set; }

        public virtual Bill Bill { get; set; } = null!;
        public virtual Booking Booking { get; set; } = null!;
    }
}
