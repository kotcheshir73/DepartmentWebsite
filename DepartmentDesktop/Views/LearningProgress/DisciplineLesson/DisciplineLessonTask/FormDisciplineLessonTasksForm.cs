using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.LearningProgress.DisciplineLesson.DisciplineLessonTask
{
    public partial class FormDisciplineLessonTasksForm : Form
    {
        private readonly ILearningProgressProcess _process;

        private Guid _dlId;

        public FormDisciplineLessonTasksForm(ILearningProgressProcess process, Guid dlId)
        {
            InitializeComponent();
            _process = process;
            _dlId = dlId;
        }

        private void textBoxCountTasks_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxCountTasks.Text))
            {
                int count = 0;
                if (int.TryParse(textBoxCountTasks.Text, out count))
                {
                    groupBoxTasks.Controls.Clear();
                    for (int i = 0; i < count; ++i)
                    {
                        var textBoxDiscription = new TextBox
                        {
                            Location = new Point(6, 15 + i * 60),
                            Multiline = true,
                            Name = "textBoxDiscription" + i,
                            Size = new Size(325, 55),
                            TabIndex = i
                        };
                        groupBoxTasks.Controls.Add(textBoxDiscription);
                    }
                }
            }
        }

        private void checkBoxMaxBall_CheckedChanged(object sender, EventArgs e)
        {
            textBoxMaxBall.Enabled = checkBoxMaxBall.Checked;
        }

        private void buttonForm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(labelTaskTemplate.Text))
            {
                MessageBox.Show("Укажите шаблон заголовка задания", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (groupBoxTasks.Controls.Count == 0)
            {
                MessageBox.Show("Нет описаний заданиий", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<string> tasks = new List<string>();
            for (int i = 0; i < groupBoxTasks.Controls.Count; ++i)
            {
                var textBox = groupBoxTasks.Controls[i] as TextBox;
                if (textBox == null)
                {
                    MessageBox.Show("Неизвестный элемент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    MessageBox.Show(string.Format("{0}: Описание пустое", i + 1), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                tasks.Add(textBox.Text);
            }
            if (checkBoxIsNecessarily.Checked)
            {
                decimal maxBal = 0;
                if (!decimal.TryParse(textBoxMaxBall.Text, out maxBal))
                {
                    MessageBox.Show("Невозможно получить максимальный балл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            ResultService result = _process.FormDisciplineLessonTaskss(new LearningProcessFormDisciplineLessonTasksBindingModel
            {
                DisciplineLessonId = _dlId,
                TitleTemplate = textBoxTaskTemplate.Text,
                IsNecessarily = checkBoxIsNecessarily.Checked,
                MaxBall = checkBoxMaxBall.Checked ? Convert.ToDecimal(textBoxMaxBall.Text) : (decimal?)null,
                Tasks = tasks
            });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
            }
            MessageBox.Show("Сделано", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
