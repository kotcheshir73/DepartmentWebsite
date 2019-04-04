using Models.AcademicYearData;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Base
{
    /// <summary>
    /// Класс, описыывающий блоки дисциплин, на которые разбиваются все дисциплины в расчете штатов
    /// </summary>
    [DataContract]
    public class DisciplineBlock : BaseEntity
    {
        [Required]
        [DataMember]
        public string Title { get; set; }

        [MaxLength(20)]
        [DataMember]
        public string DisciplineBlockBlueAsteriskName { get; set; }

        [DataMember]
        public bool DisciplineBlockUseForGrouping { get; set; }

        [DataMember]
        public int DisciplineBlockOrder { get; set; }

        [NotMapped]
        public string DisciplineBlockBlueAsteriskCode { get; set; }

        //-------------------------------------------------------------------------

        //-------------------------------------------------------------------------

        [ForeignKey("DisciplineBlockId")]
		public virtual List<Discipline> Disciplines { get; set; }

        [ForeignKey("DisciplineBlockId")]
        public virtual List<TimeNorm> TimeNorms { get; set; }
    }
}
