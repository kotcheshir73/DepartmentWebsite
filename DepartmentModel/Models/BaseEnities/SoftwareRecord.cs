using System;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Класс, описывающий ПО, установленное на ПК
    /// </summary>
    [DataContract]
    public class SoftwareRecord : BaseEntity
    {
        [DataMember]
        public Guid MaterialTechnicalValueId { get; set; }

        [DataMember]
        public string SoftwareName { get; set; }

        [DataMember]
        public string SoftwareDescription { get; set; }

        [DataMember]
        public string SoftwareKey { get; set; }

        [DataMember]
        public string SoftwareK { get; set; }

        [DataMember]
        public string ClaimNumber { get; set; }

        //-------------------------------------------------------------------------

        public virtual MaterialTechnicalValue MaterialTechnicalValue { get; set; }

        //-------------------------------------------------------------------------
    }
}
