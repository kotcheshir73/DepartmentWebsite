using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DepartmentWebCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentWebCore.Controllers
{
    public class SharedController : Controller
    {
        public IActionResult Error(string message)
        {
            return View(new ErrorViewModel {
                Message = message
            });
        }
    }
}