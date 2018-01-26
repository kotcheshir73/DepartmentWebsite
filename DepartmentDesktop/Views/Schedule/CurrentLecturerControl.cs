using DepartmentDesktop.Views.Schedule.Examination;
using DepartmentDesktop.Views.Schedule.Offset;
using DepartmentDesktop.Views.Schedule.Semester;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.Schedule
{
    public partial class CurrentLecturerControl : UserControl
    {
        private readonly IScheduleService _service;

        private readonly ISemesterRecordService _serviceSR;

        private readonly IOffsetRecordService _serviceOR;

        private readonly IExaminationRecordService _serviceER;

        private readonly IConsultationRecordService _serviceCR;

        public CurrentLecturerControl(IScheduleService service, ISemesterRecordService serviceSR, IOffsetRecordService serviceOR, IExaminationRecordService serviceER,
            IConsultationRecordService serviceCR)
        {
            InitializeComponent();
            _service = service;
            _serviceSR = serviceSR;
            _serviceOR = serviceOR;
            _serviceER = serviceER;
            _serviceCR = serviceCR;
        }

        public void LoadData()
        {
            tabControlLecturer.TabPages.Clear();

            var resultCD = _service.GetCurrentDates();
            if (!resultCD.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке дат семестра возникла ошибка: ", resultCD.Errors);
            }
            var _dates = resultCD.Result;

            var currentDate = DateTime.Now;

            var resultLecturers = _service.GetLecturers(new LecturerGetBindingModel { });
            if (!resultLecturers.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", resultLecturers.Errors);
                return;
            }
            var lecturers = resultLecturers.Result.List;

            if (lecturers != null)
            {
                if (currentDate.Date <= Convert.ToDateTime(_dates.DateEndSecondHalfSemester).Date)
                {
                    for (int i = 0; i < lecturers.Count; i++)
                    {
                        TabPage tabpage = new TabPage
                        {
                            AutoScroll = true,
                            Location = new Point(23, 4),
                            Name = "tabPageSemester" + lecturers[i].Id,
                            Padding = new Padding(3),
                            Size = new Size(1140, 611),
                            Tag = i.ToString(),
                            Text = lecturers[i].FullName
                        };
                        tabControlLecturer.TabPages.Add(tabpage);
                        var control = new ScheduleSemesterControl(_service, _serviceSR, _serviceCR)
                        {
                            Dock = DockStyle.Fill
                        };
                        control.LoadData(string.Format("{0}.", lecturers[i].FullName), new ScheduleGetBindingModel { LecturerId = lecturers[i].Id });
                        tabControlLecturer.TabPages[i].Controls.Add(control);
                    }
                }
                else if (Convert.ToDateTime(_dates.DateBeginOffset).Date >= currentDate.Date && currentDate.Date <= Convert.ToDateTime(_dates.DateBeginExamination).Date)
                {
                    for (int i = 0; i < lecturers.Count; i++)
                    {
                        TabPage tabpage = new TabPage
                        {
                            AutoScroll = true,
                            Location = new Point(23, 4),
                            Name = "tabPageSemester" + lecturers[i].Id,
                            Padding = new Padding(3),
                            Size = new Size(1140, 611),
                            Tag = i.ToString(),
                            Text = lecturers[i].FullName
                        };
                        tabControlLecturer.TabPages.Add(tabpage);
                        var control = new ScheduleOffsetControl(_service, _serviceOR, _serviceCR)
                        {
                            Dock = DockStyle.Fill
                        };
                        control.LoadData(string.Format("{0}.", lecturers[i].FullName), new ScheduleGetBindingModel { LecturerId = lecturers[i].Id });
                        tabControlLecturer.TabPages[i].Controls.Add(control);
                    }
                }
                else if (Convert.ToDateTime(_dates.DateEndExamination).Date >= currentDate.Date)
                {
                    for (int i = 0; i < lecturers.Count; i++)
                    {
                        TabPage tabpage = new TabPage
                        {
                            AutoScroll = true,
                            Location = new Point(23, 4),
                            Name = "tabPageSemester" + lecturers[i].Id,
                            Padding = new Padding(3),
                            Size = new Size(1140, 611),
                            Tag = i.ToString(),
                            Text = lecturers[i].FullName
                        };
                        tabControlLecturer.TabPages.Add(tabpage);
                        var control = new ScheduleExaminationControl(_service, _serviceER, _serviceCR)
                        {
                            Dock = DockStyle.Fill
                        };
                        control.LoadData(string.Format("{0}.", lecturers[i].FullName), new ScheduleGetBindingModel { LecturerId = lecturers[i].Id });
                        tabControlLecturer.TabPages[i].Controls.Add(control);
                    }
                }
            }
        }
    }
}
