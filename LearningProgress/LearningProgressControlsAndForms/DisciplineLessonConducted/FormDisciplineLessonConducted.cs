using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using DatabaseContext;
using LearningProgressControlsAndForms.DisciplineLessonConductedStudent;
using LearningProgressInterfaces.BindingModels;
using LearningProgressInterfaces.Interfaces;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Tools;
using Unity;

namespace LearningProgressControlsAndForms.DisciplineLessonConducted
{
    public partial class FormDisciplineLessonConducted : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineLessonConductedService _service;

        private readonly ILearningProgressProcess _process;

        private Guid? _ayId = null;

        private Guid? _edId = null;

        private Guid? _dId = null;

        private Guid? _tnId = null;

        private string _semester;

        public FormDisciplineLessonConducted(IDisciplineLessonConductedService service, ILearningProgressProcess process, Guid? ayId = null, Guid? edId = null, Guid? dId = null, Guid? tnId = null,
            string semester = null, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
            _process = process;
            _ayId = ayId;
            _edId = edId;
            _dId = dId;
            _tnId = tnId;
            _semester = semester;
        }

        protected override bool LoadComponents()
        {
            var resultSemesters = _process.GetSemesters(new LearningProcessSemesterBindingModel
            {
                AcademicYearId = _ayId.Value,
                EducationDirectionId = _edId.Value,
                DisciplineId = _dId.Value,
                UserId = DepartmentUserManager.UserId.Value
            });

            if (!resultSemesters.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке семестров возникла ошибка: ", resultSemesters.Errors);
                return false;
            }

            var resultSG = _process.GetStudentGroups(new LearningProcessStudentGroupBindingModel
            {
                EducationDirectionId = _edId.Value,
                Semesters = resultSemesters.Result
            });
            if (!resultSG.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке учебных групп возникла ошибка: ", resultSG.Errors);
                return false;
            }

            comboBoxStudentGroups.ValueMember = "Value";
            comboBoxStudentGroups.DisplayMember = "Display";
            comboBoxStudentGroups.DataSource = resultSG.Result.Select(y => new { Value = y.Id, Display = y.GroupName }).ToList();
            comboBoxStudentGroups.SelectedItem = null;

            var resultDL = _service.GetDisciplineLessons(new DisciplineLessonGetBindingModel { AcademicYearId = _ayId, DisciplineId = _dId, EducationDirectionId = _edId, TimeNormId = _tnId });
            if (!resultDL.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке занятий дисциплин возникла ошибка: ", resultDL.Errors);
                return false;
            }

            comboBoxDisciplineLesson.ValueMember = "Value";
            comboBoxDisciplineLesson.DisplayMember = "Display";
            comboBoxDisciplineLesson.DataSource = resultDL.Result.List
                .Select(d => new { Value = d.Id, Display = d.Title }).ToList();

            return true;
        }

        protected override void LoadData()
        {
            var result = _service.GetDisciplineLessonConducted(new DisciplineLessonConductedGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            if (tabPageRecords.Controls.Count == 0)
            {
                var control = Container.Resolve<ControlDisciplineLessonConductedStudent>();
                control.Dock = DockStyle.Fill;
                tabPageRecords.Controls.Add(control);
            }
            (tabPageRecords.Controls[0] as ControlDisciplineLessonConductedStudent).LoadData(_id.Value, entity.StudentGroupId);

            comboBoxDisciplineLesson.SelectedValue = entity.DisciplineLessonId;
            comboBoxStudentGroups.SelectedValue = entity.StudentGroupId;
            dateTimePickerDate.Value = entity.Date;
            comboBoxSubgroup.Text = entity.Subgroup;
        }

        protected override bool CheckFill()
        {
            if (comboBoxDisciplineLesson.SelectedValue == null)
            {
                return false;
            }
            if (comboBoxStudentGroups.SelectedValue == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(comboBoxSubgroup.Text))
            {
                return false;
            }
            return true;
        }

        protected override bool Save()
        {
            ResultService result;
            if (!_id.HasValue)
            {
                result = _service.CreateDisciplineLessonConducted(new DisciplineLessonConductedSetBindingModel
                {
                    DisciplineLessonId = new Guid(comboBoxDisciplineLesson.SelectedValue.ToString()),
                    StudentGroupId = new Guid(comboBoxStudentGroups.SelectedValue.ToString()),
                    Date = dateTimePickerDate.Value,
                    Subgroup = comboBoxSubgroup.Text
                });
            }
            else
            {
                result = _service.UpdateDisciplineLessonConducted(new DisciplineLessonConductedSetBindingModel
                {
                    Id = _id.Value,
                    DisciplineLessonId = new Guid(comboBoxDisciplineLesson.SelectedValue.ToString()),
                    StudentGroupId = new Guid(comboBoxStudentGroups.SelectedValue.ToString()),
                    Date = dateTimePickerDate.Value,
                    Subgroup = comboBoxSubgroup.Text
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

        private void ComboBoxStudentGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxStudentGroups.SelectedValue != null)
            {
                var resultS = _process.GetDisciplineLessonSubgroup(new DisciplineLessonSubgroupBindingModel
                {
                    DisciplineId = _dId.Value,
                    StudentGroupId = new Guid(comboBoxStudentGroups.SelectedValue.ToString()),
                    Semester = _semester
                });
                if (!resultS.Succeeded)
                {
                    ErrorMessanger.PrintErrorMessage("При загрузке подгрупп возникла ошибка: ", resultS.Errors);
                    return;
                }

                comboBoxSubgroup.ValueMember = "Value";
                comboBoxSubgroup.DisplayMember = "Display";
                comboBoxSubgroup.DataSource = resultS.Result
                    .Select(d => new { Value = d, Display = d }).ToList();
                comboBoxSubgroup.SelectedIndex = -1;
            }
        }
    }
}