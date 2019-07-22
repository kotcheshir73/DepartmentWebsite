using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebInterfaces.BindingModels;
using WebInterfaces.Interfaces;

namespace DepartmentWebCore.Controllers
{
    public class EducationDirectionController : Controller
    {
        IWebEducationDirectionService _serviceED;

        public EducationDirectionController(IWebEducationDirectionService serviceED)
        {
            _serviceED = serviceED;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var list = _serviceED.GetEducationDirections(new WebEducationDirectionGetBindingModel());

            if (!list.Succeeded)
            {
                return new EmptyResult();
            }

            return View(list.Result.List);
        }

        [HttpGet]
        public IActionResult EducationDirection(Guid id, Guid? courseId)
        {
            var model = _serviceED.GetEducationDirection(new WebEducationDirectionGetBindingModel { Id = id });

            if (!model.Succeeded)
            {
                return new EmptyResult();
            }

            if(!courseId.HasValue)
            {
                courseId = model.Result.Courses?.FirstOrDefault().Item1;
            }

            ViewBag.CourseId = courseId;

            return View(model.Result);
        }

        [HttpGet]
        public IActionResult EducationDirectionCourse(Guid id)
        {
            var list = _serviceED.GetDisciplinesByCourses(new WebEducationDirectionDisciplineListInfoBindingModel { CourseId = id });

            if (!list.Succeeded)
            {
                return new EmptyResult();
            }

            return PartialView(list.Result);
        }
    }
}