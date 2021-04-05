using Enums;
using Models.AcademicYearData;
using Models.Web;
using System;
using System.Linq;
using WebInterfaces.ViewModels;

namespace WebImplementations
{
    public static class WebModelFactoryToViewModel
    {
        public static NewsViewModel CreateNewsViewModel(News entity)
        {
            return new NewsViewModel
            {
                Id = entity.Id,
                Title = entity.Title,
                DepartmentUserId = entity.DepartmentUserId,
                DepartmentUser = entity.DepartmentUser.UserName,
                Body = entity.Body,
                Date = entity.DateCreate,
                Tag = entity.Tag
            };
        }

        public static CommentViewModel CreateCommentViewModel(Comment entity)
        {
            return new CommentViewModel
            {
                Id = entity.Id,
                DepartmentUserId = entity.DepartmentUserId,
                DepartmentUser = entity.DepartmentUser.UserName,
                DisciplineId = entity.DisciplineId,
                NewsId = entity.NewsId,
                ParentId = entity.ParentId,
                Content = entity.Content,
                Date = entity.DateCreate,
                CountChilds = entity.Childs?.Where(x => !x.IsDeleted).Count() ?? 0
            };
        }
    }
}