using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace DepartmentDesktop.Views.EducationalProcess.LecturerPost
{
    public partial class LecturerPostForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ILecturerPostSerivce _service;

        private Guid? _id = null;

        public LecturerPostForm(ILecturerPostSerivce service, Guid? id = null)
        {
            InitializeComponent();
            _service = service;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void LecturerPostForm_Load(object sender, EventArgs e)
        {
            if (_id.HasValue)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            var result = _service.GetLecturerPost(new LecturerPostGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            textBoxPostTitle.Text = entity.PostTitle;
            textBoxHours.Text = entity.Hours.ToString();
        }

        private bool CheckFill()
        {
            if (string.IsNullOrEmpty(textBoxPostTitle.Text))
            {
                return false;
            }
            int hours = 0;
            if (!int.TryParse(textBoxHours.Text, out hours))
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
                    result = _service.CreateLecturerPost(new LecturerPostRecordBindingModel
                    {
                        PostTitle = textBoxPostTitle.Text,
                        Hours = Convert.ToInt32(textBoxHours.Text)
                    });
                }
                else
                {
                    result = _service.UpdateLecturerPost(new LecturerPostRecordBindingModel
                    {
                        Id = _id.Value,
                        PostTitle = textBoxPostTitle.Text,
                        Hours = Convert.ToInt32(textBoxHours.Text)
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
