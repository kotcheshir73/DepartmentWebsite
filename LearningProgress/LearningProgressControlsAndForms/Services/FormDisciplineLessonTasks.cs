using ControlsAndForms.Messangers;
using LearningProgressInterfaces.BindingModels;
using LearningProgressInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Tools;

namespace LearningProgressControlsAndForms.Services
{
    public partial class FormDisciplineLessonTasks : Form
    {
        private readonly ILearningProgressProcess _process;

        private Guid _dlId;

        public FormDisciplineLessonTasks(ILearningProgressProcess process, Guid dlId)
        {
            InitializeComponent();
            _process = process;
            _dlId = dlId;
        }

        private void TextBoxCountTasks_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxCountTasks.Text))
            {
                if (int.TryParse(textBoxCountTasks.Text, out int count))
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

        private void CheckBoxMaxBall_CheckedChanged(object sender, EventArgs e)
        {
            textBoxMaxBall.Enabled = checkBoxMaxBall.Checked;
        }

        private void ButtonForm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxTaskTemplate.Text))
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
                if (!decimal.TryParse(textBoxMaxBall.Text, out decimal maxBal))
                {
                    MessageBox.Show("Невозможно получить максимальный балл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            ResultService result = _process.FormDisciplineLessonTasks(new LearningProcessFormDisciplineLessonTasksBindingModel
            {
                DisciplineLessonId = _dlId,
                TitleTemplate = textBoxTaskTemplate.Text,
                IsNecessarily = checkBoxIsNecessarily.Checked,
                MaxBall = checkBoxMaxBall.Checked ? Convert.ToDecimal(textBoxMaxBall.Text) : (decimal?)null,
                Tasks = tasks
            });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
                return;
            }
            MessageBox.Show("Сделано", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}