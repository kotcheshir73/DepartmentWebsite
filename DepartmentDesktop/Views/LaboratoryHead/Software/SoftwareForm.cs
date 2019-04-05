using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Windows.Forms;
using Unity;

namespace DepartmentDesktop.Views.LaboratoryHead.Software
{
    public partial class SoftwareForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ISoftwareService _service;

        private Guid? _id = null;

        public SoftwareForm(ISoftwareService service, Guid? id = null)
        {
            InitializeComponent();
            _service = service;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void SoftwareForm_Load(object sender, EventArgs e)
        {
            if (_id.HasValue)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            var result = _service.GetSoftware(new SoftwareGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;
            
            textBoxSoftwareName.Text = entity.SoftwareName;
            textBoxSoftwareDescription.Text = entity.SoftwareDescription;
            textBoxSoftwareKey.Text = entity.SoftwareKey;
            textBoxSoftwareK.Text = entity.SoftwareK;
        }

        private bool CheckFill()
        {
            if (string.IsNullOrEmpty(textBoxSoftwareName.Text))
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
                    result = _service.CreateSoftware(new SoftwareSetBindingModel
                    {
                        SoftwareName = textBoxSoftwareName.Text,
                        SoftwareDescription = textBoxSoftwareDescription.Text,
                        SoftwareKey = textBoxSoftwareKey.Text,
                        SoftwareK = textBoxSoftwareK.Text
                    });
                }
                else
                {
                    result = _service.UpdateSoftware(new SoftwareSetBindingModel
                    {
                        Id = _id.Value,
                        SoftwareName = textBoxSoftwareName.Text,
                        SoftwareDescription = textBoxSoftwareDescription.Text,
                        SoftwareKey = textBoxSoftwareKey.Text,
                        SoftwareK = textBoxSoftwareK.Text
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
