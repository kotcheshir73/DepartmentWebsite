﻿using System;
using System.Collections.Generic;
using System.Text;
using Tools.ViewModels;

namespace WebInterfaces.ViewModels
{
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
            LecturerName = "";
            
        }
        public string Name { get; set; }

        public string LecturerName { get; set; }



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
}
