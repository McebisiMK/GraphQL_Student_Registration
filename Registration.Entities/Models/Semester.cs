using System;
using System.Collections.Generic;

namespace Registration.Entities.Models
{
    public partial class Semester
    {
        public Semester()
        {
            Subject = new HashSet<Subject>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public ICollection<Subject> Subject { get; set; }
    }
}
