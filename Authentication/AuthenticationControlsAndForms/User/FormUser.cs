using AuthenticationInterfaces.BindingModels;
using AuthenticationInterfaces.Interfaces;
using ControlsAndForms.Messangers;
using Enums;
using Interfaces.BindingModels;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Tools;
using Unity;

namespace AuthenticationControlsAndForms.User
{
    public partial class FormUser : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IUserService _service;

		private Guid? _id = null;

		public FormUser(IUserService service, Guid? id = null)
		{
			InitializeComponent();
			_service = service;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

		private void FormUser_Load(object sender, EventArgs e)
		{
			var resultS = _service.GetStudents(new StudentGetBindingModel { StudentStatus = StudentState.Учится });
			if (!resultS.Succeeded)
			{
                ErrorMessanger.PrintErrorMessage("При загрузке студентов возникла ошибка: ", resultS.Errors);
				return;
			}

			comboBoxStudent.ValueMember = "Value";
			comboBoxStudent.DisplayMember = "Display";
			comboBoxStudent.DataSource = resultS.Result.List
				.Select(d => new { Value = d.NumberOfBook, Display = d.FullName }).ToList();
			comboBoxStudent.SelectedItem = null;

			var resultL = _service.GetLecturers(new LecturerGetBindingModel { });
			if (!resultL.Succeeded)
			{
                ErrorMessanger.PrintErrorMessage("При загрузке студентов возникла ошибка: ", resultL.Errors);
				return;
			}

			comboBoxLecturer.ValueMember = "Value";
			comboBoxLecturer.DisplayMember = "Display";
			comboBoxLecturer.DataSource = resultL.Result.List
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
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				Close();
			}
			var entity = result.Result;

			textBoxLogin.Text = entity.Login;
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
			return true;
		}

		private bool Save()
		{
			if (CheckFill())
			{
				ImageConverter converter = new ImageConverter();
				ResultService result;
                Guid? studentId = null;
				if (comboBoxStudent.SelectedValue != null)
				{
					studentId = new Guid(comboBoxStudent.SelectedValue.ToString());
				}
                Guid? lecturerId = null;
				if (comboBoxLecturer.SelectedValue != null)
				{
					lecturerId = new Guid(comboBoxLecturer.SelectedValue.ToString());
				}
				if (!_id.HasValue)
				{
					result = _service.CreateUser(new UserSetBindingModel
					{
						Login = textBoxLogin.Text,
						Password = textBoxPassword.Text,
						StudentId = studentId,
						LecturerId = lecturerId,
						Avatar = (byte[])converter.ConvertTo(pictureBoxAvatar.Image, typeof(byte[])),
						IsBanned = checkBoxBanned.Checked
					});
				}
				else
				{
					result = _service.UpdateUser(new UserSetBindingModel
					{
						Id = _id.Value,
						Login = textBoxLogin.Text,
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