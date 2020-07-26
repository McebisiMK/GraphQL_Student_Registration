using System;
using System.Collections.Generic;

namespace Registration.Entities.Models
{
    public partial class Address
    {
        public Address()
        {
            Student = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string Unit { get; set; }
        public string Street { get; set; }
        public string Town { get; set; }
        public string Province { get; set; }

        public ICollection<Student> Student { get; set; }
    }
}
