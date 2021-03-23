using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using System;
using Tools;
using Unity;

namespace BaseControlsAndForms.LecturerDepartmentPost
{
	public partial class FormLecturerDepartmentPost : StandartForm
	{
		[Dependency]
		public new IUnityContainer Container { get; set; }

		private readonly ILecturerDepartmentPostSerivce _service;

		public FormLecturerDepartmentPost(ILecturerDepartmentPostSerivce service, Guid? id = null) : base(id)
		{
			InitializeComponent();
			_service = service;
        }

        protected override void LoadData()
        {
            var result = _service.GetLecturerDepartmentPost(new LecturerDepartmentPostGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            textBoxDepartmentPostTitle.Text = entity.DepartmentPostTitle;
            numericUpDownOrder.Value= entity.Order;
        }

        protected override bool CheckFill()
        {
            if (string.IsNullOrEmpty(textBoxDepartmentPostTitle.Text))
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
                result = _service.CreateLecturerDepartmentPost(new LecturerDepartmentPostSetBindingModel
                {
                    DepartmentPostTitle = textBoxDepartmentPostTitle.Text,
                    Order = Convert.ToInt32(numericUpDownOrder.Value)
                });
            }
            else
            {
                result = _service.UpdateLecturerDepartmentPost(new LecturerDepartmentPostSetBindingModel
                {
                    Id = _id.Value,
                    DepartmentPostTitle = textBoxDepartmentPostTitle.Text,
                    Order = Convert.ToInt32(numericUpDownOrder.Value)
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