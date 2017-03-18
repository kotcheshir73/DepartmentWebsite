using System.Windows.Forms;
using DepartmentService.IServices;
using DepartmentService.BindingModels;
using System;

namespace DepartmentDesktop.Views.EducationalProcess.SeasonDates
{
    public partial class SeasonDatesElementControl : UserControl
    {
        private readonly ISeasonDatesService _service;

        public SeasonDatesElementControl(ISeasonDatesService service, string title)
        {
            InitializeComponent();
            _service = service;
            groupBox.Text = title;
        }

        public void LoadData()
        {
            var result = _service.GetSeasonDates(new SeasonDatesGetBindingModel { Title = groupBox.Text });
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				return;
			}
			var entity = result.Result;
			dateTimePickerDateBeginExamination.Value = Convert.ToDateTime(entity.DateBeginExamination);
            dateTimePickerDateBeginOffset.Value = Convert.ToDateTime(entity.DateBeginOffset);
            dateTimePickerDateBeginSemester.Value = Convert.ToDateTime(entity.DateBeginSemester);
            dateTimePickerDateEndExamination.Value = Convert.ToDateTime(entity.DateEndExamination);
            dateTimePickerDateEndOffset.Value = Convert.ToDateTime(entity.DateEndOffset);
            dateTimePickerDateEndSemester.Value = Convert.ToDateTime(entity.DateEndSemester);
            dateTimePickerDateBeginPractic.Enabled = !string.IsNullOrEmpty(entity.DateBeginPractice);
            if(!string.IsNullOrEmpty(entity.DateBeginPractice))
            {
                dateTimePickerDateBeginPractic.Value = Convert.ToDateTime(entity.DateBeginPractice);
            }
            dateTimePickerDateEndPractic.Enabled = !string.IsNullOrEmpty(entity.DateEndPractice);
            if (!string.IsNullOrEmpty(entity.DateEndPractice))
            {
                dateTimePickerDateEndPractic.Value = Convert.ToDateTime(entity.DateEndPractice);
            }
        }
    }
}
