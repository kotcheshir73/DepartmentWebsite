using System.Windows.Forms;
using DepartmentService.IServices;

namespace DepartmentDesktop.Views.Services.Schedule
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
                    var control = new ScheduleExaminationClassroomControl(_service, _serviceER, _serviceCR);
                    control.Dock = DockStyle.Fill;
                    control.LoadData(classrooms[i].Id);
                    tabControlSemester.TabPages[i].Controls.Add(control);
                }
            }
        }
    }
}
