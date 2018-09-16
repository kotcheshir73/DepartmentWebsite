using DepartmentModel.Enums;
using DepartmentModel.Models;
using DepartmentProcessAccountingModel.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentModel.Models.ProcessAccountingModels
{
    /// <summary>
    ///  Класс, описывающий кафедральный процесс
    /// </summary>
    [DataContract]
    public class DepartmentProcess : BaseEntity
    {
        [Required]
        [DataMember]
        public Post Executor { get; set; }
        
        [DataMember]
        public DateTime? DateStart { get; set; }

        [DataMember]
        public DateTime? DateFinish { get; set; }

        [DataMember]
        public SemesterDates? SemesterDateStart { get; set; }
        
        [DataMember]
        public int? SemesterDateStartIndent { get; set; }

        [DataMember]
        public TimeDuration? Periodicity { get; set; }

        [DataMember]
        public SemesterDates? SemesterDateFinish { get; set; }

        [DataMember]
        public int? SemesterDateFinishIndent { get; set; }

        [DataMember]
        public bool Confirmability { get; set; }

        [Required]
        [DataMember]
        public string Title { get; set; }
        
        [DataMember]
        public string Description { get; set; }

        //-------------------------------------------------------------------------


        //-------------------------------------------------------------------------

        [ForeignKey("DepartmentProcessId")]
        public virtual List<ProcessDirectionRecord> ProcessDirectionRecords { get; set; }

        [ForeignKey("DepartmentProcessId")]
        public virtual List<AcademicYearProcess> AcademicYearProcesses { get; set; }
    }
}
