using Enums;
using Models.AcademicYearData;
using Models.Web;
using System;
using WebInterfaces.BindingModels;

namespace WebImplementations
{
    public static class WebModelFacotryFromBindingModel
    {
        public static News CreateNews(NewsSetBindingModel model, News entity = null)
        {
            if (entity == null)
            {
                entity = new News();

            }
            entity.DepartmentUserId = model.DepartmentUserId;
            entity.Title = model.Title;
            entity.Body = model.Body;
            entity.Tag = model.Tag;

            return entity;
        }

        public static Comment CreateComment(CommentSetBindingModel model, Comment entity = null)
        {
            if (entity == null)
            {
                entity = new Comment
                {
                    DepartmentUserId = model.DepartmentUserId.Value,
                    DisciplineId = model.DisciplineId,
                    NewsId = model.NewsId,
                    ParentId = model.ParentId
                };
            }

            entity.Content = model.Content;

            return entity;
        }
    }
}