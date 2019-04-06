using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.LecturerData
{
    /// <summary>
    /// Класс, описывающий заголовки индивидуального плана
    /// </summary>
    [DataContract]
    public class IndividualPlanTitle : BaseEntity
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public int Order { get; set; }

        //-------------------------------------------------------------------------

        //-------------------------------------------------------------------------

        [ForeignKey("IndividualPlanTitleId")]
		public virtual List<IndividualPlanKindOfWork> IndividualPlanKindOfWorks { get; set; }
	}
}