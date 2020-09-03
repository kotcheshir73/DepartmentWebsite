using Enums;
using Tools.BindingModels;

namespace WebInterfaces.BindingModels
{
    public class WebStudentGroupGetBindingModel : PageSettingGetBinidingModel
    {
        public string GroupName { get; set; }

        public AcademicCourse Course { get; set; }
    }
}