using DepartmentModel.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Класс, хранящий информацию по нормам времени
    /// </summary>
    [DataContract]
    public class TimeNorm : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid AcademicYearId { get; set; }

        [Required]
        [DataMember]
        public Guid DisciplineBlockId { get; set; }

        [MaxLength(50)]
		[Required]
        [DataMember]
        public string TimeNormName { get; set; }

        [MaxLength(5)]
        [Required]
        [DataMember]
        public string TimeNormShortName { get; set; }

        [Required]
        [DataMember]
        public int TimeNormOrder { get; set; }

        [DataMember]
        public AcademicLevel? TimeNormAcademicLevel { get; set; }

        /// <summary>
        /// Тип нагрузки, к которой относится норма времени
        /// </summary>
        [MaxLength(100)]
        [Required]
        [DataMember]
        public string KindOfLoadName { get; set; }

        /// <summary>
        /// Атрибут для поиска нагрузки в старой версии планов
        /// </summary>
        [MaxLength(10)]
        [DataMember]
        public string KindOfLoadAttributeName { get; set; }

        /// <summary>
        /// Название нагрузки в справочнике видов работ в новой версии планов
        /// </summary>
        [MaxLength(100)]
        [DataMember]
        public string KindOfLoadBlueAsteriskName { get; set; }

        /// <summary>
        /// Название атрибута по которму извлекать часы для расчетов из строк Плана
        /// </summary>
        [MaxLength(100)]
        [DataMember]
        public string KindOfLoadBlueAsteriskAttributeName { get; set; }

        /// <summary>
        /// Название практики для извлечения из вида практик (указывать только для нагрузок, связанных с практикой)
        /// </summary>
        [MaxLength(100)]
        [DataMember]
        public string KindOfLoadBlueAsteriskPracticName { get; set; }

        /// <summary>
        /// Код вида работ в справочнике видов работ в новой версии планов, чтобы потом искать работу в строках плана
        /// </summary>
        [NotMapped]
        public string KindOfLoadBlueAsteriskCode { get; set; }

        /// <summary>
        /// Код вида практики в справочнике видов практик в новой версии планов, чтобы потом искать практику в строках плана
        /// </summary>
        [NotMapped]
        public string KindOfLoadBlueAsteriskPracticCode { get; set; }

        /// <summary>
        /// Значения множителя 1 для расчетов (количество объектов)
        /// </summary>
        [Required]
        [DataMember]
        public KindOfLoadType KindOfLoadType { get; set; }

        /// <summary>
        /// Значения множителя 2 для расчетов (если не заполнено, то часы берутся из УП) (количество часов)
        /// </summary>
        [DataMember]
        public decimal? Hours { get; set; }
        
        /// <summary>
        /// Множитель 3 может в себе хранить число
        /// </summary>
        [DataMember]
        public decimal? NumKoef { get; set; }

        /// <summary>
        /// Или коэффициент, по которому надо расчитывать (может совсем не указываться)
        /// </summary>
        [Required]
        [DataMember]
        public TimeNormKoef TimeNormKoef { get; set; }

        //-------------------------------------------------------------------------

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual DisciplineBlock DisciplineBlock { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("TimeNormId")]
        public virtual List<AcademicPlanRecordElement> AcademicPlanRecordElements { get; set; }

        [ForeignKey("TimeNormId")]
        public virtual List<GraficRecord> GraficRecords { get; set; }

        [ForeignKey("TimeNormId")]
        public virtual List<GraficClassroom> GraficClassroomss { get; set; }
    }
}
