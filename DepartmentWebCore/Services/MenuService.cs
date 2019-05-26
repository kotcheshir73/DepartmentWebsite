using DepartmentWebCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepartmentWebCore.Services
{
    public class MenuService
    {
        public static List<MenuElementModel> getMenuElementList()
        {
            List<MenuElementModel> menuElements = new List<MenuElementModel>();
            MenuElementModel lecturer = new MenuElementModel() { Name = "Преподаватели", Child = new List<MenuElementModel>(), Controller = "Lecturer", Action = "Index" };

            var list = LecturerService.GetLecturers(new BaseInterfaces.BindingModels.LecturerGetBindingModel());
            foreach (var tmp in list.Result)
            {
                lecturer.Child.Add(new MenuElementModel() { Id = tmp.Abbreviation, Name = tmp.FullName, Controller = "Lecturer", Action = "Lecturer" });
            }

            menuElements.Add(lecturer);
            return menuElements;
        }
    }
}
