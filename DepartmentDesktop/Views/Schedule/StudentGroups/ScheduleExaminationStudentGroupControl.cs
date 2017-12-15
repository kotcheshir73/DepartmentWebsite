using DepartmentDAL;
using DepartmentDesktop.Views.Schedule.Consultation;
using DepartmentDesktop.Views.Schedule.Examination;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.Schedule.StudentGroups
{
    public partial class ScheduleExaminationStudentGroupControl : UserControl
    {
        private readonly IScheduleService _service;

        private readonly IExaminationRecordService _serviceER;

        private readonly IConsultationRecordService _serviceCR;

        private string _groupName;

        private DateTime _selectDate;

        private SeasonDatesViewModel _dates;

        private Color _consultationColor = Color.Green;

        public ScheduleExaminationStudentGroupControl(IScheduleService service, IExaminationRecordService serviceER,
            IConsultationRecordService serviceCR)
        {
            InitializeComponent();
            _service = service;
            _serviceER = serviceER;
            _serviceCR = serviceCR;
            _selectDate = DateTime.Now;

			var result = _service.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel { Title = "экзамен" });
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке столбцов ошибка: ", result.Errors);
			}
			var lessons = result.Result.List;
			result = _service.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel { Title = "консультация" });
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке столбцов ошибка: ", result.Errors);
			}
			lessons.AddRange(result.Result.List);
			if (lessons != null)
            {
                for (int i = 0; i < lessons.Count; ++i)
                {
                    dataGridViewFirstWeek.Columns[i + 1].HeaderCell.Value = lessons[i].Text;
                }
            }
        }

        public void LoadData(string groupName)
        {
            try
            {
                _groupName = groupName;

				var resultCD = _service.GetCurrentDates();
				if (!resultCD.Succeeded)
				{
					Program.PrintErrorMessage("При загрузке дат семестра возникла ошибка: ", resultCD.Errors);
				}
				_dates = resultCD.Result;

				labelTop.Text = string.Format("Группа {0}. {1}", _groupName, _dates.Title);

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
                        for (int i = 0; i < 7; i++)
                            dataGridViewFirstWeek.Rows[j].Cells[i].Style.BackColor = Color.Aqua;
                    currentdate = currentdate.AddDays(1);
                }
                currentdate = dateBeginExamination;
                var result = _serviceER.GetExaminationSchedule(new ScheduleGetBindingModel { GroupName = _groupName });
				if (!result.Succeeded)
				{
					Program.PrintErrorMessage("Невозможно получить список занятий в семестре: ", result.Errors);
				}
				var list = result.Result;
				foreach (var record in list)
                {
                    if ((record.DateConsultation - dateBeginExamination).Days > -1 && (record.DateConsultation - dateBeginExamination).Days <= days)
                    {
                        if (record.DateConsultation.Hour == 16)
                        {
                            dataGridViewFirstWeek.Rows[(record.DateConsultation - dateBeginExamination).Days].Cells[5].Value = record.Text;
                            dataGridViewFirstWeek.Rows[(record.DateConsultation - dateBeginExamination).Days].Cells[5].Tag = record.Id;
                        }
                        else if (record.DateConsultation.Hour == 17)
                        {
                            dataGridViewFirstWeek.Rows[(record.DateConsultation - dateBeginExamination).Days].Cells[6].Value = record.Text;
                            dataGridViewFirstWeek.Rows[(record.DateConsultation - dateBeginExamination).Days].Cells[6].Tag = record.Id;
                        }
                    }
                    if ((record.DateExamination - dateBeginExamination).Days > -1 && (record.DateExamination - dateBeginExamination).Days <= days)
                    {
                        if (record.DateExamination.Hour == 8)
                        {
                            dataGridViewFirstWeek.Rows[(record.DateExamination - dateBeginExamination).Days].Cells[1].Value = record.Text;
                            dataGridViewFirstWeek.Rows[(record.DateExamination - dateBeginExamination).Days].Cells[1].Tag = record.Id;
                        }
                        else if (record.DateExamination.Hour == 12)
                        {
                            dataGridViewFirstWeek.Rows[(record.DateExamination - dateBeginExamination).Days].Cells[2].Value = record.Text;
                            dataGridViewFirstWeek.Rows[(record.DateExamination - dateBeginExamination).Days].Cells[2].Tag = record.Id;
                        }
                    }
                }
                var resultConsults = _serviceCR.GetConsultationSchedule(new ScheduleGetBindingModel
                {
                    DateBegin = dateBeginExamination,
                    DateEnd = dateEndExamination,
                    GroupName = _groupName
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
                                    LoadData(_groupName);
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
                            LoadData(_groupName);
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
            LoadData(_groupName);
        }
    }
}
