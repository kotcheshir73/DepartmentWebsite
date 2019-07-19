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

        public WebLoginViewModel Login(string login, string hash)
        {
            using (var context = DepartmentUserManager.GetContext)
            {
                var user = context.DepartmentUsers.FirstOrDefault(u => u.UserName == login && u.PasswordHash == hash);

                if (user == null)
                {
                    throw new Exception("Введен неверный логин/пароль");
                }
                if (user.IsLocked)
                {
                    throw new Exception("Пользователь заблокирован");
                }

                user.DateLastVisit = DateTime.Now;
                context.SaveChanges();

                var roles = context.DepartmentUserRoles.Where(x => x.UserId == user.Id).Select(x => x.Role.RoleName).ToList();

                return WebModelFactoryToViewModel.CreateLoginViewModel(user, roles);
            }
        }



        public ResultService<WebProcessEventWithCommentViewModel> GetEventWithComment(NewsGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Newses.Include(x => x.DepartmentUser).FirstOrDefault(x => x.Id == model.Id);
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
                        Content = entity.Body,
                        Date = entity.DateCreate,
                        DepartmentUser = entity.DepartmentUser.UserName,
                        Tag = entity.Tag,
                        Title = entity.Title,
                        commentList = GetListLevelComment(new CommentGetBindingModel { NewsId = model.Id, ParentId = null}).Result
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

                    if (model.NewsId.HasValue)
                    {
                        query = query.Where(x => x.NewsId == model.NewsId.Value);
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
                DepartmentUser = entity.DepartmentUser.UserName,
                commentList = GetListLevelComment(new CommentGetBindingModel { NewsId = entity.NewsId, DisciplineId = entity.DisciplineId, ParentId = entity.Id }).Result
            };
        }
    }
}
