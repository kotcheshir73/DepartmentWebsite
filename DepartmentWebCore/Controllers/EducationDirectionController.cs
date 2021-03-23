using DepartmentWebCore.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace DepartmentWebCore.Controllers
{
	public class EducationDirectionController : Controller
    {
        private readonly BaseService _baseService;

        public EducationDirectionController(BaseService baseService)
        {
            _baseService = baseService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var list = _baseService.GetEducationDirections();
            if (list == null)
            {
                return new EmptyResult();
            }

            return View(list);
        }

        [HttpGet]
        public IActionResult EducationDirection(Guid id, Guid? courseId)
        {
            var model = _baseService.GetEducationDirection(id);

            if (model == null)
            {
                return new EmptyResult();
            }

            var courses = _baseService.GetCourses(id);

            if(!courseId.HasValue)
            {
                courseId = courses.FirstOrDefault().Id;
            }

            ViewBag.CourseId = courseId;
            ViewBag.Courses = courses;

            return View(model);
        }

        [HttpGet]
        public IActionResult EducationDirectionCourse(Guid id)
        {
            var list = _baseService.GetEducationDirectionDisciplineByCourses(id);
            if (list == null)
            {
                return new EmptyResult();
            }

            return PartialView(list);
        }
    }
}