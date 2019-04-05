using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Unity;

namespace DepartmentDesktop.Views.LearningProgress.DisciplineLessonConducted.DisciplineLessonConductedStudent
{
    public partial class DisciplineLessonConductedStudentForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineLessonConductedStudentService _service;

        private Guid? _id = null;

        private Guid? _dlcId = null;

        private Guid? _sgId = null;

        public DisciplineLessonConductedStudentForm(IDisciplineLessonConductedStudentService service, Guid? dlcId = null, Guid? sgId = null, Guid? id = null)
        {
            InitializeComponent();
            _service = service;
            _dlcId = dlcId;
            _sgId = sgId;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void DisciplineLessonConductedStudentForm_Load(object sender, EventArgs e)
        {
            foreach (var elem in Enum.GetValues(typeof(DisciplineLessonStudentStatus)))
            {
                comboBoxStatus.Items.Add(elem.ToString());
            }
            comboBoxStatus.SelectedIndex = -1;

            var resultDLC = _service.GetDisciplineLessonConducteds(new DisciplineLessonConductedGetBindingModel { Id = _dlcId });
            if (!resultDLC.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке занятий возникла ошибка: ", resultDLC.Errors);
                return;
            }

            comboBoxDisciplineLesson.ValueMember = "Value";
            comboBoxDisciplineLesson.DisplayMember = "Display";
            comboBoxDisciplineLesson.DataSource = resultDLC.Result.List
                .Select(d => new { Value = d.Id, Display = d.DisciplineLesson }).ToList();
            comboBoxDisciplineLesson.SelectedValue = _dlcId;

            var resultS = _service.GetStudents(new StudentGetBindingModel { StudentGroupId = _sgId });
            if (!resultS.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке студентов возникла ошибка: ", resultS.Errors);
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

        private void LoadData()
        {
            var result = _service.GetDisciplineLessonConductedStudent(new DisciplineLessonConductedStudentGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;
            
            comboBoxDisciplineLesson.SelectedValue = entity.DisciplineLessonConductedId;
            comboBoxStudent.SelectedValue = entity.StudentId;
            comboBoxStatus.SelectedIndex = comboBoxStatus.Items.IndexOf(entity.Status.ToString());
            checkBoxBall.Checked = entity.Ball.HasValue;
            textBoxBall.Text = entity.Ball?.ToString() ?? "";
            textBoxComment.Text = entity.Comment;
        }

        private bool CheckFill()
        {
            if (comboBoxDisciplineLesson.SelectedValue == null)
            {
                return false;
            }
            if (comboBoxStudent.SelectedValue == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(comboBoxStatus.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxBall.Text) && checkBoxBall.Checked)
            {
                return false;
            }
            if (!string.IsNullOrEmpty(textBoxBall.Text))
            {
                int ball = 0;
                if (!int.TryParse(textBoxBall.Text, out ball))
                {
                    return false;
                }
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
                    result = _service.CreateDisciplineLessonConductedStudent(new DisciplineLessonConductedStudentSetBindingModel
                    {
                        DisciplineLessonConductedId = new Guid(comboBoxDisciplineLesson.SelectedValue.ToString()),
                        StudentId = new Guid(comboBoxStudent.SelectedValue.ToString()),
                        Status = comboBoxStatus.Text,
                        Ball = checkBoxBall.Checked? Convert.ToInt32(textBoxBall.Text) : (int?)null,
                        Comment = textBoxComment.Text
                    });
                }
                else
                {
                    result = _service.UpdateDisciplineLessonConductedStudent(new DisciplineLessonConductedStudentSetBindingModel
                    {
                        Id = _id.Value,
                        DisciplineLessonConductedId = new Guid(comboBoxDisciplineLesson.SelectedValue.ToString()),
                        StudentId = new Guid(comboBoxStudent.SelectedValue.ToString()),
                        Status = comboBoxStatus.Text,
                        Ball = checkBoxBall.Checked ? Convert.ToInt32(textBoxBall.Text) : (int?)null,
                        Comment = textBoxComment.Text
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

        private void checkBoxBall_CheckedChanged(object sender, EventArgs e)
        {
            textBoxBall.Enabled = checkBoxBall.Checked;
        }
    }
}
