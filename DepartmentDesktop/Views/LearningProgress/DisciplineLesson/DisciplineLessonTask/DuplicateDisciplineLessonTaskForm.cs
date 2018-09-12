using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.LearningProgress.DisciplineLesson.DisciplineLessonTask
{
    public partial class DuplicateDisciplineLessonTaskForm : Form
    {
        private readonly ILearningProgressProcess _process;

        private Guid _dltId;

        public DuplicateDisciplineLessonTaskForm(ILearningProgressProcess process, Guid dltId)
        {
            InitializeComponent();
            _process = process;
            _dltId = dltId;
        }

        private void DuplicateDisciplineLessonTaskForm_Load(object sender, EventArgs e)
        {
            var resultDLT = _process.GetDisiplineLessonTasksForDuplicate(new GetDisiplineLessonTasksForDuplicate { DisciplineLessonTaskId = _dltId });
            if (!resultDLT.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке заданий возникла ошибка: ", resultDLT.Errors);
                return;
            }

            comboBoxDisciplineLessonTask.ValueMember = "Value";
            comboBoxDisciplineLessonTask.DisplayMember = "Display";
            comboBoxDisciplineLessonTask.DataSource = resultDLT.Result
                .Select(d => new { Value = d.Id, Display = d.Task }).ToList();
            comboBoxDisciplineLessonTask.SelectedItem = null;
        }

        private void buttonDuplicate_Click(object sender, EventArgs e)
        {
            ResultService result = _process.DuplicateDisiplineLessonTasks(new DuplicateDisiplineLessonTasks
            {
                DisciplineLessonTaskToId = _dltId,
                DisciplineLessonTaskFromId = new Guid(comboBoxDisciplineLessonTask.SelectedValue.ToString())
            });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При дублировании возникла ошибка: ", result.Errors);
            }
            MessageBox.Show("Сделано", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
