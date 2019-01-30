using DepartmentDesktop.Views.Schedule.Consultation;
using DepartmentModel;
using DepartmentService.ViewModels;
using ScheduleServiceInterfaces.BindingModels;
using ScheduleServiceInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.Schedule.Examination
{
    public partial class ScheduleExaminationControl : UserControl
    {
        private readonly IScheduleProcess _process;

        private readonly IExaminationRecordService _serviceER;

        private readonly IConsultationRecordService _serviceCR;

        private ScheduleGetBindingModel _model;

        private DateTime _selectDate;

        private SeasonDatesViewModel _dates;

        private Color _consultationColor = Color.Green;

        private KeyValuePair<DateTime, int> _consultFirstIndex;

        private KeyValuePair<DateTime, int> _consultSecondIndex;

        private KeyValuePair<DateTime, int> _examFirstIndex;

        private KeyValuePair<DateTime, int> _examSecondIndex;

        public ScheduleExaminationControl(IScheduleProcess service, IExaminationRecordService serviceER, IConsultationRecordService serviceCR)
        {
            InitializeComponent();
            _process = service;
            _serviceER = serviceER;
            _serviceCR = serviceCR;
            _selectDate = DateTime.Now;

            var result = _process.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel { Title = "экзамен" });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке столбцов ошибка: ", result.Errors);
            }
            var lessons = result.Result.List;
            result = _process.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel { Title = "консультация" });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке столбцов ошибка: ", result.Errors);
            }
            lessons.AddRange(result.Result.List);
            if (lessons != null)
            {
                for (int i = 0; i < lessons.Count; ++i)
                {
                    if (lessons[i].Text.Contains("Утренний"))
                    {
                        _examFirstIndex = new KeyValuePair<DateTime, int>(Convert.ToDateTime(lessons[i].TimeBeginLesson), i + 1);
                    }
                    if (lessons[i].Text.Contains("Дневной"))
                    {
                        _examSecondIndex = new KeyValuePair<DateTime, int>(Convert.ToDateTime(lessons[i].TimeBeginLesson), i + 1);
                    }
                    if (lessons[i].Text.Contains("Первая"))
                    {
                        _consultFirstIndex = new KeyValuePair<DateTime, int>(Convert.ToDateTime(lessons[i].TimeBeginLesson), i + 1);
                    }
                    if (lessons[i].Text.Contains("Вторая"))
                    {
                        _consultSecondIndex = new KeyValuePair<DateTime, int>(Convert.ToDateTime(lessons[i].TimeBeginLesson), i + 1);
                    }
                    dataGridViewFirstWeek.Columns[i + 1].HeaderCell.Value = lessons[i].Text;
                }
            }
        }

        public void LoadData(string title, ScheduleGetBindingModel model)
        {
            try
            {
                _model = model;

                var resultCD = _process.GetCurrentDates();
                if (!resultCD.Succeeded)
                {
                    Program.PrintErrorMessage("При загрузке дат семестра возникла ошибка: ", resultCD.Errors);
                }
                _dates = resultCD.Result;

                labelTop.Text = string.Format("{0} {1}", title, _dates.Title);

                LoadRecords();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private void LoadRecords()
        {
            try
            {
                DateTime currentdate = _selectDate;
                var dateBeginExamination = Convert.ToDateTime(_dates.DateBeginExamination);
                var dateEndExamination = Convert.ToDateTime(_dates.DateEndExamination);

                if (_selectDate.Date == DateTime.Now.Date)
                {
                    currentdate = dateBeginExamination;
                    _selectDate = currentdate;
                }
                var days = (dateEndExamination - dateBeginExamination).Days;
                dataGridViewFirstWeek.Rows.Clear();
                for (int j = 0; j <= days; j++)
                {
                    dataGridViewFirstWeek.Rows.Add();//добавляем строки
                    dataGridViewFirstWeek.Rows[j].Height = 45;
                    dataGridViewFirstWeek.Rows[j].Cells[0].Value = string.Format("{0}{1}{2}", currentdate.ToShortDateString(), Environment.NewLine,
                       CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName(currentdate.DayOfWeek));
                    if (currentdate.Date == DateTime.Now.Date)
                    {
                        for (int i = 0; i < dataGridViewFirstWeek.Columns.Count; i++)
                        {
                            dataGridViewFirstWeek.Rows[j].Cells[i].Style.BackColor = Color.Aqua;
                        }
                    }
                    if (currentdate.DayOfWeek == DayOfWeek.Sunday)
                    {
                        for (int i = 0; i < dataGridViewFirstWeek.Columns.Count; i++)
                        {
                            dataGridViewFirstWeek.Rows[j].Cells[i].Style.BackColor = Color.Gray;
                        }
                    }
                    currentdate = currentdate.AddDays(1);
                }
                currentdate = dateBeginExamination;
                var result = _serviceER.GetExaminationSchedule(_model);
                if (!result.Succeeded)
                {
                    Program.PrintErrorMessage("Невозможно получить список занятий в семестре: ", result.Errors);
                }
                var list = result.Result;
                foreach (var record in list)
                {
                    // для аудиторий: консультация может быть в одной аудитории, а экзамен в другой
                    if (_model.ClassroomId.HasValue && !string.IsNullOrEmpty(record.LessonConsultationClassroom))
                    {
                        int daysCons = (record.DateConsultation - dateBeginExamination).Days;
                        if (daysCons > -1 && daysCons <= days)
                        {
                            if (record.DateConsultation.Hour == _consultFirstIndex.Key.Hour)
                            {
                                dataGridViewFirstWeek.Rows[daysCons].Cells[_consultFirstIndex.Value].Value = record.Text;
                                dataGridViewFirstWeek.Rows[daysCons].Cells[_consultFirstIndex.Value].Tag = record.Id;
                            }
                            else if (record.DateConsultation.Hour == _consultSecondIndex.Key.Hour)
                            {
                                dataGridViewFirstWeek.Rows[daysCons].Cells[_consultSecondIndex.Value].Value = record.Text;
                                dataGridViewFirstWeek.Rows[daysCons].Cells[_consultSecondIndex.Value].Tag = record.Id;
                            }
                        }
                    }
                    else
                    {
                        int daysCons = (record.DateConsultation - dateBeginExamination).Days;
                        if (daysCons > -1 && daysCons <= days)
                        {
                            if (record.DateConsultation.Hour == _consultFirstIndex.Key.Hour)
                            {
                                dataGridViewFirstWeek.Rows[daysCons].Cells[_consultFirstIndex.Value].Value = record.Text;
                                dataGridViewFirstWeek.Rows[daysCons].Cells[_consultFirstIndex.Value].Tag = record.Id;
                            }
                            else if (record.DateConsultation.Hour == _consultSecondIndex.Key.Hour)
                            {
                                dataGridViewFirstWeek.Rows[daysCons].Cells[_consultSecondIndex.Value].Value = record.Text;
                                dataGridViewFirstWeek.Rows[daysCons].Cells[_consultSecondIndex.Value].Tag = record.Id;
                            }
                        }
                        int daysExam = (record.DateExamination - dateBeginExamination).Days;
                        if (daysExam > -1 && daysExam <= days)
                        {
                            if (record.DateExamination.Hour == _examFirstIndex.Key.Hour)
                            {
                                dataGridViewFirstWeek.Rows[daysExam].Cells[_examFirstIndex.Value].Value = record.Text;
                                dataGridViewFirstWeek.Rows[daysExam].Cells[_examFirstIndex.Value].Tag = record.Id;
                            }
                            else if (record.DateExamination.Hour == _examSecondIndex.Key.Hour)
                            {
                                dataGridViewFirstWeek.Rows[daysExam].Cells[_examSecondIndex.Value].Value = record.Text;
                                dataGridViewFirstWeek.Rows[daysExam].Cells[_examSecondIndex.Value].Tag = record.Id;
                            }
                        }
                    }
                }

                _model.DateBegin = dateBeginExamination;
                _model.DateEnd = dateEndExamination;
                var resultConsults = _serviceCR.GetConsultationSchedule(_model);
                if (!resultConsults.Succeeded)
                {
                    Program.PrintErrorMessage("Невозможно получить список консультаций в семестре: ", resultConsults.Errors);
                }
                var consults = resultConsults.Result;
                foreach (var record in consults)
                {
                    if (record.Day <= days)
                    {
                        dataGridViewFirstWeek.Rows[record.Day].Cells[record.Lesson + 1].Value = record.Text;
                        dataGridViewFirstWeek.Rows[record.Day].Cells[record.Lesson + 1].Tag = record.Id;
                        dataGridViewFirstWeek.Rows[record.Day].Cells[record.Lesson + 1].Style.BackColor = _consultationColor;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private void DataGridView_Resize(object sender, EventArgs e)
        {
            for (int i = 0; i < ((DataGridView)sender).Rows.Count; i++)
            {
                ((DataGridView)sender).Rows[i].Height = 45;
            }
        }

        private void DataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Delete")
            {
                try
                {
                    if (((DataGridView)sender).SelectedCells.Count > 0 && ((DataGridView)sender).SelectedCells[0].ColumnIndex > 0 && ((DataGridView)sender).SelectedCells[0].Tag != null)
                        if (MessageBox.Show("Удалить запись?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            ResultService result;
                            if (((DataGridView)sender).SelectedCells[0].ColumnIndex != 3 || ((DataGridView)sender).SelectedCells[0].ColumnIndex != 4)
                            {
                                result = _serviceER.DeleteExaminationRecord(
                                    new ScheduleGetBindingModel
                                    {
                                        Id = new Guid(((DataGridView)sender).SelectedCells[0].Tag.ToString())
                                    });
                            }
                            else
                            {
                                result = _serviceCR.DeleteConsultationRecord(
                                    new ScheduleGetBindingModel
                                    {
                                        Id = new Guid(((DataGridView)sender).SelectedCells[0].Tag.ToString())
                                    });
                            }
                            if (!result.Succeeded)
                            {
                                Program.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
                            }
                            LoadRecords();
                        }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void DataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (((DataGridView)sender).SelectedCells.Count > 0 && ((DataGridView)sender).SelectedCells[0].ColumnIndex > 0)
                {
                    if (((DataGridView)sender).SelectedCells[0].Tag != null)
                    {//если в Tag есть данные, то это id записи
                        if (((DataGridView)sender).SelectedCells[0].Style.BackColor != _consultationColor)
                        {
                            ScheduleExaminationRecordForm form = new ScheduleExaminationRecordForm(_serviceER, _process,
                                new Guid(((DataGridView)sender).SelectedCells[0].Tag.ToString()));
                            form.ShowDialog();
                        }
                        else
                        {
                            ScheduleConsultationRecordForm form = new ScheduleConsultationRecordForm(_serviceCR, _process,
                                  new Guid(((DataGridView)sender).SelectedCells[0].Tag.ToString()));
                            form.ShowDialog();
                        }
                    }
                    else
                    {//иначе пустая ячейка
                        DateTime datetime = _selectDate.Date.AddDays(dataGridViewFirstWeek.SelectedCells[0].RowIndex);
                        ScheduleExaminationRecordForm form = new ScheduleExaminationRecordForm(_serviceER, _process);
                        form.ShowDialog();
                    }
                    LoadRecords();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ToolStripButtonAdd_Click(object sender, EventArgs e)
        {
            //TODO
            int? lesson = null;
            if (dataGridViewFirstWeek.SelectedCells.Count > 0 && dataGridViewFirstWeek.SelectedCells[0].ColumnIndex > 0)
            {
                lesson =
                              Convert.ToInt32(dataGridViewFirstWeek.Tag) * 100 +
                              dataGridViewFirstWeek.SelectedCells[0].RowIndex * 10 +
                              dataGridViewFirstWeek.SelectedCells[0].ColumnIndex;
            }
            ScheduleExaminationRecordForm form = new ScheduleExaminationRecordForm(_serviceER, _process);
            form.ShowDialog();
        }

        private void ToolStripButtonUpd_Click(object sender, EventArgs e)
        {
            //TODO
            if (dataGridViewFirstWeek.SelectedCells.Count > 0 && dataGridViewFirstWeek.SelectedCells[0].ColumnIndex > 0)
            {
                if (dataGridViewFirstWeek.SelectedCells[0].Tag != null)
                {//если в Tag есть данные, то это id записи
                    if (dataGridViewFirstWeek.SelectedCells[0].Style.BackColor != _consultationColor)
                    {
                        ScheduleExaminationRecordForm form = new ScheduleExaminationRecordForm(_serviceER, _process,
                            new Guid(dataGridViewFirstWeek.SelectedCells[0].Tag.ToString()));
                        form.ShowDialog();
                    }
                    else
                    {
                        ScheduleConsultationRecordForm form = new ScheduleConsultationRecordForm(_serviceCR, _process,
                           new Guid(dataGridViewFirstWeek.SelectedCells[0].Tag.ToString()));
                        form.ShowDialog();
                    }
                    LoadRecords();
                }
            }
        }

        private void ToolStripButtonDel_Click(object sender, EventArgs e)
        {
            //TODO
            if (dataGridViewFirstWeek.SelectedCells.Count > 0 && dataGridViewFirstWeek.SelectedCells[0].ColumnIndex > 0)
            {
                if (dataGridViewFirstWeek.SelectedCells[0].Tag != null)
                {//если в Tag есть данные, то это id записи
                    if (dataGridViewFirstWeek.SelectedCells[0].Style.BackColor != _consultationColor)
                    {
                        if (MessageBox.Show("Вы уверены, что хотите удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Guid id = new Guid(dataGridViewFirstWeek.SelectedCells[0].Tag.ToString());
                            var result = _serviceER.DeleteExaminationRecord(new ScheduleGetBindingModel { Id = id });
                            if (!result.Succeeded)
                            {
                                Program.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
                            }
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("Вы уверены, что хотите удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Guid id = new Guid(dataGridViewFirstWeek.SelectedCells[0].Tag.ToString());
                            var result = _serviceCR.DeleteConsultationRecord(new ScheduleGetBindingModel { Id = id });
                            if (!result.Succeeded)
                            {
                                Program.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
                            }
                        }
                    }
                }
            }
            LoadRecords();
        }

        private void ToolStripButtonRef_Click(object sender, EventArgs e)
        {
            LoadRecords();
        }

        private void ToolStripButtonConsultation_Click(object sender, EventArgs e)
        {
            DateTime? datetime = null;
            if (dataGridViewFirstWeek.SelectedCells.Count > 0 && dataGridViewFirstWeek.SelectedCells[0].ColumnIndex > 0)
            {
                datetime = _selectDate.Date.AddDays(dataGridViewFirstWeek.SelectedCells[0].RowIndex);
                var result = _process.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel { Title = "экзамен" });
                if (!result.Succeeded)
                {
                    Program.PrintErrorMessage("При загрузке столбцов ошибка: ", result.Errors);
                }
                var lessons = result.Result.List;
                result = _process.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel { Title = "консультация" });
                if (!result.Succeeded)
                {
                    Program.PrintErrorMessage("При загрузке столбцов ошибка: ", result.Errors);
                }
                lessons.AddRange(result.Result.List);
                datetime = datetime.Value.AddHours(lessons[dataGridViewFirstWeek.SelectedCells[0].ColumnIndex - 1].DateBeginLesson.Hour)
                                .AddMinutes(lessons[dataGridViewFirstWeek.SelectedCells[0].ColumnIndex - 1].DateBeginLesson.Minute);
                ScheduleConsultationRecordForm form = new ScheduleConsultationRecordForm(_serviceCR, _process, datetime: datetime, model: _model);
                form.ShowDialog();
            }
        }
    }
}
