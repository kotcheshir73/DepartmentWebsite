using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using LaboratoryHeadInterfaces.BindingModels;
using LaboratoryHeadInterfaces.IServices;
using System;
using System.Windows.Forms;
using Tools;
using Unity;

namespace LaboratoryHeadControlsAndForms.Software
{
    public partial class FormSoftware : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ISoftwareService _service;

        public FormSoftware(ISoftwareService service, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
        }

        private void FormSoftware_Load(object sender, EventArgs e)
        {
            StandartForm_Load();
        }

        protected override void LoadData()
        {
            var result = _service.GetSoftware(new SoftwareGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
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

        protected override bool Save()
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
    }
}