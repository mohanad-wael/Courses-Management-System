using System.ComponentModel.DataAnnotations;

namespace Task.Models
{
    public class Course : BaseEntity
    {
       
        public bool Deleted { get; set; }
        public List<Teacher> Teacher { get; set; } = new List<Teacher>();

    }
}
