﻿using BaseInterfaces.BindingModels;
using ControlsAndForms.Messangers;
using ScheduleInterfaces.BindingModels;
using ScheduleInterfaces.Interfaces;
using System.Drawing;
using System.Windows.Forms;

namespace ScheduleControlsAndForms.Offset
{
    public partial class ScheduleOffsetTabControl : UserControl
    {
        private readonly IScheduleProcess _process;

        private readonly IOffsetRecordService _serviceOR;

        public ScheduleOffsetTabControl(IScheduleProcess process, IOffsetRecordService serviceOR)
        {
            InitializeComponent();
            _process = process;
            _serviceOR = serviceOR;
        }

        public void LoadData(int type)
        {
            tabControlSemester.TabPages.Clear();
            switch (type)
            {
                case 0://расписание по аудиториям
					var resultClassrooms = _process.GetClassrooms(new ClassroomGetBindingModel { });
					if (!resultClassrooms.Succeeded)
					{
                        ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", resultClassrooms.Errors);
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
                                Name = "tabPageOffset" + classrooms[i].Id,
                                Padding = new Padding(3),
                                Size = new Size(1140, 611),
                                Tag = i.ToString(),
                                Text = "Аудитория " + classrooms[i].Number
                            };
                            tabControlSemester.TabPages.Add(tabpage);
                            var control = new ScheduleOffsetControl(_process, _serviceOR)
                            {
                                Dock = DockStyle.Fill
                            };
                            control.LoadData(string.Format("{0} аудитория.", classrooms[i].Number), new ScheduleGetBindingModel { ClassroomId = classrooms[i].Id });
                            tabControlSemester.TabPages[i].Controls.Add(control);
                        }
                    }
                    break;
                case 1:
					var resultStudentGroups = _process.GetStudentGroups(new StudentGroupGetBindingModel { });
					if (!resultStudentGroups.Succeeded)
					{
                        ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", resultStudentGroups.Errors);
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
                                Name = "tabPageOffset" + studentGroups[i].Id,
                                Padding = new Padding(3),
                                Size = new Size(1140, 611),
                                Tag = i.ToString(),
                                Text = studentGroups[i].GroupName
                            };
                            tabControlSemester.TabPages.Add(tabpage);
                            var control = new ScheduleOffsetControl(_process, _serviceOR)
                            {
                                Dock = DockStyle.Fill
                            };
                            control.LoadData(string.Format("Группа {0}.", studentGroups[i].GroupName), new ScheduleGetBindingModel { StudentGroupName = studentGroups[i].GroupName });
                            tabControlSemester.TabPages[i].Controls.Add(control);
                        }
                    }
                    break;
				case 2://расписание по преподавателям
					var resultLecturers = _process.GetLecturers(new LecturerGetBindingModel { });
					if (!resultLecturers.Succeeded)
					{
                        ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", resultLecturers.Errors);
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
                            var control = new ScheduleOffsetControl(_process, _serviceOR)
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