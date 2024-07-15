using System;
using System.Collections.Generic;

namespace PetSpaBussinessObject
{
    public partial class Spot
    {
        public Spot()
        {
            Availables = new HashSet<Available>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime Created { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<Available> Availables { get; set; }
    }
}
