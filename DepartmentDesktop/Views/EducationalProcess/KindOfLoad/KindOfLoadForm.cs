using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.EducationalProcess.KindOfLoad
{
	public partial class KindOfLoadForm : Form
	{
		private readonly IKindOfLoadService _service;

		private Guid? _id;

		public KindOfLoadForm(IKindOfLoadService service, Guid? id = null)
		{
			InitializeComponent();
			_service = service;
			_id = id;
		}

		private void KindOfLoadForm_Load(object sender, EventArgs e)
		{
			foreach (var elem in Enum.GetValues(typeof(KindOfLoadType)))
			{
				comboBoxKindOfLoadTypes.Items.Add(elem.ToString());
			}
			comboBoxKindOfLoadTypes.SelectedIndex = 0;
			if (_id.HasValue)
			{
				LoadData();
			}
		}

		private void LoadData()
		{
			var result = _service.GetKindOfLoad(new KindOfLoadGetBindingModel { Id = _id.Value });
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				Close();
			}
			var entity = result.Result;

			textBoxTitle.Text = entity.KindOfLoadName;
			comboBoxKindOfLoadTypes.SelectedIndex = comboBoxKindOfLoadTypes.Items.IndexOf(entity.KindOfLoadType);
		}

		private bool CheckFill()
		{
			if (string.IsNullOrEmpty(comboBoxKindOfLoadTypes.Text))
			{
				return false;
			}
			if (string.IsNullOrEmpty(textBoxTitle.Text))
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
					result = _service.CreateKindOfLoad(new KindOfLoadRecordBindingModel
					{
						KindOfLoadType = comboBoxKindOfLoadTypes.Text,
						KindOfLoadName = textBoxTitle.Text
					});
				}
				else
				{
					result = _service.UpdateKindOfLoad(new KindOfLoadRecordBindingModel
					{
						Id = _id.Value,
						KindOfLoadType = comboBoxKindOfLoadTypes.Text,
						KindOfLoadName = textBoxTitle.Text
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
