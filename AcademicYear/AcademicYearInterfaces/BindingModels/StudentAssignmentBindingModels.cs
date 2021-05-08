using System;
using Tools.BindingModels;

namespace AcademicYearInterfaces.BindingModels
{
    public class StudentAssignmentGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? AcademicYearId { get; set; }

        public Guid? EducationDirectionId { get; set; }

        public Guid? LecturerId { get; set; }
    }

    public class StudentAssignmentSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid AcademicYearId { get; set; }

        public Guid EducationDirectionId { get; set; }

        public Guid LecturerId { get; set; }

        public int CountStudents { get; set; }
    }
}
