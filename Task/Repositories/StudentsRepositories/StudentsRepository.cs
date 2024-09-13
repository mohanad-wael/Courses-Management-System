using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using Task.Data;
using Task.Models;

namespace Task.Repositories
{
    public class StudentsRepository : BaseRepository<Student>, IStudentsRepository
    {
        private readonly ApplicationDbContext appDbContext;
        public StudentsRepository(ApplicationDbContext appDbContext) : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            try
            {
                var Students = appDbContext.Students.Include(e => e.StudentTeacher).ThenInclude(e=>e.Teacher).ThenInclude(e=>e.Course).ToList();

                return Students;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
