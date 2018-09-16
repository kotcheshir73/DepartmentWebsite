using DepartmentModel.Models;
using DepartmentProcessAccountingModel.Enums;
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
    ///  Класс, описывающий кафедральный процесс в академическом году
    /// </summary>
    [DataContract]
    public class AcademicYearProcess : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid DepartmentProcessId { get; set; }

        [Required]
        [DataMember]
        public Guid AcademicYearId { get; set; }

        [Required]
        [DataMember]
        public Guid UserId { get; set; }

        [Required]
        [DataMember]
        public ProcessStatus Status { get; set; }

        [Required]
        [DataMember]
        public bool IsConfirmed { get; set; }
        
        [DataMember]
        public Guid? UserConfirmedId { get; set; }

        //-------------------------------------------------------------------------

        public virtual DepartmentProcess DepartmentProcess { get; set; }

        //-------------------------------------------------------------------------

    }
}
