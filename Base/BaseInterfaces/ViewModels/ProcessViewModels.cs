using System;

namespace BaseInterfaces.ViewModels
{
    public class StudentOrderShowViewModel
    {
        public Guid Id { get; set; }

        public string OrderNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public string StudentOrderType { get; set; }

        public string StudentOrderBlockType { get; set; }

        public string StudentGromFrom { get; set; }

        public string StudentGroupTo { get; set; }
    }
}