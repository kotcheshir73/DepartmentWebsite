using BaseInterfaces.BindingModels;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using Enums;
using LearningProgressInterfaces.BindingModels;
using LearningProgressInterfaces.Interfaces;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Tools;
using Unity;

namespace LearningProgressControlsAndForms.DisciplineStudentRecord
{
    public partial class FormDisciplineStudentRecord : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineStudentRecordService _service;

        private Guid? _dId = null;

        private Guid? _sgId = null;

        private string _semester = null;

        public FormDisciplineStudentRecord(IDisciplineStudentRecordService service, Guid? dId = null, Guid? sgId = null, string semester = null, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
            _dId = dId;
            _sgId = sgId;
            _semester = semester;
        }

        private void FormDisciplineStudentRecord_Load(object sender, EventArgs e)
        {
            foreach (var elem in Enum.GetValues(typeof(Semesters)))
            {
                comboBoxSemester.Items.Add(elem.ToString());
            }
            comboBoxSemester.SelectedIndex = comboBoxSemester.Items.IndexOf(_semester);

            var resultD = _service.GetDisciplines(new DisciplineGetBindingModel { Id = _dId });
            if (!resultD.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке дисциплин возникла ошибка: ", resultD.Errors);
                return;
            }

            comboBoxDiscipline.ValueMember = "Value";
            comboBoxDiscipline.DisplayMember = "Display";
            comboBoxDiscipline.DataSource = resultD.Result.List
                .Select(d => new { Value = d.Id, Display = d.DisciplineName }).ToList();
            comboBoxDiscipline.SelectedValue = _dId;

            var resultSG = _service.GetStudentGroups(new StudentGroupGetBindingModel { Id = _sgId });
            if (!resultSG.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке групп возникла ошибка: ", resultSG.Errors);
                return;
            }

            comboBoxStudentGroup.ValueMember = "Value";
            comboBoxStudentGroup.DisplayMember = "Display";
            comboBoxStudentGroup.DataSource = resultSG.Result.List
                .Select(d => new { Value = d.Id, Display = d.GroupName }).ToList();
            comboBoxStudentGroup.SelectedValue = _sgId;

            var resultS = _service.GetStudents(new StudentGetBindingModel { StudentGroupId = _sgId });
            if (!resultS.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке студентов возникла ошибка: ", resultS.Errors);
                return;
            }

            comboBoxStudent.ValueMember = "Value";
            comboBoxStudent.DisplayMember = "Display";
            comboBoxStudent.DataSource = resultS.Result.List
                .Select(d => new { Value = d.Id, Display = string.Format("{0} {1}", d.LastName, d.FirstName) }).ToList();
            comboBoxStudent.SelectedItem = null;

            if (_id.HasValue)
            {
                LoadData();
            }
        }

        protected override void LoadData()
        {
            var result = _service.GetDisciplineStudentRecord(new DisciplineStudentRecordGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;
            
            comboBoxDiscipline.SelectedValue = entity.DisciplineId;
            comboBoxStudentGroup.SelectedValue = entity.StudentGroupId;
            comboBoxStudent.SelectedValue = entity.StudentId;
            comboBoxSemester.SelectedIndex = comboBoxSemester.Items.IndexOf(entity.Semester.ToString());
            textBoxVariant.Text = entity.Variant;
            textBoxSubgroup.Text = entity.SubGroup.ToString();
        }

        private bool CheckFill()
        {
            if (comboBoxStudent.SelectedValue == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(comboBoxSemester.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxVariant.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxSubgroup.Text))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(textBoxSubgroup.Text))
            {
                if (!int.TryParse(textBoxSubgroup.Text, out int subgroup))
                {
                    return false;
                }
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
                    result = _service.CreateDisciplineStudentRecord(new DisciplineStudentRecordSetBindingModel
                    {
                        DisciplineId = new Guid(comboBoxDiscipline.SelectedValue.ToString()),
                        StudentId = new Guid(comboBoxStudent.SelectedValue.ToString()),
                        Semester = comboBoxSemester.Text,
                        Variant = textBoxVariant.Text,
                        SubGroup = Convert.ToInt32(textBoxSubgroup.Text)
                    });
                }
                else
                {
                    result = _service.UpdateDisciplineStudentRecord(new DisciplineStudentRecordSetBindingModel
                    {
                        Id = _id.Value,
                        DisciplineId = new Guid(comboBoxDiscipline.SelectedValue.ToString()),
                        StudentId = new Guid(comboBoxStudent.SelectedValue.ToString()),
                        Semester = comboBoxSemester.Text,
                        Variant = textBoxVariant.Text,
                        SubGroup = Convert.ToInt32(textBoxSubgroup.Text)
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