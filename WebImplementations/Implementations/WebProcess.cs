using BaseInterfaces.Interfaces;
using Enums;
using Microsoft.EntityFrameworkCore;
using Models.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Tools;
using WebImplementations.Helpers;
using WebInterfaces.BindingModels;
using WebInterfaces.Interfaces;
using WebInterfaces.ViewModels;

namespace WebImplementations.Implementations
{
    public class WebProcess : IWebProcess
    {
        private readonly IDisciplineService _serviceD;

        //определить путь для папок
        private string Path => @"D:\Department\";

        public WebProcess(/*IDisciplineService serviceD*/)
        {            
          //  _serviceD = serviceD;
        }

        public ResultService<List<WebProcessDisciplineByCoursesViewModel>> GetDisciplinesByCourses(WebProcessDisciplineListInfoBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.AcademicPlanRecordElements.Where(x => !x.IsDeleted)
                        .Include(x => x.TimeNorm)
                        .Include(x => x.AcademicPlanRecord.Discipline.DisciplineBlock)
                        .Include(x => x.AcademicPlanRecord.Contingent.EducationDirection)
                        // TODO практики и госы
                        .Where(x => x.TimeNorm.TimeNormShortName == "Экз" || x.TimeNorm.TimeNormShortName == "ЗсО" || x.TimeNorm.TimeNormShortName == "Зач"
                                    || x.TimeNorm.TimeNormShortName == "КР" || x.TimeNorm.TimeNormShortName == "КП")
                        .Where(x => x.TimeNorm.AcademicYearId == ServiceHelper.GetCurrentAcademicYear().Result.Id && x.AcademicPlanRecord.ContingentId == model.CourseId);


                    var result = query.Select(x => new WebProcessDisciplineByCoursesViewModel
                    {
                        DisciplineId = x.AcademicPlanRecord.DisciplineId,
                        DisciplineName = x.AcademicPlanRecord.Discipline.DisciplineName,
                        Semester = (int)x.AcademicPlanRecord.Semester.Value,
                        TimeNormName = x.TimeNorm.KindOfLoadName
                    }).Distinct()
                       .OrderBy(x => x.Semester).ThenBy(x => x.DisciplineName)
                       .ToList();

                    return ResultService<List<WebProcessDisciplineByCoursesViewModel>>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<List<WebProcessDisciplineByCoursesViewModel>>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public void CreateFolderDis(List<WebProcessFolderLoadSetBindingModel> model)
        {            
            string tmp = "";
            var dis = model.FirstOrDefault().DisciplineName;
            DirectoryInfo dirInfo;

            foreach (var sem in model.Select(x => new { Semestr = x.Semestr  }).GroupBy(x => x.Semestr))
            {
                foreach (var tn in model.Select(x => new { TimeNorm = x.TimeNorm }).GroupBy(x => x.TimeNorm))
                {
                    tmp = tn.Key;
                    if (tn.Key.Contains("Руководство и прием курсовых"))
                    {
                        tmp="Курсовая";
                    }
                    else if (tn.Key.Contains("Практическое занятие"))
                    {
                        tmp = "Практики";
                    }
                    else if (tn.Key.Contains("Лабораторное занятие"))
                    {
                        tmp = "Лабораторные";
                    }
                    else if (tn.Key.Contains("Лекция"))
                    {
                        tmp = "Лекции";
                    }
                    dirInfo = new DirectoryInfo(Path + dis + @"\" + sem.Key + @"\" + tmp);
                    if (!dirInfo.Exists)
                    {
                        dirInfo.Create();
                    }
                }
                dirInfo = new DirectoryInfo(Path + dis + @"\" + sem.Key + @"\Дополнительно");
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }
            }
        }

        public ResultService<WebProcessDisciplineForDownloadViewModel> GetDisciplineForDownload(WebProcessDisciplineForDownloadGetBindingModel model)
        {
            try
            {
                DirectoryInfo discipline = new DirectoryInfo(Path + model.DisciplineName);
                if(discipline == null)
                {
                    return ResultService<WebProcessDisciplineForDownloadViewModel>.Error(new Exception("Папки не иницализированы"), ResultServiceStatusCode.Error);
                }
                var dis = _serviceD.GetDiscipline(new BaseInterfaces.BindingModels.DisciplineGetBindingModel
                {
                    DisciplineName=model.DisciplineName
                }).Result;
                WebProcessDisciplineForDownloadViewModel disciplineForDownload
                = new WebProcessDisciplineForDownloadViewModel() {
                    DisciplineId = dis.Id,
                    Name = model.DisciplineName,
                    Description = dis.DisciplineDescription,
                    Comments= GetListLevelComment(new CommentGetBindingModel { DisciplineId = dis.Id, ParentId = null }).Result
                };

                

                foreach (var semestr in discipline.GetDirectories())
                {
                    var semestrForDownload = new WebProcessSemestrForDownloadViewModel() { Name = semestr.Name };
                    foreach (var timenorm in semestr.GetDirectories())
                    {
                        var timenormForDownload = new WebProcessTimeNormForDownloadViewModel() { Name = timenorm.Name };
                        foreach (var file in timenorm.GetFiles())
                        {
                            timenormForDownload.Files.Add(new WebProcessFileForDownloadViewModel()
                            {
                                Name = file.Name,
                                Path = $@"{discipline.Name}\{semestr.Name}\{timenorm.Name}\{file.Name}"
                            });
                        }
                        semestrForDownload.TimeNorms.Add(timenormForDownload);
                    }
                    disciplineForDownload.Semestrs.Add(semestrForDownload);
                }
                return ResultService<WebProcessDisciplineForDownloadViewModel>.Success(disciplineForDownload);
            }
            catch (Exception ex)
            {
                return ResultService<WebProcessDisciplineForDownloadViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<WebProcessEventWithCommentViewModel> GetEventWithComment(EventGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Events.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<WebProcessEventWithCommentViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<WebProcessEventWithCommentViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    WebProcessEventWithCommentViewModel viewModel = new WebProcessEventWithCommentViewModel
                    {
                        EventId = entity.Id,
                        Content = entity.Content,
                        Date = entity.DateCreate,
                        DepartmentUser = entity.DepartmentUser,
                        Tag = entity.Tag,
                        Title = entity.Title,
                        commentList = GetListLevelComment(new CommentGetBindingModel { EventId = model.Id, ParentId = null}).Result
                    };

                    return ResultService<WebProcessEventWithCommentViewModel>.Success(viewModel);
                }
            }
            catch (Exception ex)
            {
                return ResultService<WebProcessEventWithCommentViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<List<WebProcessLevelCommentViewModel>> GetListLevelComment(CommentGetBindingModel model)
        {
            try
            {
                //DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.Comments.Where(x => !x.IsDeleted).AsQueryable();

                    if (model.EventId.HasValue)
                    {
                        query = query.Where(x => x.EventId == model.EventId.Value);
                    }
                    if (model.DisciplineId.HasValue)
                    {
                        query = query.Where(x => x.DisciplineId == model.DisciplineId.Value);
                    }
                    if (model.ParentId.HasValue)
                    {
                        query = query.Where(x => x.ParentId == model.ParentId.Value);
                    }
                    else {
                        query = query.Where(x => x.ParentId == null);
                    }

                    query = query.OrderBy(x => x.DateCreate);

                    var result = query.Select(CreateWebProcessLevelCommentViewModel).ToList();

                    return ResultService<List<WebProcessLevelCommentViewModel>>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService< List<WebProcessLevelCommentViewModel>>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        private WebProcessLevelCommentViewModel CreateWebProcessLevelCommentViewModel(Comment entity)
        {
            return new WebProcessLevelCommentViewModel
            {
                Id = entity.Id,
                Content = entity.Content,
                Date = entity.DateCreate,
                DepartmentUser = entity.DepartmentUser,
                commentList = GetListLevelComment(new CommentGetBindingModel { EventId = entity.EventId, DisciplineId = entity.DisciplineId, ParentId = entity.Id }).Result
            };
        }
    }
}
