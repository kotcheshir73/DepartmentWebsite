using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentModel.Models.BaseEnities
{
    /// <summary>
    /// Класс, описывающий нагрузку преподавателя и часов
    /// </summary>
    [DataContract]
    public class AcademicPlanRecordMission : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid LecturerId { get; set; }

        [Required]
        [DataMember]
        public Guid AcademicPlanRecordElementId { get; set; }

        [Required]
        [DataMember]
        public decimal Hours { get; set; }

        //-------------------------------------------------------------------------

        public virtual AcademicPlanRecordElement AcademicPlanRecordElement { get; set; }

        public virtual Lecturer Lecturer { get; set; }


    }
}
