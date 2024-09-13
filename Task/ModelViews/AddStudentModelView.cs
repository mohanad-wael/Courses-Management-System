using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Task.Models;

namespace Task.ModelViews
{
    public class AddStudentModelView
    {
      
            public IEnumerable<Student> Students { get; set; }
            public IEnumerable<Course> Courses { get; set; }
            public string StudentName { get; set; }
            public int SelectedCourseId { get; set; }
            public int SelectedTeacherId { get; set; }
        

    }
}
