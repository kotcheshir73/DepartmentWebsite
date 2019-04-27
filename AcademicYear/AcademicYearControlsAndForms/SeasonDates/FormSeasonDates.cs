using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using System;
using System.Linq;
using System.Windows.Forms;
using Tools;
using Unity;

namespace AcademicYearControlsAndForms.SeasonDates
{
    public partial class FormSeasonDates : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ISeasonDatesService _service;

        private Guid _ayId;

        public FormSeasonDates(ISeasonDatesService service, Guid ayId, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
            _ayId = ayId;
        }

        protected override bool LoadComponents()
        {
            var resultAY = _service.GetAcademicYears(new AcademicYearGetBindingModel { Id = _ayId });
            if (!resultAY.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке учебных годов возникла ошибка: ", resultAY.Errors);
                return false;
            }

            comboBoxAcademicYear.ValueMember = "Value";
            comboBoxAcademicYear.DisplayMember = "Display";
            comboBoxAcademicYear.DataSource = resultAY.Result.List
                .Select(ay => new { Value = ay.Id, Display = ay.Title }).ToList();
            comboBoxAcademicYear.SelectedItem = _ayId;

            return true;
        }

        protected override void LoadData()
        {
            var result = _service.GetSeasonDates(new SeasonDatesGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            textBoxTitle.Text = entity.Title;
            dateTimePickerDateBeginExamination.Value = Convert.ToDateTime(entity.DateBeginExamination);
            dateTimePickerDateBeginOffset.Value = Convert.ToDateTime(entity.DateBeginOffset);
            dateTimePickerDateBeginFirstHalfSemester.Value = Convert.ToDateTime(entity.DateBeginFirstHalfSemester);
            dateTimePickerDateBeginSecondHalfSemester.Value = Convert.ToDateTime(entity.DateBeginSecondHalfSemester);
            dateTimePickerDateEndExamination.Value = Convert.ToDateTime(entity.DateEndExamination);
            dateTimePickerDateEndOffset.Value = Convert.ToDateTime(entity.DateEndOffset);
            dateTimePickerDateEndFirstHalfSemester.Value = Convert.ToDateTime(entity.DateEndFirstHalfSemester);
            dateTimePickerDateEndSecondHalfSemester.Value = Convert.ToDateTime(entity.DateEndSecondHalfSemester);
            dateTimePickerDateBeginPractic.Enabled = !string.IsNullOrEmpty(entity.DateBeginPractice);
            if (!string.IsNullOrEmpty(entity.DateBeginPractice))
            {
                dateTimePickerDateBeginPractic.Value = Convert.ToDateTime(entity.DateBeginPractice);
            }
            dateTimePickerDateEndPractic.Enabled = !string.IsNullOrEmpty(entity.DateEndPractice);
            if (!string.IsNullOrEmpty(entity.DateEndPractice))
            {
                dateTimePickerDateEndPractic.Value = Convert.ToDateTime(entity.DateEndPractice);
            }
            checkBoxDateBeginPractic.Checked = !string.IsNullOrEmpty(entity.DateBeginPractice);
            checkBoxDateEndPractic.Checked = !string.IsNullOrEmpty(entity.DateEndPractice);
        }

        protected override bool CheckFill()
        {
            if (string.IsNullOrEmpty(textBoxTitle.Text))
            {
                return false;
            }
            if (dateTimePickerDateBeginFirstHalfSemester.Value == dateTimePickerDateEndFirstHalfSemester.Value)
            {
                return false;
            }
            if (dateTimePickerDateBeginOffset.Value == dateTimePickerDateEndOffset.Value)
            {
                return false;
            }
            if (dateTimePickerDateBeginExamination.Value == dateTimePickerDateEndExamination.Value)
            {
                return false;
            }
            return true;
        }

        protected override bool Save()
        {
            DateTime? dateBeginPractic = null;
            DateTime? dateEndPractic = null;
            if (checkBoxDateBeginPractic.Checked)
            {
                dateBeginPractic = dateTimePickerDateBeginPractic.Value;
            }
            if (checkBoxDateEndPractic.Checked)
            {
                dateEndPractic = dateTimePickerDateEndPractic.Value;
            }
            ResultService result;
            if (!_id.HasValue)
            {
                result = _service.CreateSeasonDates(new SeasonDatesSetBindingModel
                {
                    AcademicYearId = new Guid(comboBoxAcademicYear.SelectedValue.ToString()),
                    Title = textBoxTitle.Text,
                    DateBeginExamination = dateTimePickerDateBeginExamination.Value,
                    DateBeginOffset = dateTimePickerDateBeginOffset.Value,
                    DateBeginFirstHalfSemester = dateTimePickerDateBeginFirstHalfSemester.Value,
                    DateBeginSecondHalfSemester = dateTimePickerDateBeginSecondHalfSemester.Value,
                    DateEndExamination = dateTimePickerDateEndExamination.Value,
                    DateEndOffset = dateTimePickerDateEndOffset.Value,
                    DateEndFirstHalfSemester = dateTimePickerDateEndFirstHalfSemester.Value,
                    DateEndSecondHalfSemester = dateTimePickerDateEndSecondHalfSemester.Value,
                    DateBeginPractice = dateBeginPractic,
                    DateEndPractice = dateEndPractic
                });
            }
            else
            {
                result = _service.UpdateSeasonDates(new SeasonDatesSetBindingModel
                {
                    Id = _id.Value,
                    AcademicYearId = new Guid(comboBoxAcademicYear.SelectedValue.ToString()),
                    Title = textBoxTitle.Text,
                    DateBeginExamination = dateTimePickerDateBeginExamination.Value,
                    DateBeginOffset = dateTimePickerDateBeginOffset.Value,
                    DateBeginFirstHalfSemester = dateTimePickerDateBeginFirstHalfSemester.Value,
                    DateBeginSecondHalfSemester = dateTimePickerDateBeginSecondHalfSemester.Value,
                    DateEndExamination = dateTimePickerDateEndExamination.Value,
                    DateEndOffset = dateTimePickerDateEndOffset.Value,
                    DateEndFirstHalfSemester = dateTimePickerDateEndFirstHalfSemester.Value,
                    DateEndSecondHalfSemester = dateTimePickerDateEndSecondHalfSemester.Value,
                    DateBeginPractice = dateBeginPractic,
                    DateEndPractice = dateEndPractic
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

        private void CheckBoxDateBeginPractic_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerDateBeginPractic.Enabled = checkBoxDateBeginPractic.Checked;
        }

        private void CheckBoxDateEndPractic_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerDateEndPractic.Enabled = checkBoxDateEndPractic.Checked;
        }
    }
}