using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using DepartmentService;
using Microsoft.Practices.Unity;

namespace DepartmentDesktop
{
    public partial class FormMain : Form
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public FormMain()
        {
            InitializeComponent();
        }

        private void MakeTicketsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = new Controllers.MakeTicketsUS();
            control.Left = 0;
            control.Top = 25;
            control.Height = Height - 60;
            control.Width = Width - 15;
            control.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top
                        | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            while(Controls.Count > 1)
            {
                Controls.RemoveAt(Controls.Count - 1);
            }
            Controls.Add(control);
        }

        private void UsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = new Controllers.ListUsersUC();
            control.Left = 0;
            control.Top = 25;
            control.Height = Height - 60;
            control.Width = Width - 15;
            control.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top
                        | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            while (Controls.Count > 1)
            {
                Controls.RemoveAt(Controls.Count - 1);
            }
            Controls.Add(control);
            control.LoadData();
        }

        private void ScheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParsingScheduleHTML parsing = new ParsingScheduleHTML();

            parsing.Parsing("http://www.ulstu.ru/schedule/students/", new string[] { "ИСЭбд-11"});
        }

        private void educationDirectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = Container.Resolve<Views.EducationalProcess.EducationDirection.EducationDirectionControl>();
            control.Left = 0;
            control.Top = 25;
            control.Height = Height - 60;
            control.Width = Width - 15;
            control.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top
                        | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            while (Controls.Count > 1)
            {
                Controls.RemoveAt(Controls.Count - 1);
            }
            Controls.Add(control);
            control.LoadData();
        }

        private void studentGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = Container.Resolve<Views.EducationalProcess.StudentGroup.StudentGroupControl>();
            control.Left = 0;
            control.Top = 25;
            control.Height = Height - 60;
            control.Width = Width - 15;
            control.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top
                        | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            while (Controls.Count > 1)
            {
                Controls.RemoveAt(Controls.Count - 1);
            }
            Controls.Add(control);
            control.LoadData();
        }

        private void classroomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = Container.Resolve<Views.EducationalProcess.Classroom.ClassroomControl>();
            control.Left = 0;
            control.Top = 25;
            control.Height = Height - 60;
            control.Width = Width - 15;
            control.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top
                        | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            while (Controls.Count > 1)
            {
                Controls.RemoveAt(Controls.Count - 1);
            }
            Controls.Add(control);
            control.LoadData();
        }
    }
}
