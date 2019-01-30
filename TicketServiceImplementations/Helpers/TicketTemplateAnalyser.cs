using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TicketModels.Models;

namespace TicketServiceImplementations.Helpers
{
    public static class TicketTemplateAnalyser
    {
        private static Dictionary<string, int> _questions;

        public static Dictionary<string, string> RandomQuestions { get; set; }

        public static Dictionary<string, int> AnalysisBody(TicketTemplateBody body)
        {
            if (_questions == null)
            {
                _questions = new Dictionary<string, int>();
            }
            else
            {
                _questions.Clear();
            }
            if (RandomQuestions == null)
            {
                RandomQuestions = new Dictionary<string, string>();
            }
            else
            {
                RandomQuestions.Clear();
            }
            AnalysisParagraphs(body.TicketTemplateParagraphs);
            if (body.TicketTemplateTables != null)
            {
                foreach (var table in body.TicketTemplateTables)
                {
                    if (table.TicketTemplateTableRows != null)
                    {
                        foreach (var row in table.TicketTemplateTableRows)
                        {
                            if (row.TicketTemplateTableCells != null)
                            {
                                foreach (var cell in row.TicketTemplateTableCells)
                                {
                                    AnalysisParagraphs(cell.TicketTemplateParagraphs);
                                }
                            }
                        }
                    }
                }
            }

            return _questions;
        }

        private static void AnalysisParagraphs(List<TicketTemplateParagraph> Paragraphs)
        {
            if (Paragraphs != null)
            {
                foreach (var paragraph in Paragraphs)
                {
                    if (paragraph.TicketTemplateParagraphDatas != null)
                    {
                        foreach (var data in paragraph.TicketTemplateParagraphDatas)
                        {
                            if (!string.IsNullOrEmpty(data.Text))
                            {
                                string str = data.Text;
                                Match match = Regex.Match(data.Text, @"\{\#[a-z,A-Z,0-9,\:,\,]*\}");
                                if(match.Success)
                                {
                                    string matchValue = match.Value.Substring(2, match.Value.Length - 3);
                                    if (matchValue.StartsWith("question"))
                                    {
                                        if (_questions.ContainsKey(matchValue))
                                        {
                                            _questions[matchValue]++;
                                        }
                                        else
                                        {
                                            _questions.Add(matchValue, 1);
                                        }
                                    }
                                    if(matchValue.StartsWith("random"))
                                    {
                                        string randomName = matchValue.Split(':')[0];
                                        if(!randomName.ToLower().Contains("random"))
                                        {
                                            randomName = $"random{randomName}";
                                        }
                                        if (_questions.ContainsKey(randomName))
                                        {
                                            _questions[randomName]++;
                                        }
                                        else
                                        {
                                            _questions.Add(randomName, 1);
                                        }
                                        if(RandomQuestions.ContainsKey(randomName))
                                        {
                                            RandomQuestions[randomName] = matchValue.Split(':')[1];
                                        }
                                        else
                                        {
                                            RandomQuestions.Add(randomName, matchValue.Split(':')[1]);
                                        }
                                        string[] subMatchValue = matchValue.Split(':')[1].Split(',');
                                        foreach (var subStr in subMatchValue)
                                        {
                                            if (!_questions.ContainsKey(subStr))
                                            {
                                                _questions.Add(subStr, 0);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}