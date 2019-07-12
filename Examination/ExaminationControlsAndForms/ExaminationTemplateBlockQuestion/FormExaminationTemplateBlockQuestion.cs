﻿using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.Interfaces;
using System;
using System.Drawing;
using System.Windows.Forms;
using Tools;
using Unity;

namespace ExaminationControlsAndForms.ExaminationTemplateBlockQuestion
{
    public partial class FormExaminationTemplateBlockQuestion : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private IExaminationTemplateBlockQuestionService _service;

        public FormExaminationTemplateBlockQuestion(IExaminationTemplateBlockQuestionService service, IExaminationTemplateBlockService serviceB,
            Guid examinationTemplateBlockId, Guid examinationTemplateId, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
            examinationTemplateBlockElement.Service = serviceB;
            examinationTemplateBlockElement.ExaminationTemplateId = examinationTemplateId;
            examinationTemplateBlockElement.Id = examinationTemplateBlockId;
        }

        protected override void LoadData()
        {
            var result = _service.GetExaminationTemplateBlockQuestion(new ExaminationTemplateBlockQuestionGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            numericUpDownQuestionNumber.Value = entity.QuestionNumber;
            textBoxQuestionText.Text = entity.QuestionText;
            pictureBoxQuestionImage.Image = entity.QuestionImage;
        }

        protected override bool Save()
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
                ErrorMessanger.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
                return false;
            }
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