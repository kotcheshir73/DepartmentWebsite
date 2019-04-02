using System;
using System.Runtime.Serialization;

namespace Models.LaboratoryHead
{
    /// <summary>
    /// Класс, описывающий сведение по мат тех ценности
    /// </summary>
    [DataContract]
    public class MaterialTechnicalValueRecord : BaseEntity
    {
        [DataMember]
        public Guid MaterialTechnicalValueId { get; set; }

        [DataMember]
        public Guid MaterialTechnicalValueGroupId { get; set; }

        [DataMember]
        public string FieldName { get; set; }
        
        public string FieldValue { get; set; }

        [DataMember]
        public int Order { get; set; }

        //-------------------------------------------------------------------------

        public virtual MaterialTechnicalValue MaterialTechnicalValue { get; set; }

        public virtual MaterialTechnicalValueGroup MaterialTechnicalValueGroup { get; set; }

        //-------------------------------------------------------------------------
    }
}