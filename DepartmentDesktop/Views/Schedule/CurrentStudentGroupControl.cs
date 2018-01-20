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
    public partial class CurrentStudentGroupControl : UserControl
    {
        private readonly IScheduleService _service;

        private readonly ISemesterRecordService _serviceSR;

        private readonly IOffsetRecordService _serviceOR;

        private readonly IExaminationRecordService _serviceER;

        private readonly IConsultationRecordService _serviceCR;

        public CurrentStudentGroupControl(IScheduleService service, ISemesterRecordService serviceSR, IOffsetRecordService serviceOR, IExaminationRecordService serviceER,
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
            tabControlStudentGroup.TabPages.Clear();

            var resultCD = _service.GetCurrentDates();
            if (!resultCD.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке дат семестра возникла ошибка: ", resultCD.Errors);
            }
            var _dates = resultCD.Result;

            var currentDate = DateTime.Now;

            var resultStudentGroups = _service.GetStudentGroups(new StudentGroupGetBindingModel { });
            if (!resultStudentGroups.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", resultStudentGroups.Errors);
                return;
            }
            var studentGroups = resultStudentGroups.Result.List;

            if (studentGroups != null)
            {
                if (currentDate.Date <= Convert.ToDateTime(_dates.DateEndSemester).Date)
                {
                    for (int i = 0; i < studentGroups.Count; i++)
                    {
                        TabPage tabpage = new TabPage
                        {
                            AutoScroll = true,
                            Location = new Point(23, 4),
                            Name = "tabPageSemester" + studentGroups[i].Id,
                            Padding = new Padding(3),
                            Size = new Size(1140, 611),
                            Tag = i.ToString(),
                            Text = studentGroups[i].GroupName
                        };
                        tabControlStudentGroup.TabPages.Add(tabpage);
                        var control = new ScheduleSemesterControl(_service, _serviceSR, _serviceCR)
                        {
                            Dock = DockStyle.Fill
                        };
                        control.LoadData(string.Format("Группа {0}.", studentGroups[i].GroupName), new ScheduleGetBindingModel { StudentGroupName = studentGroups[i].GroupName });
                        tabControlStudentGroup.TabPages[i].Controls.Add(control);
                    }
                }
                else if (Convert.ToDateTime(_dates.DateBeginOffset).Date >= currentDate.Date && currentDate.Date <= Convert.ToDateTime(_dates.DateBeginExamination).Date)
                {
                    for (int i = 0; i < studentGroups.Count; i++)
                    {
                        TabPage tabpage = new TabPage
                        {
                            AutoScroll = true,
                            Location = new Point(23, 4),
                            Name = "tabPageOffset" + studentGroups[i].Id,
                            Padding = new Padding(3),
                            Size = new Size(1140, 611),
                            Tag = i.ToString(),
                            Text = studentGroups[i].GroupName
                        };
                        tabControlStudentGroup.TabPages.Add(tabpage);
                        var control = new ScheduleOffsetControl(_service, _serviceOR, _serviceCR)
                        {
                            Dock = DockStyle.Fill
                        };
                        control.LoadData(string.Format("Группа {0}.", studentGroups[i].GroupName), new ScheduleGetBindingModel { StudentGroupName = studentGroups[i].GroupName });
                        tabControlStudentGroup.TabPages[i].Controls.Add(control);
                    }
                }
                else if (Convert.ToDateTime(_dates.DateEndExamination).Date >= currentDate.Date)
                {
                    for (int i = 0; i < studentGroups.Count; i++)
                    {
                        TabPage tabpage = new TabPage
                        {
                            AutoScroll = true,
                            Location = new Point(23, 4),
                            Name = "tabPageExamination" + studentGroups[i].Id,
                            Padding = new Padding(3),
                            Size = new Size(1140, 611),
                            Tag = i.ToString(),
                            Text = studentGroups[i].GroupName
                        };
                        tabControlStudentGroup.TabPages.Add(tabpage);
                        var control = new ScheduleExaminationControl(_service, _serviceER, _serviceCR)
                        {
                            Dock = DockStyle.Fill
                        };
                        control.LoadData(string.Format("Группа {0}.", studentGroups[i].GroupName), new ScheduleGetBindingModel { StudentGroupName = studentGroups[i].GroupName });
                        tabControlStudentGroup.TabPages[i].Controls.Add(control);
                    }
                }
            }
        }
    }
}
