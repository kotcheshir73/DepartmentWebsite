﻿using System;
using Tools.ViewModels;

namespace ExaminationInterfaces.ViewModels
{
    public class TicketTemplateDocumentSettingPageViewModel : PageSettingListViewModel<TicketTemplateDocumentSettingViewModel> { }

    public class TicketTemplateDocumentSettingViewModel : PageSettingElementViewModel
    {
        public Guid? TicketTemplateId { get; set; }

        public string InnerXml { get; set; }

        public int Order { get; set; }
    }
}