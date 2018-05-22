using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace DepartmentDesktop.Views.EducationalProcess.Student
{
    public partial class StudentForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IStudentService _service;

        private Guid? _id = null;

		public StudentForm(IStudentService service, Guid? id = null)
        {
			InitializeComponent();
            _service = service;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void StudentForm_Load(object sender, EventArgs e)
        {
			var resultSG = _service.GetStudentGroups(new StudentGroupGetBindingModel { } );
			if (!resultSG.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке групп возникла ошибка: ", resultSG.Errors);
				return;
			}

			comboBoxStudentGroup.ValueMember = "Value";
            comboBoxStudentGroup.DisplayMember = "Display";
            comboBoxStudentGroup.DataSource = resultSG.Result.List
				.Select(ed => new { Value = ed.Id, Display = ed.GroupName }).ToList();
            comboBoxStudentGroup.SelectedItem = null;

            if (_id.HasValue)
            {
				LoadData();
			}
		}

		private void LoadData()
		{
			var result = _service.GetStudent(new StudentGetBindingModel { Id = _id.Value });
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				Close();
			}
			var entity = result.Result;

			textBoxNumberOfBook.Text = entity.NumberOfBook;
			textBoxNumberOfBook.Enabled = false;
			textBoxLastName.Text = entity.LastName;
			textBoxFirstName.Text = entity.FirstName;
			textBoxPatronymic.Text = entity.Patronymic;
			textBoxEmail.Text = entity.Email;
			textBoxDescription.Text = entity.Description;
			if (entity.Photo != null)
			{
				pictureBoxPhoto.Image = entity.Photo;
			}
			comboBoxStudentGroup.SelectedValue = entity.StudentGroupId;
		}

		private bool CheckFill()
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

		private bool Save()
		{
			if (CheckFill())
			{
				ImageConverter converter = new ImageConverter();
				ResultService result;
				if (_id.HasValue)
				{
					result = _service.UpdateStudent(new StudentSetBindingModel
					{
                        Id = _id.Value,
						NumberOfBook = textBoxNumberOfBook.Text,
						LastName = textBoxLastName.Text,
						FirstName = textBoxFirstName.Text,
						Patronymic = textBoxPatronymic.Text,
						Email = textBoxEmail.Text,
						Description = textBoxDescription.Text,
						Photo = (byte[])converter.ConvertTo(pictureBoxPhoto.Image, typeof(byte[]))
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
						Program.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
						return false;
					}
				}
				return false;
			}
			else
			{
				MessageBox.Show("Заполните все обязательные поля", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
		}

		private void buttonUpload_Click(object sender, EventArgs e)
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
