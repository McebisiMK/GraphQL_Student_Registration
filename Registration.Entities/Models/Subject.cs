using System;
using System.Collections.Generic;

namespace Registration.Entities.Models
{
    public partial class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CourseId { get; set; }
        public int SemesterId { get; set; }

        public Course Course { get; set; }
        public Semester Semester { get; set; }
    }
}
