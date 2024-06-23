using System;
using System.Collections.Generic;

namespace PetSpaBussinessObject
{
    public partial class Service
    {
        public Service()
        {
            Availables = new HashSet<Available>();
            Feedbacks = new HashSet<Feedback>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Duration { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<Available> Availables { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
