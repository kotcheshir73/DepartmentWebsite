﻿using DepartmentDesktop.Views.Services.Schedule.Lecturers;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.Services.Schedule
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
                    var resultClassrooms = _service.GetClassrooms(new ClassroomGetBindingModel { UserId = AuthorizationService.UserId });
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
                            TabPage tabpage = new TabPage();
                            tabpage.AutoScroll = true;
                            tabpage.Location = new System.Drawing.Point(23, 4);
                            tabpage.Name = "tabPageConsultation" + classrooms.List[i].Id;
                            tabpage.Padding = new Padding(3);
                            tabpage.Size = new System.Drawing.Size(1140, 611);
                            tabpage.Tag = i.ToString();
                            tabpage.Text = "Аудитория " + classrooms.List[i].Id;
                            tabControlSemester.TabPages.Add(tabpage);
                            var control = new ScheduleConsultationClassroomControl(_service, _serviceCR);
                            control.Dock = DockStyle.Fill;
                            control.LoadData(classrooms.List[i].Id);
                            tabControlSemester.TabPages[i].Controls.Add(control);
                        }
                    }
                    break;
                case 1:
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
                            tabpage.Name = "tabPageConsultation" + studentGroups[i].Id;
                            tabpage.Padding = new Padding(3);
                            tabpage.Size = new System.Drawing.Size(1140, 611);
                            tabpage.Tag = i.ToString();
                            tabpage.Text = studentGroups[i].GroupName;
                            tabControlSemester.TabPages.Add(tabpage);
                            var control = new ScheduleConsultationStudentGroupControl(_service, _serviceCR);
                            control.Dock = DockStyle.Fill;
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
							var control = new ScheduleConsultationLecturerControl(_service, _serviceCR);
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
