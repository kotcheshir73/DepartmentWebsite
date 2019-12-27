using ControlsAndForms.Messangers;
using ScheduleInterfaces.BindingModels;
using ScheduleInterfaces.Interfaces;
using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace ScheduleControlsAndForms.Consultation
{
    public partial class ScheduleConsultationControl : UserControl
    {
        private readonly IScheduleProcess _process;

        private readonly IConsultationRecordService _serviceCR;

        private ScheduleGetBindingModel _model;

        public ScheduleConsultationControl(IScheduleProcess process, IConsultationRecordService serviceCR)
        {
            InitializeComponent();
            _process = process;
            _serviceCR = serviceCR;
        }

        public void LoadData(string title, ScheduleGetBindingModel model)
        {
            _model = model;

            labelTop.Text = title;
        }

        private void LoadRecords()
        {
            var result = _serviceCR.GetConsultationSchedule(_model);
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                return;
            }
            var list = result.Result;

            if (list.Count == 0)
            {
                return;
            }

            var dateBeginConsultation = list.First().ScheduleDate;
            while (dateBeginConsultation.DayOfWeek != DayOfWeek.Monday)
            {
                dateBeginConsultation = dateBeginConsultation.AddDays(-1);
            }

            var dateEndConsultation = list.Last().ScheduleDate;
            while (dateEndConsultation.DayOfWeek != DayOfWeek.Sunday)
            {
                dateEndConsultation = dateEndConsultation.AddDays(1);
            }

            var days = (dateEndConsultation - dateBeginConsultation).Days;
            dataGridView.Rows.Clear();
            var currentdate = dateBeginConsultation;
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

            foreach (var record in list)
            {
                int daysCons = (record.ScheduleDate.Date - dateBeginConsultation.Date).Days;
                if (daysCons > -1 && daysCons <= days)
                {
                    dataGridView.Rows[daysCons].Cells[0].Value = record.Text;
                    dataGridView.Rows[daysCons].Cells[0].Tag = record.Id;
                }
            }

            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                dataGridView.Rows[i].Height = (dataGridView.Height - 35) / dataGridView.Rows.Count;
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
                            var result = _serviceCR.DeleteConsultationRecord(
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
                        ScheduleConsultationRecordForm form = new ScheduleConsultationRecordForm(_serviceCR, _process,
                            new Guid(((DataGridView)sender).SelectedCells[0].Tag.ToString()));
                        form.ShowDialog();
                    }
                    else
                    {//иначе пустая ячейка
                        ScheduleConsultationRecordForm form = new ScheduleConsultationRecordForm(_serviceCR, _process);
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
            var form = new ScheduleConsultationRecordForm(_serviceCR, _process);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadRecords();
            }
        }

        private void ToolStripButtonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = new ScheduleConsultationRecordForm(_serviceCR, _process, new Guid(dataGridView.SelectedRows[0].Cells[0].Tag.ToString()));
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadRecords();
                }
            }
        }

        private void ToolStripButtonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = 0; i < dataGridView.SelectedRows.Count; ++i)
                    {
                        Guid id = new Guid(dataGridView.SelectedRows[i].Cells[0].Value.ToString());
                        var result = _serviceCR.DeleteConsultationRecord(new ScheduleGetBindingModel { Id = id });
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
            }
        }

        private void ToolStripButtonRef_Click(object sender, EventArgs e)
        {
            LoadRecords();
        }
    }
}