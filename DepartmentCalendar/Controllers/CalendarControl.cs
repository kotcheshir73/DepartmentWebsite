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

namespace DepartmentCalendar.Controllers
{
    public partial class CalendarControl : UserControl
    {
        private MonthView monthView;

        public CalendarControl()
        {
            monthView = this.monthView1;
            InitializeComponent();
        }

        public CalendarControl(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private void CalendarControl_Load(object sender, EventArgs e)
        {
            
        }
    }
}
