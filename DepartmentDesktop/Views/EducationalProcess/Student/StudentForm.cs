using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.EducationalProcess.Student
{
    public partial class StudentForm : Form
    {
        private readonly IStudentService _service;

        private string _id = string.Empty;

        public StudentForm(IStudentService service)
        {
            InitializeComponent();
            _service = service;
        }

        public StudentForm(IStudentService service, string id)
        {
            InitializeComponent();
            _service = service;
            _id = id;
        }

        private void StudentForm_Load(object sender, EventArgs e)
        {
			var resultSG = _service.GetStudentGroups();
			if (!resultSG.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке групп возникла ошибка: ", resultSG.Errors);
				return;
			}

			comboBoxStudentGroup.ValueMember = "Value";
            comboBoxStudentGroup.DisplayMember = "Display";
            comboBoxStudentGroup.DataSource = resultSG.Result
				.Select(ed => new { Value = ed.Id, Display = ed.GroupName }).ToList();
            comboBoxStudentGroup.SelectedItem = null;

            if (!string.IsNullOrEmpty(_id))
            {
				LoadData();
			}
		}

		private void LoadData()
		{
			var result = _service.GetStudent(new StudentGetBindingModel { NumberOfBook = _id });
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				Close();
			}
			var entity = result.Result;

			textBoxNumberOfBook.Text = _id;
			textBoxNumberOfBook.Enabled = false;
			textBoxLastName.Text = entity.LastName;
			textBoxFirstName.Text = entity.FirstName;
			textBoxPatronymic.Text = entity.Patronymic;
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
            return true;
		}

		private bool Save()
		{
			if (CheckFill())
			{
				ImageConverter converter = new ImageConverter();
				ResultService result;
				if (!string.IsNullOrEmpty(_id))
				{
					result = _service.UpdateStudent(new StudentRecordBindingModel
					{
						NumberOfBook = textBoxNumberOfBook.Text,
						LastName = textBoxLastName.Text,
						FirstName = textBoxFirstName.Text,
						Patronymic = textBoxPatronymic.Text,
						Description = textBoxDescription.Text,
						Photo = (byte[])converter.ConvertTo(pictureBoxPhoto.Image, typeof(byte[]))
					});
					if (result.Succeeded)
					{
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
