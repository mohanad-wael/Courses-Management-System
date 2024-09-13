using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task.Models
{
    public class Teacher : BaseEntity
    {
       
        public bool Deleted { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; } = default!;
        public int CourseId { get; set; }
    }
}
