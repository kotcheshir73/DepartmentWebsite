using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using System;
using Tools;
using Unity;

namespace BaseControlsAndForms.LecturerStudyPost
{
    public partial class FormLecturerStudyPost : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ILecturerStudyPostSerivce _service;

        public FormLecturerStudyPost(ILecturerStudyPostSerivce service, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
        }

        protected override void LoadData()
        {
            var result = _service.GetLecturerStudyPost(new LecturerStudyPostGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            textBoxPostTitle.Text = entity.StudyPostTitle;
            textBoxHours.Text = entity.Hours.ToString();
        }

        protected override bool CheckFill()
        {
            if (string.IsNullOrEmpty(textBoxPostTitle.Text))
            {
                return false;
            }
			if (!int.TryParse(textBoxHours.Text, out _))
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
                result = _service.CreateLecturerStudyPost(new LecturerStudyPostSetBindingModel
                {
                    StudyPostTitle = textBoxPostTitle.Text,
                    Hours = Convert.ToInt32(textBoxHours.Text)
                });
            }
            else
            {
                result = _service.UpdateLecturerStudyPost(new LecturerStudyPostSetBindingModel
                {
                    Id = _id.Value,
                    StudyPostTitle = textBoxPostTitle.Text,
                    Hours = Convert.ToInt32(textBoxHours.Text)
                });
            }
            if (result.Succeeded)
            {
                if (result.Result != null)
                {
                    if (result.Result is Guid guid)
                    {
                        _id = guid;
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