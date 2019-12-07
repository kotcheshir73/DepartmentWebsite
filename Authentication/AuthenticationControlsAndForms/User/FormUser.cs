using AuthenticationControlsAndForms.Services;
using AuthenticationInterfaces.BindingModels;
using AuthenticationInterfaces.Interfaces;
using BaseInterfaces.BindingModels;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using Enums;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Tools;
using Unity;
using Unity.Resolution;

namespace AuthenticationControlsAndForms.User
{
    public partial class FormUser : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IUserService _service;

        public FormUser(IUserService service, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
        }

        protected override bool LoadComponents()
        {
            var resultS = _service.GetStudents(new StudentGetBindingModel { StudentStatus = StudentState.Учится });
            if (!resultS.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке студентов возникла ошибка: ", resultS.Errors);
                return false;
            }

            comboBoxStudent.ValueMember = "Value";
            comboBoxStudent.DisplayMember = "Display";
            comboBoxStudent.DataSource = resultS.Result.List
                .Select(d => new { Value = d.Id, Display = d.FullName }).ToList();
            comboBoxStudent.SelectedItem = null;

            var resultL = _service.GetLecturers(new LecturerGetBindingModel { });
            if (!resultL.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке студентов возникла ошибка: ", resultL.Errors);
                return false;
            }

            comboBoxLecturer.ValueMember = "Value";
            comboBoxLecturer.DisplayMember = "Display";
            comboBoxLecturer.DataSource = resultL.Result.List
                .Select(d => new { Value = d.Id, Display = d.FullName }).ToList();
            comboBoxLecturer.SelectedItem = null;

            return true;
        }

        protected override void LoadData()
        {
            textBoxPassword.Enabled = false;
            buttonChangePassword.Visible = true;

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

        protected override bool CheckFill()
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

        protected override bool Save()
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

        private void ButtonUpload_Click(object sender, EventArgs e)
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

        private void ButtonChangePassword_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<ChangeUserPasswordForm>(
                new ParameterOverrides
                {
                    { "id", _id.Value }
                }
                .OnType<ChangeUserPasswordForm>());
            form.ShowDialog();
        }
    }
}