using System;

namespace DepartmentWebCore.Models
{
    public class SubmenuModel
    {
        public SubmenuModel()
        {
            isActive = false;
        }

        public Guid? AcademicYearId { get; set; }

        public string Name { get; set; }

        public string ActionName { get; set; }

        public bool isActive { get; set; }
    }
}
