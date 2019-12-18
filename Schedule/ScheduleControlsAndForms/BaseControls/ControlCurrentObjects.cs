using BaseInterfaces.BindingModels;
using ControlsAndForms.Messangers;
using Enums;
using ScheduleInterfaces.BindingModels;
using ScheduleInterfaces.Interfaces;
using ScheduleInterfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ScheduleControlsAndForms.BaseControls
{
    public partial class ControlCurrentObjects : UserControl
    {
        private readonly IScheduleProcess _process;

        private readonly LoadScheduleBindingModel _model;

        private ScheduleObjectLoad _scheduleObjectLoad;

        public ControlCurrentObjects(IScheduleProcess process)
        {
            InitializeComponent();
            _process = process;

            _model = new LoadScheduleBindingModel();

            controlCurrentTableView.SetIScheduleProcess(_process);
            controlCurrentTableView.ConfigColumns();
            controlCurrentTableView.EventLoadRecords += LoadRecords;
        }

        /// <summary>
        /// Загрузка компонента для определенной сущности
        /// </summary>
        /// <param name="scheduleObjectLoad"></param>
        public void LoadData(ScheduleObjectLoad scheduleObjectLoad)
        {
            try
            {
                _scheduleObjectLoad = scheduleObjectLoad;

                LoadRecords();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Загрузка строк
        /// </summary>
        private void LoadRecords()
        {
            controlCurrentTableView.ClearTable();
            controlCurrentTableView.AddTimeRow(0);
            _model.BeginDate = dateTimePicker.Value.Date;
            _model.EndDate = dateTimePicker.Value.Date.AddDays(1).AddSeconds(-1);

            var records = _process.LoadSchedule(_model);
            if (!records.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке расписания возникла ошибка: ", records.Errors);
                return;
            }

            switch(_scheduleObjectLoad)
            {
                case ScheduleObjectLoad.Classrooms:
                    LoadClassrooms(records.Result);
                    break;
                case ScheduleObjectLoad.Disciplines:
                    LoadDisciplines(records.Result);
                    break;
                case ScheduleObjectLoad.Lecturers:
                    LoadLecturers(records.Result);
                    break;
                case ScheduleObjectLoad.StudentGroups:
                    LoadStudentGroups(records.Result);
                    break;
            }

            controlCurrentTableView.AddEmptyRow();
        }

        private void LoadClassrooms(List<ScheduleRecordViewModel> records)
        {
            var resultClassrooms = _process.GetClassrooms(new ClassroomGetBindingModel { });
            if (!resultClassrooms.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", resultClassrooms.Errors);
                return;
            }
            var classrooms = resultClassrooms.Result.List;

            for(int i = 0; i < classrooms.Count; ++i)
            {
                var label = new Label
                {
                    Location = new Point(0, 0),
                    Dock = DockStyle.Fill,
                    Margin = new Padding(0),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = classrooms[i].Number
                };
                controlCurrentTableView.AddRow(label, 0, i + 1);

                var selectedRecords = records.Where(x => x.ClassroomId == classrooms[i].Id).ToList();

                if (selectedRecords.Count > 0)
                {
                    controlCurrentTableView.LoadDay(selectedRecords, i + 1);
                }
            }
        }

        private void LoadDisciplines(List<ScheduleRecordViewModel> records)
        {
            var resultDisciplines = _process.GetDisciplines(new DisciplineGetBindingModel { });
            if (!resultDisciplines.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", resultDisciplines.Errors);
                return;
            }
            var disciplines = resultDisciplines.Result.List;

            for (int i = 0; i < disciplines.Count; ++i)
            {
                var label = new Label
                {
                    Location = new Point(0, 0),
                    Dock = DockStyle.Fill,
                    Margin = new Padding(0),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = disciplines[i].DisciplineShortName
                };
                controlCurrentTableView.AddRow(label, 0, i + 1);

                var selectedRecords = records.Where(x => x.DisciplineId == disciplines[i].Id).ToList();

                if (selectedRecords.Count > 0)
                {
                    controlCurrentTableView.LoadDay(selectedRecords, i + 1);
                }
            }
        }

        private void LoadLecturers(List<ScheduleRecordViewModel> records)
        {
            var resultLecturers = _process.GetLecturers(new LecturerGetBindingModel { });
            if (!resultLecturers.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", resultLecturers.Errors);
                return;
            }
            var lecturers = resultLecturers.Result.List;

            for (int i = 0; i < lecturers.Count; ++i)
            {
                var label = new Label
                {
                    Location = new Point(0, 0),
                    Dock = DockStyle.Fill,
                    Margin = new Padding(0),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = lecturers[i].FullName
                };
                controlCurrentTableView.AddRow(label, 0, i + 1);

                var selectedRecords = records.Where(x => x.LecturerId == lecturers[i].Id).ToList();

                if (selectedRecords.Count > 0)
                {
                    controlCurrentTableView.LoadDay(selectedRecords, i + 1);
                }
            }
        }

        private void LoadStudentGroups(List<ScheduleRecordViewModel> records)
        {
            var resultStudentGroups = _process.GetStudentGroups(new StudentGroupGetBindingModel { });
            if (!resultStudentGroups.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", resultStudentGroups.Errors);
                return;
            }
            var studentGroups = resultStudentGroups.Result.List;

            for (int i = 0; i < studentGroups.Count; ++i)
            {
                var label = new Label
                {
                    Location = new Point(0, 0),
                    Dock = DockStyle.Fill,
                    Margin = new Padding(0),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = studentGroups[i].GroupName
                };
                controlCurrentTableView.AddRow(label, 0, i + 1);

                var selectedRecords = records.Where(x => x.LecturerId == studentGroups[i].Id).ToList();

                if (selectedRecords.Count > 0)
                {
                    controlCurrentTableView.LoadDay(selectedRecords, i + 1);
                }
            }
        }

        private void DateTimePicker_ValueChanged(object sender, EventArgs e) => LoadRecords();

        private void ButtonPrevDate_Click(object sender, EventArgs e) => dateTimePicker.Value = dateTimePicker.Value.AddDays(-1);

        private void ButtonNextDate_Click(object sender, EventArgs e) => dateTimePicker.Value = dateTimePicker.Value.AddDays(1);
    }
}
