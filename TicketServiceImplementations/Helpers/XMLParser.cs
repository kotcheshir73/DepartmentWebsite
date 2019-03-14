using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using TicketModels.Models;

namespace TicketServiceImplementations.Helpers
{
    public static class XMLParser
    {
        private static List<TicketTemplateElementaryAttribute> GetAttributes(XmlNode xmlNode, Guid? TicketTemplateElementaryUnitId = null, Guid? TicketTemplateParagraphDataId = null, 
            Guid? TicketTemplateParagraphId = null, Guid? TicketTemplateTableRowId = null)
        {
            if (!TicketTemplateElementaryUnitId.HasValue && !TicketTemplateParagraphDataId.HasValue && !TicketTemplateParagraphId.HasValue && !TicketTemplateTableRowId.HasValue)
            {
                throw new Exception("Не передан ни один идентификатор для связки Attribute");
            }
            if (xmlNode.Attributes != null && xmlNode.Attributes.Count > 0)
            {
                List<TicketTemplateElementaryAttribute> attributes = new List<TicketTemplateElementaryAttribute>();
                foreach (XmlAttribute attr in xmlNode.Attributes)
                {
                    attributes.Add(new TicketTemplateElementaryAttribute
                    {
                        Name = attr.Name,
                        Value = attr.Value,
                        TicketTemplateElementaryUnitId = TicketTemplateElementaryUnitId,
                        TicketTemplateParagraphDataId = TicketTemplateParagraphDataId,
                        TicketTemplateParagraphId = TicketTemplateParagraphId,
                        TicketTemplateTableRowId = TicketTemplateTableRowId
                    });
                }
                return attributes;
            }
            return null;
        }

        private static List<TicketTemplateElementaryUnit> GetChilds(XmlNode xmlNode, Guid ParentElementaryUnitId)
        {
            if (xmlNode.ChildNodes != null && xmlNode.ChildNodes.Count > 0)
            {
                List<TicketTemplateElementaryUnit> childNodes = new List<TicketTemplateElementaryUnit>();
                int order = 0;
                foreach (XmlNode elem in xmlNode.ChildNodes)
                {
                    childNodes.Add(GetElementaryUnit(elem, order++, false, ParentElementaryUnitId: ParentElementaryUnitId));
                }

                return childNodes;
            }
            return null;
        }

        private static TicketTemplateElementaryUnit GetElementaryUnit(XmlNode xmlNode, int order, bool notLink, Guid? TicketTemplateParagraphDataId = null, Guid? ParentElementaryUnitId = null)
        {
            if(!notLink && !TicketTemplateParagraphDataId.HasValue && !ParentElementaryUnitId.HasValue)
            {
                throw new Exception("Не передан ни один идентификатор для связки ElementaryUnit");
            }
            TicketTemplateElementaryUnit node = new TicketTemplateElementaryUnit
            {
                Name = xmlNode.Name,
                Value = xmlNode.Value,
                Order = order,
                TicketTemplateParagraphDataId = TicketTemplateParagraphDataId,
                ParentElementaryUnitId = ParentElementaryUnitId
            };
            node.TicketTemplateElementaryAttributes = GetAttributes(xmlNode, TicketTemplateElementaryUnitId: node.Id);
            node.ChildElementaryUnits = GetChilds(xmlNode, ParentElementaryUnitId: node.Id);

            return node;
        }

        private static TicketTemplateParagraphData GetParagraphData(XmlNode xmlNode, int order, Guid TicketTemplateParagraphId)
        {
            if (xmlNode.LocalName != TicketTemplateConstant.ParagraphDataName)
            {
                return null;
            }
            TicketTemplateParagraphData paragraphData = new TicketTemplateParagraphData
            {
                Name = xmlNode.Name,
                Order = order,
                TicketTemplateParagraphId = TicketTemplateParagraphId,
            };
            paragraphData.TicketTemplateElementaryAttributes = GetAttributes(xmlNode, TicketTemplateParagraphDataId: paragraphData.Id);

            int nodeOrder = 0;
            foreach (XmlNode node in xmlNode.ChildNodes)
            {
                if (node.LocalName == TicketTemplateConstant.ParagraphFontName)
                {
                    paragraphData.Font = GetElementaryUnit(node, 0, true);
                    paragraphData.FontId = paragraphData.Font.Id;
                }
                else if (node.LocalName == TicketTemplateConstant.ParagraphTextName)
                {
                    paragraphData.TextName = node.Name;
                    paragraphData.Text = node.InnerXml;
                }
                else
                {
                    if (paragraphData.TicketTemplateElementaryUnits == null)
                    {
                        paragraphData.TicketTemplateElementaryUnits = new List<TicketTemplateElementaryUnit>();
                    }
                    paragraphData.TicketTemplateElementaryUnits.Add(GetElementaryUnit(node, nodeOrder++, false, TicketTemplateParagraphDataId: paragraphData.Id));
                }
            }

            return paragraphData;
        }

