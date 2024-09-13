using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using Task.Data;
using Task.Models;

namespace Task.Repositories
{
    public class TeachersRepository : BaseRepository<Teacher>, ITeachersRepository
    {
        private readonly ApplicationDbContext appDbContext;
        public TeachersRepository(ApplicationDbContext appDbContext) : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Teacher>> GetAllTeachers()
        {
            try
            {
                var Courses = appDbContext.Teachers.Include(e => e.Course).ToList();

                return Courses;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<IEnumerable<Teacher>> GetTeachersByCourseId(int courseid)
        {
            try
            {
                var Courses = await appDbContext.Teachers.Where(e=>e.CourseId== courseid).ToListAsync();

                return Courses;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
