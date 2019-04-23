using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using System;
using System.Windows.Forms;
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

        private void FormLecturerPost_Load(object sender, EventArgs e)
        {
            StandartForm_Load();
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

        protected override bool Save()
        {
            if (CheckFill())
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
            else
            {
                MessageBox.Show("Заполните все обязательные поля", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}