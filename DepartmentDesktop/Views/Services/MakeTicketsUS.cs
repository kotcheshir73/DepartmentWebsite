using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TicketModels.Models;

namespace DepartmentDesktop.Controllers
{
    public partial class MakeTicketsUS : UserControl
    {
        /// <summary>
        /// Файловый диалог с путем до файла-шаблона и его именем (чтобы потом туда же билеты кинуть)
        /// </summary>
        private OpenFileDialog _ofd;
        /// <summary>
        /// Сколько требуется вопросов вставлять. Ключ - группа вопросов, значение - список вопросов
        /// В билете вместо ключа будет подставляться вопрос из списка
        /// </summary>
        private Dictionary<string, List<string>> _listQuestions;
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, int> _listCountQuestions;

        //private TicketPageSetup _ticketPageSetup;

        //private List<TicketParagraph> _ticketParagraphs;

        //private TicketTable _ttable;

        public MakeTicketsUS()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Загрузка шаблона в программу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLoadTemplate_Click(object sender, EventArgs e)
        {
            _ofd = new OpenFileDialog
            {
                Filter = "doc|*.doc|docx|*.docx"
            };
            //if (_ofd.ShowDialog() == DialogResult.OK)
            //{
            //    Spire.Doc.Document doc = new Spire.Doc.Document();
            //    doc.LoadFromFile(_ofd.FileName);
            //    doc.SaveToFile(_ofd.FileName + ".xml", Spire.Doc.FileFormat.Xml);
            //    try
            //    {
            //        tabControlTemplates.TabPages.Clear();

            //        TabPage tabPage = new TabPage();
            //        tabPage.Text = "Шаблон документа";
            //        RichTextBox rtb = new RichTextBox();//вывод шаблона для просмотра
            //        rtb.Dock = DockStyle.Fill;
            //        Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();
            //        try
            //        {
            //            object missing = System.Reflection.Missing.Value;
            //            Document document = winword.Documents.Open(_ofd.FileName, ref missing, true, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
            //                ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);

                        
            //            _ticketPageSetup = new TicketPageSetup()
            //            {
            //                PaperSize = document.PageSetup.PaperSize,
            //                BottomMargin = document.PageSetup.BottomMargin,
            //                LeftMargin = document.PageSetup.LeftMargin,
            //                RightMargin = document.PageSetup.RightMargin,
            //                TopMargin = document.PageSetup.TopMargin,
            //                PageHeight = document.PageSetup.PageHeight,
            //                PageWidth = document.PageSetup.PageWidth,
            //                Orientation = document.PageSetup.Orientation,
            //                VerticalAlignment = document.PageSetup.VerticalAlignment
            //            };
            //            _ticketParagraphs = new List<TicketParagraph>();

            //            Paragraphs wordparagraphs = document.Paragraphs;
            //            if (_listQuestions != null)
            //            {
            //                _listQuestions.Clear();
            //                _listCountQuestions.Clear();
            //            }
            //            _listQuestions = new Dictionary<string, List<string>>();
            //            _listCountQuestions = new Dictionary<string, int>();
            //            for (int i = 0; i < wordparagraphs.Count; ++i)
            //            {
            //                if (wordparagraphs[i + 1].Range.Text != "\r")
            //                {
            //                    TicketParagraphFont ticketParagraphFont = new TicketParagraphFont(wordparagraphs[i + 1].Range.Font);

            //                    TicketParagraphFormat ticketParagraphSetup = new TicketParagraphFormat(wordparagraphs[i + 1].Range.ParagraphFormat);

            //                    TicketParagraph ticketParagraph = new TicketParagraph
            //                    {
            //                        TicketParagraphFont = ticketParagraphFont,
            //                        TicketParagraphFormat = ticketParagraphSetup,
            //                        Text = wordparagraphs[i + 1].Range.Text
            //                    };
            //                    _ticketParagraphs.Add(ticketParagraph);
            //                    string str = wordparagraphs[i + 1].Range.Text;
            //                    if (str.Contains('#') && str.Contains('{') && str.Contains('}'))
            //                    {//ищем строчки с шаблонным текстом
            //                        string st = Regex.Match(str, @"\{([^=;]*)\}").Groups[0].Value;
            //                        if (st.Contains("question"))
            //                        {//если шаблон - это место вставки вопроса
            //                            if (!_listQuestions.Keys.ToList().Contains(st))
            //                            {//если вопрос с таким номером не появлялся, значит новая группа вопросов
            //                                _listQuestions.Add(st, new List<string>());
            //                                _listCountQuestions.Add(st, 1);
            //                            }
            //                            else
            //                            {
            //                                _listCountQuestions[st]++;
            //                            }
            //                        }
            //                    }
            //                    switch (wordparagraphs[i + 1].Range.ParagraphFormat.Alignment)
            //                    {//выравнивание текста, как в документе
            //                        case WdParagraphAlignment.wdAlignParagraphCenter:
            //                            rtb.SelectionAlignment = HorizontalAlignment.Center;
            //                            break;
            //                        case WdParagraphAlignment.wdAlignParagraphLeft:
            //                            rtb.SelectionAlignment = HorizontalAlignment.Left;
            //                            break;
            //                        case WdParagraphAlignment.wdAlignParagraphRight:
            //                            rtb.SelectionAlignment = HorizontalAlignment.Right;
            //                            break;
            //                        case WdParagraphAlignment.wdAlignParagraphJustify:
            //                            rtb.SelectionAlignment = HorizontalAlignment.Left;
            //                            break;
            //                    }
            //                    rtb.AppendText(str);
            //                }
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            return;
            //        }
            //        finally
            //        {
            //            winword.Quit(WdSaveOptions.wdPromptToSaveChanges, WdOriginalFormat.wdWordDocument, Type.Missing);
            //        }

            //        rtb.ReadOnly = true;
            //        tabPage.Controls.Add(rtb);
            //        tabControlTemplates.TabPages.Add(tabPage);

            //        for (int i = 0; i < _listQuestions.Count; ++i)
            //        {//на каждую группу вопросов делаем вкладки для загрузки списка вопрсов
            //            tabPage = new TabPage();
            //            tabPage.Text = "Вопрос " + (i + 1);
            //            Button button = new Button();
            //            button.Text = "Загрузить вопросы " + (i + 1);
            //            button.Left = 5;
            //            button.Top = 5;
            //            button.Height = 25;
            //            button.Width = 120;
            //            button.Tag = i + 1;
            //            button.Click += buttonLoad_Click;
            //            tabPage.Controls.Add(button);

            //            ListBox listBox = new ListBox();
            //            listBox.Width = tabPage.Width - 10;
            //            listBox.Height = tabPage.Height - 35;
            //            listBox.Left = 5;
            //            listBox.Top = 35;
            //            listBox.Anchor = ((AnchorStyles)((((AnchorStyles.Top
            //                | AnchorStyles.Bottom)
            //                | AnchorStyles.Left)
            //                | AnchorStyles.Right)));
            //            tabPage.Controls.Add(listBox);

            //            tabControlTemplates.TabPages.Add(tabPage);
            //        }
            //        buttonMakeTickets.Enabled = true;
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
        }
        /// <summary>
        /// Загрузка списка вопросов в программу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "txt file|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                int tag = Convert.ToInt32(((Button)sender).Tag);
                ListBox lb = (ListBox)tabControlTemplates.TabPages[tag].Controls[1];
                lb.Items.Clear();

