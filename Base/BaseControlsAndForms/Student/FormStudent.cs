using BaseControlsAndForms.Services;
using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Tools;
using Unity;
using Unity.Resolution;

namespace BaseControlsAndForms.Student
{
    public partial class FormStudent : StandartForm
{
    [Dependency]
    public new IUnityContainer Container { get; set; }

    private readonly IStudentService _service;

        public FormStudent(IStudentService service, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
        }

        protected override bool LoadComponents()
        {
            var resultSG = _service.GetStudentGroups(new StudentGroupGetBindingModel { });
            if (!resultSG.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке групп возникла ошибка: ", resultSG.Errors);
                return false;
            }

            comboBoxStudentGroup.ValueMember = "Value";
            comboBoxStudentGroup.DisplayMember = "Display";
            comboBoxStudentGroup.DataSource = resultSG.Result.List
                .Select(ed => new { Value = ed.Id, Display = ed.GroupName }).ToList();
            comboBoxStudentGroup.SelectedItem = null;

            return true;
        }

        protected override void LoadData()
        {
            var result = _service.GetStudent(new StudentGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            textBoxNumberOfBook.Text = entity.NumberOfBook;
            textBoxLastName.Text = entity.LastName;
            textBoxFirstName.Text = entity.FirstName;
            textBoxPatronymic.Text = entity.Patronymic;
            textBoxEmail.Text = entity.Email;
            textBoxDescription.Text = entity.Description;
            if (entity.Photo != null)
            {
                pictureBoxPhoto.Image = entity.Photo;
            }
            if (entity.StudentGroupId.HasValue)
            {
                comboBoxStudentGroup.SelectedValue = entity.StudentGroupId;
            }
            checkBoxIsSteward.Checked = entity.IsSteward;
        }

        protected override bool CheckFill()
        {
            if (comboBoxStudentGroup.SelectedValue == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxNumberOfBook.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxLastName.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxFirstName.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxEmail.Text))
            {
                return false;
            }
            return true;
        }

        protected override bool Save()
        {
            ImageConverter converter = new ImageConverter();
            ResultService result;
            if (_id.HasValue)
            {
                result = _service.UpdateStudent(new StudentSetBindingModel
                {
                    Id = _id.Value,
                    StudentGroupId = new Guid(comboBoxStudentGroup.SelectedValue.ToString()),
                    NumberOfBook = textBoxNumberOfBook.Text,
                    LastName = textBoxLastName.Text,
                    FirstName = textBoxFirstName.Text,
                    Patronymic = textBoxPatronymic.Text,
                    Email = textBoxEmail.Text,
                    Description = textBoxDescription.Text,
                    Photo = pictureBoxPhoto.Image != null ? (byte[])converter.ConvertTo(pictureBoxPhoto.Image, typeof(byte[])) : null,
                    IsSteward = checkBoxIsSteward.Checked
                });
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
            return false;
        }

        private void ButtonUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBoxPhoto.ClientSize = new Size(150, 150);
                    pictureBoxPhoto.Image = new Bitmap(dialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка при загрузке файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonStudentOrdersShow_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormStudentOrdersShow>(
                new ParameterOverrides
                {
                    { "id", _id }
                }
                .OnType<FormStudentOrdersShow>());
            form.ShowDialog();
        }
    }
}