using AcademicYearInterfaces.ViewModels;
using ControlsAndForms.Messangers;
using ScheduleInterfaces.BindingModels;
using ScheduleInterfaces.Interfaces;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScheduleControlsAndForms.Offset
{
    public partial class ScheduleOffsetControl : UserControl
    {
        private readonly IScheduleProcess _process;

        private readonly IOffsetRecordService _serviceOR;

        private ScheduleGetBindingModel _model;

        private DateTime _selectDate;

        private SeasonDatesViewModel _dates;

        public ScheduleOffsetControl(IScheduleProcess process, IOffsetRecordService serviceOR)
        {
            InitializeComponent();
            _process = process;
            _serviceOR = serviceOR;
            _selectDate = DateTime.Now;

            var result = _process.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel { Title = "пара" });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке столбцов ошибка: ", result.Errors);
            }
            var lessons = result.Result.List;
            if (lessons != null)
            {
                for (int i = 0; i < lessons.Count; ++i)
                {
                    dataGridViewFirstWeek.Columns[i + 1].HeaderCell.Value = lessons[i].Text;
                    dataGridViewSecondWeek.Columns[i + 1].HeaderCell.Value = lessons[i].Text;
                }
            }
        }

        public void LoadData(string title, ScheduleGetBindingModel model)
        {
            try
            {
                _model = model;

                var resultCD = _process.GetCurrentDates();
                if (!resultCD.Succeeded)
                {
                    ErrorMessanger.PrintErrorMessage("При загрузке дат семестра возникла ошибка: ", resultCD.Errors);
                }
                _dates = resultCD.Result;

                labelTop.Text = string.Format("{0} {1}", title, _dates.Title);

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
                //Заполняем даты
                DateTime currentdate = _selectDate;
                var dateBeginOffset = Convert.ToDateTime(_dates.DateBeginOffset);
                var dateEndOffset = Convert.ToDateTime(_dates.DateEndOffset);
                if (_selectDate.Date == DateTime.Now.Date)
                {
                    currentdate = dateBeginOffset;
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
                var result = _serviceOR.GetOffsetSchedule(_model);
                if (!result.Succeeded)
                {
                    ErrorMessanger.PrintErrorMessage("Невозможно получить список зачетов в семестре: ", result.Errors);
                }
                var list = result.Result;
                for (int r = 0; r < list.Count; ++r)
                {
                    string text = string.Format("{0} {1} {2}{3}{4}{3}{5}", "зач.", list[r].LessonDiscipline, list[r].LessonClassroom,
                        Environment.NewLine, list[r].LessonLecturer, list[r].LessonGroup);
                    if (list[r].Week == 0)
                    {
                        dataGridViewFirstWeek.Rows[list[r].Day].Cells[list[r].Lesson + 1].Value = text;
                        dataGridViewFirstWeek.Rows[list[r].Day].Cells[list[r].Lesson + 1].Tag = list[r].Id;
                    }
                    if (list[r].Week == 1)
                    {
                        dataGridViewSecondWeek.Rows[list[r].Day].Cells[list[r].Lesson + 1].Value = text;
                        dataGridViewSecondWeek.Rows[list[r].Day].Cells[list[r].Lesson + 1].Tag = list[r].Id;
                    }
                }
                _model.DateBegin = _selectDate;
                _model.DateEnd = dateEndOffset;

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
                        int lesson =
                            Convert.ToInt32(((DataGridView)sender).Tag) * 100 +
                            ((DataGridView)sender).SelectedCells[0].RowIndex * 10 +
                            ((DataGridView)sender).SelectedCells[0].ColumnIndex;
                        ScheduleOffsetRecordForm form = new ScheduleOffsetRecordForm(_serviceOR, _process, lesson: lesson);
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
            int? lesson = null;
            if (dataGridViewFirstWeek.SelectedCells.Count > 0 && dataGridViewFirstWeek.SelectedCells[0].ColumnIndex > 0)
            {
                lesson =
                              Convert.ToInt32(dataGridViewFirstWeek.Tag) * 100 +
                              dataGridViewFirstWeek.SelectedCells[0].RowIndex * 10 +
                              dataGridViewFirstWeek.SelectedCells[0].ColumnIndex;
            }
            if (dataGridViewSecondWeek.SelectedCells.Count > 0 && dataGridViewSecondWeek.SelectedCells[0].ColumnIndex > 0)
            {
                lesson =
                              Convert.ToInt32(dataGridViewSecondWeek.Tag) * 100 +
                              dataGridViewSecondWeek.SelectedCells[0].RowIndex * 10 +
                              dataGridViewSecondWeek.SelectedCells[0].ColumnIndex;
            }
            ScheduleOffsetRecordForm form = new ScheduleOffsetRecordForm(_serviceOR, _process, lesson: lesson);
            form.ShowDialog();
        }

        private void ToolStripButtonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewFirstWeek.SelectedCells.Count > 0 && dataGridViewFirstWeek.SelectedCells[0].ColumnIndex > 0)
            {
                if (dataGridViewFirstWeek.SelectedCells[0].Tag != null)
                {//если в Tag есть данные, то это id записи
                    ScheduleOffsetRecordForm form = new ScheduleOffsetRecordForm(_serviceOR, _process,
                        new Guid(dataGridViewFirstWeek.SelectedCells[0].Tag.ToString()));
                    form.ShowDialog();
                    LoadRecords();
                }
            }
            if (dataGridViewSecondWeek.SelectedCells.Count > 0 && dataGridViewSecondWeek.SelectedCells[0].ColumnIndex > 0)
            {
                if (dataGridViewSecondWeek.SelectedCells[0].Tag != null)
                {//если в Tag есть dataGridViewSecondWeek, то это id записи
                    ScheduleOffsetRecordForm form = new ScheduleOffsetRecordForm(_serviceOR, _process,
                        new Guid(dataGridViewSecondWeek.SelectedCells[0].Tag.ToString()));
                    form.ShowDialog();
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
                        Guid id = new Guid(dataGridViewFirstWeek.SelectedCells[0].Tag.ToString());
                        var result = _serviceOR.DeleteOffsetRecord(new ScheduleGetBindingModel { Id = id });
                        if (!result.Succeeded)
                        {
                            ErrorMessanger.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
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
                        Guid id = new Guid(dataGridViewSecondWeek.SelectedCells[0].Tag.ToString());
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