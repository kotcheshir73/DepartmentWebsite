using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentService.ViewModels.StandartViewModels.LearningProgress
{
    public class DisciplineLessonRecordPageViewModel : PageViewModel<DisciplineLessonRecordViewModel> { }

    public class DisciplineLessonRecordViewModel
    {
        public Guid Id { get; set; }

        public Guid DisciplineLessonId { get; set; }

        public DateTime Date { get; set; }

        public string Subgroup { get; set; }
    }
}
