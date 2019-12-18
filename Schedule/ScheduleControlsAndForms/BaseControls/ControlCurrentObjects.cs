using BaseInterfaces.BindingModels;
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
    public partial class ControlCurrentObjects : UserControl
    {
        private readonly IScheduleProcess _process;

        private readonly LoadScheduleBindingModel _model;

        private readonly int step = 10;

        private readonly DateTime startDate = DateTime.Now.Date.AddHours(8);

        private readonly DateTime finishDate = DateTime.Now.Date.AddHours(21).AddMinutes(30);

        private readonly List<DateTime> times;

        private ScheduleObjectLoad _scheduleObjectLoad;

        private readonly Color ConsultColor = Color.LightGreen;

        private readonly Color ExamColor = Color.DarkCyan;

        private readonly Color OffsetColor = Color.SandyBrown;

        public ControlCurrentObjects(IScheduleProcess process)
        {
            InitializeComponent();
            _process = process;

            _model = new LoadScheduleBindingModel();

            times = _process.GetScheduleLessonTimes().Result;

            ConfigColumns();
        }

        /// <summary>
        /// Загрузка компонента для определенной сущности
        /// </summary>
        /// <param name="scheduleObjectLoad"></param>
        public void LoadData(ScheduleObjectLoad scheduleObjectLoad)
        {
            try
            {
                _scheduleObjectLoad = scheduleObjectLoad;

                LoadRecords();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Формирование столбцов
        /// </summary>
        private void ConfigColumns()
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
        /// Загрузка строк
        /// </summary>
        private void LoadRecords()
        {
            ClearTable();
            AddTimeRow(0);
            _model.BeginDate = dateTimePicker.Value.Date;
            _model.EndDate = dateTimePicker.Value.Date.AddDays(1).AddSeconds(-1);

            var records = _process.LoadSchedule(_model);
            if (!records.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке расписания возникла ошибка: ", records.Errors);
                return;
            }

            switch(_scheduleObjectLoad)
            {
                case ScheduleObjectLoad.Classrooms:
                    LoadClassrooms(records.Result);
                    break;
                case ScheduleObjectLoad.Disciplines:
                    LoadDisciplines(records.Result);
                    break;
                case ScheduleObjectLoad.Lecturers:
                    LoadLecturers(records.Result);
                    break;
                case ScheduleObjectLoad.StudentGroups:
                    LoadStudentGroups(records.Result);
                    break;
            }

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
        /// Отчистка строк
        /// </summary>
        private void ClearTable()
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
        private void AddTimeRow(int row)
        {
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));

            int counter = 0;
            for (int i = 0; i < times.Count; ++i)
            {
                while (startDate.AddMinutes(counter * step) < times[i])
                {
                    counter++;
                }

                int colspan = 0;
                while (startDate.AddMinutes(counter * step) < times[i].AddMinutes(90))
                {
                    counter++;
                    colspan++;
                }
                var buttonWeek = new Button
                {
                    Location = new Point(0, 0),
                    Dock = DockStyle.Fill,
                    Margin = new Padding(0),
                    Text = $"{times[i].ToString("HH:mm")}-{times[i].AddMinutes(90).ToString("HH:mm")}"
                };
                tableLayoutPanel.Controls.Add(buttonWeek, counter - colspan + 1, row);
                if (colspan > 1)
                {
                    tableLayoutPanel.SetColumnSpan(buttonWeek, colspan);
                }
            }
        }


        private void LoadClassrooms(List<ScheduleRecordViewModel> records)
        {
            var resultClassrooms = _process.GetClassrooms(new ClassroomGetBindingModel { });
            if (!resultClassrooms.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", resultClassrooms.Errors);
                return;
            }
            var classrooms = resultClassrooms.Result.List;

            for(int i = 0; i < classrooms.Count; ++i)
            {
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));

                var label = new Label
                {
                    Location = new Point(0, 0),
                    Dock = DockStyle.Fill,
                    Margin = new Padding(0),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = classrooms[i].Number
                };
                tableLayoutPanel.Controls.Add(label, 0, i + 1);

                var selectedRecords = records.Where(x => x.ClassroomId == classrooms[i].Id).ToList();

                if (selectedRecords.Count > 0)
                {
                    LoadDay(selectedRecords, i + 1);
                }
            }
        }

        private void LoadDisciplines(List<ScheduleRecordViewModel> records)
        {
            var resultDisciplines = _process.GetDisciplines(new DisciplineGetBindingModel { });
            if (!resultDisciplines.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", resultDisciplines.Errors);
                return;
            }
            var disciplines = resultDisciplines.Result.List;

            for (int i = 0; i < disciplines.Count; ++i)
            {
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));

                var label = new Label
                {
                    Location = new Point(0, 0),
                    Dock = DockStyle.Fill,
                    Margin = new Padding(0),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = disciplines[i].DisciplineShortName
                };
                tableLayoutPanel.Controls.Add(label, 0, i + 1);

                var selectedRecords = records.Where(x => x.DisciplineId == disciplines[i].Id).ToList();

                if (selectedRecords.Count > 0)
                {
                    LoadDay(selectedRecords, i + 1);
                }
            }
        }

        private void LoadLecturers(List<ScheduleRecordViewModel> records)
        {
            var resultLecturers = _process.GetLecturers(new LecturerGetBindingModel { });
            if (!resultLecturers.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", resultLecturers.Errors);
                return;
            }
            var lecturers = resultLecturers.Result.List;

            for (int i = 0; i < lecturers.Count; ++i)
            {
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));

                var label = new Label
                {
                    Location = new Point(0, 0),
                    Dock = DockStyle.Fill,
                    Margin = new Padding(0),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = lecturers[i].FullName
                };
                tableLayoutPanel.Controls.Add(label, 0, i + 1);

                var selectedRecords = records.Where(x => x.LecturerId == lecturers[i].Id).ToList();

                if (selectedRecords.Count > 0)
                {
                    LoadDay(selectedRecords, i + 1);
                }
            }
        }

        private void LoadStudentGroups(List<ScheduleRecordViewModel> records)
        {
            var resultStudentGroups = _process.GetStudentGroups(new StudentGroupGetBindingModel { });
            if (!resultStudentGroups.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", resultStudentGroups.Errors);
                return;
            }
            var studentGroups = resultStudentGroups.Result.List;

            for (int i = 0; i < studentGroups.Count; ++i)
            {
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));

                var label = new Label
                {
                    Location = new Point(0, 0),
                    Dock = DockStyle.Fill,
                    Margin = new Padding(0),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = studentGroups[i].GroupName
                };
                tableLayoutPanel.Controls.Add(label, 0, i + 1);

                var selectedRecords = records.Where(x => x.LecturerId == studentGroups[i].Id).ToList();

                if (selectedRecords.Count > 0)
                {
                    LoadDay(selectedRecords, i + 1);
                }
            }
        }

        /// <summary>
        /// Загрузка дня дл сущности
        /// </summary>
        /// <param name="list"></param>
        /// <param name="row"></param>
        private void LoadDay(List<ScheduleRecordViewModel> list, int row)
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
        private void SetRecord(ScheduleRecordViewModel record, int row)
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
                ContextMenuStrip = contextMenuStripDel
            };
            buttonRecord.Click += Button_Click;

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

        private void DateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            LoadRecords();
        }

        private void AddSemesterRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScheduleSemesterRecordForm form = new ScheduleSemesterRecordForm(_process.GetSemesterRecordService(), _process);
            form.ShowDialog();
            LoadRecords();
        }

        private void AddOffsetRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScheduleOffsetRecordForm form = new ScheduleOffsetRecordForm(_process.GetOffsetRecordService(), _process);
            form.ShowDialog();
            LoadRecords();
        }

        private void AddExaminationRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScheduleExaminationRecordForm form = new ScheduleExaminationRecordForm(_process.GetExaminationRecordService(), _process);
            form.ShowDialog();
            LoadRecords();
        }

        private void AddConsultationRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScheduleConsultationRecordForm form = new ScheduleConsultationRecordForm(_process.GetConsultationRecordService(), _process);
            form.ShowDialog();
            LoadRecords();
        }

        private void Button_Click(object sender, EventArgs e)
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
                    LoadRecords();
                }
                else
                {
                    ErrorMessanger.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
                }
            }
        }

        private void ButtonPrevDate_Click(object sender, EventArgs e) => dateTimePicker.Value = dateTimePicker.Value.AddDays(-1);

        private void ButtonNextDate_Click(object sender, EventArgs e) => dateTimePicker.Value = dateTimePicker.Value.AddDays(1);
    }
}
