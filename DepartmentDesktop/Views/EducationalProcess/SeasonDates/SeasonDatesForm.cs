using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.EducationalProcess.SeasonDates
{
    public partial class SeasonDatesForm : Form
    {
        private readonly ISeasonDatesService _service;

        private long _id = 0;

        public SeasonDatesForm(ISeasonDatesService service)
        {
            InitializeComponent();
            _service = service;
        }

        public SeasonDatesForm(ISeasonDatesService service, long id)
        {
            InitializeComponent();
            _service = service;
            _id = id;
        }

        private void SeasonDatesForm_Load(object sender, EventArgs e)
        {
            if (_id != 0)
            {
                var entity = _service.GetSeasonDates(new SeasonDatesGetBindingModel { Id = _id });
                if (entity == null)
                {
                    MessageBox.Show("Запись не найдена", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
                textBoxTitle.Text = entity.Title;
                dateTimePickerDateBeginExamination.Value = Convert.ToDateTime(entity.DateBeginExamination);
                dateTimePickerDateBeginOffset.Value = Convert.ToDateTime(entity.DateBeginOffset);
                dateTimePickerDateBeginSemester.Value = Convert.ToDateTime(entity.DateBeginSemester);
                dateTimePickerDateEndExamination.Value = Convert.ToDateTime(entity.DateEndExamination);
                dateTimePickerDateEndOffset.Value = Convert.ToDateTime(entity.DateEndOffset);
                dateTimePickerDateEndSemester.Value = Convert.ToDateTime(entity.DateEndSemester);
                dateTimePickerDateBeginPractic.Enabled = !string.IsNullOrEmpty(entity.DateBeginPractice);
                if (!string.IsNullOrEmpty(entity.DateBeginPractice))
                {
                    dateTimePickerDateBeginPractic.Value = Convert.ToDateTime(entity.DateBeginPractice);
                }
                dateTimePickerDateEndPractic.Enabled = !string.IsNullOrEmpty(entity.DateEndPractice);
                if (!string.IsNullOrEmpty(entity.DateEndPractice))
                {
                    dateTimePickerDateEndPractic.Value = Convert.ToDateTime(entity.DateEndPractice);
                }
                checkBoxDateBeginPractic.Checked = !string.IsNullOrEmpty(entity.DateBeginPractice);
                checkBoxDateEndPractic.Checked = !string.IsNullOrEmpty(entity.DateEndPractice);
            }
        }

        private bool CheckFill()
        {
            if (string.IsNullOrEmpty(textBoxTitle.Text))
            {
                return false;
            }
            if (dateTimePickerDateBeginSemester.Value == dateTimePickerDateEndSemester.Value)
            {
                return false;
            }
            if (dateTimePickerDateBeginOffset.Value == dateTimePickerDateEndOffset.Value)
            {
                return false;
            }
            if (dateTimePickerDateBeginExamination.Value == dateTimePickerDateEndExamination.Value)
            {
                return false;
            }
            return true;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (CheckFill())
            {
                DateTime? dateBeginPractic = null;
                if (checkBoxDateBeginPractic.Checked)
                {
                    dateBeginPractic = dateTimePickerDateBeginPractic.Value;
                }
                DateTime? dateEndPractic = null;
                if (checkBoxDateEndPractic.Checked)
                {
                    dateEndPractic = dateTimePickerDateEndPractic.Value;
                }
                if (_id == 0)
                {
                    var res = _service.CreateSeasonDates(new SeasonDatesRecordBindingModel
                    {
                        Title = textBoxTitle.Text,
                        DateBeginExamination = dateTimePickerDateBeginExamination.Value,
                        DateBeginOffset = dateTimePickerDateBeginOffset.Value,
                        DateBeginSemester = dateTimePickerDateBeginSemester.Value,
                        DateEndExamination = dateTimePickerDateEndExamination.Value,
                        DateEndOffset = dateTimePickerDateEndOffset.Value,
                        DateEndSemester = dateTimePickerDateEndSemester.Value,
                        DateBeginPractice = dateBeginPractic,
                        DateEndPractice = dateEndPractic
                    });
                    if (res.Succeeded)
                    {
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("При сохранении возникла ошибка: " + res.Errors["error"], "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    var res = _service.UpdateSeasonDates(new SeasonDatesRecordBindingModel
                    {
                        Id = _id,
                        Title = textBoxTitle.Text,
                        DateBeginExamination = dateTimePickerDateBeginExamination.Value,
                        DateBeginOffset = dateTimePickerDateBeginOffset.Value,
                        DateBeginSemester = dateTimePickerDateBeginSemester.Value,
                        DateEndExamination = dateTimePickerDateEndExamination.Value,
                        DateEndOffset = dateTimePickerDateEndOffset.Value,
                        DateEndSemester = dateTimePickerDateEndSemester.Value,
                        DateBeginPractice = dateBeginPractic,
                        DateEndPractice = dateEndPractic
                    });
                    if (res.Succeeded)
                    {
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("При сохранении возникла ошибка: " + res.Errors["error"], "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Заполните все обязательные поля", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
