using ControlsAndForms.Messangers;
using Enums;
using LearningProgressInterfaces.BindingModels;
using LearningProgressInterfaces.Interfaces;
using System;
using System.Windows.Forms;
using Tools;

namespace LearningProgressControlsAndForms.Services
{
    public partial class FormDisciplineLessons : Form
    {
        private readonly ILearningProgressProcess _process;

        private LearningProcessFormDisciplineLessonsBindingModel _model;

        public FormDisciplineLessons(ILearningProgressProcess process, LearningProcessFormDisciplineLessonsBindingModel model)
        {
            InitializeComponent();
            _process = process;
            _model = model;
        }

        private void FormDisciplineLessons_Load(object sender, EventArgs e)
        {
            foreach (var elem in Enum.GetValues(typeof(Semesters)))
            {
                comboBoxSemester.Items.Add(elem.ToString());
            }
            comboBoxSemester.SelectedIndex = 0;
        }

        private void ButtonForm_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(textBoxCountLessons.Text))
            {
                MessageBox.Show("Укажите количество занятий", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(textBoxCountLessons.Text, out int order))
            {
                MessageBox.Show("Невозможно получить количество занятий", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _model.Semester = comboBoxSemester.Text;
            _model.CountLessons = order;

            ResultService result = _process.FormDisciplineLessons(_model);
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
            }
            MessageBox.Show("Сделано", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}