using System;

namespace DepartmentModel.Models
{
    public class SoftwareRecord : BaseEntity
    {
        public Guid MaterialTechnicalValueId { get; set; }

        public string SoftwareName { get; set; }

        public string SoftwareDescription { get; set; }

        public string SoftwareKey { get; set; }

        public string SoftwareK { get; set; }

        public string ClaimNumber { get; set; }

        //-------------------------------------------------------------------------

        public virtual MaterialTechnicalValue MaterialTechnicalValue { get; set; }

        //-------------------------------------------------------------------------
    }
}
