using Enums;
using ExaminationInterfaces.BindingModels;
using Models.Examination;
using System;
using System.Linq;

namespace ExaminationImplementations
{
    public static class ExaminationModelFacotryFromBindingModel
    {
        public static Statement CreateStatement(StatementSetBindingModel model, Statement entity = null)
        {
            if (entity == null)
            {
                entity = new Statement();
            }
            entity.LecturerId = model.LecturerId;
            entity.DisciplineId = model.DisciplineId;
            entity.StudentGroupId = model.StudentGroupId;
            entity.AcademicYearId = model.AcademicYearId;
            entity.Semester = (Semesters)Enum.Parse(typeof(Semesters), model.Semester);
            entity.TypeOfTest = (TypeOfTest)Enum.Parse(typeof(TypeOfTest), model.TypeOfTest.Replace(' ', '_'));
            entity.DateCreate = model.Date;
            entity.IsMain = model.IsMain;
            return entity;
        }

        public static StatementRecord CreateStatementRecord(StatementRecordSetBindingModel model, StatementRecord entity = null)
        {
            if (entity == null)
            {
                entity = new StatementRecord();
            }
            entity.StatementId = model.StatementId;
            entity.StudentId = model.StudentId;
            entity.Description = model.Description;
            entity.Score = model.Score;
            return entity;
        }

        public static ExaminationList CreateExaminationList(ExaminationListSetBindingModel model, ExaminationList entity = null)
        {
            if (entity == null)
            {
                entity = new ExaminationList();
            }
            entity.LecturerId = model.LecturerId;
            entity.DisciplineId = model.DisciplineId;
            entity.StudentGroupId = model.StudentGroupId;
            entity.StudentId = model.StudentId;
            entity.AcademicYearId = model.AcademicYearId;
            entity.Semester = (Semesters)Enum.Parse(typeof(Semesters), model.Semester);
            entity.TypeOfTest = (TypeOfTest)Enum.Parse(typeof(TypeOfTest), model.TypeOfTest.Replace(' ', '_'));
            entity.DateCreate = model.Date;
            entity.Number = model.Number;
            entity.Score = model.Score;
            return entity;
        }

        public static ExaminationTemplate CreateExaminationTemplate(ExaminationTemplateSetBindingModel model, ExaminationTemplate entity = null)
        {
            if (entity == null)
            {
                entity = new ExaminationTemplate();
            }
            entity.DisciplineId = model.DisciplineId;
            entity.EducationDirectionId = model.EducationDirectionId;
            entity.Semester = model.Semester;
            entity.ExaminationTemplateName = model.ExaminationTemplateName;

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
            entity.QuestionTagInTemplate = model.QuestionTagInTemplate;
            entity.CountQuestionInTicket = model.CountQuestionInTicket;
            entity.IsCombine = model.IsCombine;
            entity.CombineBlocks = model.CombineBlocks;

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
            entity.ExaminationTemplateBlockId = model.ExaminationTemplateBlockId;
            entity.Order = model.Order;

            return entity;
        }

        public static TicketTemplate CreateTicketTemplate(TicketTemplateSetBindingModel model, TicketTemplate entity = null)
        {
            if (entity == null)
            {
                entity = new TicketTemplate();
            }
            entity.ExaminationTemplateId = model.ExaminationTemplateId;
            entity.TicketTemplateBodyId = model.TicketTemplateBodyId;
            entity.TemplateName = model.TemplateName;
            if(model.TicketTemplateBodySetBindingModel != null)
            {
                entity.TicketTemplateBody = CreateTicketTemplateBody(model.TicketTemplateBodySetBindingModel, entity.TicketTemplateBody);
            }

            return entity;
        }

        public static TicketTemplateBody CreateTicketTemplateBody(TicketTemplateBodySetBindingModel model, TicketTemplateBody entity = null)
        {
            if (entity == null)
            {
                entity = new TicketTemplateBody();
            }

            entity.TicketTemplateId = model.TicketTemplateId;
            entity.TicketTemplateBodyPropertiesId = model.TicketTemplateBodyPropertiesId;
            if (model.TicketTemplateBodyPropertiesSetBindingModel != null)
            {
                entity.TicketTemplateBodyProperties = CreateTicketTemplateBodyProperties(model.TicketTemplateBodyPropertiesSetBindingModel, entity.TicketTemplateBodyProperties);
            }
            if (model.TicketTemplateParagraphSetBindingModels != null && model.TicketTemplateParagraphSetBindingModels.Count > 0)
            {
                if (entity.TicketTemplateParagraphs == null)
                {
                    entity.TicketTemplateParagraphs = new System.Collections.Generic.List<TicketTemplateParagraph>();
                }
                foreach (var elem in model.TicketTemplateParagraphSetBindingModels)
                {
                    entity.TicketTemplateParagraphs.Add(CreateTicketTemplateParagraph(elem, entity.TicketTemplateParagraphs.FirstOrDefault(x => x.Id == elem.Id)));
                }
            }

            return entity;
        }

