using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using System;
using Tools;
using Unity;

namespace BaseControlsAndForms.LecturerPost
{
    public partial class FormLecturerPost : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ILecturerPostSerivce _service;

        public FormLecturerPost(ILecturerPostSerivce service, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
        }

        protected override void LoadData()
        {
            var result = _service.GetLecturerPost(new LecturerPostGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            textBoxPostTitle.Text = entity.PostTitle;
            textBoxHours.Text = entity.Hours.ToString();
        }

        protected override bool CheckFill()
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

        protected override bool Save()
        {
            ResultService result;
            if (!_id.HasValue)
            {
                result = _service.CreateLecturerPost(new LecturerPostSetBindingModel
                {
                    PostTitle = textBoxPostTitle.Text,
                    Hours = Convert.ToInt32(textBoxHours.Text)
                });
            }
            else
            {
                result = _service.UpdateLecturerPost(new LecturerPostSetBindingModel
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
                ErrorMessanger.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
                return false;
            }
        }
    }
}