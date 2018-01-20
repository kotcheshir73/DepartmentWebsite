using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.EducationalProcess.SeasonDates
{
    public partial class SeasonDatesForm : Form
    {
        private readonly ISeasonDatesService _service;

        private Guid? _id;

        public SeasonDatesForm(ISeasonDatesService service, Guid? id = null)
        {
            InitializeComponent();
            _service = service;
            _id = id;
        }

        private void SeasonDatesForm_Load(object sender, EventArgs e)
        {
            if (_id.HasValue)
            {
				LoadData();
			}
		}

		private void LoadData()
		{
			var result = _service.GetSeasonDates(new SeasonDatesGetBindingModel { Id = _id.Value });
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				Close();
			}
			var entity = result.Result;

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

		private bool Save()
		{
			if (CheckFill())
			{
				DateTime? dateBeginPractic = null;
				DateTime? dateEndPractic = null;
				if(checkBoxDateBeginPractic.Checked)
				{
					dateBeginPractic = dateTimePickerDateBeginPractic.Value;
				}
				if(checkBoxDateEndPractic.Checked)
				{
					dateBeginPractic = dateTimePickerDateEndPractic.Value;
				}
				ResultService result;
				if (!_id.HasValue)
				{
					result = _service.CreateSeasonDates(new SeasonDatesRecordBindingModel
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
				}
				else
				{
					result = _service.UpdateSeasonDates(new SeasonDatesRecordBindingModel
					{
						Id = _id.Value,
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

		private void buttonSave_Click(object sender, EventArgs e)
		{
			if (Save())
			{
				MessageBox.Show("Сохранение прошло успешно", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
				LoadData();
			}
		}

		private void buttonSaveAndClose_Click(object sender, EventArgs e)
		{
			if (Save())
			{
				DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
