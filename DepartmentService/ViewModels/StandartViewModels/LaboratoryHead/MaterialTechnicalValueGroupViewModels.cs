using System;

namespace DepartmentService.ViewModels
{
    public class MaterialTechnicalValueGroupPageViewModel : PageViewModel<MaterialTechnicalValueGroupViewModel> { }

    public class MaterialTechnicalValueGroupViewModel
    {
        public Guid Id { get; set; }

        public string GroupName { get; set; }
        
        public int Order { get; set; }
    }
}
