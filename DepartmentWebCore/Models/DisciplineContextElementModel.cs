using System;
using System.Collections.Generic;

namespace DepartmentWebCore.Models
{
    public class DisciplineContextElementModel
    {
        public string FullPath { get; set; }

        public string Name { get; set; }

        public bool IsFile { get; set; }

        public DateTime DateUpdate { get; set; }

        public List<DisciplineContextElementModel> Childs { get; set; }
    }
}