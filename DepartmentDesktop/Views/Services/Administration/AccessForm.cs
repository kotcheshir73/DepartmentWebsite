using DepartmentDAL;
using DepartmentDAL.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.Services.Administration
{
	public partial class AccessForm : Form
	{
		private readonly IAccessService _service;

		private long _roleId;

		private long? _id;

		public AccessForm(IAccessService service, long roleId, long? id = null)
		{
			InitializeComponent();
			_service = service;
			_roleId = roleId;
			_id = id;
		}

		private void AccessForm_Load(object sender, EventArgs e)
		{
			foreach (var elem in Enum.GetValues(typeof(AccessOperation)))
			{
				comboBoxAccessOperation.Items.Add(elem.ToString());
			}
			comboBoxAccessOperation.SelectedIndex = 0;

			foreach (var elem in Enum.GetValues(typeof(AccessType)))
			{
				comboBoxAccessType.Items.Add(elem.ToString());
			}
			comboBoxAccessType.SelectedIndex = 0;

			if (_id.HasValue)
			{
				LoadData();
			}
		}

		private void LoadData()
		{
			var result = _service.GetAccess(new AccessGetBindingModel { Id = _id.Value });
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				Close();
			}
			var entity = result.Result;

			comboBoxAccessOperation.SelectedIndex = comboBoxAccessOperation.Items.IndexOf(entity.Operation);
			comboBoxAccessType.SelectedIndex = comboBoxAccessType.Items.IndexOf(entity.AccessType);
		}

		private bool CheckFill()
		{
			if (string.IsNullOrEmpty(comboBoxAccessOperation.Text))
			{
				return false;
			}
			if (string.IsNullOrEmpty(comboBoxAccessType.Text))
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
					result = _service.CreateAccess(new AccessRecordBindingModel
					{
						AccessType = comboBoxAccessType.Text,
						Operation = comboBoxAccessOperation.Text,
						RoleId = _roleId
					});
				}
				else
				{
					result = _service.UpdateAccess(new AccessRecordBindingModel
					{
						Id = _id.Value,
						AccessType = comboBoxAccessType.Text,
						Operation = comboBoxAccessOperation.Text,
						RoleId = _roleId
					});
				}
				if (result.Succeeded)
				{
					if (result.Result != null)
					{
						if (result.Result is long)
						{
							_id = (long)result.Result;
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
