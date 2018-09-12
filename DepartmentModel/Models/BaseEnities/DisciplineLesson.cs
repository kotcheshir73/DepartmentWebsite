using DepartmentModel.Enums;
using DepartmentModel.Models.BaseEnities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    ///  Класс, описывающий занятие по дисциплине
    ///  Это может быть лекция, практическое занятие или лабораторная работа
    /// </summary>
    [DataContract]
    public class DisciplineLesson : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid AcademicYearId { get; set; }

        [Required]
        [DataMember]
        public Guid EducationDirectionId { get; set; }

        [Required]
        [DataMember]
        public Guid DisciplineId { get; set; }

        [Required]
        [DataMember]
        public Guid TimeNormId { get; set; }

        [Required]
        [DataMember]
        public Semesters Semester { get; set; }
        
		[MaxLength(100)]
		[Required]
        [DataMember]
        public string Title { get; set; }
        
        [DataMember]
        public string Description { get; set; }

        [Required]
        [DataMember]
        public int Order { get; set; }

        [Required]
        [DataMember]
        public int CountOfPairs { get; set; }

        [DataMember]
        public DateTime? Date { get; set; }

        [DataMember]
        public byte[] DisciplineLessonFile { get; set; }

        //-------------------------------------------------------------------------

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual EducationDirection EducationDirection { get; set; }

        public virtual Discipline Discipline { get; set; }

        public virtual TimeNorm TimeNorm { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("DisciplineLessonId")]
		public virtual List<DisciplineLessonTask> DisciplineLessonTasks { get; set; }

		[ForeignKey("DisciplineLessonId")]
		public virtual List<DisciplineLessonRecord> DisciplineLessonRecords { get; set; }
	}
}
