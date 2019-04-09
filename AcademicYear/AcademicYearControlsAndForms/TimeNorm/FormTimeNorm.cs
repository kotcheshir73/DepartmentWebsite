using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using BaseInterfaces.BindingModels;
using ControlsAndForms.Messangers;
using Enums;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Tools;

namespace AcademicYearControlsAndForms.TimeNorm
{
    public partial class FormTimeNorm : Form
	{
		private readonly ITimeNormService _service;

        private Guid _ayId;

		private Guid? _id = null;

        public FormTimeNorm(ITimeNormService service, Guid ayId, Guid? id = null)
		{
			InitializeComponent();
			_service = service;
            _ayId = ayId;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

		private void FormTimeNorm_Load(object sender, EventArgs e)
        {
            var resultAY = _service.GetAcademicYears(new AcademicYearGetBindingModel { Id = _ayId });
            if (!resultAY.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке учебных годов возникла ошибка: ", resultAY.Errors);
                return;
            }

            var resultDB = _service.GetDisciplineBlocks(new DisciplineBlockGetBindingModel { });
            if (!resultDB.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке блоков дисциплин возникла ошибка: ", resultDB.Errors);
                return;
            }

            comboBoxAcademicYear.ValueMember = "Value";
            comboBoxAcademicYear.DisplayMember = "Display";
            comboBoxAcademicYear.DataSource = resultAY.Result.List
                .Select(ay => new { Value = ay.Id, Display = ay.Title }).ToList();
            comboBoxAcademicYear.SelectedValue = _ayId;

            comboBoxDisciplineBlock.ValueMember = "Value";
            comboBoxDisciplineBlock.DisplayMember = "Display";
            comboBoxDisciplineBlock.DataSource = resultDB.Result.List
                .Select(ay => new { Value = ay.Id, Display = ay.Title }).ToList();
            comboBoxDisciplineBlock.SelectedItem = null;

            foreach (var elem in Enum.GetValues(typeof(AcademicLevel)))
            {
                comboBoxAcademicLevel.Items.Add(elem.ToString());
            }

            foreach (var elem in Enum.GetValues(typeof(KindOfLoadType)))
			{
				comboBoxSelectKindOfLoadType.Items.Add(elem.ToString());
			}
			comboBoxSelectKindOfLoadType.SelectedIndex = -1;

            foreach (var elem in Enum.GetValues(typeof(TimeNormKoef)))
            {
                comboBoxTimeNormKoef.Items.Add(elem.ToString());
            }
            comboBoxTimeNormKoef.SelectedIndex = -1;

            if (_id.HasValue)
			{
				LoadData();
			}
		}

		private void LoadData()
		{
			var result = _service.GetTimeNorm(new TimeNormGetBindingModel { Id = _id.Value });
			if (!result.Succeeded)
			{
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				Close();
			}
			var entity = result.Result;

            comboBoxDisciplineBlock.SelectedValue = entity.DisciplineBlockId;

			textBoxTimeNormName.Text = entity.TimeNormName;
            textBoxTimeNormShortName.Text = entity.TimeNormShortName;
            textBoxTimeNormOrder.Text = entity.TimeNormOrder.ToString();
            if (entity.TimeNormAcademicLevel != null)
            {
                comboBoxAcademicLevel.SelectedIndex = comboBoxAcademicLevel.Items.IndexOf(entity.TimeNormAcademicLevel);
            }

            textBoxKindOfLoadName.Text = entity.KindOfLoadName;
            textBoxKindOfLoadAttributeName.Text = entity.KindOfLoadAttributeName;
            textBoxKindOfLoadBlueAsteriskName.Text = entity.KindOfLoadBlueAsteriskName;
            textBoxKindOfLoadBlueAsteriskAttributeName.Text = entity.KindOfLoadBlueAsteriskAttributeName;
            textBoxKindOfLoadBlueAsteriskPracticName.Text = entity.KindOfLoadBlueAsteriskPracticName;

            textBoxHours.Text = entity.Hours.ToString();
            comboBoxSelectKindOfLoadType.SelectedIndex = comboBoxSelectKindOfLoadType.Items.IndexOf(entity.KindOfLoadType);
            textBoxNumKoef.Text = entity.NumKoef.ToString();
            comboBoxTimeNormKoef.SelectedIndex = comboBoxTimeNormKoef.Items.IndexOf(entity.TimeNormKoef);

            checkBoxUseInLearningProgress.Checked = entity.UseInLearningProgress;
        }

		private bool CheckFill()
        {
            if (comboBoxDisciplineBlock.SelectedValue == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxTimeNormName.Text))
			{
				return false;
            }
            if (string.IsNullOrEmpty(textBoxTimeNormShortName.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxTimeNormOrder.Text))
            {
                return false;
            }
            int order = 0;
            if (!int.TryParse(textBoxTimeNormOrder.Text, out order))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxKindOfLoadName.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(comboBoxSelectKindOfLoadType.Text))
            {
                return false;
            }
            decimal count = 0;
            if (!string.IsNullOrEmpty(textBoxHours.Text) && !decimal.TryParse(textBoxHours.Text, out count))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(textBoxNumKoef.Text) && !decimal.TryParse(textBoxNumKoef.Text, out count))
            {
                return false;
            }
            return true;
		}

		private bool Save()
		{
            if (CheckFill())
			{
				ResultService result;
				if (!_id.HasValue)
				{
                    result = _service.CreateTimeNorm(new TimeNormSetBindingModel
                    {
                        AcademicYearId = new Guid(comboBoxAcademicYear.SelectedValue.ToString()),
                        DisciplineBlockId = new Guid(comboBoxDisciplineBlock.SelectedValue.ToString()),
                        TimeNormName = textBoxTimeNormName.Text,
                        TimeNormShortName = textBoxTimeNormShortName.Text,
                        TimeNormOrder = Convert.ToInt32(textBoxTimeNormOrder.Text),
                        TimeNormAcademicLevel = string.IsNullOrEmpty(comboBoxAcademicLevel.Text) ? null : comboBoxAcademicLevel.Text,
                        KindOfLoadName = textBoxKindOfLoadName.Text,
                        KindOfLoadAttributeName = textBoxKindOfLoadAttributeName.Text,
                        KindOfLoadBlueAsteriskName = textBoxKindOfLoadBlueAsteriskName.Text,
                        KindOfLoadBlueAsteriskAttributeName = textBoxKindOfLoadBlueAsteriskAttributeName.Text,
                        KindOfLoadBlueAsteriskPracticName = textBoxKindOfLoadBlueAsteriskPracticName.Text,
                        KindOfLoadType = comboBoxSelectKindOfLoadType.Text,
                        Hours = !string.IsNullOrEmpty(textBoxHours.Text) ? Convert.ToDecimal(textBoxHours.Text) : (decimal?) null,
                        NumKoef = !string.IsNullOrEmpty(textBoxNumKoef.Text) ? Convert.ToDecimal(textBoxNumKoef.Text) : (decimal?)null,
                        TimeNormKoef = comboBoxTimeNormKoef.Text,
                        UseInLearningProgress = checkBoxUseInLearningProgress.Checked
                    });
				}
				else
				{
					result = _service.UpdateTimeNorm(new TimeNormSetBindingModel
					{
						Id = _id.Value,
                        AcademicYearId = new Guid(comboBoxAcademicYear.SelectedValue.ToString()),
                        DisciplineBlockId = new Guid(comboBoxDisciplineBlock.SelectedValue.ToString()),
                        TimeNormName = textBoxTimeNormName.Text,
                        TimeNormShortName = textBoxTimeNormShortName.Text,
                        TimeNormOrder = Convert.ToInt32(textBoxTimeNormOrder.Text),
                        TimeNormAcademicLevel = string.IsNullOrEmpty(comboBoxAcademicLevel.Text) ? null : comboBoxAcademicLevel.Text,
                        KindOfLoadName = textBoxKindOfLoadName.Text,
                        KindOfLoadAttributeName = textBoxKindOfLoadAttributeName.Text,
                        KindOfLoadBlueAsteriskName = textBoxKindOfLoadBlueAsteriskName.Text,
                        KindOfLoadBlueAsteriskAttributeName = textBoxKindOfLoadBlueAsteriskAttributeName.Text,
                        KindOfLoadBlueAsteriskPracticName = textBoxKindOfLoadBlueAsteriskPracticName.Text,
                        KindOfLoadType = comboBoxSelectKindOfLoadType.Text,
                        Hours = !string.IsNullOrEmpty(textBoxHours.Text) ? Convert.ToDecimal(textBoxHours.Text) : (decimal?)null,
                        NumKoef = !string.IsNullOrEmpty(textBoxNumKoef.Text) ? Convert.ToDecimal(textBoxNumKoef.Text) : (decimal?)null,
                        TimeNormKoef = comboBoxTimeNormKoef.Text,
                        UseInLearningProgress = checkBoxUseInLearningProgress.Checked
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