using DepartmentDesktop.Views.Schedule.Classrooms;
using DepartmentDesktop.Views.Schedule.Lecturers;
using DepartmentDesktop.Views.Schedule.StudentGroups;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.Schedule.Consultation
{
	public partial class ScheduleConsultationControl : UserControl
    {
        private readonly IScheduleService _service;

        private readonly IConsultationRecordService _serviceCR;

        public ScheduleConsultationControl(IScheduleService service, IConsultationRecordService serviceCR)
        {
            InitializeComponent();
            _service = service;
            _serviceCR = serviceCR;
        }

        public void LoadData(int type)
        {
            tabControlSemester.TabPages.Clear();

            switch (type)
            {
                case 0:
                    var resultClassrooms = _service.GetClassrooms(new ClassroomGetBindingModel { });
					if (!resultClassrooms.Succeeded)
					{
						Program.PrintErrorMessage("При загрузке возникла ошибка: ", resultClassrooms.Errors);
						return;
					}
					var classrooms = resultClassrooms.Result;
					if (classrooms != null)
                    {
                        for (int i = 0; i < classrooms.List.Count; i++)
                        {
                            TabPage tabpage = new TabPage
                            {
                                AutoScroll = true,
                                Location = new System.Drawing.Point(23, 4),
                                Name = "tabPageConsultation" + classrooms.List[i].Id,
                                Padding = new Padding(3),
                                Size = new System.Drawing.Size(1140, 611),
                                Tag = i.ToString(),
                                Text = "Аудитория " + classrooms.List[i].Id
                            };
                            tabControlSemester.TabPages.Add(tabpage);
                            var control = new ScheduleConsultationClassroomControl(_service, _serviceCR)
                            {
                                Dock = DockStyle.Fill
                            };
                            control.LoadData(classrooms.List[i].Id);
                            tabControlSemester.TabPages[i].Controls.Add(control);
                        }
                    }
                    break;
                case 1:
                    var resultStudentGroups = _service.GetStudentGroups(new StudentGroupGetBindingModel { });
					if (!resultStudentGroups.Succeeded)
					{
						Program.PrintErrorMessage("При загрузке возникла ошибка: ", resultStudentGroups.Errors);
						return;
					}
					var studentGroups = resultStudentGroups.Result;
					if (studentGroups != null)
                    {
                        for (int i = 0; i < studentGroups.List.Count; i++)
                        {
                            TabPage tabpage = new TabPage
                            {
                                AutoScroll = true,
                                Location = new System.Drawing.Point(23, 4),
                                Name = "tabPageConsultation" + studentGroups.List[i].Id,
                                Padding = new Padding(3),
                                Size = new System.Drawing.Size(1140, 611),
                                Tag = i.ToString(),
                                Text = studentGroups.List[i].GroupName
                            };
                            tabControlSemester.TabPages.Add(tabpage);
                            var control = new ScheduleConsultationStudentGroupControl(_service, _serviceCR)
                            {
                                Dock = DockStyle.Fill
                            };
                            control.LoadData(studentGroups.List[i].GroupName);
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
					var lecturers = resultLecturers.Result;
					if (lecturers != null)
					{
						for (int i = 0; i < lecturers.List.Count; i++)
						{
                            TabPage tabpage = new TabPage
                            {
                                AutoScroll = true,
                                Location = new System.Drawing.Point(23, 4),
                                Name = "tabPageSemester" + lecturers.List[i].Id,
                                Padding = new Padding(3),
                                Size = new System.Drawing.Size(1140, 611),
                                Tag = i.ToString(),
                                Text = lecturers.List[i].FullName
                            };
                            tabControlSemester.TabPages.Add(tabpage);
                            var control = new ScheduleConsultationLecturerControl(_service, _serviceCR)
                            {
                                Dock = DockStyle.Fill
                            };
                            control.LoadData(lecturers.List[i].Id, lecturers.List[i].FullName);
							tabControlSemester.TabPages[i].Controls.Add(control);
						}
					}
					break;
			}
        }
    }
}
