using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Task.Data;
using Task.Models;
using Task.ModelViews;

namespace Task.Repositories
{
    public class CoursesRepository : BaseRepository<Course>, ICoursesRepository
    {
        private readonly ApplicationDbContext appDbContext;
        public CoursesRepository(ApplicationDbContext appDbContext) : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<IEnumerable<SelectListItem>> GetCoursesListItems()
        {
            try
            {
            var Courses = appDbContext.Courses.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.Name
            }).OrderBy(e => e.Text).AsNoTracking().ToList() ;
           
            return Courses;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            try
            {
                var Courses = appDbContext.Courses.Include(e => e.Teacher).ToList();

                return Courses;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<Course> GetCourseById(int id)
        {
            try
            {
            var Courses =await appDbContext.Courses.Include(e=>e.Teacher).FirstOrDefaultAsync(e=>e.Id==id) ;
           
            return Courses;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
