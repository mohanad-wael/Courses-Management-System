using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Task.Models;

namespace Task.ModelViews
{
    public class AddCourseModelView
    {
        public int Id { get; set; }
        [MaxLength(100)]
        [Display(Name = "Course Name")]
        public string Name { get; set; } = String.Empty;      
    }
}
