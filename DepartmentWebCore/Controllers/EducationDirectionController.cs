using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

            return View(list.Result.List);
        }
    }
}