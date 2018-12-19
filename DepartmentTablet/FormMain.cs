using System;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace DepartmentTablet
{
    public partial class FormMain : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public FormMain()
        {
            InitializeComponent();
            Font = Program.Font;
            menuStripMain.Font = Program.Font;
        }

        private void ApplyControl(Control control)
        {
            while (Controls.Count > 1)
            {
                Controls.RemoveAt(Controls.Count - 1);
            }
            Controls.Add(control);
            control.Left = 0;
            control.Top = 25;
            control.Height = Height - 60;
            control.Width = Width - 15;
            control.Anchor = (((AnchorStyles.Top
                        | AnchorStyles.Bottom)
                        | AnchorStyles.Left)
                        | AnchorStyles.Right);
        }

        private void ConductedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = Container.Resolve<Conducted.ControlConducted>();
            ApplyControl(control);
            control.LoadData();
        }

        private void AcceptToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void StudentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control = Container.Resolve<Student.ControlStudentConfig>();
            ApplyControl(control);
            control.LoadData();
        }
    }
}
