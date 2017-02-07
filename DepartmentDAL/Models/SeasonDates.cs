using System;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, описывающий даты для учебного полугодья
    /// </summary>
    public class SeasonDates
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public DateTime DateBeginSemester { get; set; }

        public DateTime DateEndSemester { get; set; }

        public DateTime DateBeginOffset { get; set; }

        public DateTime DateEndOffset { get; set; }

        public DateTime DateBeginExamination { get; set; }

        public DateTime DateEndExamination { get; set; }

        public DateTime? DateBeginPractice { get; set; }

        public DateTime? DateEndPractice { get; set; }
    }
}
