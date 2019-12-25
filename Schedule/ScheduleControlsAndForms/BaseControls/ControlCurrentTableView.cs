using ControlsAndForms.Messangers;
using Enums;
using ScheduleControlsAndForms.Consultation;
using ScheduleControlsAndForms.Examination;
using ScheduleControlsAndForms.Offset;
using ScheduleControlsAndForms.Semester;
using ScheduleInterfaces.BindingModels;
using ScheduleInterfaces.Interfaces;
using ScheduleInterfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Tools;

namespace ScheduleControlsAndForms.BaseControls
{
    public partial class ControlCurrentTableView : UserControl
    {
        private IScheduleProcess _process;

        private readonly int step = 10;

        private readonly DateTime startDate = DateTime.Now.Date.AddHours(8);

        private readonly DateTime finishDate = DateTime.Now.Date.AddHours(21).AddMinutes(30);

        private readonly Color TimeColor = Color.LightGray;

        private event Action LoadRecords;

        private readonly List<int> rowsTime;

        public ControlCurrentTableView()
        {
            InitializeComponent();
            rowsTime = new List<int>();
        }

        public void SetIScheduleProcess(IScheduleProcess process)
        {
            _process = process;
        }

        public event Action EventLoadRecords
        {
            add { LoadRecords += value; }
            remove { LoadRecords -= value; }
        }

        /// <summary>
        /// Дата для консультации/зачета/экзамена
        /// </summary>
        public DateTime? ScheduleDate { get; set; }

