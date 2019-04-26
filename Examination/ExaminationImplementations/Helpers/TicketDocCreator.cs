using Models.Examination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TicketServiceImplementations.Helpers
{
    public class TicketDocCreator
    {
        private ExaminationTemplate _examinationTemplate;

        private ExaminationTemplateTicket _examinationTemplateTicket;

        private List<ExaminationTemplateTicketQuestion> _examinationTemplateTicketQuestions;

        private List<ExaminationTemplateBlock> _examinationTemplateBlocks;

        private int _counter = 0;

        private string _val = "";

        private string ReplacmentText(string maket)
        {
            if (maket.StartsWith("question"))
            {
                if (_examinationTemplateBlocks != null && _examinationTemplateBlocks.Count > 0)
                {
                    var block = _examinationTemplateBlocks.FirstOrDefault(x => x.QuestionTagInTemplate == maket);
                    if (block != null)
                    {
                        var question = _examinationTemplateTicketQuestions.FirstOrDefault(x => x.ExaminationTemplateBlockId == block.Id);
                        if (question != null)
                        {
                            _examinationTemplateTicketQuestions.Remove(question);
                            //TODO Image
                            return question.ExaminationTemplateBlockQuestion.QuestionText;
                        }
                    }
                }
            }
            else if (maket.StartsWith("random"))
            {
                if (_examinationTemplateBlocks != null && _examinationTemplateBlocks.Count > 0)
                {
                    var block = _examinationTemplateBlocks.FirstOrDefault(x => x.QuestionTagInTemplate == maket.Split(':')[0]);
                    if (block != null)
                    {
                        var question = _examinationTemplateTicketQuestions.FirstOrDefault(x => x.ExaminationTemplateBlockId == block.Id);
                        if (question != null)
                        {
                            _examinationTemplateTicketQuestions.Remove(question);
                            //TODO Image
                            return question.ExaminationTemplateBlockQuestion.QuestionText;
                        }
                    }
                }
            }
            else if (maket.StartsWith("number"))
            {
                if (_examinationTemplateTicket != null)
                {
                    return _examinationTemplateTicket.TicketNumber.ToString();
                }
            }
            else if (maket.StartsWith("discipline"))
            {
                if (_examinationTemplate != null && _examinationTemplate.Discipline != null)
                {
                    return _examinationTemplate.Discipline.DisciplineName;
                }
            }
            else if (maket.StartsWith("education"))
            {
                if (_examinationTemplate != null && _examinationTemplate.EducationDirection != null)
                {
                    return $"{_examinationTemplate.EducationDirection.Cipher} {_examinationTemplate.EducationDirection.Title}";
                }
            }
            else if (maket.StartsWith("semester"))
            {
                if (_examinationTemplate != null && _examinationTemplate.Semester != null)
                {
                    return _examinationTemplate.Semester.ToString();
                }
            }
            else if (maket.StartsWith("date"))
            {
                return DateTime.Now.ToShortDateString();
            }

            return maket;
        }

        private string GetAttribute(TicketTemplateElementaryAttribute element)
        {
            if (element == null)
            {
                return string.Empty;
            }
            if (string.IsNullOrEmpty(element.Name) || string.IsNullOrEmpty(element.Value))
            {
                return string.Empty;
            }
            return string.Format(" {0}=\"{1}\"", element.Name, element.Value);
        }

        private string GetUnit(TicketTemplateElementaryUnit element)
        {
            if (element == null)
            {
                return string.Empty;
            }
            if (string.IsNullOrEmpty(element.Name))
            {
                return string.Empty;
            }
            StringBuilder attributes = new StringBuilder();
            if (element.TicketTemplateElementaryAttributes != null)
            {
                foreach (var attr in element.TicketTemplateElementaryAttributes)
                {
                    string attrString = GetAttribute(attr);
                    if(element.Name.Contains("w:ilfo") && attr.Name == "w:val")
                    {
                        // Для списокв надо сбрасывать счетчик, чтобы в документе нумерация сбрасывалась в 1
                        if(_val != attr.Value)
                        {
                            _counter++;
                            _val = attr.Value;
                        }
                        attrString = $" w:val=\"{(_counter)}\"";
                    }
                    attributes.Append(attrString);
                }
            }

            if (element.ChildElementaryUnits != null && element.ChildElementaryUnits.Count > 0)
            {
                StringBuilder nodes = new StringBuilder();
                foreach (var child in element.ChildElementaryUnits)
                {
                    nodes.Append(GetUnit(child));
                }

                return string.Format("<{0}{1}>{2}</{0}>", element.Name, attributes.ToString(), nodes.ToString());
            }
            else
            {
                return string.Format("<{0}{1}/>", element.Name, attributes.ToString());
            }
        }

        private string GetParagraptData(TicketTemplateParagraphData element)
        {
            if (element == null)
            {
                return string.Empty;
            }
            if (string.IsNullOrEmpty(element.Name))
            {
                return string.Empty;
            }

            StringBuilder attributes = new StringBuilder();
            if (element.TicketTemplateElementaryAttributes != null)
            {
                foreach (var attr in element.TicketTemplateElementaryAttributes)
                {
                    attributes.Append(GetAttribute(attr));
                }
            }
            StringBuilder inits = new StringBuilder();
            if (element.TicketTemplateElementaryUnits != null)
            {
                foreach (var unit in element.TicketTemplateElementaryUnits.OrderBy(x => x.Order))
                {
                    inits.Append(GetUnit(unit));
                }
            }
            StringBuilder text = new StringBuilder();
            if (!string.IsNullOrEmpty(element.Text))
            {
                string textResult = element.Text;
                Match match = Regex.Match(textResult, @"\{\#[a-z,A-Z,0-9,\:,\,]*\}");
                if (match.Success)
                {
                    string matchValue = match.Value.Substring(2, match.Value.Length - 3);
                    textResult = textResult.Replace(match.Value, ReplacmentText(matchValue));
                }
                text.AppendFormat("<{0}>{1}</{0}>", element.TextName, textResult);
            }

            return string.Format("<{0}{1}>{2}{3}{4}</{0}>", element.Name, attributes.ToString(), GetUnit(element.Font), inits.ToString(), text.ToString());
        }

        private string GetParagraph(TicketTemplateParagraph element)
        {
            if (element == null)
            {
                return string.Empty;
            }
            if (string.IsNullOrEmpty(element.Name))
            {
                return string.Empty;
            }

            StringBuilder attributes = new StringBuilder();
            if (element.TicketTemplateElementaryAttributes != null)
            {
                foreach (var attr in element.TicketTemplateElementaryAttributes)
                {
                    attributes.Append(GetAttribute(attr));
                }
            }
            StringBuilder nodes = new StringBuilder();
            if (element.TicketTemplateParagraphDatas != null)
            {
                foreach (var node in element.TicketTemplateParagraphDatas.OrderBy(x => x.Order))
                {
                    if (node != null)
                    {
                        nodes.Append(GetParagraptData(node));
                    }
                }
            }

            return string.Format("<{0}{1}>{2}{3}</{0}>", element.Name, attributes.ToString(), GetUnit(element.ParagraphFormat), nodes.ToString());
        }

        private string GetTableCell(TicketTemplateTableCell element)
        {
            if (element == null)
            {
                return string.Empty;
            }
            if (string.IsNullOrEmpty(element.Name))
            {
                return string.Empty;
            }

            StringBuilder nodes = new StringBuilder();
            if (element.TicketTemplateParagraphs != null)
            {
                foreach (var node in element.TicketTemplateParagraphs.OrderBy(x => x.Order))
                {
                    if (node != null)
                    {
                        nodes.Append(GetParagraph(node));
                    }
                }
            }

            return string.Format("<{0}>{1}{2}</{0}>", element.Name, GetUnit(element.Properties), nodes.ToString());
        }

        private string GetTableRow(TicketTemplateTableRow element)
        {
            if (element == null)
            {
                return string.Empty;
            }
            if (string.IsNullOrEmpty(element.Name))
            {
                return string.Empty;
            }

            StringBuilder attributes = new StringBuilder();
            if (element.TicketTemplateElementaryAttributes != null)
            {
                foreach (var attr in element.TicketTemplateElementaryAttributes)
                {
                    attributes.Append(GetAttribute(attr));
                }
            }
            StringBuilder nodes = new StringBuilder();
            if (element.TicketTemplateTableCells != null)
            {
                foreach (var node in element.TicketTemplateTableCells.OrderBy(x => x.Order))
                {
                    if (node != null)
                    {
                        nodes.Append(GetTableCell(node));
                    }
                }
            }

            return string.Format("<{0}{1}>{2}{3}</{0}>", element.Name, attributes.ToString(), GetUnit(element.Properties), nodes.ToString());
        }

        private string GetTabel(TicketTemplateTable element)
        {
            if (element == null)
            {
                return string.Empty;
            }
            if (string.IsNullOrEmpty(element.Name))
            {
                return string.Empty;
            }

            StringBuilder nodes = new StringBuilder();
            if (element.TicketTemplateTableRows != null)
            {
                foreach (var node in element.TicketTemplateTableRows.OrderBy(x => x.Order))
                {
                    if (node != null)
                    {
                        nodes.Append(GetTableRow(node));
                    }
                }
            }

            return string.Format("<{0}>{1}{2}{3}</{0}>", element.Name, GetUnit(element.Properties), GetUnit(element.Columns), nodes.ToString());
        }

        public string GetBody(TicketTemplateBody element, ExaminationTemplate examinationTemplate, ExaminationTemplateTicket examinationTemplateTicket,
            List<ExaminationTemplateTicketQuestion> examinationTemplateTicketQuestions, List<ExaminationTemplateBlock> examinationTemplateBlocks)
        {
            if (element == null)
            {
                return string.Empty;
            }
            _examinationTemplate = examinationTemplate;
            _examinationTemplateTicket = examinationTemplateTicket;
            _examinationTemplateTicketQuestions = examinationTemplateTicketQuestions;
            _examinationTemplateBlocks = examinationTemplateBlocks;

            StringBuilder sb = new StringBuilder();

            var paragraphs = element.TicketTemplateParagraphs?.OrderBy(x => x.Order).ToList() ?? new List<TicketTemplateParagraph>();
            var tables = element.TicketTemplateTables?.OrderBy(x => x.Order).ToList() ?? new List<TicketTemplateTable>();

            for (int i = 0; i < paragraphs.Count + tables.Count; ++i)
            {
                var paragraph = paragraphs.FirstOrDefault(x => x.Order == i);
                if (paragraph != null)
                {
                    sb.Append(GetParagraph(paragraph));
                }
                var table = tables.FirstOrDefault(x => x.Order == i);
                if (table != null)
                {
                    sb.Append(GetTabel(table));
                }
            }

            return sb.ToString();
        }

        public string GetBodyFormat(TicketTemplateBody element)
        {
            if (element == null)
            {
                return string.Empty;
            }

            return GetUnit(element.BodyFormat);
        }
    }
}