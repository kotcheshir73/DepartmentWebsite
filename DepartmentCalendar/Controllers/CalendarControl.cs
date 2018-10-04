using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DepartmentCalendar.Controllers
{
    public partial class CalendarControl : UserControl
    {
        public CalendarControl()
        {
            InitializeComponent();
        }

        public CalendarControl(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
