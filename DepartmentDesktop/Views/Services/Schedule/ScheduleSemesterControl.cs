using DepartmentDesktop.Views.Services.Schedule.Lecturers;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.Services.Schedule
{
	public partial class ScheduleSemesterControl : UserControl
    {
        private readonly IScheduleService _service;

        private readonly ISemesterRecordService _serviceSR;

        private readonly IConsultationRecordService _serviceCR;

        public ScheduleSemesterControl(IScheduleService service, ISemesterRecordService serviceSR,
            IConsultationRecordService serviceCR)
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
					var resultClassrooms = _service.GetClassrooms(new ClassroomGetBindingModel { UserId = AuthorizationService.UserId });
					if (!resultClassrooms.Succeeded)
					{
						Program.PrintErrorMessage("При загрузке возникла ошибка: ", resultClassrooms.Errors);
						return;
					}
					var classrooms = resultClassrooms.Result;
					if (classrooms != null)
                    {
                        for (int i = 0; i < classrooms.Count; i++)
                        {
                            TabPage tabpage = new TabPage();
                            tabpage.AutoScroll = true;
                            tabpage.Location = new System.Drawing.Point(23, 4);
                            tabpage.Name = "tabPageSemester" + classrooms[i].Id;
                            tabpage.Padding = new Padding(3);
                            tabpage.Size = new System.Drawing.Size(1140, 611);
                            tabpage.Tag = i.ToString();
                            tabpage.Text = "Аудитория " + classrooms[i].Id;
                            tabControlSemester.TabPages.Add(tabpage);
                            var control = new ScheduleSemesterClassroomControl(_service, _serviceSR, _serviceCR);
                            control.Dock = DockStyle.Fill;
                            control.LoadData(classrooms[i].Id);
                            tabControlSemester.TabPages[i].Controls.Add(control);
                        }
                    }
                    break;
                case 1://расписание по группам
					var resultStudentGroups = _service.GetStudentGroups();
					if (!resultStudentGroups.Succeeded)
					{
						Program.PrintErrorMessage("При загрузке возникла ошибка: ", resultStudentGroups.Errors);
						return;
					}
					var studentGroups = resultStudentGroups.Result;
					if (studentGroups != null)
                    {
                        for (int i = 0; i < studentGroups.Count; i++)
                        {
                            TabPage tabpage = new TabPage();
                            tabpage.AutoScroll = true;
                            tabpage.Location = new System.Drawing.Point(23, 4);
                            tabpage.Name = "tabPageSemester" + studentGroups[i].Id;
                            tabpage.Padding = new Padding(3);
                            tabpage.Size = new System.Drawing.Size(1140, 611);
                            tabpage.Tag = i.ToString();
                            tabpage.Text = studentGroups[i].GroupName;
                            tabControlSemester.TabPages.Add(tabpage);
                            var control = new ScheduleSemesterStudentGroupControl(_service, _serviceSR, _serviceCR);
                            control.Dock = DockStyle.Fill;
                            control.LoadData(studentGroups[i].GroupName);
                            tabControlSemester.TabPages[i].Controls.Add(control);
                        }
                    }
                    break;
				case 2://расписание по преподавателям
					var resultLecturers = _service.GetLecturers();
					if (!resultLecturers.Succeeded)
					{
						Program.PrintErrorMessage("При загрузке возникла ошибка: ", resultLecturers.Errors);
						return;
					}
					var lecturers = resultLecturers.Result;
					if (lecturers != null)
					{
						for (int i = 0; i < lecturers.Count; i++)
						{
							TabPage tabpage = new TabPage();
							tabpage.AutoScroll = true;
							tabpage.Location = new System.Drawing.Point(23, 4);
							tabpage.Name = "tabPageSemester" + lecturers[i].Id;
							tabpage.Padding = new Padding(3);
							tabpage.Size = new System.Drawing.Size(1140, 611);
							tabpage.Tag = i.ToString();
							tabpage.Text = lecturers[i].FullName;
							tabControlSemester.TabPages.Add(tabpage);
							var control = new ScheduleSemesterLecturerControl(_service, _serviceSR, _serviceCR);
							control.Dock = DockStyle.Fill;
							control.LoadData(lecturers[i].Id, lecturers[i].FullName);
							tabControlSemester.TabPages[i].Controls.Add(control);
						}
					}
					break;
			}
        }
    }
}
