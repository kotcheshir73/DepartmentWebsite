using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using TicketModels.Models;
using TicketServiceInterfaces.ViewModels;

namespace TicketServiceImplementations
{
    public static class TicketModelFactoryToViewModel
    {
        public static ExaminationTemplateViewModel CreateExaminationTemplateViewModel(ExaminationTemplate entity)
        {
            return new ExaminationTemplateViewModel
            {
                Id = entity.Id,
                DisciplineId = entity.DisciplineId,
                DisciplneName = entity.Discipline.DisciplineName,
                EducationDirectionId = entity.EducationDirectionId,
                EducationDirectionName = entity.EducationDirection?.ShortName,
                Semester = entity.Semester.ToString(),
                ExaminationTemplateName = entity.ExaminationTemplateName
            };
        }

        public static ExaminationTemplateBlockViewModel CreateExaminationTemplateBlockViewModel(ExaminationTemplateBlock entity)
        {
            return new ExaminationTemplateBlockViewModel
            {
                Id = entity.Id,
                ExaminationTemplateId = entity.ExaminationTemplateId,
                ExaminationTemplateName = entity.ExaminationTemplate.ExaminationTemplateName,
                BlockName = entity.BlockName,
                QuestionTagInTemplate = entity.QuestionTagInTemplate,
                CountQuestionInTicket = entity.CountQuestionInTicket
            };
        }

        public static ExaminationTemplateBlockQuestionViewModel CreateExaminationTemplateBlockQuestionViewModel(ExaminationTemplateBlockQuestion entity)
        {
            return new ExaminationTemplateBlockQuestionViewModel
            {
                Id = entity.Id,
                ExaminationTemplateBlockId = entity.ExaminationTemplateBlockId,
                ExaminationTemplateBlockName = entity.ExaminationTemplateBlock.BlockName,
                QuestionNumber = entity.QuestionNumber,
                QuestionText = entity.QuestionText,
                QuestionImage = entity.QuestionImage != null && entity.QuestionImage.Length > 0 ? Image.FromStream(new MemoryStream(entity.QuestionImage)) : null,
            };
        }

        public static ExaminationTemplateTicketViewModel CreateExaminationTemplateTicketViewModel(ExaminationTemplateTicket entity)
        {
            return new ExaminationTemplateTicketViewModel
            {
                Id = entity.Id,
                ExaminationTemplateId = entity.ExaminationTemplateId,
                ExaminationTemplateName = entity.ExaminationTemplate.ExaminationTemplateName,
                TicketNumber = entity.TicketNumber
            };
        }

        public static ExaminationTemplateTicketQuestionViewModel CreateExaminationTemplateTicketQuestionViewModel(ExaminationTemplateTicketQuestion entity)
        {
            return new ExaminationTemplateTicketQuestionViewModel
            {
                Id = entity.Id,
                ExaminationTemplateBlockQuestionId = entity.ExaminationTemplateBlockQuestionId,
                ExaminationTemplateBlockQuestionNumber = entity.ExaminationTemplateBlockQuestion.QuestionNumber,
                ExaminationTemplateTicketId = entity.ExaminationTemplateTicketId,
                ExaminationTemplateTicketNumber = entity.ExaminationTemplateTicket.TicketNumber,
                Order = entity.Order
            };
        }

        public static TicketTemplateViewModel CreateTicketTemplate(TicketTemplate entity)
        {
            return new TicketTemplateViewModel
            {
                Id = entity.Id,
                TemplateName = entity.TemplateName,
                Body = CreateTicketProcessBody(entity.TicketTemplateBodies?.FirstOrDefault())
            };
        }

        public static TicketProcessBodyViewModel CreateTicketProcessBody(TicketTemplateBody entity)
        {
            if(entity == null)
            {
                return null;
            }
            TicketProcessBodyViewModel model = new TicketProcessBodyViewModel
            {
                Id = entity.Id,
                BodyName = entity.BodyName,
                SectName = entity.SectName,
                BodyFormat = entity.BodyFormat != null ? CreateTicketProcessElementaryUnit(entity.BodyFormat) : null
            };

            if (entity.TicketTemplateParagraphs != null && entity.TicketTemplateParagraphs.Count > 0)
            {
                model.Paragraphs = new List<TicketProcessParagraphViewModel>();
                foreach (var elem in entity.TicketTemplateParagraphs)
                {
                    model.Paragraphs.Add(CreateTicketProcessParagraph(elem));
                }
            }

            if (entity.TicketTemplateTables != null && entity.TicketTemplateTables.Count > 0)
            {
                model.Tables = new List<TicketProcessTableViewModel>();
                foreach (var elem in entity.TicketTemplateTables)
                {
                    model.Tables.Add(CreateTicketProcessTable(elem));
                }
            }

            return model;
        }

        public static TicketProcessTableViewModel CreateTicketProcessTable(TicketTemplateTable entity)
        {
            TicketProcessTableViewModel model = new TicketProcessTableViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Order = entity.Order,
                Properties = entity.Properties != null ? CreateTicketProcessElementaryUnit(entity.Properties) : null,
                Columns = entity.Columns != null ? CreateTicketProcessElementaryUnit(entity.Columns) : null
            };

            if (entity.TicketTemplateTableRows != null && entity.TicketTemplateTableRows.Count > 0)
            {
                model.TableRows = new List<TicketProcessTableRowViewModel>();
                foreach (var elem in entity.TicketTemplateTableRows)
                {
                    model.TableRows.Add(CreateTicketProcessTableRow(elem));
                }
            }

            return model;
        }

