using DepartmentDesktop.Views.Schedule.Classrooms;
using DepartmentDesktop.Views.Schedule.Lecturers;
using DepartmentDesktop.Views.Schedule.StudentGroups;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.Schedule.Examination
{
	public partial class ScheduleExaminationControl : UserControl
    {
        private readonly IScheduleService _service;

        private readonly IExaminationRecordService _serviceER;

        private readonly IConsultationRecordService _serviceCR;

        public ScheduleExaminationControl(IScheduleService service, IExaminationRecordService serviceER,
            IConsultationRecordService serviceCR)
        {
            InitializeComponent();
            _service = service;
            _serviceER = serviceER;
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
                                Name = "tabPageExamination" + classrooms.List[i].Id,
                                Padding = new Padding(3),
                                Size = new System.Drawing.Size(1140, 611),
                                Tag = i.ToString(),
                                Text = "Аудитория " + classrooms.List[i].Id
                            };
                            tabControlSemester.TabPages.Add(tabpage);
                            var control = new ScheduleExaminationClassroomControl(_service, _serviceER, _serviceCR)
                            {
                                Dock = DockStyle.Fill
                            };
                            control.LoadData(classrooms.List[i].Id);
                            tabControlSemester.TabPages[i].Controls.Add(control);
                        }
                    }
                    break;
                case 1:
					var resultStudentGroups = _service.GetStudentGroups(new StudentGroupGetBindingModel { } );
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
                                Location = new System.Drawing.Point(23, 4),
                                Name = "tabPageExamination" + studentGroups[i].Id,
                                Padding = new Padding(3),
                                Size = new System.Drawing.Size(1140, 611),
                                Tag = i.ToString(),
                                Text = studentGroups[i].GroupName
                            };
                            tabControlSemester.TabPages.Add(tabpage);
                            var control = new ScheduleExaminationStudentGroupControl(_service, _serviceER, _serviceCR)
                            {
                                Dock = DockStyle.Fill
                            };
                            control.LoadData(studentGroups[i].GroupName);
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
                            var control = new ScheduleExaminationLecturerControl(_service, _serviceER, _serviceCR)
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
