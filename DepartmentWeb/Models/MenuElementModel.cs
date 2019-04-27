using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DepartmentWeb.Models
{
    public class MenuElementModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public List<MenuElementModel> Child { get; set; }
    }
}