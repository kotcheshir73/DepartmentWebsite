using ControlsAndForms.Messangers;
using ScheduleInterfaces.BindingModels;
using ScheduleInterfaces.Interfaces;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace ScheduleControlsAndForms.BaseControls
{
    public partial class ControlCurrentDates : UserControl
    {
        private readonly IScheduleProcess _process;

        private LoadScheduleBindingModel _model;

        private DateTime _selectDate;

        public ControlCurrentDates(IScheduleProcess process)
        {
            InitializeComponent();
            _process = process;

            _selectDate = DateTime.Now.Date;
            while (_selectDate.DayOfWeek != DayOfWeek.Monday)
            {
                _selectDate = _selectDate.AddDays(-1);
            }

            controlCurrentTableView.SetIScheduleProcess(_process);
            controlCurrentTableView.ConfigColumns();
            controlCurrentTableView.EventLoadRecords += LoadRecords;
        }

        public void LoadData(string title, LoadScheduleBindingModel model)
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
            controlCurrentTableView.ClearTable();
            controlCurrentTableView.AddTimeRow(0);
            _model.BeginDate = _selectDate;
            _model.EndDate = _selectDate.AddDays(13);

            labelStartDate.Text = _selectDate.ToLongDateString();
            labelFinishDate.Text = _selectDate.AddDays(13).ToLongDateString();

            var records = _process.LoadSchedule(_model);
            if (!records.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке расписания возникла ошибка: ", records.Errors);
                return;
            }

            int row = 1;
            for (DateTime currentdate = _selectDate; currentdate < _selectDate.AddDays(14); currentdate = currentdate.AddDays(1), row++)
            {
                var label = new Label
                {
                    Location = new Point(0, 0),
                    Dock = DockStyle.Fill,
                    Margin = new Padding(0),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = $"{CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName(currentdate.DayOfWeek)}\r\n{currentdate.ToShortDateString()}"
                };
                controlCurrentTableView.AddRow(label, 0, row);


                var selectedRecords = records.Result.Where(x => x.ScheduleDate.Date == currentdate.Date).ToList();

                if (selectedRecords.Count > 0)
                {
                    controlCurrentTableView.LoadDay(selectedRecords, row);
                }

                if (currentdate == _selectDate.AddDays(6))
                {
                    controlCurrentTableView.AddTimeRow(++row);
                }
            }
        }

        private void ButtonPrevDate_Click(object sender, EventArgs e)
        {
            _selectDate = _selectDate.AddDays(-7);
            LoadRecords();
        }

        private void ButtonNextDate_Click(object sender, EventArgs e)
        {
            _selectDate = _selectDate.AddDays(7);
            LoadRecords();
        }
    }
}
