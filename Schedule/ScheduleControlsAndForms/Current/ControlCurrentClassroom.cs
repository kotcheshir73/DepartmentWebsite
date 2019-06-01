using BaseInterfaces.BindingModels;
using ControlsAndForms.Messangers;
using ScheduleControlsAndForms.Examination;
using ScheduleControlsAndForms.Offset;
using ScheduleControlsAndForms.Semester;
using ScheduleInterfaces.BindingModels;
using ScheduleInterfaces.Interfaces;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScheduleControlsAndForms.Current
{
    public partial class ControlCurrentClassroom : UserControl
    {
        private readonly IScheduleProcess _process;

        private readonly ISemesterRecordService _serviceSR;

        private readonly IOffsetRecordService _serviceOR;

        private readonly IExaminationRecordService _serviceER;

        private readonly IConsultationRecordService _serviceCR;

        private readonly IStreamingLessonService _serviceSL;

        public ControlCurrentClassroom(IScheduleProcess process, ISemesterRecordService serviceSR, IOffsetRecordService serviceOR, IExaminationRecordService serviceER, 
            IConsultationRecordService serviceCR, IStreamingLessonService serviceSL)
        {
            InitializeComponent();
            _process = process;
            _serviceSR = serviceSR;
            _serviceOR = serviceOR;
            _serviceER = serviceER;
            _serviceCR = serviceCR;
            _serviceSL = serviceSL;
        }

        public void LoadData()
        {
            tabControlClassroom.TabPages.Clear();

            var resultCD = _process.GetCurrentDates();
            if (!resultCD.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке дат семестра возникла ошибка: ", resultCD.Errors);
            }
            var _dates = resultCD.Result;

            var currentDate = DateTime.Now;

            var resultClassrooms = _process.GetClassrooms(new ClassroomGetBindingModel { });
            if (!resultClassrooms.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", resultClassrooms.Errors);
                return;
            }
            var classrooms = resultClassrooms.Result.List;

            if (classrooms != null)
            {
                if (currentDate.Date <= Convert.ToDateTime(_dates.DateEndSecondHalfSemester).Date)
                {
                    for (int i = 0; i < classrooms.Count; i++)
                    {
                        TabPage tabpage = new TabPage
                        {
                            AutoScroll = true,
                            Location = new Point(23, 4),
                            Name = "tabPageSemester" + classrooms[i].Id,
                            Padding = new Padding(3),
                            Size = new Size(1140, 611),
                            Tag = i.ToString(),
                            Text = "Аудитория " + classrooms[i].Number
                        };
                        tabControlClassroom.TabPages.Add(tabpage);
                        var control = new ScheduleSemesterControl(_process, _serviceSR, _serviceCR, _serviceSL)
                        {
                            Dock = DockStyle.Fill
                        };
                        control.LoadData(string.Format("{0} аудитория.", classrooms[i].Number), new ScheduleGetBindingModel { ClassroomId = classrooms[i].Id });
                        tabControlClassroom.TabPages[i].Controls.Add(control);
                    }
                }
                else if (Convert.ToDateTime(_dates.DateBeginOffset).Date >= currentDate.Date && currentDate.Date < Convert.ToDateTime(_dates.DateBeginExamination).Date)
                {
                    for (int i = 0; i < classrooms.Count; i++)
                    {
                        TabPage tabpage = new TabPage
                        {
                            AutoScroll = true,
                            Location = new Point(23, 4),
                            Name = "tabPageOffset" + classrooms[i].Id,
                            Padding = new Padding(3),
                            Size = new Size(1140, 611),
                            Tag = i.ToString(),
                            Text = "Аудитория " + classrooms[i].Number
                        };
                        tabControlClassroom.TabPages.Add(tabpage);
                        var control = new ScheduleOffsetControl(_process, _serviceOR, _serviceCR)
                        {
                            Dock = DockStyle.Fill
                        };
                        control.LoadData(string.Format("{0} аудитория.", classrooms[i].Number), new ScheduleGetBindingModel { ClassroomId = classrooms[i].Id });
                        tabControlClassroom.TabPages[i].Controls.Add(control);
                    }
                }
                else if (Convert.ToDateTime(_dates.DateEndExamination).Date >= currentDate.Date)
                {
                    for (int i = 0; i < classrooms.Count; i++)
                    {
                        TabPage tabpage = new TabPage
                        {
                            AutoScroll = true,
                            Location = new Point(23, 4),
                            Name = "tabPageExamination" + classrooms[i].Id,
                            Padding = new Padding(3),
                            Size = new Size(1140, 611),
                            Tag = i.ToString(),
                            Text = "Аудитория " + classrooms[i].Number
                        };
                        tabControlClassroom.TabPages.Add(tabpage);
                        var control = new ScheduleExaminationControl(_process, _serviceER, _serviceCR)
                        {
                            Dock = DockStyle.Fill
                        };
                        control.LoadData(string.Format("{0} аудитория.", classrooms[i].Number), new ScheduleGetBindingModel { ClassroomId = classrooms[i].Id, ClassroomNumber = classrooms[i].Number });
                        tabControlClassroom.TabPages[i].Controls.Add(control);
                    }
                }
            }
        }
    }
}