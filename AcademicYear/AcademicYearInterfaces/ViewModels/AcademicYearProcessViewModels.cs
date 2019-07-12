using System;
using System.Collections.Generic;
using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
    public class AcademicPlanRecordForDiciplinePageViewModel : PageSettingListViewModel<AcademicPlanRecordForDiciplineViewModel> { }

    public class AcademicPlanRecordForDiciplineViewModel
    {
        public Guid Id { get; set; }

        public Guid AcademicPlanId { get; set; }

        public Guid DisciplineId { get; set; }

        public string EducationDirectionShortName { get; set; }

        public string Semesters { get; set; }

        public string Disciplne { get; set; }

        public string Semester { get; set; }

        public int Zet { get; set; }
    }

    public class LecturerDisciplineTimeDistribution
    {
        public Guid DisciplineTimeDistributionId { get; set; }

        public string EducationDirection { get; set; }

        public string StudentGroup { get; set; }

        public string Discipline { get; set; }

        public string Semestr { get; set; }

        public string AcademicYear { get; set; }

        public string Comment { get; set; }

        public string CommentWishesOfTeacher { get; set; }

        public List<LecturerDisciplineTimeDistributionElement> LecturerDisciplineTimeDistributionElements { get; set; }
    }

    public class LecturerDisciplineTimeDistributionElement
    {
        public string TimeNorm { get; set; }

        public decimal TotalSum { get; set; }

        public Guid? DisciplineTimeDistributionRecordFirstWeekFirstHalfId { get; set; }

        public Guid? DisciplineTimeDistributionRecordSecondWeekFirstHalfId { get; set; }

        public Guid? DisciplineTimeDistributionRecordFirstWeekSecondHalfId { get; set; }

        public Guid? DisciplineTimeDistributionRecordSecondWeekSecondHalfId { get; set; }

        public Guid? DisciplineTimeDistributionClassroomId { get; set; }

        public double? DisciplineTimeDistributionRecordFirstWeekFirstHalf { get; set; }

        public double? DisciplineTimeDistributionRecordSecondWeekFirstHalf { get; set; }

        public double? DisciplineTimeDistributionRecordFirstWeekSecondHalf { get; set; }

        public double? DisciplineTimeDistributionRecordSecondWeekSecondHalf { get; set; }

        public string DisciplineTimeDistributionClassroom { get; set; }
    }
}