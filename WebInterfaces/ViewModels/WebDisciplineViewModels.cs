using System;
using System.Collections.Generic;
using Tools.ViewModels;

namespace WebInterfaces.ViewModels
{
    public class WebDisciplinePageViewModel : PageSettingListViewModel<WebDisciplineViewModel>
    {
        public string EducationDirectionName { get; set; }

        public string Course { get; set; }
    }

    public class WebDisciplineViewModel : PageSettingElementViewModel
    {
        public string DisciplineName { get; set; }

        public string DisciplineDescription { get; set; }

        public List<Tuple<Guid, string>> DisciplineLecturers { get; set; }
    }
}