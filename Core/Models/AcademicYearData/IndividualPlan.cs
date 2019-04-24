using Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace Models.AcademicYearData
{
    /// <summary>
    /// Класс, описывающий годовой индивидуальный план
    /// </summary>
    [DataContract]
    public class IndividualPlan : BaseEntity
    {
        [DataMember]
        public Guid LecturerId { get; set; }

        [DataMember]
        public Guid AcademicYearId { get; set; }

        //-------------------------------------------------------------------------

        public virtual Lecturer Lecturer { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("IndividualPlanId")]
        public virtual List<IndividualPlanRecord> IndividualPlanRecords { get; set; }

        [ForeignKey("IndividualPlanId")]
        public virtual List<IndividualPlanNIRContractualWork> IndividualPlanNIRContractualWorks { get; set; }

        [ForeignKey("IndividualPlanId")]
        public virtual List<IndividualPlanNIRScientificArticle> IndividualPlanNIRScientificArticles { get; set; }

        //-------------------------------------------------------------------------

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            if (AcademicYear != null)
            {
                result.Append(AcademicYear.ToString());
                result.Append("-");
            }
            if (Lecturer != null)
            {
                result.Append(Lecturer.ToString());
            }
            return result.ToString();
        }
    }
}