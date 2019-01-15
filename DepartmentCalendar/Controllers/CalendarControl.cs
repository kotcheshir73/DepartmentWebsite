using DepartmentProcessAccountingService.IServices;
using DepartmentWebsite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace DepartmentCalendar.Controllers
{
    public partial class CalendarControl : UserControl
    {

        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDepartmentProcessService service;

        private MonthView monthView;

        public CalendarControl(IDepartmentProcessService service)
        {
            this.service = service;
            InitializeComponent();
        }

        private void CalendarControl_Load(object sender, EventArgs e)
        {
            
        }
    }
}
