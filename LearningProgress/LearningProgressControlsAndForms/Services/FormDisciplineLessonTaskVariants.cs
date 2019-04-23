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
    public partial class FormDisciplineLessonTaskVariants : Form
    {
        private readonly ILearningProgressProcess _process;

        private Guid _dltId;

        public FormDisciplineLessonTaskVariants(ILearningProgressProcess process, Guid dltId)
        {
            InitializeComponent();
            _process = process;
            _dltId = dltId;
        }

        private void TextBoxCountVariants_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxCountVariants.Text))
            {
                if (int.TryParse(textBoxCountVariants.Text, out int count))
                {
                    panelVariants.Controls.Clear();
                    for (int i = 0; i < count; ++i)
                    {
                        var textBoxDiscription = new TextBox
                        {
                            Location = new Point(6, 15 + i * 60),
                            Multiline = true,
                            Name = "textBoxDiscription" + i,
                            Size = new Size(350, 55),
                            TabIndex = i
                        };
                        panelVariants.Controls.Add(textBoxDiscription);
                    }
                }

            }
        }

        private void ButtonForm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxVariantNumberTemplate.Text))
            {
                MessageBox.Show("Укажите шаблон заголовка номеров вариантов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (panelVariants.Controls.Count == 0)
            {
                MessageBox.Show("Нет описаний вариантов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<string> variants = new List<string>();
            for (int i = 0; i < panelVariants.Controls.Count; ++i)
            {
                var textBox = panelVariants.Controls[i] as TextBox;
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
                variants.Add(textBox.Text);
            }

            ResultService result = _process.FormDisciplineLessonVariants(new LearningProcessFormDisciplineLessonTaskVariantsBindingModel
            {
                DisciplineLessonTaskId = _dltId,
                VariantNumberTemplate = textBoxVariantNumberTemplate.Text,
                Variants = variants
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