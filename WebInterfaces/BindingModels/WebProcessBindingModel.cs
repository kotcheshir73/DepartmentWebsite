using System;

namespace WebInterfaces.BindingModels
{
    public class WebProcessDisciplineListInfoBindingModel
    {
        public Guid CourseId { get; set; }
    }

    public class WebProcessFolderLoadSetBindingModel
    {
        public string DisciplineName { get; set; }

        public string Semestr { get; set; }

        public string TimeNorm { get; set; }

        public string DisciplineDescription { get; set; }

        public string LecturerName { get; set; }

    }

    public class WebProcessDisciplineForDownloadGetBindingModel
    {
        public string DisciplineName { get; set; }
    }
}