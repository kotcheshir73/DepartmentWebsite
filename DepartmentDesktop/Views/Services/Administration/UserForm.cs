using DepartmentDAL;
using DepartmentDAL.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.Services.Administration
{
	public partial class UserForm : Form
	{
		private readonly IUserService _service;

		private long? _id = 0;

		public UserForm(IUserService service, long? id = null)
		{
			InitializeComponent();
			_service = service;
			_id = id;
		}

		private void UserForm_Load(object sender, EventArgs e)
		{
			var resultR = _service.GetRoles();
			if (!resultR.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке ролей возникла ошибка: ", resultR.Errors);
				return;
			}

			comboBoxRole.ValueMember = "Value";
			comboBoxRole.DisplayMember = "Display";
			comboBoxRole.DataSource = resultR.Result
				.Select(d => new { Value = d.Id, Display = d.RoleName }).ToList();
			comboBoxRole.SelectedItem = null;

			var resultS = _service.GetStudents(new StudentGetBindingModel { StudentStatus = StudentState.Учится });
			if (!resultS.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке студентов возникла ошибка: ", resultS.Errors);
				return;
			}

			comboBoxStudent.ValueMember = "Value";
			comboBoxStudent.DisplayMember = "Display";
			comboBoxStudent.DataSource = resultS.Result.List
				.Select(d => new { Value = d.NumberOfBook, Display = d.FullName }).ToList();
			comboBoxStudent.SelectedItem = null;

			var resultL = _service.GetLecturers();
			if (!resultL.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке студентов возникла ошибка: ", resultL.Errors);
				return;
			}

			comboBoxLecturer.ValueMember = "Value";
			comboBoxLecturer.DisplayMember = "Display";
			comboBoxLecturer.DataSource = resultL.Result
				.Select(d => new { Value = d.Id, Display = d.FullName }).ToList();
			comboBoxLecturer.SelectedItem = null;

			if (_id.HasValue)
			{
				LoadData();
			}
			else
			{
				textBoxPassword.Enabled = true;
			}
		}

		private void LoadData()
		{
			var result = _service.GetUser(new UserGetBindingModel { Id = _id.Value });
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				Close();
			}
			var entity = result.Result;

			textBoxLogin.Text = entity.Login;
			comboBoxRole.SelectedValue = entity.RoleId;
			if (entity.StudentId != null)
			{
				comboBoxStudent.SelectedValue = entity.StudentId;
			}
			if (entity.LecturerId != null)
			{
				comboBoxLecturer.SelectedValue = entity.LecturerId;
			}
			checkBoxBanned.Checked = entity.IsBanned;
		}

		private bool CheckFill()
		{
			if (string.IsNullOrEmpty(textBoxLogin.Text))
			{
				return false;
			}
			if (string.IsNullOrEmpty(textBoxPassword.Text) && textBoxPassword.Enabled)
			{
				return false;
			}
			if (comboBoxRole.SelectedValue == null)
			{
				return false;
			}
			return true;
		}

		private bool Save()
		{
			if (CheckFill())
			{
				ImageConverter converter = new ImageConverter();
				ResultService result;
				long? studentId = null;
				if (comboBoxStudent.SelectedValue != null)
				{
					studentId = Convert.ToInt64(comboBoxStudent.SelectedValue);
				}
				long? lecturerId = null;
				if (comboBoxLecturer.SelectedValue != null)
				{
					lecturerId = Convert.ToInt64(comboBoxLecturer.SelectedValue);
				}
				if (!_id.HasValue)
				{
					result = _service.CreateUser(new UserRecordBindingModel
					{
						Login = textBoxLogin.Text,
						Password = textBoxPassword.Text,
						RoleId = Convert.ToInt64(comboBoxRole.SelectedValue),
						StudentId = studentId,
						LecturerId = lecturerId,
						Avatar = (byte[])converter.ConvertTo(pictureBoxAvatar.Image, typeof(byte[])),
						IsBanned = checkBoxBanned.Checked
					});
				}
				else
				{
					result = _service.UpdateUser(new UserRecordBindingModel
					{
						Id = _id.Value,
						Login = textBoxLogin.Text,
						RoleId = Convert.ToInt64(comboBoxRole.SelectedValue),
						StudentId = studentId,
						LecturerId = lecturerId,
						Avatar = (byte[])converter.ConvertTo(pictureBoxAvatar.Image, typeof(byte[])),
						IsBanned = checkBoxBanned.Checked
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

		private void buttonUpload_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					pictureBoxAvatar.ClientSize = new Size(150, 150);
					pictureBoxAvatar.Image = new Bitmap(dialog.FileName);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Ошибка при загрузке файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
	}
}
