using System.Windows.Forms;
using DepartmentService.IServices;

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

        public void LoadData()
        {
            tabControlSemester.TabPages.Clear();
            var classrooms = _service.GetClassrooms();
            if (classrooms != null)
            {
                for (int i = 0; i < classrooms.Count; i++)
                {
                    TabPage tabpage = new TabPage();
                    tabpage.AutoScroll = true;
                    tabpage.Location = new System.Drawing.Point(23, 4);
                    tabpage.Name = "tabPageSemester" + classrooms[i].Id;
                    tabpage.Padding = new System.Windows.Forms.Padding(3);
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
        }
    }
}
