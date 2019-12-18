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

        private readonly Color ConsultColor = Color.LightGreen;

        private readonly Color ExamColor = Color.DarkCyan;

        private readonly Color OffsetColor = Color.SandyBrown;

        private event Action LoadRecords;

        public ControlCurrentTableView()
        {
            InitializeComponent();
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
                    Text = $"{times[i].ToString("HH:mm")}-{times[i].AddMinutes(90).ToString("HH:mm")}"
                };
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
            var group = list.GroupBy(x => new { x.ScheduleDate, x.TimeSpanMinutes }).OrderBy(x => x.Key.ScheduleDate).ToList();
            for (int i = 0; i < group.Count; i++)
            {
                if (group[i].Count() > 1)
                {// потоки

                }
                else
                {
                    SetRecord(group[i].First(), row);
                }

                if (i + 1 < group.Count && group[i].Key.ScheduleDate.AddMinutes(group[i].Key.TimeSpanMinutes) > group[i + 1].Key.ScheduleDate)
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

            var buttonRecord = new Button
            {
                Location = new Point(0, 0),
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
                Text = text,
                Tag = record.Id,
                ContextMenuStrip = contextMenuStripButton
            };
            buttonRecord.DoubleClick += Button_DoubleClick;

            switch (record.ScheduleRecordType)
            {
                case ScheduleRecordType.Consultation:
                    buttonRecord.BackColor = ConsultColor;
                    break;
                case ScheduleRecordType.Examination:
                    buttonRecord.BackColor = ExamColor;
                    break;
                case ScheduleRecordType.Offset:
                    buttonRecord.BackColor = OffsetColor;
                    break;
                case ScheduleRecordType.Semester:
                    break;
            }

            tableLayoutPanel.Controls.Add(buttonRecord, (int)counter, row);
            if (colspan > 1)
            {
                tableLayoutPanel.SetColumnSpan(buttonRecord, colspan);
            }
        }

        private void AddSemesterRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScheduleSemesterRecordForm form = new ScheduleSemesterRecordForm(_process.GetSemesterRecordService(), _process);
            form.ShowDialog();
            LoadRecords?.Invoke();
        }

        private void AddOffsetRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScheduleOffsetRecordForm form = new ScheduleOffsetRecordForm(_process.GetOffsetRecordService(), _process);
            form.ShowDialog();
            LoadRecords?.Invoke();
        }

        private void AddExaminationRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScheduleExaminationRecordForm form = new ScheduleExaminationRecordForm(_process.GetExaminationRecordService(), _process);
            form.ShowDialog();
            LoadRecords?.Invoke();
        }

        private void AddConsultationRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScheduleConsultationRecordForm form = new ScheduleConsultationRecordForm(_process.GetConsultationRecordService(), _process);
            form.ShowDialog();
            LoadRecords?.Invoke();
        }

        private void Button_DoubleClick(object sender, EventArgs e)
        {
            var control = sender as Button;
            var id = new Guid(control.Tag.ToString());
            if (control.BackColor == ConsultColor)
            {
                ScheduleConsultationRecordForm form = new ScheduleConsultationRecordForm(_process.GetConsultationRecordService(), _process, id: id);
                form.ShowDialog();
            }
            else if (control.BackColor == ExamColor)
            {
                ScheduleExaminationRecordForm form = new ScheduleExaminationRecordForm(_process.GetExaminationRecordService(), _process, id: id);
                form.ShowDialog();
            }
            else if (control.BackColor == OffsetColor)
            {
                ScheduleOffsetRecordForm form = new ScheduleOffsetRecordForm(_process.GetOffsetRecordService(), _process, id: id);
                form.ShowDialog();
            }
            else
            {
                ScheduleSemesterRecordForm form = new ScheduleSemesterRecordForm(_process.GetSemesterRecordService(), _process, id: id);
                form.ShowDialog();
            }
        }

        private void UpdRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var menu = (sender as ToolStripMenuItem).GetCurrentParent() as ContextMenuStrip;
            var control = menu.SourceControl as Button;

            var id = new Guid(control.Tag.ToString());
            if (control.BackColor == ConsultColor)
            {
                ScheduleConsultationRecordForm form = new ScheduleConsultationRecordForm(_process.GetConsultationRecordService(), _process, id: id);
                form.ShowDialog();
            }
            else if (control.BackColor == ExamColor)
            {
                ScheduleExaminationRecordForm form = new ScheduleExaminationRecordForm(_process.GetExaminationRecordService(), _process, id: id);
                form.ShowDialog();
            }
            else if (control.BackColor == OffsetColor)
            {
                ScheduleOffsetRecordForm form = new ScheduleOffsetRecordForm(_process.GetOffsetRecordService(), _process, id: id);
                form.ShowDialog();
            }
            else
            {
                ScheduleSemesterRecordForm form = new ScheduleSemesterRecordForm(_process.GetSemesterRecordService(), _process, id: id);
                form.ShowDialog();
            }
        }

        private void DelRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var menu = (sender as ToolStripMenuItem).GetCurrentParent() as ContextMenuStrip;
                var control = menu.SourceControl as Button;

                ResultService result;
                if (control.BackColor == ConsultColor)
                {
                    var service = _process.GetConsultationRecordService();
                    result = service.DeleteConsultationRecord(new ScheduleGetBindingModel { Id = new Guid(control.Tag.ToString()) });
                }
                else if (control.BackColor == ExamColor)
                {
                    var service = _process.GetExaminationRecordService();
                    result = service.DeleteExaminationRecord(new ScheduleGetBindingModel { Id = new Guid(control.Tag.ToString()) });
                }
                else if (control.BackColor == OffsetColor)
                {
                    var service = _process.GetOffsetRecordService();
                    result = service.DeleteOffsetRecord(new ScheduleGetBindingModel { Id = new Guid(control.Tag.ToString()) });
                }
                else
                {
                    var service = _process.GetSemesterRecordService();
                    result = service.DeleteSemesterRecord(new ScheduleGetBindingModel { Id = new Guid(control.Tag.ToString()) });
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
        }
    }
}