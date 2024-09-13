using Microsoft.AspNetCore.Mvc.Rendering;
using Task.Models;

namespace Task.Repositories
{
    public interface IStudentsRepository : IBaseRepository<Student>
    {

        Task<IEnumerable<Student>> GetAllStudents();


    }
}
