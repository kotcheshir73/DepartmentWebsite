using Models.Examination;
using System;
using System.Collections.Generic;
using System.Linq;
using Tools;

namespace TicketServiceImplementations.Helpers
{
    public static class TicketBodyGet
    {
        public static TicketTemplateBody GetBody(Guid ticketTemplateId)
        {
            using (var context = DepartmentUserManager.GetContext)
            {
                var body = context.TicketTemplateBodies.FirstOrDefault(x => x.TicketTemplateId == ticketTemplateId);
                if (body != null)
                {
                    body.BodyFormat = GetElementaryUnitById(body.BodyFormatId);
                    body.TicketTemplateParagraphs = GetTicketTemplateParagraphs(bodyId: body.Id);
                    body.TicketTemplateTables = GetTicketTemplateTables(bodyId: body.Id);
                }
                return body;
            }
        }

        private static List<TicketTemplateTable> GetTicketTemplateTables(Guid? bodyId)
        {
            if (bodyId.HasValue)
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    List<TicketTemplateTable> tabels = context.TicketTemplateTables.Where(x => x.TicketTemplateBodyId == bodyId && !x.IsDeleted).OrderBy(x => x.Order).ToList();
                    foreach (var tabel in tabels)
                    {
                        tabel.Properties = GetElementaryUnitById(tabel.PropertiesId);
                        tabel.Columns = GetElementaryUnitById(tabel.ColumnsId);
                        tabel.TicketTemplateTableRows = context.TicketTemplateTableRows.Where(x => x.TicketTemplateTableId == tabel.Id && !x.IsDeleted).OrderBy(x => x.Order).ToList();
                        foreach (var row in tabel.TicketTemplateTableRows)
                        {
                            row.Properties = GetElementaryUnitById(row.PropertiesId);
                            row.TicketTemplateElementaryAttributes = context.TicketTemplateElementaryAttributes.Where(x => x.TicketTemplateTableRowId == row.Id && !x.IsDeleted).ToList();
                            row.TicketTemplateTableCells = context.TicketTemplateTableCells.Where(x => x.TicketTemplateTableRowId == row.Id && !x.IsDeleted).OrderBy(x => x.Order).ToList();
                            foreach (var cell in row.TicketTemplateTableCells)
                            {
                                cell.Properties = GetElementaryUnitById(cell.PropertiesId);
                                cell.TicketTemplateParagraphs = GetTicketTemplateParagraphs(cellId: cell.Id);
                            }
                        }
                    }

                    return tabels;
                }
            }

            return null;
        }

        private static List<TicketTemplateParagraph> GetTicketTemplateParagraphs(Guid? bodyId = null, Guid? cellId = null)
        {
            List<TicketTemplateParagraph> paragraphs = null;
            using (var context = DepartmentUserManager.GetContext)
            {
                if (bodyId.HasValue)
                {
                    paragraphs = context.TicketTemplateParagraphs.Where(x => x.TicketTemplateBodyId == bodyId && !x.IsDeleted).OrderBy(x => x.Order).ToList();
                }
                if (cellId.HasValue)
                {
                    paragraphs = context.TicketTemplateParagraphs.Where(x => x.TicketTemplateTableCellId == cellId && !x.IsDeleted).OrderBy(x => x.Order).ToList();
                }

                if (paragraphs != null)
                {
                    foreach (var paragraph in paragraphs)
                    {
                        paragraph.ParagraphFormat = GetElementaryUnitById(paragraph.ParagraphFormatId);
                        paragraph.TicketTemplateElementaryAttributes = context.TicketTemplateElementaryAttributes.Where(x => x.TicketTemplateParagraphId == paragraph.Id && !x.IsDeleted).ToList();
                        paragraph.TicketTemplateParagraphDatas = context.TicketTemplateParagraphDatas.Where(x => x.TicketTemplateParagraphId == paragraph.Id && !x.IsDeleted).OrderBy(x => x.Order).ToList();
                        foreach (var data in paragraph.TicketTemplateParagraphDatas)
                        {
                            data.Font = GetElementaryUnitById(data.FontId);
                            data.TicketTemplateElementaryAttributes = context.TicketTemplateElementaryAttributes.Where(x => x.TicketTemplateParagraphDataId == data.Id && !x.IsDeleted).ToList();
                            data.TicketTemplateElementaryUnits = context.TicketTemplateElementaryUnits.Where(x => x.TicketTemplateParagraphDataId == data.Id && !x.IsDeleted).OrderBy(x => x.Order).ToList();
                            foreach (var unit in data.TicketTemplateElementaryUnits)
                            {
                                GetSubElems(unit);
                            }
                        }
                    }

                    return paragraphs;
                }
            }
            return paragraphs;
        }

        /// <summary>
        /// Поулчение элемента по идентификатору
        /// </summary>
        /// <param name="context"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        private static TicketTemplateElementaryUnit GetElementaryUnitById(Guid? Id)
        {
            if (Id.HasValue)
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.TicketTemplateElementaryUnits.FirstOrDefault(x => x.Id == Id);
                    GetSubElems(entity);

                    return entity;
                }
            }

            return null;
        }

        /// <summary>
        /// Получение дочерних элементов и атрибутов элемента
        /// </summary>
        /// <param name="context"></param>
        /// <param name="unit"></param>
        private static void GetSubElems(TicketTemplateElementaryUnit unit)
        {
            if (unit != null)
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    unit.TicketTemplateElementaryAttributes = context.TicketTemplateElementaryAttributes.Where(x => x.TicketTemplateElementaryUnitId == unit.Id && !x.IsDeleted).ToList();
                    unit.ChildElementaryUnits = context.TicketTemplateElementaryUnits.Where(x => x.ParentElementaryUnitId == unit.Id && !x.IsDeleted).OrderBy(x => x.Order).ToList();
                    foreach (var child in unit.ChildElementaryUnits)
                    {
                        GetSubElems(child);
                    }
                }
            }
        }
    }
}