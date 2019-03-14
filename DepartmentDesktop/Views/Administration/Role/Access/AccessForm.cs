using AuthenticationServiceInterfaces.BindingModels;
using AuthenticationServiceInterfaces.Interfaces;
using DepartmentModel;
using DepartmentModel.Enums;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace DepartmentDesktop.Views.Administration.Access
{
    public partial class AccessForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IAccessService _service;

		private Guid _roleId;

		private Guid? _id = null;

		public AccessForm(IAccessService service, Guid roleId, Guid? id = null)
		{
			InitializeComponent();
			_service = service;
			_roleId = roleId;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

		private void AccessForm_Load(object sender, EventArgs e)
		{
			foreach (var elem in Enum.GetValues(typeof(AccessOperation)))
			{
				comboBoxAccessOperation.Items.Add(elem.ToString());
			}
			comboBoxAccessOperation.SelectedItem = null;

			foreach (var elem in Enum.GetValues(typeof(AccessType)))
			{
				comboBoxAccessType.Items.Add(elem.ToString());
			}
			comboBoxAccessType.SelectedItem = null;

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
					result = _service.CreateAccess(new AccessSetBindingModel
					{
						AccessType = comboBoxAccessType.Text,
						Operation = comboBoxAccessOperation.Text,
						RoleId = _roleId
					});
				}
				else
				{
					result = _service.UpdateAccess(new AccessSetBindingModel
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
