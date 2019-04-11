using System;
using Tools.ViewModels;

namespace BaseInterfaces.ViewModels
{
    public class StudentOrderPageViewModel : PageSettingListViewModel<StudentOrderViewModel> { }

    public class StudentOrderViewModel : PageSettingElementViewModel
    {
        public string OrderNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public string StudentOrderType { get; set; }

        public int CountStudents { get; set; }
    }
}