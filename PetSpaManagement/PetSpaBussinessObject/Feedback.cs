using System;
using System.Collections.Generic;

namespace PetSpaBussinessObject
{
    public partial class Feedback
    {
        public int Id { get; set; }
        public string Information { get; set; } = null!;
        public bool Status { get; set; }
        public int Rating { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int AccId { get; set; }
        public int ServiceId { get; set; }

        public virtual Account Acc { get; set; } = null!;
        public virtual Service Service { get; set; } = null!;
    }
}
