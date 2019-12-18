using ControlsAndForms.Messangers;
using ScheduleInterfaces.BindingModels;
using ScheduleInterfaces.Interfaces;
using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace ScheduleControlsAndForms.Examination
{
    public partial class ScheduleExaminationControl : UserControl
    {
        private readonly IScheduleProcess _process;

        private readonly IExaminationRecordService _serviceER;

        private ScheduleGetBindingModel _model;

        public ScheduleExaminationControl(IScheduleProcess service, IExaminationRecordService serviceER)
        {
            InitializeComponent();
            _process = service;
            _serviceER = serviceER;
        }

        public void LoadData(string title, ScheduleGetBindingModel model)
        {
            try
            {
                _model = model;

                labelTop.Text = title;

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
                var result = _serviceER.GetExaminationSchedule(_model);
                if (!result.Succeeded)
                {
                    ErrorMessanger.PrintErrorMessage("Невозможно получить список экзаменов: ", result.Errors);
                }
                var list = result.Result;

                if (list.Count == 0)
                {
                    return;
                }

                var dateBeginExamination = list.First().DateConsultation;
                while (dateBeginExamination.DayOfWeek != DayOfWeek.Monday)
                {
                    dateBeginExamination = dateBeginExamination.AddDays(-1);
                }

                var dateEndExamination = list.Last().ScheduleDate;
                while (dateEndExamination.DayOfWeek != DayOfWeek.Sunday)
                {
                    dateEndExamination = dateEndExamination.AddDays(1);
                }

                var days = (dateEndExamination - dateBeginExamination).Days;
                dataGridView.Rows.Clear();
                var currentdate = dateBeginExamination;
                for (int j = 0; j <= days; j++, currentdate = currentdate.AddDays(1))
                {
                    dataGridView.Rows.Add();//добавляем строки
                    dataGridView.Rows[j].Height = 45;
                    dataGridView.Rows[j].Cells[0].Value = string.Format("{0}{1}{2}", currentdate.ToShortDateString(), Environment.NewLine,
                       CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName(currentdate.DayOfWeek));
                    if (currentdate.Date == DateTime.Now.Date)
                    {
                        for (int i = 0; i < dataGridView.Columns.Count; i++)
                        {
                            dataGridView.Rows[j].Cells[i].Style.BackColor = Color.Aqua;
                        }
                    }
                    if (currentdate.DayOfWeek == DayOfWeek.Sunday)
                    {
                        for (int i = 0; i < dataGridView.Columns.Count; i++)
                        {
                            dataGridView.Rows[j].Cells[i].Style.BackColor = Color.Gray;
                        }
                    }
                }

                int examTime= 9;
                int consTime = 16;
                int examIndex = 1;
                int consIndex = 3;

                foreach (var record in list)
                {
                    int daysCons = (record.DateConsultation - dateBeginExamination).Days;
                    if (daysCons > -1 && daysCons <= days)
                    {
                        // если выводим по аудитории, то проверка, что консультация стоит в ней, либо вывод не по аудиториям
                        if((_model.ClassroomId.HasValue && record.LessonConsultationClassroom == _model.ClassroomNumber) || !_model.ClassroomId.HasValue)
                        {
                            if (record.DateConsultation.Hour == consTime)
                            {
                                dataGridView.Rows[daysCons].Cells[consIndex].Value = record.Text;
                                dataGridView.Rows[daysCons].Cells[consIndex].Tag = record.Id;
                            }
                            else
                            {
                                dataGridView.Rows[daysCons].Cells[consIndex + 1].Value = record.Text;
                                dataGridView.Rows[daysCons].Cells[consIndex + 1].Tag = record.Id;
                            }
                        }
                    }

                    int daysExam = (record.ScheduleDate - dateBeginExamination).Days;
                    if (daysExam > -1 && daysExam <= days)
                    {
                        if ((_model.ClassroomId.HasValue && record.LessonClassroom == _model.ClassroomNumber) || !_model.ClassroomId.HasValue)
                        {
                            if (record.ScheduleDate.Hour == examTime)
                            {
                                dataGridView.Rows[daysExam].Cells[examIndex].Value = record.Text;
                                dataGridView.Rows[daysExam].Cells[examIndex].Tag = record.Id;
                            }
                            else
                            {
                                dataGridView.Rows[daysExam].Cells[examIndex + 1].Value = record.Text;
                                dataGridView.Rows[daysExam].Cells[examIndex + 1].Tag = record.Id;
                            }
                        }
                    }
                }

                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    dataGridView.Rows[i].Height = (dataGridView.Height - 35) / dataGridView.Rows.Count;
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
                            var result = _serviceER.DeleteExaminationRecord(
                                    new ScheduleGetBindingModel
                                    {
                                        Id = new Guid(((DataGridView)sender).SelectedCells[0].Tag.ToString())
                                    });
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
                        ScheduleExaminationRecordForm form = new ScheduleExaminationRecordForm(_serviceER, _process,
                            new Guid(((DataGridView)sender).SelectedCells[0].Tag.ToString()));
                        form.ShowDialog();
                    }
                    else
                    {//иначе пустая ячейка
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
            ScheduleExaminationRecordForm form = new ScheduleExaminationRecordForm(_serviceER, _process);
            form.ShowDialog();
        }

        private void ToolStripButtonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedCells.Count > 0 && dataGridView.SelectedCells[0].ColumnIndex > 0)
            {
                if (dataGridView.SelectedCells[0].Tag != null)
                {//если в Tag есть данные, то это id записи
                    ScheduleExaminationRecordForm form = new ScheduleExaminationRecordForm(_serviceER, _process,
                        new Guid(dataGridView.SelectedCells[0].Tag.ToString()));
                    form.ShowDialog();
                    LoadRecords();
                }
            }
        }

        private void ToolStripButtonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedCells.Count > 0 && dataGridView.SelectedCells[0].ColumnIndex > 0)
            {
                if (dataGridView.SelectedCells[0].Tag != null)
                {//если в Tag есть данные, то это id записи
                    if (MessageBox.Show("Вы уверены, что хотите удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Guid id = new Guid(dataGridView.SelectedCells[0].Tag.ToString());
                        var result = _serviceER.DeleteExaminationRecord(new ScheduleGetBindingModel { Id = id });
                        if (!result.Succeeded)
                        {
                            ErrorMessanger.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
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