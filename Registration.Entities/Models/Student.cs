using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Registration.Entities.Models
{
    public partial class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string StudentNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int AddressId { get; set; }
        public string Cellphone { get; set; }
        public string IdNumber { get; set; }
        public int CourseId { get; set; }

        public Address Address { get; set; }
        public Course Course { get; set; }
    }
}
