using Microsoft.AspNetCore.Mvc.Rendering;
using Task.Models;

namespace Task.Repositories
{
    public interface ICoursesRepository : IBaseRepository<Course>
    {
       Task<IEnumerable<SelectListItem>> GetCoursesListItems();
        Task<IEnumerable<Course>> GetAllCourses();
        Task<Course> GetCourseById(int id);
    }
}
