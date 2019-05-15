using Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Tools;
using WebInterfaces.BindingModels;
using WebInterfaces.Interfaces;
using WebInterfaces.ViewModels;

namespace WebImplementations.Implementations
{
    public class WebProcessService : IWebProcessService
    {
        public void CreateFolderDis(List<WebProcessFolderLoadSetBindingModel> model)
        {
            //определить путь для папок
            string path = @"D:\Department\";
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
                    dirInfo = new DirectoryInfo(path + dis + @"\" + sem.Key + @"\" + tmp);
                    if (!dirInfo.Exists)
                    {
                        dirInfo.Create();
                    }
                }
                dirInfo = new DirectoryInfo(path + dis + @"\" + sem.Key + @"\Дополнительно");
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }
            }

        }

        public ResultService<WebProcessLevelCommentPageViewModel> GetListLevelComment(CommentGetBindingModel model)
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

                    query = query.Include(x => x.DepartmentUser);

                    var result = new WebProcessLevelCommentPageViewModel
                    {
                        List = query.Select(ModelFactoryToViewModel.CreateWebProcessLevelCommentViewModel).ToList()
                    };

                    return ResultService<WebProcessLevelCommentPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<WebProcessLevelCommentPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }


    }
}
