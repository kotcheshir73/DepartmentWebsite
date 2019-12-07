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
            entity.TicketTemplateId = model.TicketTemplateId;
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

                if (model.TicketTemplateDocumentSettingSetBindingModels != null)
                {
                    model.TicketTemplateDocumentSettingSetBindingModels.ForEach(x => x.TicketTemplateId = entity.Id);
                }
                if (model.TicketTemplateFontTableSetBindingModels != null)
                {
                    model.TicketTemplateFontTableSetBindingModels.ForEach(x => x.TicketTemplateId = entity.Id);
                }
                if (model.TicketTemplateNumberingSetBindingModels != null)
                {
                    model.TicketTemplateNumberingSetBindingModels.ForEach(x => x.TicketTemplateId = entity.Id);
                }
                if (model.TicketTemplateStyleDefinitionSetBindingModels != null)
                {
                    model.TicketTemplateStyleDefinitionSetBindingModels.ForEach(x => x.TicketTemplateId = entity.Id);
                }
                if (model.TicketTemplateThemePartSetBindingModels != null)
                {
                    model.TicketTemplateThemePartSetBindingModels.ForEach(x => x.TicketTemplateId = entity.Id);
                }
                if (model.TicketTemplateWebSettingSetBindingModels != null)
                {
                    model.TicketTemplateWebSettingSetBindingModels.ForEach(x => x.TicketTemplateId = entity.Id);
                }

                if (model.TicketTemplateBodySetBindingModel != null)
                {
                    model.TicketTemplateBodySetBindingModel.TicketTemplateId = entity.Id;
                }
            }
            entity.TemplateName = model.TemplateName;

            if (model.TicketTemplateDocumentSettingSetBindingModels != null && model.TicketTemplateDocumentSettingSetBindingModels.Count > 0)
            {
                if (entity.TicketTemplateDocumentSettings == null)
                {
                    entity.TicketTemplateDocumentSettings = new System.Collections.Generic.List<TicketTemplateDocumentSetting>();
                }
                foreach (var elem in model.TicketTemplateDocumentSettingSetBindingModels)
                {
                    entity.TicketTemplateDocumentSettings.Add(CreateTicketTemplateDocumentSetting(elem, entity.TicketTemplateDocumentSettings.FirstOrDefault(x => x.Id == elem.Id)));
                }
            }

            if (model.TicketTemplateFontTableSetBindingModels != null && model.TicketTemplateFontTableSetBindingModels.Count > 0)
            {
                if (entity.TicketTemplateFontTables == null)
                {
                    entity.TicketTemplateFontTables = new System.Collections.Generic.List<TicketTemplateFontTable>();
                }
                foreach (var elem in model.TicketTemplateFontTableSetBindingModels)
                {
                    entity.TicketTemplateFontTables.Add(CreateTicketTemplateFontTable(elem, entity.TicketTemplateFontTables.FirstOrDefault(x => x.Id == elem.Id)));
                }
            }

            if (model.TicketTemplateNumberingSetBindingModels != null && model.TicketTemplateNumberingSetBindingModels.Count > 0)
            {
                if (entity.TicketTemplateNumberings == null)
                {
                    entity.TicketTemplateNumberings = new System.Collections.Generic.List<TicketTemplateNumbering>();
                }
                foreach (var elem in model.TicketTemplateNumberingSetBindingModels)
                {
                    entity.TicketTemplateNumberings.Add(CreateTicketTemplateNumbering(elem, entity.TicketTemplateNumberings.FirstOrDefault(x => x.Id == elem.Id)));
                }
            }

            if (model.TicketTemplateStyleDefinitionSetBindingModels != null && model.TicketTemplateStyleDefinitionSetBindingModels.Count > 0)
            {
                if (entity.TicketTemplateStyleDefinitions == null)
                {
                    entity.TicketTemplateStyleDefinitions = new System.Collections.Generic.List<TicketTemplateStyleDefinition>();
                }
                foreach (var elem in model.TicketTemplateStyleDefinitionSetBindingModels)
                {
                    entity.TicketTemplateStyleDefinitions.Add(CreateTicketTemplateStyleDefinition(elem, entity.TicketTemplateStyleDefinitions.FirstOrDefault(x => x.Id == elem.Id)));
                }
            }

            if (model.TicketTemplateThemePartSetBindingModels != null && model.TicketTemplateThemePartSetBindingModels.Count > 0)
            {
                if (entity.TicketTemplateThemeParts == null)
                {
                    entity.TicketTemplateThemeParts = new System.Collections.Generic.List<TicketTemplateThemePart>();
                }
                foreach (var elem in model.TicketTemplateThemePartSetBindingModels)
                {
                    entity.TicketTemplateThemeParts.Add(CreateTicketTemplateThemePart(elem, entity.TicketTemplateThemeParts.FirstOrDefault(x => x.Id == elem.Id)));
                }
            }

            if (model.TicketTemplateWebSettingSetBindingModels != null && model.TicketTemplateWebSettingSetBindingModels.Count > 0)
            {
                if (entity.TicketTemplateWebSettings == null)
                {
                    entity.TicketTemplateWebSettings = new System.Collections.Generic.List<TicketTemplateWebSetting>();
                }
                foreach (var elem in model.TicketTemplateWebSettingSetBindingModels)
                {
                    entity.TicketTemplateWebSettings.Add(CreateTicketTemplateWebSetting(elem, entity.TicketTemplateWebSettings.FirstOrDefault(x => x.Id == elem.Id)));
                }
            }

            if (model.TicketTemplateBodySetBindingModel != null)
            {
                if(entity.TicketTemplateBodies == null)
                {
                    entity.TicketTemplateBodies = new System.Collections.Generic.List<TicketTemplateBody>();
                }
                entity.TicketTemplateBodies.Add(CreateTicketTemplateBody(model.TicketTemplateBodySetBindingModel, entity.TicketTemplateBodies.FirstOrDefault()));
            }

            return entity;
        }


        public static TicketTemplateDocumentSetting CreateTicketTemplateDocumentSetting(TicketTemplateDocumentSettingSetBindingModel model, TicketTemplateDocumentSetting entity = null)
        {
            if (entity == null)
            {
                entity = new TicketTemplateDocumentSetting();
            }

            entity.TicketTemplateId = model.TicketTemplateId;
            entity.InnerXml = model.InnerXml;
            entity.Order = model.Order;

            return entity;
        }

        public static TicketTemplateFontTable CreateTicketTemplateFontTable(TicketTemplateFontTableSetBindingModel model, TicketTemplateFontTable entity = null)
        {
            if (entity == null)
            {
                entity = new TicketTemplateFontTable();
            }

            entity.TicketTemplateId = model.TicketTemplateId;
            entity.InnerXml = model.InnerXml;
            entity.Order = model.Order;

            return entity;
        }

        public static TicketTemplateNumbering CreateTicketTemplateNumbering(TicketTemplateNumberingSetBindingModel model, TicketTemplateNumbering entity = null)
        {
            if (entity == null)
            {
                entity = new TicketTemplateNumbering();
            }

            entity.TicketTemplateId = model.TicketTemplateId;
            entity.InnerXml = model.InnerXml;
            entity.Order = model.Order;

            return entity;
        }

        public static TicketTemplateStyleDefinition CreateTicketTemplateStyleDefinition(TicketTemplateStyleDefinitionSetBindingModel model, TicketTemplateStyleDefinition entity = null)
        {
            if (entity == null)
            {
                entity = new TicketTemplateStyleDefinition();
            }

            entity.TicketTemplateId = model.TicketTemplateId;
            entity.InnerXml = model.InnerXml;
            entity.Order = model.Order;

            return entity;
        }

        public static TicketTemplateThemePart CreateTicketTemplateThemePart(TicketTemplateThemePartSetBindingModel model, TicketTemplateThemePart entity = null)
        {
            if (entity == null)
            {
                entity = new TicketTemplateThemePart();
            }

            entity.TicketTemplateId = model.TicketTemplateId;
            entity.InnerXml = model.InnerXml;
            entity.Order = model.Order;

            return entity;
        }

        public static TicketTemplateWebSetting CreateTicketTemplateWebSetting(TicketTemplateWebSettingSetBindingModel model, TicketTemplateWebSetting entity = null)
        {
            if (entity == null)
            {
                entity = new TicketTemplateWebSetting();
            }

            entity.TicketTemplateId = model.TicketTemplateId;
            entity.InnerXml = model.InnerXml;
            entity.Order = model.Order;

            return entity;
        }


        public static TicketTemplateBody CreateTicketTemplateBody(TicketTemplateBodySetBindingModel model, TicketTemplateBody entity = null)
        {
            if (entity == null)
            {
                entity = new TicketTemplateBody
                {
                    Id = model.Id
                };
                if (model.TicketTemplateBodyPropertiesSetBindingModel != null)
                {
                    model.TicketTemplateBodyPropertiesSetBindingModel.TicketTemplateBodyId = entity.Id;
                }
            }

            entity.TicketTemplateId = model.TicketTemplateId;

            if (model.TicketTemplateBodyPropertiesSetBindingModel != null)
            {
                if(entity.TicketTemplateBodyProperties == null)
                {
                    entity.TicketTemplateBodyProperties = new System.Collections.Generic.List<TicketTemplateBodyProperties>();
                }
                entity.TicketTemplateBodyProperties.Add(CreateTicketTemplateBodyProperties(model.TicketTemplateBodyPropertiesSetBindingModel, entity.TicketTemplateBodyProperties.FirstOrDefault()));
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

            if (model.TicketTemplateTableSetBindingModels != null && model.TicketTemplateTableSetBindingModels.Count > 0)
            {
                if (entity.TicketTemplateTables == null)
                {
                    entity.TicketTemplateTables = new System.Collections.Generic.List<TicketTemplateTable>();
                }
                foreach (var elem in model.TicketTemplateTableSetBindingModels)
                {
                    entity.TicketTemplateTables.Add(CreateTicketTemplateTable(elem, entity.TicketTemplateTables.FirstOrDefault(x => x.Id == elem.Id)));
                }
            }

            return entity;
        }

        public static TicketTemplateBodyProperties CreateTicketTemplateBodyProperties(TicketTemplateBodyPropertiesSetBindingModel model, TicketTemplateBodyProperties entity = null)
        {
            if (entity == null)
            {
                entity = new TicketTemplateBodyProperties
                {
                    Id = model.Id
                };
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

        public static TicketTemplateTable CreateTicketTemplateTable(TicketTemplateTableSetBindingModel model, TicketTemplateTable entity = null)
        {
            if (entity == null)
            {
                entity = new TicketTemplateTable
                {
                    Id = model.Id
                };
                if (model.TicketTemplateTablePropertiesSetBindingModel != null)
                {
                    model.TicketTemplateTablePropertiesSetBindingModel.TicketTemplateTableId = entity.Id;
                }
            }

            entity.TicketTemplateBodyId = model.TicketTemplateBodyId;
            entity.Order = model.Order;

            if (model.TicketTemplateTablePropertiesSetBindingModel != null)
            {
                if(entity.TicketTemplateTableProperties == null)
                {
                    entity.TicketTemplateTableProperties = new System.Collections.Generic.List<TicketTemplateTableProperties>();
                }
                entity.TicketTemplateTableProperties.Add(CreateTicketTemplateTableProperties(model.TicketTemplateTablePropertiesSetBindingModel, entity.TicketTemplateTableProperties.FirstOrDefault()));
            }
            if (model.TicketTemplateTableGridColumnSetBindingModels != null && model.TicketTemplateTableGridColumnSetBindingModels.Count > 0)
            {
                if (entity.TicketTemplateTableGridColumns == null)
                {
                    entity.TicketTemplateTableGridColumns = new System.Collections.Generic.List<TicketTemplateTableGridColumn>();
                }
                foreach (var elem in model.TicketTemplateTableGridColumnSetBindingModels)
                {
                    entity.TicketTemplateTableGridColumns.Add(CreateTicketTemplateTableGridColumn(elem, entity.TicketTemplateTableGridColumns.FirstOrDefault(x => x.Id == elem.Id)));
                }
            }
            if (model.TicketTemplateTableRowSetBindingModels != null && model.TicketTemplateTableRowSetBindingModels.Count > 0)
            {
                if (entity.TicketTemplateTableRows == null)
                {
                    entity.TicketTemplateTableRows = new System.Collections.Generic.List<TicketTemplateTableRow>();
                }
                foreach (var elem in model.TicketTemplateTableRowSetBindingModels)
                {
                    entity.TicketTemplateTableRows.Add(CreateTicketTemplateTableRow(elem, entity.TicketTemplateTableRows.FirstOrDefault(x => x.Id == elem.Id)));
                }
            }

            return entity;
        }

        public static TicketTemplateTableProperties CreateTicketTemplateTableProperties(TicketTemplateTablePropertiesSetBindingModel model, TicketTemplateTableProperties entity = null)
        {
            if (entity == null)
            {
                entity = new TicketTemplateTableProperties
                {
                    Id = model.Id
                };
            }
            entity.Width = model.Width;
            entity.LookValue = model.LookValue;
            entity.LookFirstRow = model.LookFirstRow;
            entity.LookLastRow = model.LookLastRow;
            entity.LookFirstColumn = model.LookFirstColumn;
            entity.LookLastColumn = model.LookLastColumn;
            entity.LookNoHorizontalBand = model.LookNoHorizontalBand;
            entity.LookNoVerticalBand = model.LookNoVerticalBand;
            entity.LayoutType = model.LayoutType;

            entity.BorderTopValue = model.BorderTopValue;
            entity.BorderTopColor = model.BorderTopColor;
            entity.BorderTopSize = model.BorderTopSize;
            entity.BorderTopSpace = model.BorderTopSpace;

            entity.BorderBottomValue = model.BorderBottomValue;
            entity.BorderBottomColor = model.BorderBottomColor;
            entity.BorderBottomSize = model.BorderBottomSize;
            entity.BorderBottomSpace = model.BorderBottomSpace;

            entity.BorderLeftValue = model.BorderLeftValue;
            entity.BorderLeftColor = model.BorderLeftColor;
            entity.BorderLeftSize = model.BorderLeftSize;
            entity.BorderLeftSpace = model.BorderLeftSpace;

            entity.BorderRightValue = model.BorderRightValue;
            entity.BorderRightColor = model.BorderRightColor;
            entity.BorderRightSize = model.BorderRightSize;
            entity.BorderRightSpace = model.BorderRightSpace;

            return entity;
        }

        public static TicketTemplateTableGridColumn CreateTicketTemplateTableGridColumn(TicketTemplateTableGridColumnSetBindingModel model, TicketTemplateTableGridColumn entity = null)
        {
            if (entity == null)
            {
                entity = new TicketTemplateTableGridColumn
                {
                    Id = model.Id
                };
            }

            entity.TicketTemplateTableId = model.TicketTemplateTableId;
            entity.Order = model.Order;
            entity.Width = model.Width;

            return entity;
        }

        public static TicketTemplateTableRow CreateTicketTemplateTableRow(TicketTemplateTableRowSetBindingModel model, TicketTemplateTableRow entity = null)
        {
            if (entity == null)
            {
                entity = new TicketTemplateTableRow
                {
                    Id = model.Id
                };
                if (model.TicketTemplateTableRowPropertiesSetBindingModel != null)
                {
                    model.TicketTemplateTableRowPropertiesSetBindingModel.TicketTemplateTableRowId = entity.Id;
                }
            }

            entity.TicketTemplateTableId = model.TicketTemplateTableId;
            entity.Order = model.Order;

            if (model.TicketTemplateTableRowPropertiesSetBindingModel != null)
            {
                if(entity.TicketTemplateTableRowProperties == null)
                {
                    entity.TicketTemplateTableRowProperties = new System.Collections.Generic.List<TicketTemplateTableRowProperties>();
                }
                entity.TicketTemplateTableRowProperties.Add(CreateTicketTemplateTableRowProperties(model.TicketTemplateTableRowPropertiesSetBindingModel, entity.TicketTemplateTableRowProperties.FirstOrDefault()));
            }
            if (model.TicketTemplateTableCellSetBindingModels != null && model.TicketTemplateTableCellSetBindingModels.Count > 0)
            {
                if (entity.TicketTemplateTableCells == null)
                {
                    entity.TicketTemplateTableCells = new System.Collections.Generic.List<TicketTemplateTableCell>();
                }
                foreach (var elem in model.TicketTemplateTableCellSetBindingModels)
                {
                    entity.TicketTemplateTableCells.Add(CreateTicketTemplateTableCell(elem, entity.TicketTemplateTableCells.FirstOrDefault(x => x.Id == elem.Id)));
                }
            }

            return entity;
        }

        public static TicketTemplateTableRowProperties CreateTicketTemplateTableRowProperties(TicketTemplateTableRowPropertiesSetBindingModel model, TicketTemplateTableRowProperties entity = null)
        {
            if (entity == null)
            {
                entity = new TicketTemplateTableRowProperties
                {
                    Id = model.Id
                };
            }
            entity.CantSplit = model.CantSplit;
            entity.TableRowHeight = model.TableRowHeight;

            return entity;
        }

        public static TicketTemplateTableCell CreateTicketTemplateTableCell(TicketTemplateTableCellSetBindingModel model, TicketTemplateTableCell entity = null)
        {
            if (entity == null)
            {
                entity = new TicketTemplateTableCell
                {
                    Id = model.Id
                };
                if (model.TicketTemplateTableCellPropertiesSetBindingModel != null)
                {
                    model.TicketTemplateTableCellPropertiesSetBindingModel.TicketTemplateTableCellId = entity.Id;
                }
            }

            entity.TicketTemplateTableRowId = model.TicketTemplateTableRowId;
            entity.Order = model.Order;

            if (model.TicketTemplateTableCellPropertiesSetBindingModel != null)
            { 
                if(entity.TicketTemplateTableCellProperties == null)
                {
                    entity.TicketTemplateTableCellProperties = new System.Collections.Generic.List<TicketTemplateTableCellProperties>();
                }
                entity.TicketTemplateTableCellProperties.Add(CreateTicketTemplateTableCellProperties(model.TicketTemplateTableCellPropertiesSetBindingModel, entity.TicketTemplateTableCellProperties.FirstOrDefault()));
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

        public static TicketTemplateTableCellProperties CreateTicketTemplateTableCellProperties(TicketTemplateTableCellPropertiesSetBindingModel model, TicketTemplateTableCellProperties entity = null)
        {
            if (entity == null)
            {
                entity = new TicketTemplateTableCellProperties
                {
                    Id = model.Id
                };
            }
            entity.TableCellWidth = model.TableCellWidth;
            entity.GridSpan = model.GridSpan;
            entity.VerticalMerge = model.VerticalMerge;
            entity.ShadingValue = model.ShadingValue;
            entity.ShadingColor = model.ShadingColor;
            entity.ShadingFill = model.ShadingFill;

            return entity;
        }

        public static TicketTemplateParagraph CreateTicketTemplateParagraph(TicketTemplateParagraphSetBindingModel model, TicketTemplateParagraph entity = null)
        {
            if (entity == null)
            {
                entity = new TicketTemplateParagraph
                {
                    Id = model.Id
                };
                if (model.TicketTemplateParagraphPropertiesSetBindingModel != null)
                {
                    model.TicketTemplateParagraphPropertiesSetBindingModel.TicketTemplateParagraphId = entity.Id;
                }
            }

            entity.TicketTemplateBodyId = model.TicketTemplateBodyId;
            entity.TicketTemplateTableCellId = model.TicketTemplateTableCellId;
            entity.Order = model.Order;

            if (model.TicketTemplateParagraphPropertiesSetBindingModel != null)
            {
                if(entity.TicketTemplateParagraphProperties == null)
                {
                    entity.TicketTemplateParagraphProperties = new System.Collections.Generic.List<TicketTemplateParagraphProperties>();
                }
                entity.TicketTemplateParagraphProperties.Add(CreateTicketTemplateParagraphProperties(model.TicketTemplateParagraphPropertiesSetBindingModel, entity.TicketTemplateParagraphProperties.FirstOrDefault()));
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
                entity = new TicketTemplateParagraphProperties
                {
                    Id = model.Id
                };
            }

            entity.TicketTemplateParagraphId = model.TicketTemplateParagraphId;
            entity.NumberingLevelReference = model.NumberingLevelReference;
            entity.NumberingId = model.NumberingId;
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
                entity = new TicketTemplateParagraphRun
                {
                    Id = model.Id
                };
                if (model.TicketTemplateParagraphRunPropertiesSetBindingModel != null)
                {
                    model.TicketTemplateParagraphRunPropertiesSetBindingModel.TicketTemplateParagraphRunId = entity.Id;
                }
            }
            entity.TicketTemplateParagraphId = model.TicketTemplateParagraphId;
            entity.Order = model.Order;
            entity.Text = model.Text;
            entity.TabChar = model.TabChar;
            entity.Break = model.Break;
            entity.BreakType = model.BreakType;
            if (model.TicketTemplateParagraphRunPropertiesSetBindingModel != null)
            {
                if(entity.TicketTemplateParagraphRunProperties == null)
                {
                    entity.TicketTemplateParagraphRunProperties = new System.Collections.Generic.List<TicketTemplateParagraphRunProperties>();
                }
                entity.TicketTemplateParagraphRunProperties.Add(CreateTicketTemplateParagraphRunProperties(model.TicketTemplateParagraphRunPropertiesSetBindingModel, entity.TicketTemplateParagraphRunProperties.FirstOrDefault()));
            }

            return entity;
        }

        public static TicketTemplateParagraphRunProperties CreateTicketTemplateParagraphRunProperties(TicketTemplateParagraphRunPropertiesSetBindingModel model, TicketTemplateParagraphRunProperties entity = null)
        {
            if (entity == null)
            {
                entity = new TicketTemplateParagraphRunProperties
                {
                    Id = model.Id
                };
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