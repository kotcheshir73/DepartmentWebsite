using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Класс, хранящий инорфмацию по учебным годам
    /// </summary>
    [DataContract]
    public class AcademicYear: BaseEntity
	{
		[MaxLength(10)]
		[Required]
        [DataMember]
        public string Title { get; set; }

        //-------------------------------------------------------------------------

        //-------------------------------------------------------------------------

        [ForeignKey("AcademicYearId")]
		public virtual List<AcademicPlan> AcademicPlans { get; set; }

        [ForeignKey("AcademicYearId")]
        public virtual List<TimeNorm> TimeNorms { get; set; }

        [ForeignKey("AcademicYearId")]
        public virtual List<SeasonDates> SeasonDates { get; set; }

        [ForeignKey("AcademicYearId")]
        public virtual List<StreamLesson> StreamLessons { get; set; }

        [ForeignKey("AcademicYearId")]
        public virtual List<IndividualPlanRecord> IndividualPlanRecords { get; set; }
    }
}
