using System;
using System.Drawing;
using System.Windows.Forms;
using DepartmentService.IServices;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using DepartmentDAL.Enums;
using DepartmentDAL;

namespace DepartmentDesktop.Views.Services.Schedule
{
	public partial class ScheduleSemesterClassroomControl : UserControl
	{
		private readonly IScheduleService _service;

		private readonly ISemesterRecordService _serviceSR;

		private readonly IConsultationRecordService _serviceCR;

		private string _classroomID;

		private DateTime _selectDate;

		private SeasonDatesViewModel _dates;

		private Color _consultationColor = Color.Green;

		public ScheduleSemesterClassroomControl(IScheduleService service, ISemesterRecordService serviceSR,
			IConsultationRecordService serviceCR)
		{
			InitializeComponent();
			_service = service;
			_serviceSR = serviceSR;
			_serviceCR = serviceCR;
			_selectDate = DateTime.Now;

			var result = _service.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel { Title = "пара" });
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке столбцов ошибка: ", result.Errors);
			}
			var lessons = result.Result;
			if (lessons != null)
			{
				for (int i = 0; i < lessons.Count; ++i)
				{
					dataGridViewFirstWeek.Columns[i + 1].HeaderCell.Value = lessons[i].Text;
					dataGridViewSecondWeek.Columns[i + 1].HeaderCell.Value = lessons[i].Text;
				}
			}
		}

		public void LoadData(string classroomID)
		{
			try
			{
				_classroomID = classroomID;

				var resultCD = _service.GetCurrentDates();
				if (!resultCD.Succeeded)
				{
					Program.PrintErrorMessage("При загрузке дат семестра возникла ошибка: ", resultCD.Errors);
				}
				_dates = resultCD.Result;

				labelTop.Text = string.Format("{0} аудитория. {1}", _classroomID, _dates.Title);

				//Заполняем даты
				DateTime currentdate = _selectDate;
				var dateBeginSemester = Convert.ToDateTime(_dates.DateBeginSemester);
				var dateEndSemester = Convert.ToDateTime(_dates.DateEndSemester);
				if (_selectDate.Date == DateTime.Now.Date)
				{
					currentdate = dateBeginSemester.AddDays(((DateTime.Now - dateBeginSemester).Days / 14) * 14);
					_selectDate = currentdate;
				}

				var days = new[] { "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ" };//дни недели
				dataGridViewFirstWeek.Rows.Clear();
				dataGridViewSecondWeek.Rows.Clear();
				for (int j = 0; j < 6; j++)
				{
					dataGridViewFirstWeek.Rows.Add();//добавляем строки
					dataGridViewFirstWeek.Rows[j].Cells[0].Value = days[j] + "\r\n" + currentdate.ToShortDateString();//в первый столбец записываем день недели
					if (currentdate.Date == DateTime.Now.Date)
						for (int i = 0; i < 9; i++)
							dataGridViewFirstWeek.Rows[j].Cells[i].Style.BackColor = Color.Aqua;

					dataGridViewSecondWeek.Rows.Add();
					dataGridViewSecondWeek.Rows[j].Cells[0].Value = days[j] + "\r\n" + currentdate.AddDays(7).ToShortDateString();
					if (currentdate.AddDays(7).Date == DateTime.Now.Date)
						for (int i = 0; i < 9; i++)
							dataGridViewSecondWeek.Rows[j].Cells[i].Style.BackColor = Color.Aqua;

					currentdate = currentdate.AddDays(1);
				}
				var result = _serviceSR.GetSemesterSchedule(new ScheduleGetBindingModel { ClassroomId = classroomID });
				if (!result.Succeeded)
				{
					Program.PrintErrorMessage("Невозможно получить список занятий в семестре: ", result.Errors);
				}
				var list = result.Result;
				for (int r = 0; r < list.Count; ++r)
				{
					string text = string.Format("{0} {1} {2}{3}{4}{3}{5}", list[r].LessonType, list[r].LessonDiscipline, list[r].LessonClassroom,
						Environment.NewLine, list[r].LessonLecturer, list[r].LessonGroup);
					if (list[r].Week == 0)
					{
						if (list[r].IsStreaming)
						{
							dataGridViewFirstWeek.Rows[list[r].Day].Cells[list[r].Lesson + 1].Style.BackColor = Color.FloralWhite;
						}
						if (list[r].LessonType == LessonTypes.нд.ToString())
						{
							dataGridViewFirstWeek.Rows[list[r].Day].Cells[list[r].Lesson + 1].Style.BackColor = Color.YellowGreen;
						}
						if (list[r].LessonType == LessonTypes.удл.ToString())
						{
							dataGridViewFirstWeek.Rows[list[r].Day].Cells[list[r].Lesson + 1].Style.BackColor = Color.Gray;
						}
						dataGridViewFirstWeek.Rows[list[r].Day].Cells[list[r].Lesson + 1].Value = text;
						dataGridViewFirstWeek.Rows[list[r].Day].Cells[list[r].Lesson + 1].Tag = list[r].Id;
					}
					if (list[r].Week == 1)
					{
						if (list[r].IsStreaming)
						{
							dataGridViewSecondWeek.Rows[list[r].Day].Cells[list[r].Lesson + 1].Style.BackColor = Color.FloralWhite;
						}
						if (list[r].LessonType == LessonTypes.нд.ToString())
						{
							dataGridViewSecondWeek.Rows[list[r].Day].Cells[list[r].Lesson + 1].Style.BackColor = Color.YellowGreen;
						}
						if (list[r].LessonType == LessonTypes.удл.ToString())
						{
							dataGridViewSecondWeek.Rows[list[r].Day].Cells[list[r].Lesson + 1].Style.BackColor = Color.Gray;
						}
						dataGridViewSecondWeek.Rows[list[r].Day].Cells[list[r].Lesson + 1].Value = text;
						dataGridViewSecondWeek.Rows[list[r].Day].Cells[list[r].Lesson + 1].Tag = list[r].Id;
					}
				}
				var dateFinish = _selectDate.AddDays(14);
				var resultConsults = _serviceCR.GetConsultationSchedule(new ScheduleGetBindingModel
				{
					DateBegin = _selectDate,
					DateEnd = dateFinish,
					ClassroomId = _classroomID
				});
				if (!resultConsults.Succeeded)
				{
					Program.PrintErrorMessage("Невозможно получить список консультаций в семестре: ", resultConsults.Errors);
				}
				var consults = resultConsults.Result;
				foreach (var record in consults)
				{
					if (record.Week == 0)
					{
						dataGridViewFirstWeek.Rows[record.Day].Cells[record.Lesson + 1].Value = record.Text;
						dataGridViewFirstWeek.Rows[record.Day].Cells[record.Lesson + 1].Style.BackColor = _consultationColor;
						dataGridViewFirstWeek.Rows[record.Day].Cells[record.Lesson + 1].Tag = record.Id;
					}
					if (record.Week == 1)
					{
						dataGridViewSecondWeek.Rows[record.Day].Cells[record.Lesson + 1].Value = record.Text;
						dataGridViewSecondWeek.Rows[record.Day].Cells[record.Lesson + 1].Style.BackColor = _consultationColor;
						dataGridViewSecondWeek.Rows[record.Day].Cells[record.Lesson + 1].Tag = record.Id;
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

		private void buttonPrevWeek_Click(object sender, EventArgs e)
		{
			DateTime date = _selectDate;
			var dateBeginSemester = Convert.ToDateTime(_dates.DateBeginSemester);
			if (date.AddDays(-14) >= dateBeginSemester.Date)
				_selectDate = date.AddDays(-14);
			LoadData(_classroomID);
		}

		private void buttonNextWeek_Click(object sender, EventArgs e)
		{
			DateTime date = _selectDate;
			var dateEndSemester = Convert.ToDateTime(_dates.DateEndSemester);
			if (date.AddDays(14) <= dateEndSemester.Date)
				_selectDate = date.AddDays(14);
			LoadData(_classroomID);
		}

		private void dataGridView_Resize(object sender, EventArgs e)
		{
			for (int i = 0; i < ((DataGridView)sender).Rows.Count; i++)
			{
				((DataGridView)sender).Rows[i].Height = (((DataGridView)sender).Height - 35) / ((DataGridView)sender).Rows.Count;
			}
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
									if (((DataGridView)sender).SelectedCells[0].Style.BackColor != _consultationColor)
									{
										result = _serviceSR.DeleteSemesterRecord(
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
					MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
							if (((DataGridView)sender).SelectedCells[0].Style.BackColor != _consultationColor)
							{
								ScheduleSemesterRecordForm form = new ScheduleSemesterRecordForm(_serviceSR, _service, 
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
						 //DateTime date = Convert.ToDateTime(text.Split('\n')[1]);
						 //FormAddUpd form = new FormAddUpd(1, _classroomID, Convert.ToInt32(((DataGridView)sender).Tag),
						 //    ((DataGridView)sender).SelectedCells[0].RowIndex, date,
						 //    ((DataGridView)sender).SelectedCells[0].ColumnIndex - 1, null);
						 //form.ShowDialog();
							LoadData(_classroomID);
						}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void toolStripButtonAdd_Click(object sender, EventArgs e)
		{
			ScheduleSemesterRecordForm form = new ScheduleSemesterRecordForm(_serviceSR, _service);
			form.ShowDialog();
		}

		private void toolStripButtonRef_Click(object sender, EventArgs e)
		{
			LoadData(_classroomID);
		}
	}
}
