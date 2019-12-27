using ControlsAndForms.Messangers;
using ScheduleInterfaces.BindingModels;
using ScheduleInterfaces.Interfaces;
using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace ScheduleControlsAndForms.Offset
{
    public partial class ScheduleOffsetControl : UserControl
    {
        private readonly IScheduleProcess _process;

        private readonly IOffsetRecordService _serviceOR;

        private ScheduleGetBindingModel _model;

        public ScheduleOffsetControl(IScheduleProcess process, IOffsetRecordService serviceOR)
        {
            InitializeComponent();
            _process = process;
            _serviceOR = serviceOR;
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
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRecords()
        {
            try
            {
                var result = _serviceOR.GetOffsetSchedule(_model);
                if (!result.Succeeded)
                {
                    ErrorMessanger.PrintErrorMessage("Невозможно получить список зачетов в семестре: ", result.Errors);
                }
                var list = result.Result;

                if (list.Count == 0)
                {
                    return;
                }

                var dateBeginOffset = list.First().ScheduleDate;
                while (dateBeginOffset.DayOfWeek != DayOfWeek.Monday)
                {
                    dateBeginOffset = dateBeginOffset.AddDays(-1);
                }

                var dateEndOffset = list.Last().ScheduleDate;
                while (dateEndOffset.DayOfWeek != DayOfWeek.Sunday)
                {
                    dateEndOffset = dateEndOffset.AddDays(1);
                }

                var days = (dateEndOffset - dateBeginOffset).Days;
                dataGridView.Rows.Clear();
                var currentdate = dateBeginOffset;
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
                    string text = string.Format("{0} {1} {2}{3}{4}{3}{5}", "зач.", record.LessonDiscipline, record.LessonClassroom,
                        Environment.NewLine, record.LessonLecturer, record.LessonGroup);

                    int daysOffset = (record.ScheduleDate.Date - dateBeginOffset.Date).Days;
                    if (daysOffset > -1 && daysOffset <= days)
                    {
                        dataGridView.Rows[daysOffset].Cells[record.Lesson].Value = record.Text;
                        dataGridView.Rows[daysOffset].Cells[record.Lesson].Tag = record.Id;
                    }
                }

                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    dataGridView.Rows[i].Height = (dataGridView.Height - 35) / dataGridView.Rows.Count;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                    if (((DataGridView)sender).SelectedCells.Count > 0 && ((DataGridView)sender).SelectedCells[0].ColumnIndex > 0 && ((DataGridView)sender).SelectedCells[0].Tag != null)
                    {
                        if (MessageBox.Show("Удалить запись?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            var result = _serviceOR.DeleteOffsetRecord(
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
                        if (((DataGridView)sender).SelectedCells[0].Style.BackColor != Color.Green)
                        {
                            ScheduleOffsetRecordForm form = new ScheduleOffsetRecordForm(_serviceOR, _process,
                                new Guid(((DataGridView)sender).SelectedCells[0].Tag.ToString()));
                            form.ShowDialog();
                        }
                    }
                    else
                    {//иначе пустая ячейка
                        ScheduleOffsetRecordForm form = new ScheduleOffsetRecordForm(_serviceOR, _process);
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
            ScheduleOffsetRecordForm form = new ScheduleOffsetRecordForm(_serviceOR, _process);
            form.ShowDialog();
        }

        private void ToolStripButtonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedCells.Count > 0 && dataGridView.SelectedCells[0].ColumnIndex > 0)
            {
                if (dataGridView.SelectedCells[0].Tag != null)
                {//если в Tag есть данные, то это id записи
                    ScheduleOffsetRecordForm form = new ScheduleOffsetRecordForm(_serviceOR, _process,
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
                        var result = _serviceOR.DeleteOffsetRecord(new ScheduleGetBindingModel { Id = id });
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