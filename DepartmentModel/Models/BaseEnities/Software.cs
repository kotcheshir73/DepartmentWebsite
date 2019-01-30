using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Класс, описывающий ПО
    /// </summary>
    [DataContract]
    public class Software : BaseEntity
    {
        [DataMember]
        public string SoftwareName { get; set; }

        [DataMember]
        public string SoftwareDescription { get; set; }

        [DataMember]
        public string SoftwareKey { get; set; }

        [DataMember]
        public string SoftwareK { get; set; }

        //-------------------------------------------------------------------------

        //-------------------------------------------------------------------------

        [ForeignKey("SoftwareId")]
        public virtual List<SoftwareRecord> SoftwareRecords { get; set; }
    }
}
