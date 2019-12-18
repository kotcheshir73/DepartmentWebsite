using AcademicYearInterfaces.ViewModels;
using ControlsAndForms.Messangers;
using Enums;
using ScheduleInterfaces.BindingModels;
using ScheduleInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ScheduleControlsAndForms.Semester
{
    public partial class ScheduleSemesterControl : UserControl
    {
        private readonly IScheduleProcess _process;

        private readonly ISemesterRecordService _serviceSR;

        private ScheduleGetBindingModel _model;

        private DateTime _selectDate;

        public ScheduleSemesterControl(IScheduleProcess process, ISemesterRecordService serviceSR)
        {
            InitializeComponent();
            _process = process;
            _serviceSR = serviceSR;
            _selectDate = DateTime.Now;
        }

        public void LoadData(string title, ScheduleGetBindingModel model)
        {
            try
            {
                _model = model;

                labelTop.Text = title;

                LoadRecrods();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRecrods()
        {
            try
            {
                //Заполняем даты
                DateTime currentdate = _selectDate;
                //var dateBeginSemester = Convert.ToDateTime(_dates.DateBeginFirstHalfSemester);
                //var dateEndFirstHalfSemester = Convert.ToDateTime(_dates.DateEndFirstHalfSemester);
                //var dateEndSemester = Convert.ToDateTime(_dates.DateEndSecondHalfSemester);
                ////_model.IsFirstHalfSemester = _selectDate.Date < dateEndFirstHalfSemester.Date;
                //if (_selectDate.Date == DateTime.Now.Date)
                //{
                //    currentdate = dateBeginSemester.AddDays(((DateTime.Now - dateBeginSemester).Days / 14) * 14);
                //    _selectDate = currentdate;
                //}

                //var days = new[] { "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ" };//дни недели
                //dataGridViewFirstWeek.Rows.Clear();
                //dataGridViewSecondWeek.Rows.Clear();
                //for (int j = 0; j < 6; j++)
                //{
                //    dataGridViewFirstWeek.Rows.Add();//добавляем строки
                //    dataGridViewFirstWeek.Rows[j].Cells[0].Value = days[j] + "\r\n" + currentdate.ToShortDateString();//в первый столбец записываем день недели
                //    if (currentdate.Date == DateTime.Now.Date)
                //        for (int i = 0; i < 9; i++)
                //            dataGridViewFirstWeek.Rows[j].Cells[i].Style.BackColor = Color.Aqua;

                //    dataGridViewSecondWeek.Rows.Add();
                //    dataGridViewSecondWeek.Rows[j].Cells[0].Value = days[j] + "\r\n" + currentdate.AddDays(7).ToShortDateString();
                //    if (currentdate.AddDays(7).Date == DateTime.Now.Date)
                //        for (int i = 0; i < 9; i++)
                //            dataGridViewSecondWeek.Rows[j].Cells[i].Style.BackColor = Color.Aqua;

                //    currentdate = currentdate.AddDays(1);
                //}
                //var result = _serviceSR.GetSemesterSchedule(_model);
                //if (!result.Succeeded)
                //{
                //    ErrorMessanger.PrintErrorMessage("Невозможно получить список занятий в семестре: ", result.Errors);
                //}
                //var list = result.Result;
                //List<DataGridView> grids = new List<DataGridView> { dataGridViewFirstWeek, dataGridViewSecondWeek };
                //for (int week = 0; week < 2; week++)
                //{
                //    for (int day = 0; day < 6; day++)
                //    {
                //        for (int lesson = 0; lesson < 8; lesson++)
                //        {
                //            var elems = list.Where(x => x.Week == week && x.Day == day && x.Lesson == lesson).OrderBy(x => x.LessonGroup);
                //            if (elems != null && elems.Count() > 0)
                //            {
                //                // одна пара
                //                if (elems.Count() == 1)
                //                {
                //                    string text = string.Format("{0} {1} {2}{3}{4}{3}{5}", elems.First().LessonType, elems.First().LessonDiscipline, elems.First().LessonClassroom,
                //                        Environment.NewLine, elems.First().LessonLecturer, elems.First().LessonGroup);
                //                    if (elems.First().LessonType == LessonTypes.нд)
                //                    {
                //                        grids[week].Rows[day].Cells[lesson + 1].Style.BackColor = Color.YellowGreen;
                //                    }
                //                    if (elems.First().LessonType == LessonTypes.удл)
                //                    {
                //                        if (!string.IsNullOrEmpty(elems.First().NotParseRecord))
                //                        {
                //                            grids[week].Rows[day].Cells[lesson + 1].Style.BackColor = Color.Gray;
                //                        }
                //                        else
                //                        {
                //                            continue;
                //                        }
                //                    }
                //                    if (grids[week].Rows[day].Cells[lesson + 1].Value == null)
                //                    {
                //                        grids[week].Rows[day].Cells[lesson + 1].Value = text;
                //                        grids[week].Rows[day].Cells[lesson + 1].Tag = elems.First().Id;
                //                    }
                //                    else
                //                    {
                //                        throw new Exception("Накладка");
                //                    }
                //                }
                //                else
                //                {
                //                    // подгруппы
                //                    if (elems.Select(x => x.LessonGroup).Distinct().Count() == 1)
                //                    {
                //                        string text = string.Format("{0} {1} {2} {3}{2}  {4}", elems.First().LessonType,
                //                            string.Join("/", elems.Select(x => string.Format("{0} {1}", x.LessonDiscipline, x.LessonClassroom))),
                //                            Environment.NewLine, string.Join("/", elems.Select(x => x.LessonLecturer)), elems.First().LessonGroup);
                //                        if (grids[week].Rows[day].Cells[lesson + 1].Value == null)
                //                        {
                //                            grids[week].Rows[day].Cells[lesson + 1].Value = text;
                //                            grids[week].Rows[day].Cells[lesson + 1].Tag = string.Join(",", elems.Select(x => x.Id));
                //                        }
                //                        else
                //                        {
                //                            throw new Exception("Накладка");
                //                        }
                //                    }
                //                    // поток
                //                    else
                //                    {
                //                        string groups = string.Join(",", elems.Select(x => x.LessonGroup));
                //                        string text = string.Format("{0} {1} {2}{3}{4}{3}{5}", elems.First().LessonType, elems.First().LessonDiscipline, elems.First().LessonClassroom,
                //                            Environment.NewLine, elems.First().LessonLecturer, groups);
                //                        if (grids[week].Rows[day].Cells[lesson + 1].Value == null)
                //                        {
                //                            grids[week].Rows[day].Cells[lesson + 1].Style.BackColor = Color.FloralWhite;
                //                            grids[week].Rows[day].Cells[lesson + 1].Value = text;
                //                            grids[week].Rows[day].Cells[lesson + 1].Tag = string.Join(",", elems.Select(x => x.Id));
                //                        }
                //                        else
                //                        {
                //                            throw new Exception("Накладка");
                //                        }
                //                    }
                //                }
                //            }
                //        }
                //    }
                //}

                //var dateFinish = _selectDate.AddDays(14);

                //_model.DateBegin = _selectDate;
                //_model.DateEnd = dateFinish;

                //for (int i = 0; i < dataGridViewFirstWeek.Rows.Count; i++)
                //{
                //    dataGridViewFirstWeek.Rows[i].Height = (dataGridViewFirstWeek.Height - 35) / dataGridViewFirstWeek.Rows.Count;
                //}
                //for (int i = 0; i < dataGridViewSecondWeek.Rows.Count; i++)
                //{
                //    dataGridViewSecondWeek.Rows[i].Height = (dataGridViewSecondWeek.Height - 35) / dataGridViewSecondWeek.Rows.Count;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonPrevWeek_Click(object sender, EventArgs e)
        {
            //DateTime date = _selectDate;
            //var dateBeginSemester = Convert.ToDateTime(_dates.DateBeginFirstHalfSemester);
            //if (date.AddDays(-14) >= dateBeginSemester.Date)
            //    _selectDate = date.AddDays(-14);
            LoadRecrods();
        }

        private void ButtonNextWeek_Click(object sender, EventArgs e)
        {
            //DateTime date = _selectDate;
            //var dateEndSemester = Convert.ToDateTime(_dates.DateEndSecondHalfSemester);
            //if (date.AddDays(14) <= dateEndSemester.Date)
            //    _selectDate = date.AddDays(14);
            LoadRecrods();
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
                                    LoadRecrods();
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
                    LoadRecrods();
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
                    LoadRecrods();
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
                    LoadRecrods();
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
            LoadRecrods();
        }

        private void ToolStripButtonRef_Click(object sender, EventArgs e)
        {
            LoadRecrods();
        }
    }
}