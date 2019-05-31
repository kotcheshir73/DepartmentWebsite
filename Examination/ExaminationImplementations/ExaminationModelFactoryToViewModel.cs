using ExaminationInterfaces.ViewModels;
using Models.Examination;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace ExaminationImplementations
{
    public static class ExaminationModelFactoryToViewModel
    {
        public static StatementViewModel CreateStatementViewModel(Statement entity)
        {
            return new StatementViewModel
            {
                Id = entity.Id,
                LecturerId = entity.LecturerId,
                DisciplineId = entity.DisciplineId,
                StudentGroupId = entity.StudentGroupId,
                AcademicYearId = entity.AcademicYearId,
                DisciplineName = entity.Discipline?.ToString() ?? string.Empty,
                StudentGroupName = entity.StudentGroup.ToString() ?? string.Empty,
                LectureName = entity.Lecturer?.ToString(),
                AcademicYearName = entity.AcademicYear?.ToString(),
                Date = entity.DateCreate.ToShortDateString(),
                Semester = entity.Semester.ToString(),
                TypeOfTest = entity.TypeOfTest.ToString().Replace('_', ' '),
                IsMain = entity.IsMain
            };
        }

        public static StatementRecordViewModel CreateStatementRecordViewModel(StatementRecord entity)
        {
            return new StatementRecordViewModel
            {
                Id = entity.Id,
                StatementId = entity.StatementId,
                StudentId = entity.StudentId,
                StatementName = entity.Statement?.ToString(),
                StudentName = entity.Student?.ToString() ?? string.Empty,
                Description = entity.Description,
                Score = entity.Score
            };
        }

        public static ExaminationListViewModel CreateExaminationListViewModel(ExaminationList entity)
        {
            return new ExaminationListViewModel
            {
                Id = entity.Id,
                LecturerId = entity.LecturerId,
                DisciplineId = entity.DisciplineId,
                StudentGroupId = entity.StudentGroupId,
                StudentId = entity.StudentId,
                AcademicYearId = entity.AcademicYearId,
                DisciplineName = entity.Discipline?.ToString() ?? string.Empty,
                StudentGroupName = entity.StudentGroup.ToString() ?? string.Empty,
                StudentName = entity.Student?.ToString() ?? string.Empty,
                LectureName = entity.Lecturer?.ToString(),
                AcademicYearName = entity.AcademicYear?.ToString(),
                Date = entity.DateCreate.ToShortDateString(),
                Semester = entity.Semester.ToString(),
                TypeOfTest = entity.TypeOfTest.ToString().Replace('_', ' '),
                Number = entity.Number,
                Score = entity.Score
            };
        }

        public static ExaminationTemplateViewModel CreateExaminationTemplateViewModel(ExaminationTemplate entity)
        {
            return new ExaminationTemplateViewModel
            {
                Id = entity.Id,
                DisciplineId = entity.DisciplineId,
                DisciplneName = entity.Discipline.DisciplineName,
                EducationDirectionId = entity.EducationDirectionId,
                TicketTemplateId = entity.TicketTemplateId,
                EducationDirectionName = entity.EducationDirection?.ShortName,
                TicketTemplateName = entity.TicketTemplate?.TemplateName ?? string.Empty,
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
                CountQuestionInTicket = entity.CountQuestionInTicket,
                IsCombine = entity.IsCombine,
                CombineBlocks = entity.CombineBlocks
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
                ExaminationTemplateBlockQuestionQuestion = entity.ExaminationTemplateBlockQuestion.QuestionText,
                ExaminationTemplateBlockId = entity.ExaminationTemplateBlockId,
                Order = entity.Order
            };
        }

        public static TicketTemplateViewModel CreateTicketTemplate(TicketTemplate entity)
        {
            return new TicketTemplateViewModel
            {
                Id = entity.Id,
                TemplateName = entity.TemplateName,
                Body = entity.TicketTemplateBody != null ? CreateTicketTemplateBodyViewModel(entity.TicketTemplateBody) : null
            };
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

        public static TicketProcessTableRowViewModel CreateTicketProcessTableRow(TicketTemplateTableRow entity)
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
                Order = entity.Order,
                //ParagraphFormat = entity.ParagraphFormat != null ? CreateTicketProcessElementaryUnit(entity.ParagraphFormat) : null
            };

            //if (entity.TicketTemplateElementaryAttributes != null && entity.TicketTemplateElementaryAttributes.Count > 0)
            //{
            //    model.ElementaryAttributes = new List<TicketProcessElementaryAttributeViewModels>();
            //    foreach (var attr in entity.TicketTemplateElementaryAttributes)
            //    {
            //        model.ElementaryAttributes.Add(CreateTicketProcessElementaryAttribute(attr));
            //    }
            //}

            //if (entity.TicketTemplateParagraphDatas != null && entity.TicketTemplateParagraphDatas.Count > 0)
            //{
            //    model.ParagraphDatas = new List<TicketProcessParagraphDataViewModel>();
            //    foreach (var elem in entity.TicketTemplateParagraphDatas.OrderBy(x => x.Order))
            //    {
            //        model.ParagraphDatas.Add(CreateTicketProcessParagraphData(elem));
            //    }
            //}

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

            if (entity.TicketTemplateElementaryAttributes != null && entity.TicketTemplateElementaryAttributes.Count > 0)
            {
                model.ElementaryAttributes = new List<TicketProcessElementaryAttributeViewModels>();
                foreach (var attr in entity.TicketTemplateElementaryAttributes)
                {
                    model.ElementaryAttributes.Add(CreateTicketProcessElementaryAttribute(attr));
                }
            }

            if (entity.ChildElementaryUnits != null && entity.ChildElementaryUnits.Count > 0)
            {
                model.ChildElementaryUnits = new List<TicketProcessElementaryUnitViewModel>();
                foreach (var elem in entity.ChildElementaryUnits.OrderBy(x => x.Order))
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


        public static TicketTemplateBodyViewModel CreateTicketTemplateBodyViewModel(TicketTemplateBody entity)
        {
            return new TicketTemplateBodyViewModel
            {
                Id = entity.Id,
                TicketTemplateId = entity.TicketTemplateId,
                TicketTemplateBodyPropertiesId = entity.TicketTemplateBodyPropertiesId,
                TicketTemplateBodyPropertiesViewModel = entity.TicketTemplateBodyProperties != null ?
                                                        CreateTicketTemplateBodyPropertiesViewModel(entity.TicketTemplateBodyProperties) : null,
                TicketTemplateParagraphPageViewModel = entity.TicketTemplateParagraphs != null ?
                                                        new TicketTemplateParagraphPageViewModel
                                                        {
                                                            List = entity.TicketTemplateParagraphs.Select(CreateTicketTemplateParagraphViewModel).OrderBy(x => x.Order).ToList()
                                                        } : null
            };
        }

        public static TicketTemplateBodyPropertiesViewModel CreateTicketTemplateBodyPropertiesViewModel(TicketTemplateBodyProperties entity)
        {
            return new TicketTemplateBodyPropertiesViewModel
            {
                Id = entity.Id,
                TicketTemplateBodyId = entity.TicketTemplateBodyId,
                PageMarginBottom = entity.PageMarginBottom,
                PageMarginFooter = entity.PageMarginFooter,
                PageMarginGutter = entity.PageMarginGutter,
                PageMarginLeft = entity.PageMarginLeft,
                PageMarginRight = entity.PageMarginRight,
                PageMarginTop = entity.PageMarginTop,
                PageSizeHeight = entity.PageSizeHeight,
                PageSizeOrient = entity.PageSizeOrient,
                PageSizeWidth = entity.PageSizeWidth
            };
        }

        public static TicketTemplateParagraphViewModel CreateTicketTemplateParagraphViewModel(TicketTemplateParagraph entity)
        {
            return new TicketTemplateParagraphViewModel
            {
                Id = entity.Id,
                TicketTemplateBodyId = entity.TicketTemplateBodyId,
                TicketTemplateTableCellId = entity.TicketTemplateTableCellId,
                TicketTemplateParagraphPropertiesId = entity.TicketTemplateParagraphPropertiesId,
                TicketTemplateParagraphPropertiesViewModel = entity.TicketTemplateParagraphProperties != null ?
                                                            CreateTicketTemplateParagraphPropertiesViewModel(entity.TicketTemplateParagraphProperties) : null,
                Order = entity.Order,
                TicketTemplateParagraphRunPageViewModel = entity.TicketTemplateParagraphRuns != null ?
                                                        new TicketTemplateParagraphRunPageViewModel
                                                        {
                                                            List = entity.TicketTemplateParagraphRuns.Select(CreateTicketTemplateParagraphRunViewModel).OrderBy(x => x.Order).ToList()
                                                        } : null
            };
        }

        public static TicketTemplateParagraphPropertiesViewModel CreateTicketTemplateParagraphPropertiesViewModel(TicketTemplateParagraphProperties entity)
        {
            return new TicketTemplateParagraphPropertiesViewModel
            {
                Id = entity.Id,
                TicketTemplateParagraphId = entity.TicketTemplateParagraphId,
                IndentationFirstLine = entity.IndentationFirstLine,
                IndentationHanging = entity.IndentationHanging,
                IndentationLeft = entity.IndentationLeft,
                IndentationRight = entity.IndentationRight,
                Justification = entity.Justification,
                RunBold = entity.RunBold,
                RunItalic = entity.RunItalic,
                RunSize = entity.RunSize,
                RunUnderline = entity.RunUnderline,
                SpacingBetweenLinesAfter = entity.SpacingBetweenLinesAfter,
                SpacingBetweenLinesBefore = entity.SpacingBetweenLinesBefore,
                SpacingBetweenLinesLine = entity.SpacingBetweenLinesLine,
                SpacingBetweenLinesLineRule = entity.SpacingBetweenLinesLineRule
            };
        }

        public static TicketTemplateParagraphRunViewModel CreateTicketTemplateParagraphRunViewModel(TicketTemplateParagraphRun entity)
        {
            return new TicketTemplateParagraphRunViewModel
            {
                Id = entity.Id,
                TicketTemplateParagraphId = entity.TicketTemplateParagraphId,
                TicketTemplateRunPropertiesId = entity.TicketTemplateParagraphRunPropertiesId,
                TicketTemplateParagraphRunPropertiesViewModel = entity.TicketTemplateParagraphRunProperties != null ?
                                                                CreateTicketTemplateParagraphRunPropertiesViewModel(entity.TicketTemplateParagraphRunProperties) : null,
                Order = entity.Order,
                Text = entity.Text,
                TabChar = entity.TabChar
            };
        }

        public static TicketTemplateParagraphRunPropertiesViewModel CreateTicketTemplateParagraphRunPropertiesViewModel(TicketTemplateParagraphRunProperties entity)
        {
            return new TicketTemplateParagraphRunPropertiesViewModel
            {
                Id = entity.Id,
                TicketTemplateParagraphRunId = entity.TicketTemplateParagraphRunId,
                RunBold = entity.RunBold,
                RunItalic = entity.RunItalic,
                RunSize = entity.RunSize,
                RunUnderline = entity.RunUnderline
            };
        }
    }
}