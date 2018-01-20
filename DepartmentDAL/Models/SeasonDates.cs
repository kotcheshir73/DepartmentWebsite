using System;
using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, описывающий даты для учебного полугодья
    /// </summary>
    [DataContract]
    public class SeasonDates
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public DateTime DateBeginSemester { get; set; }

        [DataMember]
        public DateTime DateEndSemester { get; set; }

        [DataMember]
        public DateTime DateBeginOffset { get; set; }

        [DataMember]
        public DateTime DateEndOffset { get; set; }

        [DataMember]
        public DateTime DateBeginExamination { get; set; }

        [DataMember]
        public DateTime DateEndExamination { get; set; }

        [DataMember]
        public DateTime? DateBeginPractice { get; set; }

        [DataMember]
        public DateTime? DateEndPractice { get; set; }
    }
}
