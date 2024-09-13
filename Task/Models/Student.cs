using System.ComponentModel.DataAnnotations;

namespace Task.Models
{
    public class Student : BaseEntity
    {
       
        
        public bool Deleted { get; set; }

        public List<StudentTeacher> StudentTeacher { get; set; } = new List<StudentTeacher>();
    }
}