        /// <summary>
        /// Убираем лишние данные
        /// </summary>
        /// <param name="list"></param>
        private static void AnalysisDatas(List<TicketTemplateParagraphData> list)
        {
            if (list != null)
            {
                for (int i = 0; i < list.Count; ++i)
                {
                    for (int j = i + 1; j < list.Count; ++j)
                    {
                        var dataFirst = list[i];
                        var dataSecond = list[j];
                        if (dataFirst.Font == null && dataSecond.Font != null)
                        {
                            dataFirst.Font = dataSecond.Font;
                        }
                        if (dataFirst.Font != null && dataSecond.Font == null)
                        {
                            dataSecond.Font = dataFirst.Font;
                        }
                        if (dataFirst.Font != null && dataSecond.Font != null)
                        {
                            var brElemFirst = dataFirst.TicketTemplateElementaryUnits?.FirstOrDefault(x => x.Name == "w:br");
                            var brElemSecond = dataSecond.TicketTemplateElementaryUnits?.FirstOrDefault(x => x.Name == "w:br");
                            if (brElemFirst != null && brElemSecond == null || brElemFirst == null && brElemSecond != null)
                            {
                                break;
                            }
                            var tabElemFirst = dataFirst.TicketTemplateElementaryUnits?.FirstOrDefault(x => x.Name == "w:tab");
                            var tabElemSecond = dataSecond.TicketTemplateElementaryUnits?.FirstOrDefault(x => x.Name == "w:tab");
                            if (tabElemFirst != null && tabElemSecond == null || tabElemFirst == null && tabElemSecond != null)
                            {
                                break;
                            }

                            var bElemFirst = dataFirst.Font.ChildElementaryUnits?.FirstOrDefault(x => x.Name == "w:b");
                            var bElemSecond = dataSecond.Font.ChildElementaryUnits?.FirstOrDefault(x => x.Name == "w:b");
                            if (bElemFirst != null && bElemSecond == null || bElemFirst == null && bElemSecond != null)
                            {
                                break;
                            }
                            var rSizeFirst = dataFirst.Font?.ChildElementaryUnits?.FirstOrDefault(x => x.Name.StartsWith("w:sz"))?
                                .TicketTemplateElementaryAttributes?.FirstOrDefault(x => x.Name == "w:val");
                            var rSizeSecond = dataSecond.Font?.ChildElementaryUnits?.FirstOrDefault(x => x.Name.StartsWith("w:sz"))?
                                .TicketTemplateElementaryAttributes?.FirstOrDefault(x => x.Name == "w:val");
                            if (rSizeFirst != null && rSizeSecond != null)
                            {
                                if (rSizeFirst.Value != rSizeSecond.Value)
                                {
                                    break;
                                }
                            }

                            dataFirst.Text += dataSecond.Text;
                            if (dataFirst.TicketTemplateElementaryUnits != null && dataSecond.TicketTemplateElementaryUnits != null)
                            {
                                dataFirst.TicketTemplateElementaryUnits.AddRange(dataSecond.TicketTemplateElementaryUnits);
                            }
                            else if (dataFirst.TicketTemplateElementaryUnits == null && dataSecond.TicketTemplateElementaryUnits != null)
                            {
                                dataFirst.TicketTemplateElementaryUnits = dataSecond.TicketTemplateElementaryUnits;
                            }

                            list.Remove(dataSecond);
                            j--;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        private static TicketTemplateParagraph GetParagraph(XmlNode xmlNode, int order, Guid? TicketTemplateBodyId = null, Guid? TicketTemplateTableCellId = null)
        {
            if (!TicketTemplateBodyId.HasValue && !TicketTemplateTableCellId.HasValue)
            {
                throw new Exception("Не передан ни один идентификатор для связки Paragraph");
            }
            if (xmlNode.LocalName != TicketTemplateConstant.ParagraphName)
            {
                return null;
            }
            TicketTemplateParagraph paragraph = new TicketTemplateParagraph
            {
                Name = xmlNode.Name,
                Order = order,
                TicketTemplateBodyId = TicketTemplateBodyId,
                TicketTemplateTableCellId = TicketTemplateTableCellId,
                TicketTemplateParagraphDatas = new List<TicketTemplateParagraphData>()
            };
            paragraph.TicketTemplateElementaryAttributes = GetAttributes(xmlNode, TicketTemplateParagraphId: paragraph.Id);

            int nodeOrder = 0;
            foreach (XmlNode node in xmlNode.ChildNodes)
            {
                if (node.LocalName == TicketTemplateConstant.ParagraphFormatName)
                {
                    paragraph.ParagraphFormat = GetElementaryUnit(node, 0, true);
                    paragraph.ParagraphFormatId = paragraph.ParagraphFormat.Id;
                }
                else if (node.LocalName == TicketTemplateConstant.ParagraphDataName)
                {
                    paragraph.TicketTemplateParagraphDatas.Add(GetParagraphData(node, nodeOrder++, TicketTemplateParagraphId: paragraph.Id));
                }
            }

            AnalysisDatas(paragraph.TicketTemplateParagraphDatas);

            return paragraph;
        }

        private static TicketTemplateTableCell GetTableCell(XmlNode xmlNode, int order, Guid TicketTemplateTableRowId)
        {
            if (xmlNode.LocalName != TicketTemplateConstant.CellName)
            {
                return null;
            }
            TicketTemplateTableCell cell = new TicketTemplateTableCell
            {
                Name = xmlNode.Name,
                Order = order,
                TicketTemplateTableRowId = TicketTemplateTableRowId,
                TicketTemplateParagraphs = new List<TicketTemplateParagraph>()
            };

            int nodeCount = 0;
            foreach (XmlNode node in xmlNode.ChildNodes)
            {
                if (node.LocalName == TicketTemplateConstant.CellPropertyName)
                {
                    cell.Properties = GetElementaryUnit(node, 0, true);
                    cell.PropertiesId = cell.Properties.Id;
                }
                else if (node.LocalName == TicketTemplateConstant.ParagraphName)
                {
                    cell.TicketTemplateParagraphs.Add(GetParagraph(node, nodeCount++, TicketTemplateTableCellId: cell.Id));
                }
            }

            return cell;
        }

        private static TicketTemplateTableRow GetTableRow(XmlNode xmlNode, int order, Guid TicketTemplateTableId)
        {
            if (xmlNode.LocalName != TicketTemplateConstant.RowName)
            {
                return null;
            }
            TicketTemplateTableRow row = new TicketTemplateTableRow
            {
                Name = xmlNode.Name,
                Order = order,
                TicketTemplateTableId = TicketTemplateTableId,
                TicketTemplateTableCells = new List<TicketTemplateTableCell>()
            };
            row.TicketTemplateElementaryAttributes = GetAttributes(xmlNode, TicketTemplateTableRowId: row.Id);

            int nodeCount = 0;
            foreach (XmlNode node in xmlNode.ChildNodes)
            {
                if (node.LocalName == TicketTemplateConstant.RowPropertyName)
                {
                    row.Properties = GetElementaryUnit(node, 0, true);
                    row.PropertiesId = row.Properties.Id;
                }
                else if (node.LocalName == TicketTemplateConstant.CellName)
                {
                    row.TicketTemplateTableCells.Add(GetTableCell(node, nodeCount++, row.Id));
                }
            }

            return row;
        }

        private static TicketTemplateTable GetTable(XmlNode xmlNode, int order, Guid TicketTemplateBodyId)
        {
            if (xmlNode.LocalName != TicketTemplateConstant.TableName)
            {
                return null;
            }
            TicketTemplateTable table = new TicketTemplateTable
            {
                Name = xmlNode.Name,
                Order = order,
                TicketTemplateBodyId = TicketTemplateBodyId,
                TicketTemplateTableRows = new List<TicketTemplateTableRow>()
            };

            int nodeCount = 0;
            foreach (XmlNode node in xmlNode.ChildNodes)
            {
                if (node.LocalName == TicketTemplateConstant.TablePropertyName)
                {
                    table.Properties = GetElementaryUnit(node, 0, true);
                    table.PropertiesId = table.Properties.Id;
                }
                else if (node.LocalName == TicketTemplateConstant.TableGridName)
                {
                    table.Columns = GetElementaryUnit(node, 0, true);
                    table.ColumnsId = table.Columns.Id;
                }
                else if (node.LocalName == TicketTemplateConstant.RowName)
                {
                    table.TicketTemplateTableRows.Add(GetTableRow(node, nodeCount++, table.Id));
                }
            }

            return table;
        }

        public static TicketTemplateBody GetBody(string xmlText, Guid ticketTemplateId)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlText);

            // ищем тело документа
            foreach (XmlNode elem in xmlDoc.DocumentElement.ChildNodes)
            {
                if (elem.LocalName == "body")
                {
                    TicketTemplateBody body = new TicketTemplateBody
                    {
                        BodyName = elem.Name,
                        SectName = elem.FirstChild.Name,
                        TicketTemplateId = ticketTemplateId,
                        TicketTemplateParagraphs = new List<TicketTemplateParagraph>(),
                        TicketTemplateTables = new List<TicketTemplateTable>()
                    };
                    int order = 0;
                    // получаем элемент sect 
                    foreach (XmlNode paragpraphXML in elem.FirstChild.ChildNodes)
                    {
                        if (paragpraphXML.LocalName == TicketTemplateConstant.ParagraphName)
                        {
                            body.TicketTemplateParagraphs.Add(GetParagraph(paragpraphXML, order++, TicketTemplateBodyId: body.Id));
                        }
                        else if (paragpraphXML.LocalName == TicketTemplateConstant.TableName)
                        {
                            body.TicketTemplateTables.Add(GetTable(paragpraphXML, order++, body.Id));
                        }
                        else if (paragpraphXML.LocalName == TicketTemplateConstant.BodySectName)
                        {
                            body.BodyFormat = GetElementaryUnit(paragpraphXML, 0, true);
                            body.BodyFormatId = body.BodyFormat.Id;
                        }
                    }

                    return body;
                }
            }

            return null;
        }
    }
}