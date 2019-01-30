using System;

namespace DepartmentService.ViewModels
{
    public class GraficClassroomPageViewModel : PageViewModel<GraficClassroomViewModel> { }

    public class GraficClassroomViewModel
    {
        public Guid Id { get; set; }

        public Guid GraficId { get; set; }

        public Guid TimeNormId { get; set; }

        public string ClassroomDescription { get; set; }
    }
}