        public  static TicketProcessTableRowViewModel CreateTicketProcessTableRow(TicketTemplateTableRow entity)
        {
            TicketProcessTableRowViewModel model = new TicketProcessTableRowViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Order = entity.Order,
                Properties = entity.Properties != null ? CreateTicketProcessElementaryUnit(entity.Properties) : null
            };

            if (entity.TicketTemplateElementaryAttributes != null && entity.TicketTemplateElementaryAttributes.Count > 0)
            {
                model.ElementaryAttributes = new List<TicketProcessElementaryAttributeViewModels>();
                foreach (var attr in entity.TicketTemplateElementaryAttributes)
                {
                    model.ElementaryAttributes.Add(CreateTicketProcessElementaryAttribute(attr));
                }
            }

            if (entity.TicketTemplateTableCells != null && entity.TicketTemplateTableCells.Count > 0)
            {
                model.TableCells = new List<TicketProcessTableCellViewModel>();
                foreach (var elem in entity.TicketTemplateTableCells)
                {
                    model.TableCells.Add(CreateTicketProcessTableCell(elem));
                }
            }

            return model;
        }

        public static TicketProcessTableCellViewModel CreateTicketProcessTableCell(TicketTemplateTableCell entity)
        {
            TicketProcessTableCellViewModel model = new TicketProcessTableCellViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Order = entity.Order,
                Properties = entity.Properties != null ? CreateTicketProcessElementaryUnit(entity.Properties) : null
            };

            if (entity.TicketTemplateParagraphs != null && entity.TicketTemplateParagraphs.Count > 0)
            {
                model.Paragraphs = new List<TicketProcessParagraphViewModel>();
                foreach (var elem in entity.TicketTemplateParagraphs)
                {
                    model.Paragraphs.Add(CreateTicketProcessParagraph(elem));
                }
            }

            return model;
        }

        public static TicketProcessParagraphViewModel CreateTicketProcessParagraph(TicketTemplateParagraph entity)
        {
            TicketProcessParagraphViewModel model = new TicketProcessParagraphViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Order = entity.Order,
                ParagraphFormat = entity.ParagraphFormat != null ? CreateTicketProcessElementaryUnit(entity.ParagraphFormat) : null
            };

            if (entity.TicketTemplateElementaryAttributes != null && entity.TicketTemplateElementaryAttributes.Count > 0)
            {
                model.ElementaryAttributes = new List<TicketProcessElementaryAttributeViewModels>();
                foreach (var attr in entity.TicketTemplateElementaryAttributes)
                {
                    model.ElementaryAttributes.Add(CreateTicketProcessElementaryAttribute(attr));
                }
            }

            if (entity.TicketTemplateParagraphDatas != null && entity.TicketTemplateParagraphDatas.Count > 0)
            {
                model.ParagraphDatas = new List<TicketProcessParagraphDataViewModel>();
                foreach (var elem in entity.TicketTemplateParagraphDatas.OrderBy(x => x.Order))
                {
                    model.ParagraphDatas.Add(CreateTicketProcessParagraphData(elem));
                }
            }

            return model;
        }

        public static TicketProcessParagraphDataViewModel CreateTicketProcessParagraphData(TicketTemplateParagraphData entity)
        {
            TicketProcessParagraphDataViewModel model = new TicketProcessParagraphDataViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Order = entity.Order,
                Text = entity.Text,
                TextName = entity.TextName,
                Font = entity.Font != null ? CreateTicketProcessElementaryUnit(entity.Font) : null
            };

            if (entity.TicketTemplateElementaryAttributes != null && entity.TicketTemplateElementaryAttributes.Count > 0)
            {
                model.ElementaryAttributes = new List<TicketProcessElementaryAttributeViewModels>();
                foreach (var attr in entity.TicketTemplateElementaryAttributes)
                {
                    model.ElementaryAttributes.Add(CreateTicketProcessElementaryAttribute(attr));
                }
            }

            if (entity.TicketTemplateElementaryUnits != null && entity.TicketTemplateElementaryUnits.Count > 0)
            {
                model.ElementaryUnits = new List<TicketProcessElementaryUnitViewModel>();
                foreach (var elem in entity.TicketTemplateElementaryUnits.OrderBy(x => x.Order))
                {
                    model.ElementaryUnits.Add(CreateTicketProcessElementaryUnit(elem));
                }
            }

            return model;
        }

        public static TicketProcessElementaryUnitViewModel CreateTicketProcessElementaryUnit(TicketTemplateElementaryUnit entity)
        {
            TicketProcessElementaryUnitViewModel model = new TicketProcessElementaryUnitViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Value = entity.Value,
                Order = entity.Order
            };

            if(entity.TicketTemplateElementaryAttributes != null && entity.TicketTemplateElementaryAttributes.Count > 0)
            {
                model.ElementaryAttributes = new List<TicketProcessElementaryAttributeViewModels>();
                foreach(var attr in entity.TicketTemplateElementaryAttributes)
                {
                    model.ElementaryAttributes.Add(CreateTicketProcessElementaryAttribute(attr));
                }
            }

            if(entity.ChildElementaryUnits != null && entity.ChildElementaryUnits.Count > 0)
            {
                model.ChildElementaryUnits = new List<TicketProcessElementaryUnitViewModel>();
                foreach(var elem in entity.ChildElementaryUnits.OrderBy(x => x.Order))
                {
                    model.ChildElementaryUnits.Add(CreateTicketProcessElementaryUnit(elem));
                }
            }

            return model;
        }

        public static TicketProcessElementaryAttributeViewModels CreateTicketProcessElementaryAttribute(TicketTemplateElementaryAttribute entity)
        {
            return new TicketProcessElementaryAttributeViewModels
            {
                Id = entity.Id,
                Name = entity.Name,
                Value = entity.Value
            };
        }
    }
}