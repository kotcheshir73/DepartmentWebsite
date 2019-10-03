using ExaminationInterfaces.ViewModels;
using Models.Examination;
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


        public static TicketTemplateViewModel CreateTicketTemplateViewModel(TicketTemplate entity)
        {
            return new TicketTemplateViewModel
            {
                Id = entity.Id,
                TemplateName = entity.TemplateName,
                Body = CreateTicketTemplateBodyViewModel(entity.TicketTemplateBodies?.FirstOrDefault()),
                TicketTemplateDocumentSettingPageViewModel = entity.TicketTemplateDocumentSettings != null ?
                                                        new TicketTemplateDocumentSettingPageViewModel
                                                        {
                                                            List = entity.TicketTemplateDocumentSettings.Select(CreateTicketTemplateDocumentSettingViewModel).OrderBy(x => x.Order).ToList()
                                                        } : null,
                TicketTemplateFontTablePageViewModel = entity.TicketTemplateFontTables != null ?
                                                        new TicketTemplateFontTablePageViewModel
                                                        {
                                                            List = entity.TicketTemplateFontTables.Select(CreateTicketTemplateFontTableViewModel).OrderBy(x => x.Order).ToList()
                                                        } : null,
                TicketTemplateNumberingPageViewModel = entity.TicketTemplateNumberings != null ?
                                                        new TicketTemplateNumberingPageViewModel
                                                        {
                                                            List = entity.TicketTemplateNumberings.Select(CreateTicketTemplateNumberingViewModel).OrderBy(x => x.Order).ToList()
                                                        } : null,
                TicketTemplateStyleDefinitionPageViewModel = entity.TicketTemplateStyleDefinitions != null ?
                                                        new TicketTemplateStyleDefinitionPageViewModel
                                                        {
                                                            List = entity.TicketTemplateStyleDefinitions.Select(CreateTicketTemplateStyleDefinitionViewModel).OrderBy(x => x.Order).ToList()
                                                        } : null,
                TicketTemplateThemePartPageViewModel = entity.TicketTemplateThemeParts != null ?
                                                        new TicketTemplateThemePartPageViewModel
                                                        {
                                                            List = entity.TicketTemplateThemeParts.Select(CreateTicketTemplateThemePartViewModel).OrderBy(x => x.Order).ToList()
                                                        } : null,
                TicketTemplateWebSettingPageViewModel = entity.TicketTemplateWebSettings != null ?
                                                        new TicketTemplateWebSettingPageViewModel
                                                        {
                                                            List = entity.TicketTemplateWebSettings.Select(CreateTicketTemplateWebSettingViewModel).OrderBy(x => x.Order).ToList()
                                                        } : null
            };
        }


        public static TicketTemplateDocumentSettingViewModel CreateTicketTemplateDocumentSettingViewModel(TicketTemplateDocumentSetting entity)
        {
            return new TicketTemplateDocumentSettingViewModel
            {
                Id = entity.Id,
                TicketTemplateId = entity.TicketTemplateId,
                InnerXml = entity.InnerXml,
                Order = entity.Order
            };
        }

        public static TicketTemplateFontTableViewModel CreateTicketTemplateFontTableViewModel(TicketTemplateFontTable entity)
        {
            return new TicketTemplateFontTableViewModel
            {
                Id = entity.Id,
                TicketTemplateId = entity.TicketTemplateId,
                InnerXml = entity.InnerXml,
                Order = entity.Order
            };
        }

        public static TicketTemplateNumberingViewModel CreateTicketTemplateNumberingViewModel(TicketTemplateNumbering entity)
        {
            return new TicketTemplateNumberingViewModel
            {
                Id = entity.Id,
                TicketTemplateId = entity.TicketTemplateId,
                InnerXml = entity.InnerXml,
                Order = entity.Order
            };
        }

        public static TicketTemplateStyleDefinitionViewModel CreateTicketTemplateStyleDefinitionViewModel(TicketTemplateStyleDefinition entity)
        {
            return new TicketTemplateStyleDefinitionViewModel
            {
                Id = entity.Id,
                TicketTemplateId = entity.TicketTemplateId,
                InnerXml = entity.InnerXml,
                Order = entity.Order
            };
        }

        public static TicketTemplateThemePartViewModel CreateTicketTemplateThemePartViewModel(TicketTemplateThemePart entity)
        {
            return new TicketTemplateThemePartViewModel
            {
                Id = entity.Id,
                TicketTemplateId = entity.TicketTemplateId,
                InnerXml = entity.InnerXml,
                Order = entity.Order
            };
        }

        public static TicketTemplateWebSettingViewModel CreateTicketTemplateWebSettingViewModel(TicketTemplateWebSetting entity)
        {
            return new TicketTemplateWebSettingViewModel
            {
                Id = entity.Id,
                TicketTemplateId = entity.TicketTemplateId,
                InnerXml = entity.InnerXml,
                Order = entity.Order
            };
        }


        public static TicketTemplateBodyViewModel CreateTicketTemplateBodyViewModel(TicketTemplateBody entity)
        {
            if(entity == null)
            {
                return null;
            }

            return new TicketTemplateBodyViewModel
            {
                Id = entity.Id,
                TicketTemplateId = entity.TicketTemplateId,
                TicketTemplateBodyPropertiesViewModel = CreateTicketTemplateBodyPropertiesViewModel(entity.TicketTemplateBodyProperties?.FirstOrDefault()),
                TicketTemplateTablePageViewModel = entity.TicketTemplateTables != null ?
                                                    new TicketTemplateTablePageViewModel
                                                    {
                                                        List = entity.TicketTemplateTables.Select(CreateTicketTemplateTableViewModel).OrderBy(x => x.Order).ToList()
                                                    } : null,
                TicketTemplateParagraphPageViewModel = entity.TicketTemplateParagraphs != null ?
                                                        new TicketTemplateParagraphPageViewModel
                                                        {
                                                            List = entity.TicketTemplateParagraphs.Select(CreateTicketTemplateParagraphViewModel).OrderBy(x => x.Order).ToList()
                                                        } : null
            };
        }

        public static TicketTemplateBodyPropertiesViewModel CreateTicketTemplateBodyPropertiesViewModel(TicketTemplateBodyProperties entity)
        {
            if(entity == null)
            {
                return null;
            }

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

        public static TicketTemplateTableViewModel CreateTicketTemplateTableViewModel(TicketTemplateTable entity)
        {
            return new TicketTemplateTableViewModel
            {
                Id = entity.Id,
                TicketTemplateBodyId = entity.TicketTemplateBodyId,
                Order = entity.Order,
                TicketTemplateTablePropertiesViewModel = CreateTicketTemplateTablePropertiesViewModel(entity.TicketTemplateTableProperties?.FirstOrDefault()),
                TicketTemplateTableGridColumnPageViewModel = entity.TicketTemplateTableGridColumns != null ?
                                                        new TicketTemplateTableGridColumnPageViewModel
                                                        {
                                                            List = entity.TicketTemplateTableGridColumns.Select(CreateTicketTemplateTableGridColumnViewModel).OrderBy(x => x.Order).ToList()
                                                        } : null,
                TicketTemplateTableRowPageViewModel = entity.TicketTemplateTableRows != null ?
                                                        new TicketTemplateTableRowPageViewModel
                                                        {
                                                            List = entity.TicketTemplateTableRows.Select(CreateTicketTemplateTableRowViewModel).OrderBy(x => x.Order).ToList()
                                                        } : null
            };
        }

        public static TicketTemplateTablePropertiesViewModel CreateTicketTemplateTablePropertiesViewModel(TicketTemplateTableProperties entity)
        {
            if(entity == null)
            {
                return null;
            }

            return new TicketTemplateTablePropertiesViewModel
            {
                Id = entity.Id,
                TicketTemplateTableId = entity.TicketTemplateTableId,
                BorderBottomColor = entity.BorderBottomColor,
                BorderBottomSize = entity.BorderBottomSize,
                BorderBottomSpace = entity.BorderBottomSpace,
                BorderBottomValue = entity.BorderBottomValue,
                BorderLeftColor = entity.BorderLeftColor,
                BorderLeftSize = entity.BorderLeftSize,
                BorderLeftSpace = entity.BorderLeftSpace,
                BorderLeftValue = entity.BorderLeftValue,
                BorderRightColor = entity.BorderRightColor,
                BorderRightSize = entity.BorderRightSize,
                BorderRightSpace = entity.BorderRightSpace,
                BorderRightValue = entity.BorderRightValue,
                BorderTopColor = entity.BorderTopColor,
                BorderTopSize = entity.BorderTopSize,
                BorderTopSpace = entity.BorderTopSpace,
                BorderTopValue = entity.BorderTopValue,
                LayoutType = entity.LayoutType,
                LookFirstColumn = entity.LookFirstColumn,
                LookFirstRow = entity.LookFirstRow,
                LookLastColumn = entity.LookLastColumn,
                LookLastRow = entity.LookLastRow,
                LookNoHorizontalBand = entity.LookNoHorizontalBand,
                LookNoVerticalBand = entity.LookNoVerticalBand,
                LookValue = entity.LookValue,
                Width = entity.Width
            };
        }

        public static TicketTemplateTableGridColumnViewModel CreateTicketTemplateTableGridColumnViewModel(TicketTemplateTableGridColumn entity)
        {
            return new TicketTemplateTableGridColumnViewModel
            {
                Id = entity.Id,
                TicketTemplateTableId = entity.TicketTemplateTableId,
                Order = entity.Order,
                Width = entity.Width
            };
        }

        public static TicketTemplateTableRowViewModel CreateTicketTemplateTableRowViewModel(TicketTemplateTableRow entity)
        {
            return new TicketTemplateTableRowViewModel
            {
                Id = entity.Id,
                TicketTemplateTableId = entity.TicketTemplateTableId,
                Order = entity.Order,
                TicketTemplateTableRowPropertiesViewModel = CreateTicketTemplateTableRowPropertiesViewModel(entity.TicketTemplateTableRowProperties.FirstOrDefault()),
                TicketTemplateTableCellPageViewModel = entity.TicketTemplateTableCells != null ?
                                                        new TicketTemplateTableCellPageViewModel
                                                        {
                                                            List = entity.TicketTemplateTableCells.Select(CreateTicketTemplateTableCellViewModel).OrderBy(x => x.Order).ToList()
                                                        } : null
            };
        }

        public static TicketTemplateTableRowPropertiesViewModel CreateTicketTemplateTableRowPropertiesViewModel(TicketTemplateTableRowProperties entity)
        {
            if(entity == null)
            {
                return null;
            }

            return new TicketTemplateTableRowPropertiesViewModel
            {
                Id = entity.Id,
                TicketTemplateTableRowId = entity.TicketTemplateTableRowId,
                CantSplit = entity.CantSplit,
                TableRowHeight = entity.TableRowHeight
            };
        }

        public static TicketTemplateTableCellViewModel CreateTicketTemplateTableCellViewModel(TicketTemplateTableCell entity)
        {
            return new TicketTemplateTableCellViewModel
            {
                Id = entity.Id,
                TicketTemplateTableRowId = entity.TicketTemplateTableRowId,
                Order = entity.Order,
                TicketTemplateTableCellPropertiesViewModel = CreateTicketTemplateTableCellPropertiesViewModel(entity.TicketTemplateTableCellProperties?.FirstOrDefault()),
                TicketTemplateParagraphPageViewModel = entity.TicketTemplateParagraphs != null ?
                                                        new TicketTemplateParagraphPageViewModel
                                                        {
                                                            List = entity.TicketTemplateParagraphs.Select(CreateTicketTemplateParagraphViewModel).OrderBy(x => x.Order).ToList()
                                                        } : null
            };
        }

        public static TicketTemplateTableCellPropertiesViewModel CreateTicketTemplateTableCellPropertiesViewModel(TicketTemplateTableCellProperties entity)
        {
            if(entity == null)
            {
                return null;
            }

            return new TicketTemplateTableCellPropertiesViewModel
            {
                Id = entity.Id,
                TicketTemplateTableCellId = entity.TicketTemplateTableCellId,
                TableCellWidth = entity.TableCellWidth,
                GridSpan = entity.GridSpan,
                VerticalMerge = entity.VerticalMerge,
                ShadingColor = entity.ShadingColor,
                ShadingFill = entity.ShadingFill,
                ShadingValue = entity.ShadingValue
            };
        }

        public static TicketTemplateParagraphViewModel CreateTicketTemplateParagraphViewModel(TicketTemplateParagraph entity)
        {
            return new TicketTemplateParagraphViewModel
            {
                Id = entity.Id,
                TicketTemplateBodyId = entity.TicketTemplateBodyId,
                TicketTemplateTableCellId = entity.TicketTemplateTableCellId,
                TicketTemplateParagraphPropertiesViewModel = CreateTicketTemplateParagraphPropertiesViewModel(entity.TicketTemplateParagraphProperties?.FirstOrDefault()),
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
            if(entity == null)
            {
                return null;
            }

            return new TicketTemplateParagraphPropertiesViewModel
            {
                Id = entity.Id,
                TicketTemplateParagraphId = entity.TicketTemplateParagraphId,
                NumberingLevelReference = entity.NumberingLevelReference,
                NumberingId = entity.NumberingId,
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
                TicketTemplateParagraphRunPropertiesViewModel = CreateTicketTemplateParagraphRunPropertiesViewModel(entity.TicketTemplateParagraphRunProperties?.FirstOrDefault()),
                Order = entity.Order,
                Text = entity.Text,
                TabChar = entity.TabChar,
                Break = entity.Break,
                BreakType = entity.BreakType
            };
        }

        public static TicketTemplateParagraphRunPropertiesViewModel CreateTicketTemplateParagraphRunPropertiesViewModel(TicketTemplateParagraphRunProperties entity)
        {
            if(entity == null)
            {
                return null;
            }

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