                string key = _listQuestions.Keys.ToList()[tag - 1];
                _listQuestions[key].Clear();
                try
                {
                    using (StreamReader reader = new StreamReader(ofd.FileName))
                    {
                        string str;
                        while ((str = reader.ReadLine()) != null)
                        {
                            lb.Items.Add(str);
                            _listQuestions[key].Add(str);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _listQuestions[key].Clear();
                    lb.Items.Clear();
                }
            }
        }
        /// <summary>
        /// Генерация билетов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMakeTickets_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Сформировать билеты?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int countTikets = 0;
                //узнаем сколько можем сформировать билетов
                for (int i = 0; i < _listQuestions.Count; ++i)
                {
                    int count = _listQuestions[_listQuestions.Keys.ToList()[i]].Count / _listCountQuestions[_listQuestions.Keys.ToList()[i]];
                    if (countTikets == 0)
                    {
                        countTikets = count;
                    }
                    else if (count < countTikets)
                    {
                        countTikets = count;
                    }
                }
                //if (countTikets == 0)
                //{
                //    MessageBox.Show("Невозможно сформировать билеты, по одному из вопросов нет вариантов", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                try
                {
                    Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();
                    object missing = System.Reflection.Missing.Value;

                    Document documentTickets = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);
                    //if (_ticketPageSetup != null)
                    //{
                    //    documentTickets.PageSetup.BottomMargin = _ticketPageSetup.BottomMargin;
                    //    documentTickets.PageSetup.LeftMargin = _ticketPageSetup.LeftMargin;
                    //    documentTickets.PageSetup.RightMargin = _ticketPageSetup.RightMargin;
                    //    documentTickets.PageSetup.TopMargin = _ticketPageSetup.TopMargin;
                    //    documentTickets.PageSetup.PageHeight = _ticketPageSetup.PageHeight;
                    //    documentTickets.PageSetup.PageWidth = _ticketPageSetup.PageWidth;
                    //    documentTickets.PageSetup.PaperSize = _ticketPageSetup.PaperSize;
                    //    documentTickets.PageSetup.Orientation = _ticketPageSetup.Orientation;
                    //    documentTickets.PageSetup.VerticalAlignment = _ticketPageSetup.VerticalAlignment;
                    //}
                    progressBar.Minimum = 0;
                    progressBar.Maximum = countTikets;
                    progressBar.Value = 0;
                    Random rnd = new Random();

                    //создать копию _listQuestions

                    //if(_ttable != null)
                    //{
                    //    for(int i = 0; i < 5; ++i)
                    //    {
                    //        _ttable.GetTable(winword, documentTickets);
                    //    }
                    //}

