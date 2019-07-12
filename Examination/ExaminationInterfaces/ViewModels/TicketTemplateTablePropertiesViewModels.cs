using System;
using Tools.ViewModels;

namespace ExaminationInterfaces.ViewModels
{
    public class TicketTemplateTablePropertiesPageViewModel : PageSettingListViewModel<TicketTemplateTablePropertiesViewModel> { }

    public class TicketTemplateTablePropertiesViewModel : PageSettingElementViewModel
    {
        public Guid? TicketTemplateTableId { get; set; }
        
        public string Width { get; set; }
        
        public string LookValue { get; set; }
        
        public string LookFirstRow { get; set; }
        
        public string LookLastRow { get; set; }
        
        public string LookFirstColumn { get; set; }
        
        public string LookLastColumn { get; set; }
        
        public string LookNoHorizontalBand { get; set; }
        
        public string LookNoVerticalBand { get; set; }
        
        public string LayoutType { get; set; }
        
        public string BorderTopValue { get; set; }
        
        public string BorderTopColor { get; set; }
        
        public string BorderTopSize { get; set; }
        
        public string BorderTopSpace { get; set; }
        
        public string BorderBottomValue { get; set; }
        
        public string BorderBottomColor { get; set; }
        
        public string BorderBottomSize { get; set; }
        
        public string BorderBottomSpace { get; set; }
        
        public string BorderLeftValue { get; set; }
        
        public string BorderLeftColor { get; set; }
        
        public string BorderLeftSize { get; set; }
        
        public string BorderLeftSpace { get; set; }
        
        public string BorderRightValue { get; set; }
        
        public string BorderRightColor { get; set; }
        
        public string BorderRightSize { get; set; }
        
        public string BorderRightSpace { get; set; }
    }
}