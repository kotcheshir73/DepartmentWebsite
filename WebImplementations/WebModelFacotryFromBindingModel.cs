using Models.Web;
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
                entity = new Comment();
            }

            entity.DepartmentUserId = model.DepartmentUserId;
            entity.DisciplineId = model.DisciplineId;
            entity.NewsId = model.NewsId;
            entity.ParentId = model.ParentId;
            entity.Content = model.Content;

            return entity;
        }
    }
}