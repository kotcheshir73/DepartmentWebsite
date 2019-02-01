using DepartmentProcessAccountingService.IServices;
using DepartmentWebsite;
using DepartmentWebsite.Controllers;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;
using Unity.Resolution;

namespace DepartmentCalendar.Controllers
{
    public partial class CalendarControl : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDepartmentProcessService service;

        private MonthView monthView;
        private DescriptionBox descriptionBox;

        public CalendarControl(IDepartmentProcessService service)
        {
            this.service = service;
            InitializeComponent();
        }

        private void CalendarControl_Load(object sender, EventArgs e)
        {
            monthView = Container.Resolve<MonthView>();
            monthView.Dock = DockStyle.Fill;
            panel.Controls.Add(monthView);
            descriptionBox = Container.Resolve<DescriptionBox>(
                    new ParameterOverrides
                    {
                        { "add", false }
                    }
                    .OnType<DescriptionBox>());
            descriptionBox.Dock = DockStyle.Fill;
            panelDB.Controls.Add(descriptionBox);
        }
    }
}
