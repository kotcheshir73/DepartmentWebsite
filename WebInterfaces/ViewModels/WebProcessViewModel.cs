using System;
using System.Collections.Generic;

namespace WebInterfaces.ViewModels
{
    /// <summary>
    /// ++
    /// </summary>
    public class WebLoginViewModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public List<string> UserRoles { get; set; }
    }

    /// <summary>
    /// ++
    /// </summary>
    public class WebProcessDisciplineListInfoViewModel
    {
        public string EducationDirectionName { get; set; }

        public string Course { get; set; }

        public List<WebProcessDisciplineByCoursesViewModel> Discipline = new List<WebProcessDisciplineByCoursesViewModel>();
    }

    /// <summary>
    /// ++
    /// </summary>
    public class WebProcessFolderNamesForDiscipline
    {
        public string Semester { get; set; }

        public List<string> FolderNames { get; set; }
    }

    /// <summary>
    /// ++
    /// </summary>
    public class WebProcessDisciplineContentInfo
    {
        public string DisciplineName { get; set; }

        public List<Guid> Lecturers { get; set; }
    }

    public class WebProcessEventWithCommentViewModel
    {
        public Guid EventId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string DepartmentUser { get; set; }

        public DateTime Date { get; set; }

        public string Tag { get; set; }

        public List<WebProcessLevelCommentViewModel> commentList = new List<WebProcessLevelCommentViewModel>();
    }

    public class WebProcessLevelCommentViewModel
    {
        public string Content { get; set; }

        public string DepartmentUser { get; set; }

        public DateTime Date { get; set; }

        public List<WebProcessLevelCommentViewModel> commentList = new List<WebProcessLevelCommentViewModel>();

        public Guid Id { get; set; }
    }

    public class WebProcessDisciplineForDownloadViewModel
    {
        public WebProcessDisciplineForDownloadViewModel()
        {
            Semestrs = new List<WebProcessSemestrForDownloadViewModel>();
            Comments = new List<WebProcessLevelCommentViewModel>();
            LecturerName = "";            
        }
        public string Name { get; set; }

        public string LecturerName { get; set; }

        public string Description { get; set; }

        public Guid DisciplineId { get; set; }

        public List<WebProcessLevelCommentViewModel> Comments { get; set; }

        public List<WebProcessSemestrForDownloadViewModel> Semestrs { get; set; }
    }

    public class WebProcessSemestrForDownloadViewModel
    {
        public WebProcessSemestrForDownloadViewModel()
        {
            TimeNorms = new List<WebProcessTimeNormForDownloadViewModel>();

        }
        public string Name { get; set; }

        public List<WebProcessTimeNormForDownloadViewModel> TimeNorms { get; set; }
    }

    public class WebProcessTimeNormForDownloadViewModel
    {
        public WebProcessTimeNormForDownloadViewModel()
        {
            Files = new List<WebProcessFileForDownloadViewModel>();

        }
        public string Name { get; set; }

        public List<WebProcessFileForDownloadViewModel> Files { get; set; }
    }

    public class WebProcessFileForDownloadViewModel
    {
        public string Name { get; set; }

        public string Path { get; set; }
    }



    public class WebProcessDisciplineByCoursesViewModel
    {
        public Guid DisciplineId { get; set; }

        public string DisciplineName { get; set; }

        public string TimeNormName { get; set; }

        public int Semester { get; set; }
    }

    public class WebProcessDisciplineContext
    {
        public string Name { get; set; }

        public List<WebProcessDisciplineTimeNormContext> TimeNormContents { get; set; }
    }

    public class WebProcessDisciplineTimeNormContext
    {
        public string Name { get; set; }

        public List<WebProcessDisciplineFileContext> FileContents { get; set; }
    }

    public class WebProcessDisciplineFileContext
    {
        public string Name { get; set; }

        public string Path { get; set; }
    }
}