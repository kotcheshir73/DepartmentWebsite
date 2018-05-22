using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace DepartmentDesktop.Views.EducationalProcess.EducationDirection
{
    public partial class EducationDirectionForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IEducationDirectionService _service;

        private Guid? _id = null;

        public EducationDirectionForm(IEducationDirectionService service, Guid? id = null)
        {
            InitializeComponent();
            _service = service;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void EducationDirectionForm_Load(object sender, EventArgs e)
        {
            if(_id.HasValue)
            {
				LoadData();
			}
		}

		private void LoadData()
		{
			var result = _service.GetEducationDirection(new EducationDirectionGetBindingModel { Id = _id.Value });
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				Close();
			}
			var entity = result.Result;
			textBoxCipher.Text = entity.Cipher;
            textBoxShortName.Text = entity.ShortName;
			textBoxTitle.Text = entity.Title;
			textBoxDescription.Text = entity.Description;
		}

		private bool CheckFill()
        {
            if(string.IsNullOrEmpty(textBoxCipher.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxShortName.Text))
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
					result = _service.CreateEducationDirection(new EducationDirectionSetBindingModel
					{
						Cipher = textBoxCipher.Text,
                        ShortName = textBoxShortName.Text,
						Description = textBoxDescription.Text,
						Title = textBoxTitle.Text
					});
				}
				else
				{
					result = _service.UpdateEducationDirection(new EducationDirectionSetBindingModel
					{
						Id = _id.Value,
						Cipher = textBoxCipher.Text,
                        ShortName = textBoxShortName.Text,
                        Description = textBoxDescription.Text,
						Title = textBoxTitle.Text
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
