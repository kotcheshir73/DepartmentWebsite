using Enums;
using WebInterfaces.BindingModels;
using Models.Web;
using System;

namespace WebImplementations
{
    public static class ModelFacotryFromBindingModel
    {        
        public static Event CreateEvent(EventSetBindingModel model, Event entity = null)
        {
            if (entity == null)
            {
                entity = new Event();
            }
            entity.Content = model.Content;
            entity.DepartmentUserId = model.DepartmentUserId;
            entity.Title = model.Title;
            entity.Tag = model.Tag;

            return entity;
        }

        public static Comment CreateComment(CommentSetBindingModel model, Comment entity = null)
        {
            if (entity == null)
            {
                entity = new Comment();
            }
            entity.Content = model.Content;
            entity.DepartmentUserId = model.DepartmentUserId;
            entity.DisciplineId = model.DisciplineId;
            entity.EventId = model.EventId;
            entity.ParentId = model.ParentId;

            return entity;
        }
    }
}