using Enums;
using System;
using Tools.BindingModels;

namespace ExaminationInterfaces.BindingModels
{
    public class ExaminationListGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? LecturerId { get; set; }

        public Guid? DisciplineId { get; set; }

        public Guid? StudentGroupId { get; set; }

        public Guid? StudentId { get; set; }

        public Guid? AcademicYearId { get; set; }

        public int Number { get; set; }
    }

    public class ExaminationListSetBindingModel : PageSettingSetBinidingModel
    {
        public int Number { get; set; }
        
        public Guid LecturerId { get; set; }
        
        public Guid DisciplineId { get; set; }
        
        public Guid AcademicYearId { get; set; }
        
        public Guid StudentGroupId { get; set; }
        
        public Guid StudentId { get; set; }
        
        public TypeOfTest TypeOfTest { get; set; }
        
        public string Score { get; set; }
    }
}