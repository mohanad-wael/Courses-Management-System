using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using Task.Models;
using Task.ModelViews;
using Task.Repositories;

namespace Task.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICoursesRepository coursesRepository;
        public CoursesController(ICoursesRepository coursesRepository)
        {
            this.coursesRepository = coursesRepository;
        }
        // GET: CoursesController
        public async Task<IActionResult> Index()
        {
            var Courses = await coursesRepository.GetAllCourses();
            return View(Courses);
        }



        // GET: CoursesController/Create
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                // Create new course
                return View(new AddCourseModelView()); // Return an empty course model for creation
            }
            var course = await coursesRepository.GetByIdAsync(id.Value);
            if (course == null)
            {
                return NotFound();
            }

            AddCourseModelView CourseModelView = new AddCourseModelView() { Id = course.Id, Name = course.Name };
            return View(CourseModelView);
        }

        // POST: CoursesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddCourseModelView AddCourseModelView)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(AddCourseModelView);
                }
                Course course = new Course
                {
                    Id = AddCourseModelView.Id,
                    Name = AddCourseModelView.Name,
                    Deleted = false,
                };
                if (AddCourseModelView.Id == 0)
                {
                    await coursesRepository.Add(course);
                }
                else
                {
                    coursesRepository.Update(course);
                }
                await coursesRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(new AddCourseModelView());
            }
        }



        // POST: CoursesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CoursesController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var course = await coursesRepository.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            coursesRepository.DeleteByIdAsync(id);
            await coursesRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
