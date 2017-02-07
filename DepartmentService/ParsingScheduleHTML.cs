using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using HtmlAgilityPack;

namespace DepartmentService
{
    public class ParsingScheduleHTML
    {
        private string[] days = new string[] { "Пнд", "Втр", "Срд", "Чтв", "Птн", "Сбт" };

        public string Parsing(string scheduleUrl, string[] groupNames)
        {
            WebClient web = new WebClient();
            web.Encoding = UTF8Encoding.Default;

            string strHTML = web.DownloadString(scheduleUrl + "raspisan.htm");

            HtmlDocument document = new HtmlDocument();

            document.LoadHtml(strHTML);

            List<string> htmlPages = new List<string>();
            var nodes = document.DocumentNode.SelectNodes("//table/tr/td");
            foreach (var node in nodes)
            {
                int n = node.InnerText.IndexOf("\r\n");
                string group = node.InnerText.Remove(n, 2);
                if (groupNames.Contains(group))
                {
                    var elem = node.ChildNodes.FirstOrDefault(e => e.Name.ToLower() == "a");
                    if (elem != null)
                    {
                        htmlPages.Add(elem.Attributes.First().Value);
                    }
                }
            }
            foreach (var page in htmlPages)
            {
                ParsingPage(scheduleUrl + page);
            }
            return "";
        }

        private void ParsingPage(string schedulrUrl)
        {
            WebClient web = new WebClient();
            web.Encoding = UTF8Encoding.Default;
            string pageHTML = web.DownloadString(schedulrUrl);
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(pageHTML);
            var pageNodes = document.DocumentNode.SelectNodes("//table/tr/td");
            int week = -1;
            int day = -1;
            foreach (var pageNode in pageNodes)
            {
                string text = pageNode.InnerText.Remove(0, 2);
                if (days.Contains(text))
                {
                    if (days[0].Contains(text))
                    {
                        week++;
                    }
                    day++;
                }
                if (week > -1)
                {
                    var elem = pageNode.ChildNodes.First().NextSibling;
                    if (elem.Name.ToLower() == "font")
                    {
                        int n = pageNode.InnerText.IndexOf("\r\n");
                        var lesson = pageNode.InnerText.Remove(n, 2).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (lesson[0] == "_")
                        {
                            continue;
                        }
                        var discipline = "";
                        var teacher = "";
                        var classroom = "";
                        int i = 0;
                        for (; i < lesson.Length; ++i)
                        {
                            if (lesson[i].ToUpper() != lesson[i] || lesson[i].Length < 2)
                            {
                                discipline += lesson[i] + " ";
                            }
                            else
                                break;
                        }
                        if (i < lesson.Length - 3)
                        {
                            teacher = lesson[i++] + " " + lesson[i++] + "." + lesson[i++] + ".";
                        }
                        if (i < lesson.Length)
                        {
                            if (lesson[i] == "-")
                            {
                                i++;
                            }
                            classroom = lesson[i];
                        }

                    }
                }
            }
        }
    }
}
