using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.Services.Schedule
{
    public partial class ScheduleSemestrControl : UserControl
    {
        public ScheduleSemestrControl()
        {
            InitializeComponent();
        }

        public void LoadData()
        {
            tabControlSemester.TabPages.Clear();
            if (_classrooms != null)
                for (int i = 0; i < _classrooms.Count; i++)
                {
                    TabPage tabpage = new TabPage();
                    tabpage.AutoScroll = true;
                    tabpage.Location = new System.Drawing.Point(23, 4);
                    tabpage.Name = "tabPageSemester" + _classrooms[i].Id;
                    tabpage.Padding = new System.Windows.Forms.Padding(3);
                    tabpage.Size = new System.Drawing.Size(1140, 611);
                    tabpage.Tag = i.ToString();
                    tabpage.Text = "Аудитория " + _classrooms[i].Id;
                    tabControlSemester.TabPages.Add(tabpage);
                    UserControlScheduleSemester control = new UserControlScheduleSemester();
                    control.Dock = DockStyle.Fill;
                    control.ClassroomID = _classrooms[i].Id;
                    tabControlSemester.TabPages[i].Controls.Add(control);
                }
        }
    }
}
