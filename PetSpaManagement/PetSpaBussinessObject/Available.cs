using System;
using System.Collections.Generic;

namespace PetSpaBussinessObject
{
    public partial class Available
    {
        public Available()
        {
            Bookings = new HashSet<Booking>();
        }

        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int SpotId { get; set; }

        public Boolean Status { get; set; }

        public virtual Service Service { get; set; } = null!;
        public virtual Spot Spot { get; set; } = null!;
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
