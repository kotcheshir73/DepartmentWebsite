using DepartmentDesktop.Views.EducationalProcess.Discipline;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.EducationalProcess.LecturerCabinet
{
    public partial class LecturerCabinetForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public LecturerCabinetForm()
        {
            InitializeComponent();
        }

        private void LecturerCabinetForm_Load(object sender, EventArgs e)
        {
            var controlLC = Container.Resolve<LecturerCabinetControl>();

            controlLC.Left = 0;
            controlLC.Top = 0;
            controlLC.Height = Height - 60;
            controlLC.Width = Width - 15;
            controlLC.Anchor = (((AnchorStyles.Top
                    | AnchorStyles.Bottom)
                    | AnchorStyles.Left)
                    | AnchorStyles.Right);

            tabPageDisciplines.Controls.Add(controlLC);

            LoadData(); 
        }

        private void LoadData()
        {
            (tabPageDisciplines.Controls[0] as LecturerCabinetControl).LoadData();
        }
    }
}
