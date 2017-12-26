using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System.Drawing;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.Schedule.Semester
{
    public partial class ScheduleSemesterTabControl : UserControl
    {
        private readonly IScheduleService _service;

        private readonly ISemesterRecordService _serviceSR;

        private readonly IConsultationRecordService _serviceCR;

        public ScheduleSemesterTabControl(IScheduleService service, ISemesterRecordService serviceSR, IConsultationRecordService serviceCR)
        {
            InitializeComponent();
            _service = service;
            _serviceSR = serviceSR;
            _serviceCR = serviceCR;
        }

        public void LoadData(int type)
        {
            tabControlSemester.TabPages.Clear();
            switch (type)
            {
                case 0://расписание по аудиториям
                    var resultClassrooms = _service.GetClassrooms(new ClassroomGetBindingModel { });
                    if (!resultClassrooms.Succeeded)
                    {
                        Program.PrintErrorMessage("При загрузке возникла ошибка: ", resultClassrooms.Errors);
                        return;
                    }
                    var classrooms = resultClassrooms.Result.List;
                    if (classrooms != null)
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
                                Text = "Аудитория " + classrooms[i].Id
                            };
                            tabControlSemester.TabPages.Add(tabpage);
                            var control = new ScheduleSemesterControl(_service, _serviceSR, _serviceCR)
                            {
                                Dock = DockStyle.Fill
                            };
                            control.LoadData(string.Format("{0} аудитория.", classrooms[i].Id), new ScheduleGetBindingModel { ClassroomId = classrooms[i].Id });
                            tabControlSemester.TabPages[i].Controls.Add(control);
                        }
                    }
                    break;
                case 1://расписание по группам
                    var resultStudentGroups = _service.GetStudentGroups(new StudentGroupGetBindingModel { });
                    if (!resultStudentGroups.Succeeded)
                    {
                        Program.PrintErrorMessage("При загрузке возникла ошибка: ", resultStudentGroups.Errors);
                        return;
                    }
                    var studentGroups = resultStudentGroups.Result.List;
                    if (studentGroups != null)
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
                            tabControlSemester.TabPages.Add(tabpage);
                            var control = new ScheduleSemesterControl(_service, _serviceSR, _serviceCR)
                            {
                                Dock = DockStyle.Fill
                            };
                            control.LoadData(string.Format("Группа {0}.", studentGroups[i].GroupName), new ScheduleGetBindingModel { StudentGroupName = studentGroups[i].GroupName });
                            tabControlSemester.TabPages[i].Controls.Add(control);
                        }
                    }
                    break;
                case 2://расписание по преподавателям
                    var resultLecturers = _service.GetLecturers(new LecturerGetBindingModel { });
                    if (!resultLecturers.Succeeded)
                    {
                        Program.PrintErrorMessage("При загрузке возникла ошибка: ", resultLecturers.Errors);
                        return;
                    }
                    var lecturers = resultLecturers.Result.List;
                    if (lecturers != null)
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
                            tabControlSemester.TabPages.Add(tabpage);
                            var control = new ScheduleSemesterControl(_service, _serviceSR, _serviceCR)
                            {
                                Dock = DockStyle.Fill
                            };
                            control.LoadData(string.Format("{0}.", lecturers[i].FullName), new ScheduleGetBindingModel { LecturerId = lecturers[i].Id });
                            tabControlSemester.TabPages[i].Controls.Add(control);
                        }
                    }
                    break;
            }
        }
    }
}
