using System.ComponentModel.DataAnnotations;

namespace Task.Models
{
    public class BaseEntity
    {
    
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = String.Empty;
    }
}
