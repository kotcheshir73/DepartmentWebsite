﻿using System;
using System.Drawing;
using System.Windows.Forms;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using DepartmentService.BindingModels;
using DepartmentDAL;
using System.Globalization;
using System.Collections.Generic;

namespace DepartmentDesktop.Views.Services.Schedule
{
    public partial class ScheduleExaminationClassroomControl : UserControl
    {
        private readonly IScheduleService _service;

        private readonly IExaminationRecordService _serviceER;

        private readonly IConsultationRecordService _serviceCR;

        private string _classroomID;

        private DateTime _selectDate;

        private SeasonDatesViewModel _dates;

        private Color _consultationColor = Color.Green;

		private KeyValuePair<DateTime, int> _consultFirstIndex;

		private KeyValuePair<DateTime, int> _consultSecondIndex;

		private KeyValuePair<DateTime, int> _examFirstIndex;

		private KeyValuePair<DateTime, int> _examSecondIndex;

		public ScheduleExaminationClassroomControl(IScheduleService service, IExaminationRecordService serviceER,
            IConsultationRecordService serviceCR)
        {
            InitializeComponent();
            _service = service;
            _serviceER = serviceER;
            _serviceCR = serviceCR;
            _selectDate = DateTime.Now;

			var result = _service.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel { Title = "экзамен" });
			if(!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке столбцов ошибка: ", result.Errors);
			}
			var lessons = result.Result;
			result = _service.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel { Title = "консультация" });
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке столбцов ошибка: ", result.Errors);
			}
			lessons.AddRange(result.Result);
            if (lessons != null)
            {
                for (int i = 0; i < lessons.Count; ++i)
                {
					if(lessons[i].Text.Contains("Утренний"))
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

        public void LoadData(string classroomID)
        {
            try
            {
                _classroomID = classroomID;

				var resultCD = _service.GetCurrentDates();
				if(!resultCD.Succeeded)
				{
					Program.PrintErrorMessage("При загрузке дат семестра возникла ошибка: ", resultCD.Errors);
				}
				_dates = resultCD.Result;

                labelTop.Text = string.Format("{0} аудитория. {1}", _classroomID, _dates.Title);

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
                for (int j = 0; j < days; j++)
                {
                    dataGridViewFirstWeek.Rows.Add();//добавляем строки
                    dataGridViewFirstWeek.Rows[j].Height = 45;
                    dataGridViewFirstWeek.Rows[j].Cells[0].Value = string.Format("{0}{1}{2}", currentdate.ToShortDateString(), Environment.NewLine, 
                       CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName(currentdate.DayOfWeek));
                    if (currentdate.Date == DateTime.Now.Date)
                        for (int i = 0; i < dataGridViewFirstWeek.Columns.Count; i++)
                            dataGridViewFirstWeek.Rows[j].Cells[i].Style.BackColor = Color.Aqua;
                    currentdate = currentdate.AddDays(1);
                }
                currentdate = dateBeginExamination;
                var result = _serviceER.GetExaminationSchedule(new ScheduleGetBindingModel { ClassroomId = _classroomID });
				if (!result.Succeeded)
				{
					Program.PrintErrorMessage("Невозможно получить список занятий в семестре: ", result.Errors);
				}
				var list = result.Result;
                foreach (var record in list)
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
                var resultConsults = _serviceCR.GetConsultationSchedule(new ScheduleGetBindingModel
                {
                    DateBegin = dateBeginExamination,
                    DateEnd = dateEndExamination,
                    ClassroomId = _classroomID
                });
				if (!resultConsults.Succeeded)
				{
					Program.PrintErrorMessage("Невозможно получить список консультаций в семестре: ", resultConsults.Errors);
				}
				var consults = resultConsults.Result;
				foreach (var record in consults)
                {
                    if (record.Day <= days)
                    {
                        dataGridViewFirstWeek.Rows[record.Day].Cells[record.Lesson].Value = record.Text;
                        dataGridViewFirstWeek.Rows[record.Day].Cells[record.Lesson].Tag = record.Id;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private void dataGridView_Resize(object sender, EventArgs e)
        {
            for (int i = 0; i < ((DataGridView)sender).Rows.Count; i++)
                ((DataGridView)sender).Rows[i].Height = 45;
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Delete")
                try
                {
                    if (((DataGridView)sender).SelectedCells.Count > 0)
                        if (((DataGridView)sender).SelectedCells[0].ColumnIndex > 0)
                            if (((DataGridView)sender).SelectedCells[0].Tag != null)
                                if (MessageBox.Show("Удалить запись?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                                    DialogResult.Yes)
                                {
                                    ResultService result;
                                    if (((DataGridView)sender).SelectedCells[0].ColumnIndex != 3 ||
                                        ((DataGridView)sender).SelectedCells[0].ColumnIndex != 4)
                                    {
                                        result = _serviceER.DeleteExaminationRecord(
                                            new ScheduleGetBindingModel
                                            {
                                                Id = Convert.ToInt32(((DataGridView)sender).SelectedCells[0].Tag)
                                            });
                                    }
                                    else
                                    {
                                        result = _serviceCR.DeleteConsultationRecord(
                                            new ScheduleGetBindingModel
                                            {
                                                Id = Convert.ToInt32(((DataGridView)sender).SelectedCells[0].Tag)
                                            });
                                    }
                                    if (!result.Succeeded)
									{
										Program.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
									}
                                    LoadData(_classroomID);
                                }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        private void dataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (((DataGridView)sender).SelectedCells.Count > 0)
                    if (((DataGridView)sender).SelectedCells[0].ColumnIndex > 0)
                        if (((DataGridView)sender).SelectedCells[0].Tag != null)
                        {//если в Tag есть данные, то это id записи
                            if (((DataGridView)sender).SelectedCells[0].ColumnIndex != 3 &&
                                ((DataGridView)sender).SelectedCells[0].ColumnIndex != 4)
                            {
                                ScheduleExaminationRecordForm form = new ScheduleExaminationRecordForm(_serviceER, _service,
                                    Convert.ToInt64(((DataGridView)sender).SelectedCells[0].Tag));
                                form.ShowDialog();
                            }
                            else
                            {
                                ScheduleConsultationRecordForm form = new ScheduleConsultationRecordForm(_serviceCR, _service,
                                      Convert.ToInt64(((DataGridView)sender).SelectedCells[0].Tag));
                                form.ShowDialog();
                            }
                            LoadData(_classroomID);
                        }
                        else
                        {//иначе пустая ячейка
                            //string text = ((DataGridView)sender).Rows[((DataGridView)sender).SelectedCells[0].RowIndex].Cells[0].Value.ToString();
                            //DateTime date = Convert.ToDateTime(text);
                            //if (((DataGridView)sender).SelectedCells[0].ColumnIndex != 3 &&
                            //    ((DataGridView)sender).SelectedCells[0].ColumnIndex != 4)
                            //{
                            //    switch (((DataGridView)sender).SelectedCells[0].ColumnIndex)
                            //    {
                            //        case 1: date = date.AddHours(8 - date.Hour); break;
                            //        case 2: date = date.AddHours(12 - date.Hour); break;
                            //        case 5: date = date.AddHours(16 - date.Hour); break;
                            //        case 6: date = date.AddHours(17 - date.Hour); break;
                            //    }
                            //    FormAddUpd form = new FormAddUpd(2, _classroomID, Convert.ToInt32(((DataGridView)sender).Tag),
                            //        ((DataGridView)sender).SelectedCells[0].RowIndex, date,
                            //        ((DataGridView)sender).SelectedCells[0].ColumnIndex - 1, null);
                            //    form.ShowDialog();
                            //}
                            //else
                            //{
                            //    switch (((DataGridView)sender).SelectedCells[0].ColumnIndex)
                            //    {
                            //        case 3: date = date.AddHours(12 - date.Hour); break;
                            //        case 4: date = date.AddHours(14 - date.Hour); break;
                            //    }
                            //    FormAddUpd form = new FormAddUpd(3, _classroomID, Convert.ToInt32(((DataGridView)sender).Tag),
                            //        ((DataGridView)sender).SelectedCells[0].RowIndex, date, null, date);
                            //    form.ShowDialog();
                            //}
                            //LoadData();
                        }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            ScheduleExaminationRecordForm form = new ScheduleExaminationRecordForm(_serviceER, _service);
            form.ShowDialog();
        }

        private void toolStripButtonRef_Click(object sender, EventArgs e)
        {
            LoadData(_classroomID);
        }
    }
}
