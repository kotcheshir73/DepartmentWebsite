using WebInterfaces.ViewModels;
using Enums;
using Models.Web;
using System.Drawing;
using System.IO;
using System.Linq;
using System;

namespace WebImplementations
{
    public static class ModelFactoryToViewModel
    {
        public static EventViewModel CreateEventViewModel(Event entity)
        {
            return new EventViewModel
            {
                Id = entity.Id,
                Content = entity.Content,
                Date = entity.DateCreate,
                DepartmentUser = entity.DepartmentUser,
                Tag = entity.Tag,
                Title = entity.Title
            };
        }

        public static CommentViewModel CreateCommentViewModel(Comment entity)
        {
            return new CommentViewModel
            {
                Id = entity.Id,
                Content = entity.Content,
                Date = entity.DateCreate,
                DepartmentUser = entity.DepartmentUser               
            };
        }

        public static WebProcessLevelCommentViewModel CreateWebProcessLevelCommentViewModel(Comment entity)
        {
            return new WebProcessLevelCommentViewModel
            {
                Id = entity.Id,
                Content = entity.Content,
                Date = entity.DateCreate,
                DepartmentUser = entity.DepartmentUser,
                IsChild = entity.ChildComments.Count!=0
            };
        }
        

    }
}