using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace DepartmentDesktop.Views.EducationalProcess.AcademicYear.AcademicPlan.AcademicPlanRecord.AcademicPlanRecordElement
{
    public partial class AcademicPlanRecordElementForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IAcademicPlanRecordElementService _service;

        private Guid? _id = null;

        private Guid _aprId;

        public AcademicPlanRecordElementForm(IAcademicPlanRecordElementService service, Guid aprId, Guid? id = null)
        {
            InitializeComponent();
            _service = service;
            _aprId = aprId;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void AcademicPlanRecordElementForm_Load(object sender, EventArgs e)
        {
            if (_aprId == null)
            {
                MessageBox.Show("Неуказан учебный план", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var resultAPR = _service.GetAcademicPlanRecords(new AcademicPlanRecordGetBindingModel { });
            if (!resultAPR.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке записей учебных планов возникла ошибка: ", resultAPR.Errors);
                return;
            }

            var resultKoL = _service.GetKindOfLoads(new KindOfLoadGetBindingModel { });
            if (!resultKoL.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке видов нагрузок возникла ошибка: ", resultKoL.Errors);
                return;
            }

            comboBoxAcademicPlanRecord.ValueMember = "Value";
            comboBoxAcademicPlanRecord.DisplayMember = "Display";
            comboBoxAcademicPlanRecord.DataSource = resultAPR.Result.List
                .Select(ap => new { Value = ap.Id, Display = ap.Disciplne }).ToList();
            comboBoxAcademicPlanRecord.SelectedValue = _aprId;

            comboBoxKindOfLoad.ValueMember = "Value";
            comboBoxKindOfLoad.DisplayMember = "Display";
            comboBoxKindOfLoad.DataSource = resultKoL.Result.List
                .Select(d => new { Value = d.Id, Display = d.KindOfLoadName }).ToList();
            comboBoxKindOfLoad.SelectedItem = null;

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
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            comboBoxAcademicPlanRecord.SelectedValue = entity.AcademicPlanRecordId;
            comboBoxKindOfLoad.SelectedValue = entity.KindOfLoadId;
            textBoxHours.Text = entity.Hours.ToString();
        }

        private bool CheckFill()
        {
            if (comboBoxAcademicPlanRecord.SelectedValue == null)
            {
                return false;
            }
            if (comboBoxKindOfLoad.SelectedValue == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxHours.Text))
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

        private bool Save()
        {
            if (CheckFill())
            {
                ResultService result;
                if (!_id.HasValue)
                {
                    result = _service.CreateAcademicPlanRecordElement(new AcademicPlanRecordElementRecordBindingModel
                    {
                        AcademicPlanRecordId = new Guid(comboBoxAcademicPlanRecord.SelectedValue.ToString()),
                        KindOfLoadId = new Guid(comboBoxKindOfLoad.SelectedValue.ToString()),
                        Hours = Convert.ToInt32(textBoxHours.Text)
                    });
                }
                else
                {
                    result = _service.UpdateAcademicPlanRecordElement(new AcademicPlanRecordElementRecordBindingModel
                    {
                        Id = _id.Value,
                        AcademicPlanRecordId = new Guid(comboBoxAcademicPlanRecord.SelectedValue.ToString()),
                        KindOfLoadId = new Guid(comboBoxKindOfLoad.SelectedValue.ToString()),
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
                    Program.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
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
