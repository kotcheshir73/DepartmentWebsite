using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using ControlsAndForms.Messangers;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Tools;
using Unity;

namespace AcademicYearControlsAndForms.AcademicPlanRecordElement
{
    public partial class FormAcademicPlanRecordElement : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IAcademicPlanRecordElementService _service;

        private Guid? _id = null;

        private Guid _aprId;

        public FormAcademicPlanRecordElement(IAcademicPlanRecordElementService service, Guid aprId, Guid? id = null)
        {
            InitializeComponent();
            _service = service;
            _aprId = aprId;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void FormAcademicPlanRecordElement_Load(object sender, EventArgs e)
        {
            if (_aprId == null)
            {
                MessageBox.Show("Неуказана запись учебного плана", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var resultAPR = _service.GetAcademicPlanRecords(new AcademicPlanRecordGetBindingModel { Id = _aprId });
            if (!resultAPR.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке записей учебных планов возникла ошибка: ", resultAPR.Errors);
                return;
            }

            var resultTN = _service.GetTimeNorms(new TimeNormGetBindingModel { AcademicPlanRecordId = _aprId });
            if (!resultTN.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке норм времени возникла ошибка: ", resultTN.Errors);
                return;
            }

            comboBoxAcademicPlanRecord.ValueMember = "Value";
            comboBoxAcademicPlanRecord.DisplayMember = "Display";
            comboBoxAcademicPlanRecord.DataSource = resultAPR.Result.List
                .Select(ap => new { Value = ap.Id, Display = ap.Disciplne }).ToList();
            comboBoxAcademicPlanRecord.SelectedValue = _aprId;

            comboBoxTimeNorm.ValueMember = "Value";
            comboBoxTimeNorm.DisplayMember = "Display";
            comboBoxTimeNorm.DataSource = resultTN.Result.List
                .Select(d => new { Value = d.Id, Display = d.KindOfLoadName }).ToList();
            comboBoxTimeNorm.SelectedItem = null;

            if (_id.HasValue)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            var result = _service.GetAcademicPlanRecordElement(new AcademicPlanRecordElementGetBindingModel { Id = _id });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            comboBoxAcademicPlanRecord.SelectedValue = entity.AcademicPlanRecordId;
            comboBoxTimeNorm.SelectedValue = entity.TimeNormId;
            textBoxPlanHours.Text = entity.PlanHours.ToString();
            textBoxFactHours.Text = entity.FactHours.ToString();
        }

        private bool CheckFill()
        {
            if (comboBoxAcademicPlanRecord.SelectedValue == null)
            {
                return false;
            }
            if (comboBoxTimeNorm.SelectedValue == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxPlanHours.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxFactHours.Text))
            {
                return false;
            }
            int hours = 0;
            if (!int.TryParse(textBoxPlanHours.Text, out hours))
            {
                return false;
            }
            if (!int.TryParse(textBoxFactHours.Text, out hours))
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
                    result = _service.CreateAcademicPlanRecordElement(new AcademicPlanRecordElementSetBindingModel
                    {
                        AcademicPlanRecordId = new Guid(comboBoxAcademicPlanRecord.SelectedValue.ToString()),
                        TimeNormId = new Guid(comboBoxTimeNorm.SelectedValue.ToString()),
                        PlanHours = Convert.ToInt32(textBoxPlanHours.Text),
                        FactHours = Convert.ToInt32(textBoxFactHours.Text)
                    });
                }
                else
                {
                    result = _service.UpdateAcademicPlanRecordElement(new AcademicPlanRecordElementSetBindingModel
                    {
                        Id = _id.Value,
                        AcademicPlanRecordId = new Guid(comboBoxAcademicPlanRecord.SelectedValue.ToString()),
                        TimeNormId = new Guid(comboBoxTimeNorm.SelectedValue.ToString()),
                        PlanHours = Convert.ToInt32(textBoxPlanHours.Text),
                        FactHours = Convert.ToInt32(textBoxFactHours.Text)
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