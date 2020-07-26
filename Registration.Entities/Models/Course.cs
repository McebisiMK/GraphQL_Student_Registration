using System;
using System.Collections.Generic;

namespace Registration.Entities.Models
{
    public partial class Course
    {
        public Course()
        {
            Student = new HashSet<Student>();
            Subject = new HashSet<Subject>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Student> Student { get; set; }
        public ICollection<Subject> Subject { get; set; }
    }
}
