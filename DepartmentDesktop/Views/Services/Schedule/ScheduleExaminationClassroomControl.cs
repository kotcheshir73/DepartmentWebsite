using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using DepartmentService.BindingModels;
using DepartmentDAL;

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

        public ScheduleExaminationClassroomControl(IScheduleService service, IExaminationRecordService serviceER,
            IConsultationRecordService serviceCR)
        {
            InitializeComponent();
            _service = service;
            _serviceER = serviceER;
            _serviceCR = serviceCR;
            _selectDate = DateTime.Now;
        }

        public void LoadData(string classroomID)
        {
            try
            {
                _classroomID = classroomID;
                labelTop.Text = _classroomID + " аудитория";

                _dates = _service.GetCurrentDates();
                if (_dates == null)
                    throw new Exception("Невозможно получить даты семестра");

                DateTime currentdate = _selectDate;
                var dateBeginExamination = Convert.ToDateTime(_dates.DateBeginExamination);
                var dateEndExamination = Convert.ToDateTime(_dates.DateEndExamination);

                if (_selectDate.Date == DateTime.Now.Date)
                {
                    currentdate = dateBeginExamination;
                    _selectDate = currentdate;
                }
                var days = (dateBeginExamination - dateEndExamination).Days;
                dataGridViewFirstWeek.Rows.Clear();
                for (int j = 0; j < days; j++)
                {
                    dataGridViewFirstWeek.Rows.Add();//добавляем строки
                    dataGridViewFirstWeek.Rows[j].Height = 45;
                    dataGridViewFirstWeek.Rows[j].Cells[0].Value = currentdate.ToShortDateString() + Environment.NewLine +
                        currentdate.DayOfWeek;//в первый столбец записываем день недели
                    if (currentdate.Date == DateTime.Now.Date)
                        for (int i = 0; i < 7; i++)
                            dataGridViewFirstWeek.Rows[j].Cells[i].Style.BackColor = Color.Aqua;
                    currentdate = currentdate.AddDays(1);
                }
                currentdate = dateBeginExamination;
                var list = _service.GetScheduleExamination(new ScheduleBindingModel { ClassroomId = _classroomID });
                if (list == null)
                    throw new Exception("Невозможно получить список занятий в семестре");
                foreach (var record in list)
                {
                    string text = string.Format("{0}{1}{2}{1}{3}", record.LessonDiscipline, Environment.NewLine,
                                record.LessonLecturer, record.LessonGroup);
                    if ((record.DateConsultation - currentdate).Days > -1 && (record.DateConsultation - currentdate).Days <= days)
                    {
                        if (record.DateConsultation.Hour == 16)
                        {
                            dataGridViewFirstWeek.Rows[(record.DateConsultation - currentdate).Days].Cells[5].Value = text;
                            dataGridViewFirstWeek.Rows[(record.DateConsultation - currentdate).Days].Cells[5].Tag = record.Id;
                        }
                        else if (record.DateConsultation.Hour == 17)
                        {
                            dataGridViewFirstWeek.Rows[(record.DateConsultation - currentdate).Days].Cells[6].Value = text;
                            dataGridViewFirstWeek.Rows[(record.DateConsultation - currentdate).Days].Cells[6].Tag = record.Id;
                        }
                    }
                    if ((record.DateExamination - currentdate).Days > -1 && (record.DateExamination - currentdate).Days <= days)
                    {
                        if (record.DateExamination.Hour == 8)
                        {
                            dataGridViewFirstWeek.Rows[(record.DateExamination - currentdate).Days].Cells[1].Value = text;
                            dataGridViewFirstWeek.Rows[(record.DateExamination - currentdate).Days].Cells[1].Tag = record.Id;
                        }
                        else if (record.DateExamination.Hour == 12)
                        {
                            dataGridViewFirstWeek.Rows[(record.DateExamination - currentdate).Days].Cells[2].Value = text;
                            dataGridViewFirstWeek.Rows[(record.DateExamination - currentdate).Days].Cells[2].Tag = record.Id;
                        }
                    }
                }
                var consults = _service.GetScheduleConsultation(new ScheduleBindingModel
                {
                    DateBegin = dateBeginExamination,
                    DateEnd = dateEndExamination,
                    ClassroomId = _classroomID
                });
                if (consults == null)
                    throw new Exception("Невозможно получить список консультаций в семестре");
                foreach (var record in consults)
                {
                    string text = string.Format("{0} конс.{1}{2}{1}{3}", record.LessonDiscipline, Environment.NewLine,
                                record.LessonLecturer, record.LessonGroup);
                    if ((record.DateConsultation.Date - currentdate.Date).Days <= days)
                    {
                        dataGridViewFirstWeek.Rows[(record.DateConsultation.Date - currentdate.Date).Days].Cells[3].Value = text;
                        dataGridViewFirstWeek.Rows[(record.DateConsultation.Date - currentdate.Date).Days].Cells[3].Tag = record.Id;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private void buttonPrevWeek_Click(object sender, EventArgs e)
        {
            string text = dataGridViewFirstWeek.Rows[0].Cells[0].Value.ToString();
            DateTime date = Convert.ToDateTime(text);
            if (date.AddDays(-14) >= Convert.ToDateTime(_dates.DateBeginExamination).Date)
                _selectDate = date.AddDays(-14);
            LoadData(_classroomID);
        }

        private void buttonNextWeek_Click(object sender, EventArgs e)
        {
            string text = dataGridViewFirstWeek.Rows[0].Cells[0].Value.ToString();
            DateTime date = Convert.ToDateTime(text);
            if (date.AddDays(14) <= Convert.ToDateTime(_dates.DateEndExamination).Date)
                _selectDate = date.AddDays(14);
            LoadData(_classroomID);
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
                                            new ExaminationRecordGetBindingModel
                                            {
                                                Id = Convert.ToInt32(((DataGridView)sender).SelectedCells[0].Tag)
                                            });
                                    }
                                    else
                                    {
                                        result = _serviceCR.DeleteConsultationRecord(
                                            new ConsultationRecordGetBindingModel
                                            {
                                                Id = Convert.ToInt32(((DataGridView)sender).SelectedCells[0].Tag)
                                            });
                                    }
                                    if (!result.Succeeded)
                                    {
                                        StringBuilder strRes = new StringBuilder();
                                        foreach (var err in result.Errors)
                                        {
                                            strRes.Append(string.Format("{0} : {1}\r\n", err.Key, err.Value));
                                        }
                                        throw new Exception(strRes.ToString());
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
