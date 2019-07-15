using Models.AcademicYearData;
using Models.Base;
using Models.Web;
using System;
using System.Linq;
using WebInterfaces.ViewModels;

namespace WebImplementations
{
    public static class WebModelFactoryToViewModel
    {
        public static WebEducationDirectionViewModel CreateWebEducationDirectionViewModel(IGrouping<EducationDirection, Contingent> entity)
        {
            return new WebEducationDirectionViewModel
            {
                Id = entity.Key.Id,
                Cipher = entity.Key.Cipher,
                Title = entity.Key.Title,
                ShortName = entity.Key.ShortName,
                Qualification = entity.Key.Qualification.ToString(),
                Description = entity.Key.Description,
                Courses = entity.Select(x => new Tuple<Guid, string>(x.Id, $"Курс {Math.Log((double)x.Course, 2) + 1}")).OrderBy(x => x.Item2).ToList()
            };
        }

        public static WebLecturerViewModel CreateWebLecturerViewModel(Lecturer entity)
        {
            return new WebLecturerViewModel
            {
                Id = entity.Id,
                LecturerPostId = entity.LecturerPostId,
                LastName = entity.LastName,
                FirstName = entity.FirstName,
                Patronymic = entity.Patronymic,
                Post = entity.Post.ToString(),
                LecturerPost = entity.LecturerPost?.ToString() ?? string.Empty,
                Rank = entity.Rank.ToString(),
                Rank2 = entity.Rank2.ToString(),
                Email = entity.Email,
                Description = entity.Description,
                Photo = entity.Photo
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