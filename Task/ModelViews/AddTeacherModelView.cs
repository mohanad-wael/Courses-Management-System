using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Task.Models;

namespace Task.ModelViews
{
    public class AddTeacherModelView
    {
        public int Id { get; set; }
        [MaxLength(100)]
        [Display(Name ="Teacher Name")]
        public string Name { get; set; } = String.Empty;
        public List<SelectListItem> Courses { get; set; } = new List<SelectListItem>();
        public int CourseId { get; set; }
    }
}
