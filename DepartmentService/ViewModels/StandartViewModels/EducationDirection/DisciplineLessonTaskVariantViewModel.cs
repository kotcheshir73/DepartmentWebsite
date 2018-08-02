using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentService.ViewModels.StandartViewModels.EducationDirection
{
    public class DisciplineLessonTaskVariantPageViewModel : PageViewModel<DisciplineLessonTaskVariantViewModel> { }

    public class DisciplineLessonTaskVariantViewModel
    {
        public Guid Id { get; set; }

        public Guid DisciplineLessonTaskId { get; set; }

        public string VariantNumber { get; set; }

        public string VariantTask { get; set; }
    }
}
