using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task.Models;
using Task.ModelViews;
using Task.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Task.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ITeachersRepository teachersRepository;
        private readonly ICoursesRepository coursesRepository;

        public TeachersController(ITeachersRepository teachersRepository, ICoursesRepository coursesRepository)
        {
            this.teachersRepository = teachersRepository;
            this.coursesRepository = coursesRepository;
        }
        // GET: TeachersController
        public async Task<IActionResult> Index()
        {
            var teachers = await teachersRepository.GetAllTeachers();
            return View(teachers);

        }




        public async Task<IActionResult> Create(int? id)
        {
            AddTeacherModelView teachermodelview = new AddTeacherModelView()
            {
                Courses = (await coursesRepository.GetCoursesListItems()).ToList(),
            };
            if (id == null)
            {
                return View(teachermodelview);
            }
            Teacher teacher = await teachersRepository.GetByIdAsync(id.Value);
            teachermodelview.CourseId = teacher.CourseId;
            teachermodelview.Name = teacher.Name;
            teachermodelview.Id = id.Value;
            return View(teachermodelview);
        }

        // POST: TeachersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddTeacherModelView addTeacherModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(addTeacherModel);
                }
                Teacher teacher = new Teacher
                {
                    Id = addTeacherModel.Id,
                    CourseId = addTeacherModel.CourseId,
                    Name = addTeacherModel.Name,
                    Deleted = false
                };
                if (addTeacherModel.Id == 0)
                {
                    await teachersRepository.Add(teacher);
                }
                else
                {
                    teachersRepository.Update(teacher);
                }
                await teachersRepository.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TeachersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }



        // GET: TeachersController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var teacer = await teachersRepository.GetByIdAsync(id);
            if (teacer == null)
            {
                return NotFound();
            }
            teachersRepository.DeleteByIdAsync(id);
            await teachersRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
