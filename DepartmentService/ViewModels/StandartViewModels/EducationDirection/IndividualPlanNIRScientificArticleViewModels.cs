using System;

namespace DepartmentService.ViewModels
{
    public class IndividualPlanNIRScientificArticlePageViewModel : PageViewModel<IndividualPlanNIRScientificArticleViewModel> { }

    public class IndividualPlanNIRScientificArticleViewModel
    {
        public Guid Id { get; set; }

        public string LecturerName { get; set; }
        
        public string Name { get; set; }
        
        public string TypeOfPublication { get; set; }
        
        public string Volume { get; set; }
        
        public string Publishing { get; set; }
        
        public string Year { get; set; }
        
        public string Status { get; set; }
    }
}
