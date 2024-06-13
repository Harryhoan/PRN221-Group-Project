using System;
using System.Collections.Generic;

namespace PetSpaBussinessObject
{
    public partial class Account
    {
        public Account()
        {
            Bookings = new HashSet<Booking>();
            Feedbacks = new HashSet<Feedback>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Pass { get; set; } = null!;
        public bool Status { get; set; }
        public int CountVoucher { get; set; }
        public int RoleId { get; set; }
        public int? VoucherId { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual Voucher? Voucher { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