        public static TicketTemplateBodyProperties CreateTicketTemplateBodyProperties(TicketTemplateBodyPropertiesSetBindingModel model, TicketTemplateBodyProperties entity = null)
        {
            if (entity == null)
            {
                entity = new TicketTemplateBodyProperties();
            }

            entity.TicketTemplateBodyId = model.TicketTemplateBodyId;
            entity.PageMarginBottom = model.PageMarginBottom;
            entity.PageMarginFooter = model.PageMarginFooter;
            entity.PageMarginGutter = model.PageMarginGutter;
            entity.PageMarginLeft = model.PageMarginLeft;
            entity.PageMarginRight = model.PageMarginRight;
            entity.PageMarginTop = model.PageMarginTop;
            entity.PageSizeHeight = model.PageSizeHeight;
            entity.PageSizeOrient = model.PageSizeOrient;
            entity.PageSizeWidth = model.PageSizeWidth;

            return entity;
        }

        public static TicketTemplateParagraph CreateTicketTemplateParagraph(TicketTemplateParagraphSetBindingModel model, TicketTemplateParagraph entity = null)
        {
            if (entity == null)
            {
                entity = new TicketTemplateParagraph();
            }

            entity.TicketTemplateBodyId = model.TicketTemplateBodyId;
            entity.TicketTemplateParagraphPropertiesId = model.TicketTemplateParagraphPropertiesId;
            entity.TicketTemplateTableCellId = model.TicketTemplateTableCellId;
            entity.Order = model.Order;

            if (model.TicketTemplateParagraphPropertiesSetBindingModel != null)
            {
                entity.TicketTemplateParagraphProperties = CreateTicketTemplateParagraphProperties(model.TicketTemplateParagraphPropertiesSetBindingModel, entity.TicketTemplateParagraphProperties);
            }
            if (model.TicketTemplateParagraphRunSetBindingModels != null && model.TicketTemplateParagraphRunSetBindingModels.Count > 0)
            {
                if (entity.TicketTemplateParagraphRuns == null)
                {
                    entity.TicketTemplateParagraphRuns = new System.Collections.Generic.List<TicketTemplateParagraphRun>();
                }
                foreach (var elem in model.TicketTemplateParagraphRunSetBindingModels)
                {
                    entity.TicketTemplateParagraphRuns.Add(CreateTicketTemplateParagraphRun(elem, entity.TicketTemplateParagraphRuns.FirstOrDefault(x => x.Id == elem.Id)));
                }
            }

            return entity;
        }

        public static TicketTemplateParagraphProperties CreateTicketTemplateParagraphProperties(TicketTemplateParagraphPropertiesSetBindingModel model, TicketTemplateParagraphProperties entity = null)
        {
            if (entity == null)
            {
                entity = new TicketTemplateParagraphProperties();
            }

            entity.TicketTemplateParagraphId = model.TicketTemplateParagraphId;
            entity.IndentationFirstLine = model.IndentationFirstLine;
            entity.IndentationHanging = model.IndentationHanging;
            entity.IndentationLeft = model.IndentationLeft;
            entity.IndentationRight = model.IndentationRight;
            entity.Justification = model.Justification;
            entity.SpacingBetweenLinesAfter = model.SpacingBetweenLinesAfter;
            entity.SpacingBetweenLinesBefore = model.SpacingBetweenLinesBefore;
            entity.SpacingBetweenLinesLine = model.SpacingBetweenLinesLine;
            entity.SpacingBetweenLinesLineRule = model.SpacingBetweenLinesLineRule;
            entity.RunBold = model.RunBold;
            entity.RunItalic = model.RunBold;
            entity.RunSize = model.RunSize;
            entity.RunUnderline = model.RunUnderline;

            return entity;
        }

        public static TicketTemplateParagraphRun CreateTicketTemplateParagraphRun(TicketTemplateParagraphRunSetBindingModel model, TicketTemplateParagraphRun entity = null)
        {
            if (entity == null)
            {
                entity = new TicketTemplateParagraphRun();
            }
            entity.TicketTemplateParagraphId = model.TicketTemplateParagraphId;
            entity.TicketTemplateParagraphRunPropertiesId = model.TicketTemplateRunPropertiesId;
            entity.Order = model.Order;
            entity.Text = model.Text;
            if (model.TicketTemplateParagraphRunPropertiesSetBindingModel != null)
            {
                entity.TicketTemplateParagraphRunProperties = CreateTicketTemplateParagraphRunProperties(model.TicketTemplateParagraphRunPropertiesSetBindingModel, entity.TicketTemplateParagraphRunProperties);
            }

            return entity;
        }

        public static TicketTemplateParagraphRunProperties CreateTicketTemplateParagraphRunProperties(TicketTemplateParagraphRunPropertiesSetBindingModel model, TicketTemplateParagraphRunProperties entity = null)
        {
            if (entity == null)
            {
                entity = new TicketTemplateParagraphRunProperties();
            }
            entity.TicketTemplateParagraphRunId = model.TicketTemplateParagraphRunId;
            entity.RunBold = model.RunBold;
            entity.RunItalic = model.RunBold;
            entity.RunSize = model.RunSize;
            entity.RunUnderline = model.RunUnderline;

            return entity;
        }
    }
}