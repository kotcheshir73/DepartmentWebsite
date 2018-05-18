using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace DepartmentDesktop.Views.EducationalProcess.DisciplineBlock.DisciplineBlockRecord
{
    public partial class DisciplineBlockRecordForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineBlockRecordService _service;

        private Guid? _id = null;

        private Guid? _dbId = null;

        private Guid? _ayId = null;

        public DisciplineBlockRecordForm(IDisciplineBlockRecordService service, Guid? dbId = null, Guid? ayId = null, Guid? id = null)
        {
            InitializeComponent();
            _service = service;
            if (dbId != Guid.Empty)
            {
                _dbId = dbId;
            }
            if (ayId != Guid.Empty)
            {
                _ayId = ayId;
            }
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void DisciplineBlockRecordForm_Load(object sender, EventArgs e)
        {
            var resultDB = _service.GetDisciplineBlocks(new DisciplineBlockGetBindingModel { Id = _dbId });
            if (!resultDB.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке блоков дисциплин возникла ошибка: ", resultDB.Errors);
                return;
            }

            var resultAY = _service.GetAcademicYears(new AcademicYearGetBindingModel { Id = _ayId });
            if (!resultAY.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке учебных годов возникла ошибка: ", resultAY.Errors);
                return;
            }

            var resultED = _service.GetEducationDirections(new EducationDirectionGetBindingModel { });
            if (!resultED.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке направлений возникла ошибка: ", resultED.Errors);
                return;
            }

            var resultTN = _service.GetTimeNorms(new TimeNormGetBindingModel { });
            if (!resultTN.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке норм времени возникла ошибка: ", resultTN.Errors);
                return;
            }

            comboBoxDisciplineBlock.ValueMember = "Value";
            comboBoxDisciplineBlock.DisplayMember = "Display";
            comboBoxDisciplineBlock.DataSource = resultDB.Result.List
                .Select(x => new { Value = x.Id, Display = x.Title }).ToList();
            if (_dbId.HasValue)
            {
                comboBoxDisciplineBlock.SelectedValue = _dbId;
            }
            else
            {
                comboBoxDisciplineBlock.SelectedItem = null;
            }

            comboBoxAcademicYear.ValueMember = "Value";
            comboBoxAcademicYear.DisplayMember = "Display";
            comboBoxAcademicYear.DataSource = resultAY.Result.List
                .Select(x => new { Value = x.Id, Display = x.Title }).ToList();
            if (_ayId.HasValue)
            {
                comboBoxAcademicYear.SelectedValue = _ayId;
            }
            else
            {
                comboBoxAcademicYear.SelectedItem = null;
            }

            comboBoxEducationDirection.ValueMember = "Value";
            comboBoxEducationDirection.DisplayMember = "Display";
            comboBoxEducationDirection.DataSource = resultED.Result.List
                .Select(x => new { Value = x.Id, Display = x.Cipher + " " + x.Title }).ToList();
            comboBoxEducationDirection.SelectedItem = null;

            comboBoxTimeNorm.ValueMember = "Value";
            comboBoxTimeNorm.DisplayMember = "Display";
            comboBoxTimeNorm.DataSource = resultTN.Result.List
                .Select(x => new { Value = x.Id, Display = x.TimeNormName }).ToList();
            comboBoxTimeNorm.SelectedItem = null;

            if (_id.HasValue)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            var result = _service.GetDisciplineBlockRecord(new DisciplineBlockRecordGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            comboBoxDisciplineBlock.SelectedValue = entity.DisciplineBlockId;
            comboBoxAcademicYear.SelectedValue = entity.AcademicYearId;
            comboBoxEducationDirection.SelectedValue = entity.EducationDirectionId;
            comboBoxTimeNorm.SelectedValue = entity.TimeNormId;
            textBoxDisciplineBlockRecordTitle.Text = entity.DisciplineBlockRecordTitle;
            textBoxDisciplineBlockRecordHours.Text = entity.DisciplineBlockRecordHours.ToString();
        }

        private bool CheckFill()
        {
            if (comboBoxDisciplineBlock.SelectedValue == null)
            {
                return false;
            }
            if (comboBoxAcademicYear.SelectedValue == null)
            {
                return false;
            }
            if (comboBoxTimeNorm.SelectedValue == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxDisciplineBlockRecordTitle.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxDisciplineBlockRecordHours.Text))
            {
                return false;
            }
            decimal hours = 0;
            if(!decimal.TryParse(textBoxDisciplineBlockRecordHours.Text, out hours))
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
                    result = _service.CreateDisciplineBlockRecord(new DisciplineBlockRecordSetBindingModel
                    {
                        DisciplineBlockId = new Guid(comboBoxDisciplineBlock.SelectedValue.ToString()),
                        AcademicYearId = new Guid(comboBoxAcademicYear.SelectedValue.ToString()),
                        EducationDirectionId = comboBoxEducationDirection.SelectedValue != null ? new Guid(comboBoxEducationDirection.SelectedValue.ToString()) : (Guid?)null,
                        TimeNormId = new Guid(comboBoxTimeNorm.SelectedValue.ToString()),
                        DisciplineBlockRecordTitle = textBoxDisciplineBlockRecordTitle.Text,
                        DisciplineBlockRecordHours = Convert.ToDecimal(textBoxDisciplineBlockRecordHours.Text)
                    });
                }
                else
                {
                    result = _service.UpdateDisciplineBlockRecord(new DisciplineBlockRecordSetBindingModel
                    {
                        Id = _id.Value,
                        DisciplineBlockId = new Guid(comboBoxDisciplineBlock.SelectedValue.ToString()),
                        AcademicYearId = new Guid(comboBoxAcademicYear.SelectedValue.ToString()),
                        EducationDirectionId = comboBoxEducationDirection.SelectedValue != null ? new Guid(comboBoxEducationDirection.SelectedValue.ToString()) : (Guid?)null,
                        TimeNormId = new Guid(comboBoxTimeNorm.SelectedValue.ToString()),
                        DisciplineBlockRecordTitle = textBoxDisciplineBlockRecordTitle.Text,
                        DisciplineBlockRecordHours = Convert.ToDecimal(textBoxDisciplineBlockRecordHours.Text)
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
