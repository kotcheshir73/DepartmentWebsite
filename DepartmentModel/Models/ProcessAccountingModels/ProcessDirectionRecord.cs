using DepartmentModel.Enums;
using DepartmentModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentModel.Models.ProcessAccountingModels
{
    /// <summary>
    ///  Класс, описывающий привязку процесса к направлению
    /// </summary>
    [DataContract]
    public class ProcessDirectionRecord : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid DepartmentProcessId { get; set; }

        [Required]
        [DataMember]
        public Guid EducationDirectionId { get; set; }

        [Required]
        [DataMember]
        public AcademicCourse AcademicCourse { get; set; }

        //-------------------------------------------------------------------------

        public virtual DepartmentProcess DepartmentProcess { get; set; }

        //-------------------------------------------------------------------------

    }
}
