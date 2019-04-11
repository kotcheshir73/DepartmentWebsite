using System;
using Tools.ViewModels;

namespace BaseInterfaces.ViewModels
{
    public class StudentOrderBlockPageViewModel : PageSettingListViewModel<StudentOrderBlockViewModel> { }

    public class StudentOrderBlockViewModel : PageSettingElementViewModel
    {
        public Guid StudentOrderId { get; set; }

        public Guid? EducationDirectionId { get; set; }

        public string StudentOrder { get; set; }

        public string EducationDirection { get; set; }

        public string StudentOrderType { get; set; }
    }
}