        /// <summary>
        /// Формирование столбцов
        /// </summary>
        public void ConfigColumns()
        {
            var columns = (finishDate - startDate).TotalMinutes / step;

            tableLayoutPanel.ColumnCount = (int)columns + 1;
            tableLayoutPanel.ColumnStyles.Clear();
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            for (int i = 1; i < (int)columns + 1; i++)
            {
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1));
            }
        }

        /// <summary>
        /// Отчистка строк
        /// </summary>
        public void ClearTable()
        {
            for (int i = 0; i < tableLayoutPanel.ColumnCount; i++)
            {
                for (int j = 0; j < tableLayoutPanel.RowStyles.Count; j++)
                {
                    Control Control = tableLayoutPanel.GetControlFromPosition(i, j);
                    tableLayoutPanel.Controls.Remove(Control);
                }
            }

            tableLayoutPanel.RowStyles.Clear();
            tableLayoutPanel.RowCount = 0;
        }

        /// <summary>
        /// Загрузка строки с информацией по парам
        /// </summary>
        /// <param name="row"></param>
        public void AddTimeRow(int row)
        {
            rowsTime.Add(row);
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));

            List<DateTime> times = _process.GetScheduleLessonTimes().Result;
            for (int i = 0; i < times.Count; ++i)
            {
                int colspan = 9;
                var buttonWeek = new Button
                {
                    Location = new Point(0, 0),
                    Dock = DockStyle.Fill,
                    Margin = new Padding(0),
                    Text = $"{i + 1} пара\r\n{times[i].ToString("HH:mm")}-{times[i].AddMinutes(90).ToString("HH:mm")}",
                    Tag = times[i].ToString("HH:mm"),
                    BackColor = TimeColor
                };

                if (DateTime.Now >= times[i] && DateTime.Now <= times[i].AddMinutes(90))
                {
                    buttonWeek.BackColor = Color.AliceBlue;
                }

                buttonWeek.Click += ButtonTime_DoubleClick;

                tableLayoutPanel.Controls.Add(buttonWeek, (int)((times[i] - startDate).TotalMinutes / step + 1), row);
                tableLayoutPanel.SetColumnSpan(buttonWeek, colspan);
            }
        }

        /// <summary>
        /// Загрузка пустой строки
        /// </summary>
        public void AddEmptyRow()
        {
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
            var label = new Label
            {
                Location = new Point(0, 0),
                Dock = DockStyle.Fill,
                Margin = new Padding(0)
            };
            tableLayoutPanel.Controls.Add(label, 0, tableLayoutPanel.RowStyles.Count - 1);
        }

        /// <summary>
        /// Добавление контрола в таблицу
        /// </summary>
        /// <param name="control">Контрол</param>
        /// <param name="column">Колонка</param>
        /// <param name="row">Строка</param>
        public void AddRow(Control control, int column, int row)
        {
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
            tableLayoutPanel.Controls.Add(control, column, row);
        }

        /// <summary>
        /// Загрузка дня дл сущности
        /// </summary>
        /// <param name="list"></param>
        /// <param name="row"></param>
        public void LoadDay(List<ScheduleRecordViewModel> list, int row)
        {
            var groups = list.GroupBy(x => new { x.ScheduleDate, x.TimeSpanMinutes }).OrderBy(x => x.Key.ScheduleDate).ToList();
            for (int i = 0; i < groups.Count; i++)
            {
                if (groups[i].Count() > 1)
                {// потоки
                    var records = groups[i].ToList();
                    var control = new Panel
                    {
                        Location = new Point(0, 0),
                        Dock = DockStyle.Fill,
                        Margin = new Padding(0),
                    };
                    // группируем все занятия этой пары по типу занятий и кидаем все на панель (скорее всего, будет 1 кнопка для потокового занятия, 
                    // но может стоять пара и консультация, так как пары по факту нету)
                    var localgroup = records.GroupBy(x => new { x.ScheduleRecordType, x.LessonType });
                    int count = 0;
                    foreach (var local in localgroup)
                    {
                        count++;
                        var button = MakeButton(local.ToList());
                        button.Dock = count == localgroup.Count() ? DockStyle.Fill : DockStyle.Top;
                        control.Controls.Add(button);
                    }

                    var counter = (records[0].ScheduleDate - records[0].ScheduleDate.Date.AddHours(8)).TotalMinutes / step + 1;
                    int colspan = records[0].TimeSpanMinutes / step;

                    tableLayoutPanel.Controls.Add(control, (int)counter, row);
                    if (colspan > 1)
                    {
                        tableLayoutPanel.SetColumnSpan(control, colspan);
                    }
                }
                else
                {
                    SetRecord(groups[i].First(), row);
                }

                if (i + 1 < groups.Count && groups[i].Key.ScheduleDate.AddMinutes(groups[i].Key.TimeSpanMinutes) > groups[i + 1].Key.ScheduleDate)
                {
                    MessageBox.Show("Накладка!");
                    i++;
                }
            }
        }

        /// <summary>
        /// Установка контрола для записи
        /// </summary>
        /// <param name="record"></param>
        /// <param name="row"></param>
        public void SetRecord(ScheduleRecordViewModel record, int row)
        {
            var date = record.ScheduleDate.Date.AddHours(8);

            var counter = (record.ScheduleDate - date).TotalMinutes / step + 1;

            int colspan = record.TimeSpanMinutes / step;
            string text = string.Format("{0} {1} {2}{3}{4}{3}{5}", record.LessonType, record.LessonDiscipline, record.LessonClassroom,
                Environment.NewLine, record.LessonLecturer, record.LessonStudentGroup);

            var buttonRecord = new ButtonShare
            {
                Text = text,
                Id = record.Id,
                ScheduleRecordType = record.ScheduleRecordType,
                ContextMenuStrip = contextMenuStripButton
            };
            buttonRecord.DoubleClick += Button_DoubleClick;

            tableLayoutPanel.Controls.Add(buttonRecord, (int)counter, row);
            if (colspan > 1)
            {
                tableLayoutPanel.SetColumnSpan(buttonRecord, colspan);
            }
        }

        private Control MakeButton(List<ScheduleRecordViewModel> list)
        {
            var classroom = string.Join(",", list.Select(x => x.LessonClassroom).Distinct());
            var disciplione = string.Join(",", list.Select(x => x.LessonDiscipline).Distinct());
            var lecturer = string.Join(",", list.Select(x => x.LessonLecturer).Distinct());
            var studentgroup = string.Join(",", list.Select(x => x.LessonStudentGroup).Distinct());
            var ids = string.Join(",", list.Select(x => x.Id).Distinct());

            var buttonRecord = new ButtonShare
            {
                Text = $"{disciplione} {lecturer} {classroom} {studentgroup}",
                Ids = list.Select(x => x.Id).Distinct().ToList(),
                ScheduleRecordType = list[0].ScheduleRecordType,
                ContextMenuStrip = contextMenuStripButton
            };
            buttonRecord.DoubleClick += Button_DoubleClick;

            return buttonRecord;
        }

        private void AddSemesterRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScheduleSemesterRecordForm form = new ScheduleSemesterRecordForm(_process.GetSemesterRecordService(), _process);
            form.ShowDialog();
            LoadRecords?.Invoke();
        }

        private void AddOffsetRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScheduleOffsetRecordForm form = new ScheduleOffsetRecordForm(_process.GetOffsetRecordService(), _process, scheduleDate: ScheduleDate);
            form.ShowDialog();
            LoadRecords?.Invoke();
        }

        private void AddExaminationRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScheduleExaminationRecordForm form = new ScheduleExaminationRecordForm(_process.GetExaminationRecordService(), _process, scheduleDate: ScheduleDate);
            form.ShowDialog();
            LoadRecords?.Invoke();
        }

        private void AddConsultationRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScheduleConsultationRecordForm form = new ScheduleConsultationRecordForm(_process.GetConsultationRecordService(), _process, scheduleDate: ScheduleDate);
            form.ShowDialog();
            LoadRecords?.Invoke();
        }

        private void Button_DoubleClick(object sender, EventArgs e)
        {
            var control = sender as ButtonShare;
            List<Guid> ids = control.Ids;
            if ((ids == null || ids.Count == 0) && control.Id.HasValue)
            {
                ids = new List<Guid> { control.Id.Value };
            }

            foreach (var id in ids)
            {
                switch (control.ScheduleRecordType)
                {
                    case ScheduleRecordType.Consultation:
                        {
                            ScheduleConsultationRecordForm form = new ScheduleConsultationRecordForm(_process.GetConsultationRecordService(), _process, id: id);
                            form.ShowDialog();
                        }
                        break;
                    case ScheduleRecordType.Examination:
                        {
                            ScheduleExaminationRecordForm form = new ScheduleExaminationRecordForm(_process.GetExaminationRecordService(), _process, id: id);
                            form.ShowDialog();
                        }
                        break;
                    case ScheduleRecordType.Offset:
                        {
                            ScheduleOffsetRecordForm form = new ScheduleOffsetRecordForm(_process.GetOffsetRecordService(), _process, id: id);
                            form.ShowDialog();
                        }
                        break;
                    case ScheduleRecordType.Semester:
                        {
                            ScheduleSemesterRecordForm form = new ScheduleSemesterRecordForm(_process.GetSemesterRecordService(), _process, id: id);
                            form.ShowDialog();
                        }
                        break;
                }
            }
        }

        private void ButtonTime_DoubleClick(object sender, EventArgs e)
        {
            if (ScheduleDate.HasValue)
            {
                var control = sender as Button;
                var hour = Convert.ToInt32(control.Tag.ToString().Split(':')[0]);
                var minute = Convert.ToInt32(control.Tag.ToString().Split(':')[1]);

                ScheduleDate = ScheduleDate.Value.Date.AddHours(hour).AddMinutes(minute);

                List<DateTime> times = _process.GetScheduleLessonTimes().Result;
                foreach (var rowTime in rowsTime)
                {
                    for (int i = 0; i < times.Count; ++i)
                    {
                        var contrl = tableLayoutPanel.GetControlFromPosition((int)((times[i] - startDate).TotalMinutes / step + 1), rowTime);
                        if (contrl != null && (contrl as Button)?.Text == control.Text)
                        {
                            (contrl as Button).BackColor = Color.Gold;
                        }
                        else if (DateTime.Now >= times[i] && DateTime.Now <= times[i].AddMinutes(90))
                        {
                            (contrl as Button).BackColor = Color.AliceBlue;
                        }
                        else
                        {
                            (contrl as Button).BackColor = TimeColor;
                        }
                    }
                }
            }
        }

        private void UpdRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var menu = (sender as ToolStripMenuItem).GetCurrentParent() as ContextMenuStrip;
            var control = menu.SourceControl as ButtonShare;

            List<Guid> ids = control.Ids;
            if ((ids == null || ids.Count == 0) && control.Id.HasValue)
            {
                ids = new List<Guid> { control.Id.Value };
            }

            foreach (var id in ids)
            {
                switch (control.ScheduleRecordType)
                {
                    case ScheduleRecordType.Consultation:
                        {
                            ScheduleConsultationRecordForm form = new ScheduleConsultationRecordForm(_process.GetConsultationRecordService(), _process, id: id);
                            form.ShowDialog();
                        }
                        break;
                    case ScheduleRecordType.Examination:
                        {
                            ScheduleExaminationRecordForm form = new ScheduleExaminationRecordForm(_process.GetExaminationRecordService(), _process, id: id);
                            form.ShowDialog();
                        }
                        break;
                    case ScheduleRecordType.Offset:
                        {
                            ScheduleOffsetRecordForm form = new ScheduleOffsetRecordForm(_process.GetOffsetRecordService(), _process, id: id);
                            form.ShowDialog();
                        }
                        break;
                    case ScheduleRecordType.Semester:
                        {
                            ScheduleSemesterRecordForm form = new ScheduleSemesterRecordForm(_process.GetSemesterRecordService(), _process, id: id);
                            form.ShowDialog();
                        }
                        break;
                }
            }
        }

        private void DelRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var menu = (sender as ToolStripMenuItem).GetCurrentParent() as ContextMenuStrip;
                var control = menu.SourceControl as ButtonShare;

                List<Guid> ids = control.Ids;
                if ((ids == null || ids.Count == 0) && control.Id.HasValue)
                {
                    ids = new List<Guid> { control.Id.Value };
                }

                foreach (var id in ids)
                {
                    ResultService result = new ResultService();
                    switch (control.ScheduleRecordType)
                    {
                        case ScheduleRecordType.Consultation:
                            {
                                var service = _process.GetConsultationRecordService();
                                result = service.DeleteConsultationRecord(new ScheduleGetBindingModel { Id = new Guid(control.Tag.ToString()) });
                                break;
                            }
                        case ScheduleRecordType.Examination:
                            {
                                var service = _process.GetExaminationRecordService();
                                result = service.DeleteExaminationRecord(new ScheduleGetBindingModel { Id = new Guid(control.Tag.ToString()) });
                            }
                            break;
                        case ScheduleRecordType.Offset:
                            {
                                var service = _process.GetOffsetRecordService();
                                result = service.DeleteOffsetRecord(new ScheduleGetBindingModel { Id = new Guid(control.Tag.ToString()) });
                            }
                            break;
                        case ScheduleRecordType.Semester:
                            {
                                var service = _process.GetSemesterRecordService();
                                result = service.DeleteSemesterRecord(new ScheduleGetBindingModel { Id = new Guid(control.Tag.ToString()) });
                            }
                            break;
                    }

                    if (result.Succeeded)
                    {
                        LoadRecords?.Invoke();
                    }
                    else
                    {
                        ErrorMessanger.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
                    }
                }
                LoadRecords?.Invoke();
            }
        }
    }
}