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

        public static WebDisciplineViewModel CreateWebDisciplineViewModel(Discipline entity)
        {
            return new WebDisciplineViewModel
            {
                Id = entity.Id,
                DisciplineName = entity.DisciplineName,
                DisciplineDescription = entity.DisciplineDescription
            };
        }

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