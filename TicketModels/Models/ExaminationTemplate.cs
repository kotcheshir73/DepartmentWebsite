using DepartmentModel.Enums;
using DepartmentModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace TicketModels.Models
{
    /// <summary>
    /// Класс, описывающий экзамен
    /// </summary>
    [DataContract]
    public class ExaminationTemplate : BaseEntity
    {
        [DataMember]
        public Guid DisciplineId { get; set; }

        [DataMember]
        public Guid? EducationDirectionId { get; set; }

        [DataMember]
        public Semesters? Semester { get; set; }

        [DataMember]
        public Guid? TicketTemplateId { get; set; }

        //-------------------------------------------------------------------------

        public virtual Discipline Discipline { get; set; }

        public virtual EducationDirection EducationDirection { get; set; }

        public virtual TicketTemplate TicketTemplate { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("ExaminationTemplateId")]
        public virtual List<ExaminationTemplateBlock> ExaminationTemplateBlocks { get; set; }

        [ForeignKey("ExaminationTemplateId")]
        public virtual List<ExaminationTemplateTicket> ExaminationTemplateTickets { get; set; }
    }
}