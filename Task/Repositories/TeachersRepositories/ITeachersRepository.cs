using Microsoft.AspNetCore.Mvc.Rendering;
using Task.Models;

namespace Task.Repositories
{
    public interface ITeachersRepository : IBaseRepository<Teacher>
    {

        Task<IEnumerable<Teacher>> GetAllTeachers();

        Task<IEnumerable<Teacher>> GetTeachersByCourseId(int courseid);
    }
}
