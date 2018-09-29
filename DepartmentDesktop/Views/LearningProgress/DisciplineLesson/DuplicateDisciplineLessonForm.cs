using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.LearningProgress.DisciplineLesson
{
    public partial class DuplicateDisciplineLessonForm : Form
    {
        private readonly ILearningProgressProcess _process;

        private Guid _dlId;

        public DuplicateDisciplineLessonForm(ILearningProgressProcess process, Guid dlId)
        {
            InitializeComponent();
            _process = process;
            _dlId = dlId;
        }

        private void DuplicateDisciplineLessonForm_Load(object sender, EventArgs e)
        {
            var resultDL = _process.GetDisiplineLessonsForDuplicate(new GetDisiplineLessonsForDuplicateBindingModel { DisciplineLessonId = _dlId });
            if (!resultDL.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке занятий возникла ошибка: ", resultDL.Errors);
                return;
            }

            comboBoxDisciplineLesson.ValueMember = "Value";
            comboBoxDisciplineLesson.DisplayMember = "Display";
            comboBoxDisciplineLesson.DataSource = resultDL.Result
                .Select(d => new { Value = d.Id, Display = d.Title }).ToList();
            comboBoxDisciplineLesson.SelectedItem = null;
        }

        private void buttonDuplicate_Click(object sender, EventArgs e)
        {
            ResultService result = _process.DuplicateDisiplineLessons(new DuplicateDisiplineLessonsBindingModel
            {
                DisciplineLessonToId = _dlId,
                DisciplineLessonFromId = new Guid(comboBoxDisciplineLesson.SelectedValue.ToString()),
                CopyVariants = checkBoxDuplicateTaskVariants.Checked
            });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При дублировании возникла ошибка: ", result.Errors);
            }
            MessageBox.Show("Сделано", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
