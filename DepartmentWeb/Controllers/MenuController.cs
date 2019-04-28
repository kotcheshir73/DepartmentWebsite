using DepartmentWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DepartmentWeb.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMenuList()
        {
            //Tools.DepartmentUserManager.Login("admin", "qwerty");
            List<MenuElementModel> menuElements = new List<MenuElementModel>();
            MenuElementModel lecturer = new MenuElementModel() { Name = "Преподаватели", Child = new List<MenuElementModel>(), Controller = "Lecturers", Action = "Index" };

            var list = DepartmentWeb.Services.LecturerService.GetLecturers(new BaseInterfaces.BindingModels.LecturerGetBindingModel());
            foreach (var tmp in list.Result.List)
            {
                lecturer.Child.Add(new MenuElementModel() { Id = tmp.Id, Name = tmp.FullName, Controller = "Lecturers", Action = "Lecturer" });
            }

            menuElements.Add(lecturer);
            return PartialView("Menu", menuElements);
        }
    }
}