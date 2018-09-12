using DepartmentModel.Enums;
using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace DepartmentDesktop.Views.LearningProgress.DisciplineLesson.DisciplineLessonAttendance
{
    public partial class DisciplineLessonAttendanceStudentRecordsForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineLessonService _service;

        private Guid _dlId;

        public DisciplineLessonAttendanceStudentRecordsForm(IDisciplineLessonService service, Guid dlId)
        {
            InitializeComponent();
            _service = service;
            _dlId = dlId;
        }

        private void DisciplineLessonAttendanceStudentRecordsForm_Load(object sender, EventArgs e)
        {
            foreach (var elem in Enum.GetValues(typeof(DisciplineLessonStudentStatus)))
            {
                ColumnStatus.Items.Add(elem.ToString());
            }

        }
    }
}
