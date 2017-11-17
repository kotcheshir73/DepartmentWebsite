using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.Services.Administration
{
	public partial class RoleForm : Form
	{
		private readonly IRoleService _service;

		private readonly IAccessService _serviceA;

		private long? _id = 0;

		public RoleForm(IRoleService service, IAccessService serviceA, long? id = null)
		{
			InitializeComponent();
			_service = service;
			_serviceA = serviceA;
			_id = id;
		}

		private void RoleForm_Load(object sender, EventArgs e)
		{
			var control = new AccessControl(_serviceA);
			control.Left = 0;
			control.Top = 0;
			control.Height = Height - 60;
			control.Width = Width - 15;
			control.Anchor = (((AnchorStyles.Top
						| AnchorStyles.Bottom)
						| AnchorStyles.Left)
						| AnchorStyles.Right);
			tabPageAccesses.Controls.Add(control);

			if (_id.HasValue)
			{
				LoadData();
			}
		}

		private void LoadData()
		{
			(tabPageAccesses.Controls[0] as AccessControl).LoadData(_id.Value);
			var result = _service.GetRole(new RoleGetBindingModel { Id = _id.Value });
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				Close();
			}
			var entity = result.Result;

			textBoxRoleName.Text = entity.RoleName;
		}

		private bool CheckFill()
		{
			if (string.IsNullOrEmpty(textBoxRoleName.Text))
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
					result = _service.CreateRole(new RoleRecordBindingModel
					{
						RoleName = textBoxRoleName.Text
					});
				}
				else
				{
					result = _service.UpdateRole(new RoleRecordBindingModel
					{
						Id = _id.Value,
						RoleName = textBoxRoleName.Text
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
