using System;
using System.Collections.Generic;

namespace DepartmentWebCore.Models
{
    public class MenuElementModel
    {
        public MenuElementModel()
        {
            Child = new List<MenuElementModel>(); 
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public List<MenuElementModel> Child { get; set; }
    }
}