using ControlsAndForms.Messangers;
using LearningProgressInterfaces.BindingModels;
using LearningProgressInterfaces.Interfaces;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Tools;

namespace LearningProgressControlsAndForms.Services
{
    public partial class FormDuplicateDisciplineLessonTask : Form
    {
        private readonly ILearningProgressProcess _process;

        private Guid _dltId;

        public FormDuplicateDisciplineLessonTask(ILearningProgressProcess process, Guid dltId)
        {
            InitializeComponent();
            _process = process;
            _dltId = dltId;
        }

        private void FormDuplicateDisciplineLessonTask_Load(object sender, EventArgs e)
        {
            var resultDLT = _process.GetDisiplineLessonTasksForDuplicate(new GetDisiplineLessonTasksForDuplicateBindingModel { DisciplineLessonTaskId = _dltId });
            if (!resultDLT.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке заданий возникла ошибка: ", resultDLT.Errors);
                return;
            }

            comboBoxDisciplineLessonTask.ValueMember = "Value";
            comboBoxDisciplineLessonTask.DisplayMember = "Display";
            comboBoxDisciplineLessonTask.DataSource = resultDLT.Result
                .Select(d => new { Value = d.Id, Display = d.Task }).ToList();
            comboBoxDisciplineLessonTask.SelectedItem = null;
        }

        private void ButtonDuplicate_Click(object sender, EventArgs e)
        {
            ResultService result = _process.DuplicateDisiplineLessonTasks(new DuplicateDisiplineLessonTasksBindingModel
            {
                DisciplineLessonTaskToId = _dltId,
                DisciplineLessonTaskFromId = new Guid(comboBoxDisciplineLessonTask.SelectedValue.ToString())
            });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При дублировании возникла ошибка: ", result.Errors);
            }
            MessageBox.Show("Сделано", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}