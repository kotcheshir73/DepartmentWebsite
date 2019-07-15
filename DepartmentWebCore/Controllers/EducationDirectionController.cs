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

        IWebProcess _process;

        public EducationDirectionController(IWebEducationDirectionService serviceED, IWebProcess process)
        {
            _serviceED = serviceED;
            _process = process;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var list = _serviceED.GetEducationDirections(new WebEducationDirectionGetBindingModel());

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
            var list = _process.GetDisciplinesByCourses(new WebProcessDisciplineListInfoBindingModel { CourseId = id });

            if (!list.Succeeded)
            {
                return new EmptyResult();
            }

            return PartialView(list.Result);
        }
    }
}