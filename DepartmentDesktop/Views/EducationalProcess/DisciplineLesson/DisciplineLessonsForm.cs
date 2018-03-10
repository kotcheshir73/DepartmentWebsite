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

namespace DepartmentDesktop.Views.EducationalProcess.DisciplineLesson
{
    public partial class DisciplineLessonsForm : Form
    {

        [Dependency]
        public new IUnityContainer Container { get; set; }

        private Guid? _disciplineId = null;

        public DisciplineLessonsForm()
        {
            InitializeComponent();
        }

        private void DisciplineLessonForm_Load(object sender, EventArgs e)
        {
            var controlDL = Container.Resolve<DisciplineLessonsControl>();

            controlDL.Left = 0;
            controlDL.Top = 0;
            controlDL.Height = Height - 60;
            controlDL.Width = Width - 15;
            controlDL.Anchor = (((AnchorStyles.Top
                    | AnchorStyles.Bottom)
                    | AnchorStyles.Left)
                    | AnchorStyles.Right);
            tabPageLectures.Controls.Add(controlDL);

            var controlDL1 = Container.Resolve<DisciplineLessonsControl>();

            controlDL1.Left = 0;
            controlDL1.Top = 0;
            controlDL1.Height = Height - 60;
            controlDL1.Width = Width - 15;
            controlDL1.Anchor = (((AnchorStyles.Top
                    | AnchorStyles.Bottom)
                    | AnchorStyles.Left)
                    | AnchorStyles.Right);
            tabPageLaboratory.Controls.Add(controlDL1);

        }
    }
}
