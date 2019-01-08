using System;
using System.Collections.Generic;

namespace TicketServiceInterfaces.ViewModels
{
    public class TicketProcessElementaryAttributeViewModels
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }

    public class TicketProcessElementaryUnitViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public int Order { get; set; }

        public List<TicketProcessElementaryAttributeViewModels> ElementaryAttributes { get; set; }

        public List<TicketProcessElementaryUnitViewModel> ChildElementaryUnits { get; set; }
    }

    public class TicketProcessParagraphDataViewModel
    {
        public Guid Id { get; set; }

        public TicketProcessElementaryUnitViewModel Font { get; set; }

        public string Name { get; set; }

        public string TextName { get; set; }

        public string Text { get; set; }

        public int Order { get; set; }

        public List<TicketProcessElementaryAttributeViewModels> ElementaryAttributes { get; set; }

        public List<TicketProcessElementaryUnitViewModel> ElementaryUnits { get; set; }
    }

    public class TicketProcessParagraphViewModel
    {
        public Guid Id { get; set; }

        public TicketProcessElementaryUnitViewModel ParagraphFormat { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public List<TicketProcessElementaryAttributeViewModels> ElementaryAttributes { get; set; }

        public List<TicketProcessParagraphDataViewModel> ParagraphDatas { get; set; }
    }

    public class TicketProcessTableCellViewModel
    {
        public Guid Id { get; set; }

        public TicketProcessElementaryUnitViewModel Properties { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public List<TicketProcessParagraphViewModel> Paragraphs { get; set; }
    }

    public class TicketProcessTableRowViewModel
    {
        public Guid Id { get; set; }

        public TicketProcessElementaryUnitViewModel Properties { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public List<TicketProcessElementaryAttributeViewModels> ElementaryAttributes { get; set; }

        public List<TicketProcessTableCellViewModel> TableCells { get; set; }
    }

    public class TicketProcessTableViewModel
    {
        public Guid Id { get; set; }

        public TicketProcessElementaryUnitViewModel Properties { get; set; }

        public TicketProcessElementaryUnitViewModel Columns { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public List<TicketProcessTableRowViewModel> TableRows { get; set; }
    }

    public class TicketProcessBodyViewModel
    {
        public Guid Id { get; set; }

        public TicketProcessElementaryUnitViewModel BodyFormat { get; set; }

        public string BodyName { get; set; }

        public string SectName { get; set; }

        public List<TicketProcessParagraphViewModel> Paragraphs { get; set; }

        public List<TicketProcessTableViewModel> Tables { get; set; }
    }

    public class TicketProcessTicketViewModel
    {
        public Guid Id { get; set; }

        public string XML { get; set; }

        public string TemplateName { get; set; }

        public TicketProcessBodyViewModel Body { get; set; }
    }
}