                    //for (int i = 0; i < countTikets; ++i)
                    //{
                    //    progressBar.Value = i;
                    //    for (int j = 0; j < _ticketParagraphs.Count; ++j)
                    //    {
                    //        string str = _ticketParagraphs[j].Text;
                    //        if (str.Contains('#') && str.Contains('{') && str.Contains('}'))
                    //        {
                    //            string st = Regex.Match(str, @"\{([^=;]*)\}").Groups[0].Value;
                    //            if (st.Contains("question"))
                    //            {//подставляем вопрос
                    //                if (_listQuestions.ContainsKey(st))
                    //                {
                    //                    int index = rnd.Next(0, _listQuestions[st].Count);
                    //                    documentTickets.Paragraphs.Add(Type.Missing);
                    //                    documentTickets.Paragraphs[i * _ticketParagraphs.Count + j + 1].Range.Font = _ticketParagraphs[j].TicketParagraphFont?.Font;
                    //                    documentTickets.Paragraphs[i * _ticketParagraphs.Count + j + 1].Range.Text = _ticketParagraphs[j].Text.Replace(st, _listQuestions[st][index]);
                    //                    documentTickets.Paragraphs[i * _ticketParagraphs.Count + j + 1].Range.ParagraphFormat = _ticketParagraphs[j].TicketParagraphFormat.ParagraphFormat;
                    //                    _listQuestions[st].RemoveAt(index);
                    //                    if (!checkBoxGTRemoveQuestions.Checked)
                    //                    {
                    //                        //как только список опустел перезаполнить его, 
                    //                        //если выбрана опиция повторения вопросов в билетах 
                    //                    }
                    //                }
                    //            }
                    //            else if (st.Contains("number"))
                    //            {//подставляем номер билета
                    //                documentTickets.Paragraphs.Add(Type.Missing);
                    //                documentTickets.Paragraphs[i * _ticketParagraphs.Count + j + 1].Range.Font = _ticketParagraphs[j].TicketParagraphFont?.Font;
                    //                documentTickets.Paragraphs[i * _ticketParagraphs.Count + j + 1].Range.Text = _ticketParagraphs[j].Text.Replace(st, (i + 1).ToString());
                    //                documentTickets.Paragraphs[i * _ticketParagraphs.Count + j + 1].Range.ParagraphFormat = _ticketParagraphs[j].TicketParagraphFormat.ParagraphFormat;
                    //            }
                    //            else if (st.Contains("date"))
                    //            {//подставляем номер билета
                    //                documentTickets.Paragraphs.Add(Type.Missing);
                    //                documentTickets.Paragraphs[i * _ticketParagraphs.Count + j + 1].Range.Font = _ticketParagraphs[j].TicketParagraphFont?.Font;
                    //                documentTickets.Paragraphs[i * _ticketParagraphs.Count + j + 1].Range.Text = _ticketParagraphs[j].Text.Replace(st, GetDate());
                    //                documentTickets.Paragraphs[i * _ticketParagraphs.Count + j + 1].Range.ParagraphFormat = _ticketParagraphs[j].TicketParagraphFormat.ParagraphFormat;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            documentTickets.Paragraphs.Add(Type.Missing);
                    //            documentTickets.Paragraphs[i * _ticketParagraphs.Count + j + 1].Range.Font = _ticketParagraphs[j].TicketParagraphFont?.Font;
                    //            documentTickets.Paragraphs[i * _ticketParagraphs.Count + j + 1].Range.Text = _ticketParagraphs[j].Text;
                    //            documentTickets.Paragraphs[i * _ticketParagraphs.Count + j + 1].Range.ParagraphFormat = _ticketParagraphs[j].TicketParagraphFormat.ParagraphFormat;
                    //        }
                    //    }
                    //}
                    //if (checkBoxGTRemoveQuestions.Checked)
                    //{
                    //    //перезаполнить список вопросов для повтороной генерации
                    //}

                    progressBar.Value = countTikets;
                    documentTickets.SaveAs(_ofd.FileName + "_Tickets.doc", WdSaveFormat.wdFormatDocument, false, "", false, "",
                        false, false, false, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing);
                    winword.Quit(WdSaveOptions.wdPromptToSaveChanges, WdOriginalFormat.wdWordDocument, Type.Missing);
                    MessageBox.Show("Сделано", "", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    progressBar.Value = 0;
                }
            }
        }
        /// <summary>
        /// Генерация строки с текущей датой для проставления в билетах
        /// </summary>
        /// <returns></returns>
        private string GetDate()
        {
            string date = "\"";
            if (DateTime.Now.Day < 10)
            {
                date += "0";
            }
            date += DateTime.Now.Day + "\"";
            switch (DateTime.Now.Month)
            {
                case 1: date += " января "; break;
                case 2: date += " февраля "; break;
                case 3: date += " марта "; break;
                case 4: date += " апреля "; break;
                case 5: date += " мая "; break;
                case 6: date += " июня "; break;
                case 7: date += " июля "; break;
                case 8: date += " августа "; break;
                case 9: date += " сентября "; break;
                case 10: date += " октября "; break;
                case 11: date += " ноября "; break;
                case 12: date += " декабря "; break;
            }
            date += DateTime.Now.Year;
            return date;
        }
    }
}
