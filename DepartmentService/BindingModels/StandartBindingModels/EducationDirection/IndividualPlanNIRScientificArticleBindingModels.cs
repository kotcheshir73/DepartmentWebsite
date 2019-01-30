﻿using System;

namespace DepartmentService.BindingModels
{
    public class IndividualPlanNIRScientificArticleGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? LecturerId { get; set; }

        public string Status { get; set; }
    }

    public class IndividualPlanNIRScientificArticleSetBindingModel
    {
        public Guid Id { get; set; }
        
        public Guid LecturerId { get; set; }
        
        public string Name { get; set; }
        
        public string TypeOfPublication { get; set; }
        
        public string Volume { get; set; }
        
        public string Publishing { get; set; }
        
        public string Year { get; set; }
        
        public string Status { get; set; }
    }
}
