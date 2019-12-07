using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using Enums;
using System;
using Tools;
using Unity;

namespace BaseControlsAndForms.EducationDirection
{
    public partial class FormEducationDirection : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IEducationDirectionService _service;

        public FormEducationDirection(IEducationDirectionService service, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
        }

        protected override bool LoadComponents()
        {
            foreach (var elem in Enum.GetValues(typeof(EducationDirectionQualification)))
            {
                comboBoxQualification.Items.Add(elem.ToString());
            }
            comboBoxQualification.SelectedIndex = 0;

            return true;
        }

        protected override void LoadData()
        {
            var result = _service.GetEducationDirection(new EducationDirectionGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;
            textBoxCipher.Text = entity.Cipher;
            textBoxShortName.Text = entity.ShortName;
            textBoxTitle.Text = entity.Title;
            comboBoxQualification.SelectedIndex = comboBoxQualification.Items.IndexOf(entity.Qualification);
            textBoxProfile.Text = entity.Profile;
            textBoxDescription.Text = entity.Description;
        }

        protected override bool CheckFill()
        {
            if (string.IsNullOrEmpty(comboBoxQualification.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxCipher.Text))
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

        protected override bool Save()
        {
            ResultService result;
            if (!_id.HasValue)
            {
                result = _service.CreateEducationDirection(new EducationDirectionSetBindingModel
                {
                    Cipher = textBoxCipher.Text,
                    ShortName = textBoxShortName.Text,
                    Title = textBoxTitle.Text,
                    Qualification = comboBoxQualification.Text,
                    Profile = textBoxProfile.Text,
                    Description = textBoxDescription.Text
                });
            }
            else
            {
                result = _service.UpdateEducationDirection(new EducationDirectionSetBindingModel
                {
                    Id = _id.Value,
                    Cipher = textBoxCipher.Text,
                    ShortName = textBoxShortName.Text,
                    Title = textBoxTitle.Text,
                    Qualification = comboBoxQualification.Text,
                    Profile = textBoxProfile.Text,
                    Description = textBoxDescription.Text
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
    }
}