using System.Windows.Forms;
using DepartmentService.IServices;

namespace DepartmentDesktop.Views.Services.Schedule
{
    public partial class ScheduleOffsetControl : UserControl
    {
        private readonly IScheduleService _service;

        private readonly IOffsetRecordService _serviceOR;

        private readonly IConsultationRecordService _serviceCR;

        public ScheduleOffsetControl(IScheduleService service, IOffsetRecordService serviceOR,
            IConsultationRecordService serviceCR)
        {
            InitializeComponent();
            _service = service;
            _serviceOR = serviceOR;
            _serviceCR = serviceCR;
        }

        public void LoadData(int type)
        {
            tabControlSemester.TabPages.Clear();
            switch (type)
            {
                case 0://расписание по аудиториям
                    var classrooms = _service.GetClassrooms();
                    if (classrooms != null)
                    {
                        for (int i = 0; i < classrooms.Count; i++)
                        {
                            TabPage tabpage = new TabPage();
                            tabpage.AutoScroll = true;
                            tabpage.Location = new System.Drawing.Point(23, 4);
                            tabpage.Name = "tabPageOffset" + classrooms[i].Id;
                            tabpage.Padding = new System.Windows.Forms.Padding(3);
                            tabpage.Size = new System.Drawing.Size(1140, 611);
                            tabpage.Tag = i.ToString();
                            tabpage.Text = "Аудитория " + classrooms[i].Id;
                            tabControlSemester.TabPages.Add(tabpage);
                            var control = new ScheduleOffsetClassroomControl(_service, _serviceOR, _serviceCR);
                            control.Dock = DockStyle.Fill;
                            control.LoadData(classrooms[i].Id);
                            tabControlSemester.TabPages[i].Controls.Add(control);
                        }
                    }
                    break;
                case 1:
                    var studentGroups = _service.GetStudentGroups();
                    if (studentGroups != null)
                    {
                        for (int i = 0; i < studentGroups.Count; i++)
                        {
                            TabPage tabpage = new TabPage();
                            tabpage.AutoScroll = true;
                            tabpage.Location = new System.Drawing.Point(23, 4);
                            tabpage.Name = "tabPageOffset" + studentGroups[i].Id;
                            tabpage.Padding = new System.Windows.Forms.Padding(3);
                            tabpage.Size = new System.Drawing.Size(1140, 611);
                            tabpage.Tag = i.ToString();
                            tabpage.Text = studentGroups[i].GroupName;
                            tabControlSemester.TabPages.Add(tabpage);
                            var control = new ScheduleOffsetStudentGroupControl(_service, _serviceOR, _serviceCR);
                            control.Dock = DockStyle.Fill;
                            control.LoadData(studentGroups[i].GroupName);
                            tabControlSemester.TabPages[i].Controls.Add(control);
                        }
                    }
                    break;
            }
        }
    }
}
