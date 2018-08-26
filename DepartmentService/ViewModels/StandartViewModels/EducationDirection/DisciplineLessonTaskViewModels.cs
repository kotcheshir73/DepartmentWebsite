using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentService.ViewModels.StandartViewModels.EducationDirection
{
    public class DisciplineLessonTaskPageViewModel : PageViewModel<DisciplineLessonTaskViewModel> { }

    public class DisciplineLessonTaskViewModel
    {
        public Guid Id { get; set; }

        public Guid DisciplineLessonId { get; set; }

        public int? VariantNumber { get; set; }

        public int Order { get; set; }

        public decimal? MaxBall { get; set; }

        public string DisciplineLessonName { get; set; }

        public string Description { get; set; }

        public byte?[] Image { get; set; }
    }
}
