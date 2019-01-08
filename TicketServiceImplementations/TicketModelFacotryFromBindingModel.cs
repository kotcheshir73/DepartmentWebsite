using TicketModels.Models;
using TicketServiceInterfaces.BindingModels;

namespace TicketServiceImplementations
{
    public static class TicketModelFacotryFromBindingModel
    {
        public static ExaminationTemplate CreateExaminationTemplate(ExaminationTemplateSetBindingModel model, ExaminationTemplate entity = null)
        {
            if (entity == null)
            {
                entity = new ExaminationTemplate();
            }
            entity.DisciplineId = model.DisciplineId;
            entity.EducationDirectionId = model.EducationDirectionId;
            entity.Semester = model.Semester;
            entity.TicketTemplateId = model.TicketTemplateId;

            return entity;
        }

        public static ExaminationTemplateBlock CreateExaminationTemplateBlock(ExaminationTemplateBlockSetBindingModel model, ExaminationTemplateBlock entity = null)
        {
            if (entity == null)
            {
                entity = new ExaminationTemplateBlock();
            }
            entity.ExaminationTemplateId = model.ExaminationTemplateId;
            entity.BlockName = model.BlockName;
            entity.CountQuestionInTicket = model.CountQuestionInTicket;

            return entity;
        }

        public static ExaminationTemplateBlockQuestion CreateExaminationTemplateBlockQuestion(ExaminationTemplateBlockQuestionSetBindingModel model, ExaminationTemplateBlockQuestion entity = null)
        {
            if (entity == null)
            {
                entity = new ExaminationTemplateBlockQuestion();
            }
            entity.ExaminationTemplateBlockId = model.ExaminationTemplateBlockId;
            entity.QuestionNumber = model.QuestionNumber;
            entity.QuestionText = model.QuestionText;
            entity.QuestionImage = model.QuestionImage;

            return entity;
        }

        public static ExaminationTemplateTicket CreateExaminationTemplateTicket(ExaminationTemplateTicketSetBindingModel model, ExaminationTemplateTicket entity = null)
        {
            if (entity == null)
            {
                entity = new ExaminationTemplateTicket();
            }
            entity.ExaminationTemplateId = model.ExaminationTemplateId;
            entity.TicketNumber = model.TicketNumber;

            return entity;
        }

        public static ExaminationTemplateTicketQuestion CreateExaminationTemplateTicketQuestion(ExaminationTemplateTicketQuestionSetBindingModel model, ExaminationTemplateTicketQuestion entity = null)
        {
            if (entity == null)
            {
                entity = new ExaminationTemplateTicketQuestion();
            }
            entity.ExaminationTemplateBlockQuestionId = model.ExaminationTemplateBlockQuestionId;
            entity.ExaminationTemplateTicketId = model.ExaminationTemplateTicketId;
            entity.Order = model.Order;

            return entity;
        }
    }
}