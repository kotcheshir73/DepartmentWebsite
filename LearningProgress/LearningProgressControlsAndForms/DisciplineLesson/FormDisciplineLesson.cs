using AcademicYearInterfaces.BindingModels;
using BaseInterfaces.BindingModels;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using Enums;
using LearningProgressControlsAndForms.DisciplineLessonTask;
using LearningProgressInterfaces.BindingModels;
using LearningProgressInterfaces.Interfaces;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Tools;
using Unity;

namespace LearningProgressControlsAndForms.DisciplineLesson
{
    public partial class FormDisciplineLesson : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineLessonService _service;

        private Guid? _ayId = null;

        private Guid? _dId = null;

        private Guid? _edId = null;

        private Guid? _tnId = null;

        public FormDisciplineLesson(IDisciplineLessonService service, Guid? ayId = null, Guid? dId = null, Guid? edId = null, Guid? tnId = null, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
            _ayId = ayId;
            _edId = edId;
            _dId = dId;
            _tnId = tnId;
        }

        protected override bool LoadComponents()
        {
            foreach (var elem in Enum.GetValues(typeof(Semesters)))
            {
                comboBoxSemester.Items.Add(elem.ToString());
            }
            comboBoxSemester.SelectedIndex = 0;

            var resultAY = _service.GetAcademicYears(new AcademicYearGetBindingModel { Id = _ayId });
            if (!resultAY.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке учбеных годов возникла ошибка: ", resultAY.Errors);
                return false;
            }

            comboBoxAcademicYear.ValueMember = "Value";
            comboBoxAcademicYear.DisplayMember = "Display";
            comboBoxAcademicYear.DataSource = resultAY.Result.List
                .Select(d => new { Value = d.Id, Display = d.Title }).ToList();
            comboBoxAcademicYear.SelectedValue = _ayId;

            var resultD = _service.GetDisciplines(new DisciplineGetBindingModel { Id = _dId });
            if (!resultD.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке дисциплин возникла ошибка: ", resultD.Errors);
                return false;
            }

            comboBoxDiscipline.ValueMember = "Value";
            comboBoxDiscipline.DisplayMember = "Display";
            comboBoxDiscipline.DataSource = resultD.Result.List
                .Select(d => new { Value = d.Id, Display = d.DisciplineName }).ToList();
            comboBoxDiscipline.SelectedValue = _dId;

            var resultED = _service.GetEducationDirections(new EducationDirectionGetBindingModel { Id = _edId });
            if (!resultED.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке направлений возникла ошибка: ", resultED.Errors);
                return false;
            }

            comboBoxEducationDirection.ValueMember = "Value";
            comboBoxEducationDirection.DisplayMember = "Display";
            comboBoxEducationDirection.DataSource = resultED.Result.List
                .Select(d => new { Value = d.Id, Display = d.ToString() }).ToList();
            comboBoxEducationDirection.SelectedValue = _edId;

            var resultTN = _service.GetTimeNorms(new TimeNormGetBindingModel { Id = _tnId });
            if (!resultTN.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке норм времени возникла ошибка: ", resultTN.Errors);
                return false;
            }

            comboBoxTimeNorm.ValueMember = "Value";
            comboBoxTimeNorm.DisplayMember = "Display";
            comboBoxTimeNorm.DataSource = resultTN.Result.List
                .Select(d => new { Value = d.Id, Display = d.TimeNormName }).ToList();
            comboBoxTimeNorm.SelectedValue = _tnId;

            return true;
        }

        protected override void LoadData()
        {
            if (tabPageRecords.Controls.Count == 0)
            {
                var control = Container.Resolve<ControlDisciplineLessonTask>();
                control.Dock = DockStyle.Fill;
                tabPageRecords.Controls.Add(control);
            }
            (tabPageRecords.Controls[0] as ControlDisciplineLessonTask).LoadData(_id.Value);

            var result = _service.GetDisciplineLesson(new DisciplineLessonGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
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

        protected override bool CheckFill()
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
                if (!int.TryParse(textBoxOrder.Text, out int order))
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
                if (!int.TryParse(textBoxCountOfPairs.Text, out int order))
                {
                    return false;
                }
            }
            return true;
        }

        protected override bool Save()
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
                ErrorMessanger.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
                return false;
            }
        }

        private void ButtonAddFile_Click(object sender, EventArgs e)
        {
        }
    }
}