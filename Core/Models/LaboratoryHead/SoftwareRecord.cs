using System;
using System.Runtime.Serialization;

namespace Models.LaboratoryHead
{
    /// <summary>
    /// Класс, описывающий установленное ПО на ПК
    /// </summary>
    [DataContract]
    public class SoftwareRecord : BaseEntity
    {
        [DataMember]
        public Guid MaterialTechnicalValueId { get; set; }

        [DataMember]
        public Guid SoftwareId { get; set; }

        [DataMember]
        public string SetupDescription { get; set; }

        [DataMember]
        public string ClaimNumber { get; set; }

        //-------------------------------------------------------------------------

        public virtual MaterialTechnicalValue MaterialTechnicalValue { get; set; }

        public virtual Software Software { get; set; }

        //-------------------------------------------------------------------------
    }
}