using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Tools;
using Unity;

namespace AcademicYearControlsAndForms.DisciplineTimeDistributionRecord
{
    public partial class FormDisciplineTimeDistributionRecord : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineTimeDistributionRecordService _service;

        private Guid _dtdId;

        public FormDisciplineTimeDistributionRecord(IDisciplineTimeDistributionRecordService service, Guid dtdId, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
            _dtdId = dtdId;
        }

        private void FormDisciplineTimeDistributionRecord_Load(object sender, EventArgs e)
        {
            if (_dtdId == null)
            {
                MessageBox.Show("Неуказана расчасовка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var resultDTD = _service.GetDisciplineTimeDistributions(new DisciplineTimeDistributionGetBindingModel { Id = _dtdId });
            if (!resultDTD.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке расчасовок возникла ошибка: ", resultDTD.Errors);
                return;
            }

            var resultTN = _service.GetTimeNorms(new TimeNormGetBindingModel());
            if (!resultTN.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке норм времени возникла ошибка: ", resultTN.Errors);
                return;
            }

            comboBoxDisciplineTimeDistribution.ValueMember = "Value";
            comboBoxDisciplineTimeDistribution.DisplayMember = "Display";
            comboBoxDisciplineTimeDistribution.DataSource = resultDTD.Result.List
                .Select(ap => new { Value = ap.Id, Display = ap.DisciplineName }).ToList();
            comboBoxDisciplineTimeDistribution.SelectedValue = _dtdId;

            comboBoxTimeNorm.ValueMember = "Value";
            comboBoxTimeNorm.DisplayMember = "Display";
            comboBoxTimeNorm.DataSource = resultTN.Result.List
                .Select(d => new { Value = d.Id, Display = d.KindOfLoadName }).ToList();
            comboBoxTimeNorm.SelectedItem = null;

            StandartForm_Load();
        }

        protected override void LoadData()
        {
            var result = _service.GetDisciplineTimeDistributionRecord(new DisciplineTimeDistributionRecordGetBindingModel { Id = _id });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            comboBoxDisciplineTimeDistribution.SelectedValue = entity.DisciplineTimeDistributionId;
            comboBoxTimeNorm.SelectedValue = entity.TimeNormId;
            textBoxWeekNumber.Text = entity.WeekNumber.ToString();
            textBoxHours.Text = entity.Hours.ToString();
        }

        private bool CheckFill()
        {
            if (comboBoxDisciplineTimeDistribution.SelectedValue == null)
            {
                return false;
            }
            if (comboBoxTimeNorm.SelectedValue == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxWeekNumber.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxHours.Text))
            {
                return false;
            }
            if (!int.TryParse(textBoxWeekNumber.Text, out int week))
            {
                return false;
            }
            if (!double.TryParse(textBoxHours.Text, out double hours))
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
                    result = _service.CreateDisciplineTimeDistributionRecord(new DisciplineTimeDistributionRecordSetBindingModel
                    {
                        DisciplineTimeDistributionId = new Guid(comboBoxDisciplineTimeDistribution.SelectedValue.ToString()),
                        TimeNormId = new Guid(comboBoxTimeNorm.SelectedValue.ToString()),
                        WeekNumber = Convert.ToInt32(textBoxWeekNumber.Text),
                        Hours = Convert.ToDouble(textBoxHours.Text)
                    });
                }
                else
                {
                    result = _service.UpdateDisciplineTimeDistributionRecord(new DisciplineTimeDistributionRecordSetBindingModel
                    {
                        Id = _id.Value,
                        DisciplineTimeDistributionId = new Guid(comboBoxDisciplineTimeDistribution.SelectedValue.ToString()),
                        TimeNormId = new Guid(comboBoxTimeNorm.SelectedValue.ToString()),
                        WeekNumber = Convert.ToInt32(textBoxWeekNumber.Text),
                        Hours = Convert.ToDouble(textBoxHours.Text)
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