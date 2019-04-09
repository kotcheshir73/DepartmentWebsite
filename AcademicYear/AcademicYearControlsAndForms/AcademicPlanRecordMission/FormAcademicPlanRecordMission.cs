using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using BaseInterfaces.BindingModels;
using ControlsAndForms.Messangers;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Tools;
using Unity;

namespace AcademicYearControlsAndForms.AcademicPlanRecordMission
{
    public partial class FormAcademicPlanRecordMission : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IAcademicPlanRecordMissionService _service;

        private Guid? _id = null;

        private Guid _apreId;

        public FormAcademicPlanRecordMission(IAcademicPlanRecordMissionService service, Guid apreId, Guid? id = null)
        {
            InitializeComponent();
            _service = service;
            _apreId = apreId;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void FormAcademicPlanRecordMission_Load(object sender, EventArgs e)
        {
            if (_apreId == null)
            {
                MessageBox.Show("Неуказана запись нагрузки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var resultAPR = _service.GetAcademicPlanRecordElements(new AcademicPlanRecordElementGetBindingModel { Id = _apreId });
            if (!resultAPR.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке записей нагрузки возникла ошибка: ", resultAPR.Errors);
                return;
            }

            var resultL = _service.GetLecturers(new LecturerGetBindingModel());
            if (!resultL.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке преподавателей возникла ошибка: ", resultL.Errors);
                return;
            }

            comboBoxAcademicPlanRecordElement.ValueMember = "Value";
            comboBoxAcademicPlanRecordElement.DisplayMember = "Display";
            comboBoxAcademicPlanRecordElement.DataSource = resultAPR.Result.List
                .Select(ap => new { Value = ap.Id, Display = ap.Disciplne }).ToList();
            comboBoxAcademicPlanRecordElement.SelectedValue = _apreId;

            comboBoxLecturer.ValueMember = "Value";
            comboBoxLecturer.DisplayMember = "Display";
            comboBoxLecturer.DataSource = resultL.Result.List
                .Select(l => new { Value = l.Id, Display = string.Format("{0} {1}", l.LastName, l.FirstName) }).ToList();
            comboBoxLecturer.SelectedItem = null;

            if (_id.HasValue)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            var result = _service.GetAcademicPlanRecordMission(new AcademicPlanRecordMissionGetBindingModel { Id = _id });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            comboBoxAcademicPlanRecordElement.SelectedValue = entity.AcademicPlanRecordElementId;
            comboBoxLecturer.SelectedValue = entity.LecturerId;
            textBoxHours.Text = entity.Hours.ToString();
        }

        private bool CheckFill()
        {
            if (comboBoxAcademicPlanRecordElement.SelectedValue == null)
            {
                return false;
            }
            if (comboBoxLecturer.SelectedValue == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxHours.Text))
            {
                return false;
            }
            if (!decimal.TryParse(textBoxHours.Text, out decimal hours))
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
                    result = _service.CreateAcademicPlanRecordMission(new AcademicPlanRecordMissionSetBindingModel
                    {
                        AcademicPlanRecordElementId = new Guid(comboBoxAcademicPlanRecordElement.SelectedValue.ToString()),
                        LecturerId = new Guid(comboBoxLecturer.SelectedValue.ToString()),
                        Hours = Convert.ToDecimal(textBoxHours.Text)
                    });
                }
                else
                {
                    result = _service.UpdateAcademicPlanRecordMission(new AcademicPlanRecordMissionSetBindingModel
                    {
                        Id = _id.Value,
                        AcademicPlanRecordElementId = new Guid(comboBoxAcademicPlanRecordElement.SelectedValue.ToString()),
                        LecturerId = new Guid(comboBoxLecturer.SelectedValue.ToString()),
                        Hours = Convert.ToDecimal(textBoxHours.Text)
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