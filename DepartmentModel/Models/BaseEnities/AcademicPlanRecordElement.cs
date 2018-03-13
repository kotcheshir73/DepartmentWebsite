using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentModel.Models
{
    public class AcademicPlanRecordElement : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid AcademicPlanRecordId { get; set; }

        [Required]
        [DataMember]
        public Guid KindOfLoadId { get; set; }

        [Required]
        [DataMember]
        public decimal Hours { get; set; }

        public virtual AcademicPlanRecord AcademicPlanRecord { get; set; }

        public virtual KindOfLoad KindOfLoad { get; set; }
    }
}
