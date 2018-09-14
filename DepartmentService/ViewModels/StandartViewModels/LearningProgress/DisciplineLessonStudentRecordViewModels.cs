using DepartmentModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentService.ViewModels.StandartViewModels.LearningProgress
{
    public class DisciplineLessonStudentRecordPageViewModel : PageViewModel<DisciplineLessonStudentRecordViewModel> { }

    public class DisciplineLessonStudentRecordViewModel
    {
        public Guid Id { get; set; }

        public Guid DisciplineLessonRecordId { get; set; }

        public Guid StudentId { get; set; }

        public DisciplineLessonStudentStatus Status { get; set; }

        public string Comment { get; set; }

        public int? Ball { get; set; }
    }
}
