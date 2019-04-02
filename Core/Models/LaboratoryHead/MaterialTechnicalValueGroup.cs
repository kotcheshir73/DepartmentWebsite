using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.LaboratoryHead
{
    /// <summary>
    /// Класс, описывающий группу сведений по мат тех ценности
    /// </summary>
    [DataContract]
    public class MaterialTechnicalValueGroup : BaseEntity
    {
        [DataMember]
        public string GroupName { get; set; }

        [DataMember]
        public int Order { get; set; }

        //-------------------------------------------------------------------------

        //-------------------------------------------------------------------------

        [ForeignKey("MaterialTechnicalValueGroupId")]
        public virtual List<MaterialTechnicalValueRecord> MaterialTechnicalValueRecords { get; set; }
    }
}