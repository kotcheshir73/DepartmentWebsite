using WebInterfaces.ViewModels;
using Enums;
using Models.Web;
using System.Drawing;
using System.IO;
using System.Linq;
using System;
using Models.Base;

namespace WebImplementations
{
    public static class WebModelFactoryToViewModel
    {
        public static WebEducationDirectionViewModel CreateWebEducationDirectionViewModel(EducationDirection entity)
        {
            return new WebEducationDirectionViewModel
            {
                Id = entity.Id,
                Cipher = entity.Cipher,
                Title = entity.Title,
                ShortName = entity.ShortName,
                Description = entity.Description
            };
        }

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
                DepartmentUser = entity.DepartmentUser
            };
        }
        

    }
}