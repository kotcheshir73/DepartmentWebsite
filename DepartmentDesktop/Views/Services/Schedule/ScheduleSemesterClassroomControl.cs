using System;
using System.Drawing;
using System.Text;
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
        }

        public void LoadData(string classroomID)
        {
            try
            {
                _classroomID = classroomID;

                _dates = _service.GetCurrentDates();
                if (_dates == null)
                    throw new Exception("Невозможно получить даты семестра");

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
                var list = _service.GetScheduleSemester(new ScheduleBindingModel { ClassroomId = classroomID });
                if (list == null)
                    throw new Exception("Невозможно получить список занятий в семестре");
                for (int r = 0; r < list.Count; ++r)
                {
                    string text = string.Format("{0} {1}{2}{3}{2}{4}", list[r].LessonType, list[r].LessonDiscipline, Environment.NewLine,
                                list[r].LessonLecturer, list[r].LessonGroup);
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
                var consults = _service.GetScheduleConsultation(new ScheduleBindingModel { DateBegin = _selectDate, DateEnd = dateFinish, ClassroomId = _classroomID });
                if (consults == null)
                    throw new Exception("Невозможно получить список консультаций в семестре");
                foreach (var record in consults)
                {
                    string text = string.Format("{0} конс.{1}{2}{1}{3}", record.LessonDiscipline, Environment.NewLine,
                                record.LessonLecturer, record.LessonGroup);
                    if (record.Week == 0)
                    {
                        dataGridViewFirstWeek.Rows[record.Day].Cells[record.Lesson + 1].Value = text;
                        dataGridViewFirstWeek.Rows[record.Day].Cells[record.Lesson + 1].Style.BackColor = _consultationColor;
                        dataGridViewFirstWeek.Rows[record.Day].Cells[record.Lesson + 1].Tag = record.Id;
                    }
                    if (record.Week == 1)
                    {
                        dataGridViewSecondWeek.Rows[record.Day].Cells[record.Lesson + 1].Value = text;
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
                                            new SemesterRecordGetBindingModel
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
