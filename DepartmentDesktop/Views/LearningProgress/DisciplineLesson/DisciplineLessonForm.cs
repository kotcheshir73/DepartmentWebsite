using DepartmentDesktop.Views.LearningProgress.DisciplineLesson.DisciplineLessonTask;
using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Unity;

namespace DepartmentDesktop.Views.LearningProgress.DisciplineLesson
{
    public partial class DisciplineLessonForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineLessonService _service;

        private Guid? _id = null;

        private Guid? _ayId = null;

        private Guid? _dId = null;

        private Guid? _edId = null;

        private Guid? _tnId = null;

        public DisciplineLessonForm(IDisciplineLessonService service, Guid? ayId = null, Guid? dId = null, Guid? edId = null, Guid? tnId = null, Guid? id = null)
        {
            InitializeComponent();
            _service = service;
            _ayId = ayId;
            _edId = edId;
            _dId = dId;
            _tnId = tnId;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void DisciplineLessonForm_Load(object sender, EventArgs e)
        {
            foreach (var elem in Enum.GetValues(typeof(Semesters)))
            {
                comboBoxSemester.Items.Add(elem.ToString());
            }
            comboBoxSemester.SelectedIndex = 0;

            var resultAY = _service.GetAcademicYears(new AcademicYearGetBindingModel { Id = _ayId });
            if (!resultAY.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке учбеных годов возникла ошибка: ", resultAY.Errors);
                return;
            }

            comboBoxAcademicYear.ValueMember = "Value";
            comboBoxAcademicYear.DisplayMember = "Display";
            comboBoxAcademicYear.DataSource = resultAY.Result.List
                .Select(d => new { Value = d.Id, Display = d.Title }).ToList();
            comboBoxAcademicYear.SelectedValue = _ayId;

            var resultD = _service.GetDisciplines(new DisciplineGetBindingModel { Id = _dId });
            if (!resultD.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке дисциплин возникла ошибка: ", resultD.Errors);
                return;
            }

            comboBoxDiscipline.ValueMember = "Value";
            comboBoxDiscipline.DisplayMember = "Display";
            comboBoxDiscipline.DataSource = resultD.Result.List
                .Select(d => new { Value = d.Id, Display = d.DisciplineName }).ToList();
            comboBoxDiscipline.SelectedValue = _dId;

            var resultED = _service.GetEducationDirections(new EducationDirectionGetBindingModel { Id = _edId });
            if (!resultED.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке направлений возникла ошибка: ", resultED.Errors);
                return;
            }

            comboBoxEducationDirection.ValueMember = "Value";
            comboBoxEducationDirection.DisplayMember = "Display";
            comboBoxEducationDirection.DataSource = resultED.Result.List
                .Select(d => new { Value = d.Id, Display = d.Cipher }).ToList();
            comboBoxEducationDirection.SelectedValue = _edId;

            var resultTN = _service.GetTimeNorms(new TimeNormGetBindingModel { Id = _tnId });
            if (!resultTN.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке норм времени возникла ошибка: ", resultTN.Errors);
                return;
            }

            comboBoxTimeNorm.ValueMember = "Value";
            comboBoxTimeNorm.DisplayMember = "Display";
            comboBoxTimeNorm.DataSource = resultTN.Result.List
                .Select(d => new { Value = d.Id, Display = d.TimeNormName }).ToList();
            comboBoxTimeNorm.SelectedValue = _tnId;

            if (_id.HasValue)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            if (tabPageRecords.Controls.Count == 0)
            {
                var control = Container.Resolve<DisciplineLessonTaskControl>();
                control.Dock = DockStyle.Fill;
                tabPageRecords.Controls.Add(control);
            }
            (tabPageRecords.Controls[0] as DisciplineLessonTaskControl).LoadData(_id.Value);

            var result = _service.GetDisciplineLesson(new DisciplineLessonGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            comboBoxAcademicYear.SelectedValue = entity.AcademicYearId;
            comboBoxDiscipline.SelectedValue = entity.DisciplineId;
            comboBoxTimeNorm.SelectedValue = entity.TimeNormId;
            comboBoxSemester.SelectedIndex = comboBoxSemester.Items.IndexOf(entity.Semester.ToString());
            textBoxPostTitle.Text = entity.Title;
            textBoxDiscription.Text = entity.Description;
            textBoxOrder.Text = entity.Order.ToString();
            textBoxCountOfPairs.Text = entity.CountOfPairs.ToString();
            if (entity.DisciplineLessonFile != null)
            {
                buttonGetFile.Enabled = entity.DisciplineLessonFile.Length > 0;
            }
        }

        private bool CheckFill()
        {
            if (string.IsNullOrEmpty(comboBoxSemester.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxPostTitle.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxDiscription.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxOrder.Text))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(textBoxOrder.Text))
            {
                int order = 0;
                if (!int.TryParse(textBoxOrder.Text, out order))
                {
                    return false;
                }
            }
            if (string.IsNullOrEmpty(textBoxCountOfPairs.Text))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(textBoxCountOfPairs.Text))
            {
                int order = 0;
                if (!int.TryParse(textBoxCountOfPairs.Text, out order))
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
                    result = _service.CreateDisciplineLesson(new DisciplineLessonRecordBindingModel
                    {
                        AcademicYearId = new Guid(comboBoxAcademicYear.SelectedValue.ToString()),
                        DisciplineId = new Guid(comboBoxDiscipline.SelectedValue.ToString()),
                        EducationDirectionId = new Guid(comboBoxEducationDirection.SelectedValue.ToString()),
                        TimeNormId = new Guid(comboBoxTimeNorm.SelectedValue.ToString()),
                        Semester = comboBoxSemester.Text,
                        Title = textBoxPostTitle.Text,
                        Description = textBoxDiscription.Text,
                        Order = Convert.ToInt32(textBoxOrder.Text),
                        CountOfPairs = Convert.ToInt32(textBoxCountOfPairs.Text)
                    });
                }
                else
                {
                    result = _service.UpdateDisciplineLesson(new DisciplineLessonRecordBindingModel
                    {
                        Id = _id.Value,
                        AcademicYearId = new Guid(comboBoxAcademicYear.SelectedValue.ToString()),
                        DisciplineId = new Guid(comboBoxDiscipline.SelectedValue.ToString()),
                        EducationDirectionId = new Guid(comboBoxEducationDirection.SelectedValue.ToString()),
                        TimeNormId = new Guid(comboBoxTimeNorm.SelectedValue.ToString()),
                        Semester = comboBoxSemester.Text,
                        Title = textBoxPostTitle.Text,
                        Description = textBoxDiscription.Text,
                        Order = Convert.ToInt32(textBoxOrder.Text),
                        CountOfPairs = Convert.ToInt32(textBoxCountOfPairs.Text)
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

        private void buttonAddFile_Click(object sender, EventArgs e)
        {
        }
    }
}
