using ControlsAndForms.Messangers;
using Enums;
using ScheduleImplementations.Helpers;
using ScheduleInterfaces.BindingModels;
using ScheduleInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace ScheduleControlsAndForms.Semester
{
    public partial class ScheduleSemesterControl : UserControl
    {
        private readonly IScheduleProcess _process;

        private readonly ISemesterRecordService _serviceSR;

        private ScheduleGetBindingModel _model;

        private List<DateTime> _semesterDates;

        private int _selectedDate;

        public ScheduleSemesterControl(IScheduleProcess process, ISemesterRecordService serviceSR)
        {
            InitializeComponent();
            _process = process;
            _serviceSR = serviceSR;
        }

        public void LoadData(string title, ScheduleGetBindingModel model)
        {
            try
            {
                _model = model;

                labelTop.Text = title;

                _semesterDates = ScheduleHelper.GetSemesterDates();
                _selectedDate = 0;
                for(int i = 0; i < _semesterDates.Count; ++i)
                {
                    if(DateTime.Now <= _semesterDates[i] && i == 0)
                    {
                        _selectedDate = i;
                        break;
                    }
                    else if (i > 0 && DateTime.Now > _semesterDates[i - 1] && DateTime.Now < _semesterDates[i])
                    {
                        break;
                    }
                    else
                    {
                        _selectedDate = i;
                    }
                }

                LoadRecords();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRecords()
        {
            try
            {
                if(_selectedDate < 0 || _selectedDate >= _semesterDates.Count)
                {
                    return;
                }

                _model.DateBegin = _semesterDates[_selectedDate];
                _model.DateEnd = _semesterDates[_selectedDate].AddDays(13);

                var result = _serviceSR.GetSemesterSchedule(_model);
                if (!result.Succeeded)
                {
                    ErrorMessanger.PrintErrorMessage("Невозможно получить список зачетов в семестре: ", result.Errors);
                }
                var list = result.Result;

                var dateBeginOffset = _semesterDates[_selectedDate];
                var dateEndOffset = _semesterDates[_selectedDate].AddDays(13);

                var days = (dateEndOffset - dateBeginOffset).Days;
                dataGridViewFirstWeek.Rows.Clear();
                dataGridViewSecondWeek.Rows.Clear();
                var currentdate = dateBeginOffset;
                for (int j = 0; j <= days / 2; j++, currentdate = currentdate.AddDays(1))
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

                    dataGridViewSecondWeek.Rows.Add();
                    dataGridViewSecondWeek.Rows[j].Cells[0].Value = string.Format("{0}{1}{2}", currentdate.AddDays(7).ToShortDateString(), Environment.NewLine,
                       CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName(currentdate.DayOfWeek));
                    if (currentdate.AddDays(7).Date == DateTime.Now.Date)
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            dataGridViewSecondWeek.Rows[j].Cells[i].Style.BackColor = Color.Aqua;
                        }
                    }
                    if (currentdate.AddDays(7).DayOfWeek == DayOfWeek.Sunday)
                    {
                        for (int i = 0; i < dataGridViewSecondWeek.Columns.Count; i++)
                        {
                            dataGridViewSecondWeek.Rows[j].Cells[i].Style.BackColor = Color.Gray;
                        }
                    }
                }
               
                List<DataGridView> grids = new List<DataGridView> { dataGridViewFirstWeek, dataGridViewSecondWeek };
                for (int week = 0; week < 2; week++)
                {
                    for (int day = 0; day < 6; day++)
                    {
                        for (int lesson = 0; lesson < 8; lesson++)
                        {
                            var elems = list.Where(x => x.Week == week && x.Day == day && x.Lesson == lesson).OrderBy(x => x.LessonGroup);
                            if (elems != null && elems.Count() > 0)
                            {
                                // одна пара
                                if (elems.Count() == 1)
                                {
                                    string text = string.Format("{0} {1} {2}{3}{4}{3}{5}", elems.First().LessonType, elems.First().LessonDiscipline, elems.First().LessonClassroom,
                                        Environment.NewLine, elems.First().LessonLecturer, elems.First().LessonGroup);
                                    if (elems.First().LessonType == LessonTypes.нд)
                                    {
                                        grids[week].Rows[day].Cells[lesson + 1].Style.BackColor = Color.YellowGreen;
                                    }
                                    if (elems.First().LessonType == LessonTypes.удл)
                                    {
                                        if (!string.IsNullOrEmpty(elems.First().NotParseRecord))
                                        {
                                            grids[week].Rows[day].Cells[lesson + 1].Style.BackColor = Color.Gray;
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                    if (grids[week].Rows[day].Cells[lesson + 1].Value == null)
                                    {
                                        grids[week].Rows[day].Cells[lesson + 1].Value = text;
                                        grids[week].Rows[day].Cells[lesson + 1].Tag = elems.First().Id;
                                    }
                                    else
                                    {
                                        throw new Exception("Накладка");
                                    }
                                }
                                else
                                {
                                    // подгруппы
                                    if (elems.Select(x => x.LessonGroup).Distinct().Count() == 1)
                                    {
                                        string text = string.Format("{0} {1} {2} {3}{2}  {4}", elems.First().LessonType,
                                            string.Join("/", elems.Select(x => string.Format("{0} {1}", x.LessonDiscipline, x.LessonClassroom))),
                                            Environment.NewLine, string.Join("/", elems.Select(x => x.LessonLecturer)), elems.First().LessonGroup);
                                        if (grids[week].Rows[day].Cells[lesson + 1].Value == null)
                                        {
                                            grids[week].Rows[day].Cells[lesson + 1].Value = text;
                                            grids[week].Rows[day].Cells[lesson + 1].Tag = string.Join(",", elems.Select(x => x.Id));
                                        }
                                        else
                                        {
                                            throw new Exception("Накладка");
                                        }
                                    }
                                    // поток
                                    else
                                    {
                                        string groups = string.Join(",", elems.Select(x => x.LessonGroup));
                                        string text = string.Format("{0} {1} {2}{3}{4}{3}{5}", elems.First().LessonType, elems.First().LessonDiscipline, elems.First().LessonClassroom,
                                            Environment.NewLine, elems.First().LessonLecturer, groups);
                                        if (grids[week].Rows[day].Cells[lesson + 1].Value == null)
                                        {
                                            grids[week].Rows[day].Cells[lesson + 1].Style.BackColor = Color.FloralWhite;
                                            grids[week].Rows[day].Cells[lesson + 1].Value = text;
                                            grids[week].Rows[day].Cells[lesson + 1].Tag = string.Join(",", elems.Select(x => x.Id));
                                        }
                                        else
                                        {
                                            throw new Exception("Накладка");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < dataGridViewFirstWeek.Rows.Count; i++)
                {
                    dataGridViewFirstWeek.Rows[i].Height = (dataGridViewFirstWeek.Height - 35) / dataGridViewFirstWeek.Rows.Count;
                }
                for (int i = 0; i < dataGridViewSecondWeek.Rows.Count; i++)
                {
                    dataGridViewSecondWeek.Rows[i].Height = (dataGridViewSecondWeek.Height - 35) / dataGridViewSecondWeek.Rows.Count;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonPrevWeek_Click(object sender, EventArgs e)
        {
            if(_selectedDate > 0)
            {
                _selectedDate--;
            }
            LoadRecords();
        }

        private void ButtonNextWeek_Click(object sender, EventArgs e)
        {
            if (_selectedDate < _semesterDates.Count - 1)
            {
                _selectedDate++;
            }
            LoadRecords();
        }

        private void DataGridView_Resize(object sender, EventArgs e)
        {
            for (int i = 0; i < ((DataGridView)sender).Rows.Count; i++)
            {
                ((DataGridView)sender).Rows[i].Height = (((DataGridView)sender).Height - 35) / ((DataGridView)sender).Rows.Count;
            }
        }

        private void DataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Delete")
            {
                try
                {
                    if (((DataGridView)sender).SelectedCells.Count > 0)
                        if (((DataGridView)sender).SelectedCells[0].ColumnIndex > 0)
                            if (((DataGridView)sender).SelectedCells[0].Tag != null)
                                if (MessageBox.Show("Удалить запись?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                                    DialogResult.Yes)
                                {
                                    var result = _serviceSR.DeleteSemesterRecord(
                                        new ScheduleGetBindingModel
                                        {
                                            Id = new Guid(((DataGridView)sender).SelectedCells[0].Tag.ToString())
                                        });
                                    if (!result.Succeeded)
                                    {
                                        ErrorMessanger.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
                                    }
                                    LoadRecords();
                                }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        var tags = ((DataGridView)sender).SelectedCells[0].Tag.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var tag in tags)
                        {
                            var form = new ScheduleSemesterRecordForm(_serviceSR, _process, new Guid(tag));
                            form.Show();
                        }
                    }
                    else
                    {//иначе пустая ячейка
                        ScheduleSemesterRecordForm form = new ScheduleSemesterRecordForm(_serviceSR, _process);
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
            ScheduleSemesterRecordForm form = new ScheduleSemesterRecordForm(_serviceSR, _process);
            form.ShowDialog();
        }

        private void ToolStripButtonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewFirstWeek.SelectedCells.Count > 0 && dataGridViewFirstWeek.SelectedCells[0].ColumnIndex > 0)
            {
                if (dataGridViewFirstWeek.SelectedCells[0].Tag != null)
                {//если в Tag есть данные, то это id записи
                    var tags = dataGridViewFirstWeek.SelectedCells[0].Tag.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var tag in tags)
                    {
                        ScheduleSemesterRecordForm form = new ScheduleSemesterRecordForm(_serviceSR, _process, new Guid(tag));
                        form.Show();
                    }
                    LoadRecords();
                }
            }
            if (dataGridViewSecondWeek.SelectedCells.Count > 0 && dataGridViewSecondWeek.SelectedCells[0].ColumnIndex > 0)
            {
                if (dataGridViewSecondWeek.SelectedCells[0].Tag != null)
                {//если в Tag есть dataGridViewSecondWeek, то это id записи
                    var tags = dataGridViewSecondWeek.SelectedCells[0].Tag.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var tag in tags)
                    {
                        ScheduleSemesterRecordForm form = new ScheduleSemesterRecordForm(_serviceSR, _process, new Guid(tag));
                        form.Show();
                    }
                    LoadRecords();
                }
            }
        }

        private void ToolStripButtonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewFirstWeek.SelectedCells.Count > 0 && dataGridViewFirstWeek.SelectedCells[0].ColumnIndex > 0)
            {
                if (dataGridViewFirstWeek.SelectedCells[0].Tag != null)
                {//если в Tag есть данные, то это id записи
                    if (MessageBox.Show("Вы уверены, что хотите удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        var tags = dataGridViewFirstWeek.SelectedCells[0].Tag.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var tag in tags)
                        {
                            Guid id = new Guid(tag);
                            var result = _serviceSR.DeleteSemesterRecord(new ScheduleGetBindingModel { Id = id });
                            if (!result.Succeeded)
                            {
                                ErrorMessanger.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
                            }
                        }
                    }
                }
            }
            if (dataGridViewSecondWeek.SelectedCells.Count > 0 && dataGridViewSecondWeek.SelectedCells[0].ColumnIndex > 0)
            {
                if (dataGridViewSecondWeek.SelectedCells[0].Tag != null)
                {//если в Tag есть dataGridViewSecondWeek, то это id записи
                    if (MessageBox.Show("Вы уверены, что хотите удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        var tags = dataGridViewSecondWeek.SelectedCells[0].Tag.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var tag in tags)
                        {
                            Guid id = new Guid(tag);
                            var result = _serviceSR.DeleteSemesterRecord(new ScheduleGetBindingModel { Id = id });
                            if (!result.Succeeded)
                            {
                                ErrorMessanger.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
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
    }
}