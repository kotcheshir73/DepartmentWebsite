using DepartmentModel;
using System;
using System.Drawing;
using System.Windows.Forms;
using TicketServiceInterfaces.BindingModels;
using TicketServiceInterfaces.Interfaces;
using Unity;
using Unity.Attributes;

namespace TicketViews.Views.ExaminationTemplateBlockQuestion
{
    public partial class ExaminationTemplateBlockQuestionForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private IExaminationTemplateBlockQuestionService _service;

        private Guid? _id = null;

        public ExaminationTemplateBlockQuestionForm(IExaminationTemplateBlockQuestionService service, IExaminationTemplateBlockService serviceB,
            Guid examinationTemplateBlockId, Guid examinationTemplateId, Guid? id = null)
        {
            InitializeComponent();
            _service = service;
            examinationTemplateBlockElement.Service = serviceB;
            examinationTemplateBlockElement.ExaminationTemplateId = examinationTemplateId;
            examinationTemplateBlockElement.Id = examinationTemplateBlockId;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void ExaminationTemplateBlockQuestionForm_Load(object sender, EventArgs e)
        {
            if (_id.HasValue)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            var result = _service.GetExaminationTemplateBlockQuestion(new ExaminationTemplateBlockQuestionGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            numericUpDownQuestionNumber.Value = entity.QuestionNumber;
            textBoxQuestionText.Text = entity.QuestionText;
            pictureBoxQuestionImage.Image = entity.QuestionImage;
        }

        private bool CheckFill()
        {
            return true;
        }

        private bool Save()
        {
            if (CheckFill())
            {
                ResultService result;
                ImageConverter converter = new ImageConverter();
                if (!_id.HasValue)
                {
                    result = _service.CreateExaminationTemplateBlockQuestion(new ExaminationTemplateBlockQuestionSetBindingModel
                    {
                        ExaminationTemplateBlockId = examinationTemplateBlockElement.Id.Value,
                        QuestionNumber = (int)numericUpDownQuestionNumber.Value,
                        QuestionText = textBoxQuestionText.Text,
                        QuestionImage = (byte[])converter.ConvertTo(pictureBoxQuestionImage.Image, typeof(byte[]))
                    });
                }
                else
                {
                    result = _service.UpdateExaminationTemplateBlockQuestion(new ExaminationTemplateBlockQuestionSetBindingModel
                    {
                        Id = _id.Value,
                        ExaminationTemplateBlockId = examinationTemplateBlockElement.Id.Value,
                        QuestionNumber = (int)numericUpDownQuestionNumber.Value,
                        QuestionText = textBoxQuestionText.Text,
                        QuestionImage = (byte[])converter.ConvertTo(pictureBoxQuestionImage.Image, typeof(byte[]))
                    });
                }
                if (result.Succeeded)
                {
                    if (result.Result != null)
                    {
                        if (result.Result is Guid)
                        {
                            _id = (Guid)result.Result;
                        }
                    }
                    return true;
                }
                else
                {
                    Program.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Заполните все обязательные поля", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                MessageBox.Show("Сохранение прошло успешно", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
        }

        private void ButtonSaveAndClose_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ButtonLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBoxQuestionImage.ClientSize = new Size(150, 150);
                    pictureBoxQuestionImage.Image = new Bitmap(dialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка при загрузке файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}