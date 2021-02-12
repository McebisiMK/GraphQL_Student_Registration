using System.ComponentModel.DataAnnotations.Schema;

namespace Registration.Entities.Models
{
    public partial class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Column("Semster", TypeName = "varchar(6)")]
        public Semester Semester { get; set; }
        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}
