using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.LearningProgress.DisciplineLesson
{
    public partial class FormDisciplineLessonsForm : Form
    {
        private readonly ILearningProgressProcess _process;

        private LearningProcessFormDisciplineLessonsBindingModel _model;

        public FormDisciplineLessonsForm(ILearningProgressProcess process, LearningProcessFormDisciplineLessonsBindingModel model)
        {
            InitializeComponent();
            _process = process;
            _model = model;
        }

        private void FormDisciplineLessonsForm_Load(object sender, EventArgs e)
        {
            foreach (var elem in Enum.GetValues(typeof(Semesters)))
            {
                comboBoxSemester.Items.Add(elem.ToString());
            }
            comboBoxSemester.SelectedIndex = 0;
        }

        private void buttonForm_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(textBoxCountLessons.Text))
            {
                MessageBox.Show("Укажите количество занятий", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int order = 0;
            if (!int.TryParse(textBoxCountLessons.Text, out order))
            {
                MessageBox.Show("Невозможно получить количество занятий", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _model.Semester = comboBoxSemester.Text;
            _model.CountLessons = order;

            ResultService result = _process.FormDisciplineLessons(_model);
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
            }
            MessageBox.Show("Сделано", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
