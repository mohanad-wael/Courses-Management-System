using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Task.Models;
using Task.ModelViews;
using Task.Repositories;

namespace Task.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudentsRepository studentsRepository;
        private readonly ICoursesRepository coursesRepository;
        private readonly ITeachersRepository teachersRepository;

        public HomeController(IStudentsRepository studentsRepository,ICoursesRepository coursesRepository,ITeachersRepository teachersRepository)
        {
            this.studentsRepository = studentsRepository;
            this.coursesRepository = coursesRepository;
            this.teachersRepository = teachersRepository;
        }

        public async Task<IActionResult> Index()
        {
            var students =await studentsRepository.GetAllStudents();
            var courses =await coursesRepository.GetAllCourses();
            return View(new AddStudentModelView
            {
                Students = students,
                Courses = courses
            });           
        }

        [HttpGet]
        public async Task< IActionResult> GetTeachersByCourse(int courseId)
        {
            var teachers =(await teachersRepository.GetTeachersByCourseId(courseId));
            if (teachers == null)
            {
                return NotFound();
            }
            return Json(teachers.Select(t => new
            {
                Id = t.Id,
                Name = t.Name
            }));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddStudentModelView model)
        {
            if (model.SelectedCourseId > 0 && model.SelectedTeacherId > 0)
            {
                StudentTeacher studentTeacher = new StudentTeacher { TeacherId = model.SelectedTeacherId };
                List<StudentTeacher> studentTeachers = new List<StudentTeacher>();
                studentTeachers.Add(studentTeacher);
                var student = new Student
                {
                    Name = model.StudentName,
                     StudentTeacher= studentTeachers,
                };
               await studentsRepository.Add(student);
              await  studentsRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Reload courses and students if validation fails
            model.Courses =await coursesRepository.GetAllAsync();
            return View("Index", model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}