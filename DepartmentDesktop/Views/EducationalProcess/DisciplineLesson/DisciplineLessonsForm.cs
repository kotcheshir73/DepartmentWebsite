using Microsoft.Practices.Unity;
using System;
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
            // + загружается список студентов и занятий
            LoadData();
        }
        public void LoadData()
        {

        }
    }
}
