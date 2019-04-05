﻿using DepartmentDesktop.Views.LearningProgress.DisciplineLessonConducted.DisciplineLessonConductedStudent;
using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Unity;

namespace DepartmentDesktop.Views.LearningProgress.DisciplineLessonConducted
{
    public partial class DisciplineLessonConductedForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineLessonConductedService _service;

        private readonly ILearningProgressProcess _process;

        private Guid? _id = null;

        private Guid? _ayId = null;

        private Guid? _edId = null;

        private Guid? _dId = null;

        private Guid? _tnId = null;

        private string _semester;

        public DisciplineLessonConductedForm(IDisciplineLessonConductedService service, ILearningProgressProcess process, Guid? ayId = null, Guid? edId = null, Guid? dId = null, Guid? tnId = null, 
            string semester = null, Guid? id = null)
        {
            InitializeComponent();
            _service = service;
            _process = process;
            _ayId = ayId;
            _edId = edId;
            _dId = dId;
            _tnId = tnId;
            _semester = semester;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void DisciplineLessonConductedForm_Load(object sender, EventArgs e)
        {
            var resultSemesters = _process.GetSemesters(new LearningProcessSemesterBindingModel
            {
                AcademicYearId = _ayId.Value,
                EducationDirectionId = _edId.Value,
                DisciplineId = _dId.Value,
                UserId = AuthorizationService.UserId.Value
            });

            if (!resultSemesters.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке семестров возникла ошибка: ", resultSemesters.Errors);
                return;
            }

            var resultSG = _process.GetStudentGroups(new LearningProcessStudentGroupBindingModel
            {
                EducationDirectionId = _edId.Value,
                Semesters = resultSemesters.Result
            });
            if (!resultSG.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке учебных групп возникла ошибка: ", resultSG.Errors);
                return;
            }

            comboBoxStudentGroups.ValueMember = "Value";
            comboBoxStudentGroups.DisplayMember = "Display";
            comboBoxStudentGroups.DataSource = resultSG.Result.Select(y => new { Value = y.Id, Display = y.GroupName }).ToList();
            comboBoxStudentGroups.SelectedItem = null;

            var resultDL = _service.GetDisciplineLessons(new DisciplineLessonGetBindingModel { AcademicYearId = _ayId, DisciplineId = _dId, EducationDirectionId = _edId, TimeNormId = _tnId });
            if (!resultDL.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке занятий дисциплин возникла ошибка: ", resultDL.Errors);
                return;
            }

            comboBoxDisciplineLesson.ValueMember = "Value";
            comboBoxDisciplineLesson.DisplayMember = "Display";
            comboBoxDisciplineLesson.DataSource = resultDL.Result.List
                .Select(d => new { Value = d.Id, Display = d.Title }).ToList();            

            if (_id.HasValue)
            {
                LoadData();
            }
        }

        private void LoadData()
        {

            var result = _service.GetDisciplineLessonConducted(new DisciplineLessonConductedGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            if (tabPageRecords.Controls.Count == 0)
            {
                var control = Container.Resolve<DisciplineLessonConductedStudentControl>();
                control.Dock = DockStyle.Fill;
                tabPageRecords.Controls.Add(control);
            }
            (tabPageRecords.Controls[0] as DisciplineLessonConductedStudentControl).LoadData(_id.Value, entity.StudentGroupId);

            comboBoxDisciplineLesson.SelectedValue = entity.DisciplineLessonId;
            comboBoxStudentGroups.SelectedValue = entity.StudentGroupId;
            dateTimePickerDate.Value = entity.Date;
            comboBoxSubgroup.Text = entity.Subgroup;
        }

        private bool CheckFill()
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

        private bool Save()
        {
            if (CheckFill())
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

        private void comboBoxStudentGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxStudentGroups.SelectedValue != null)
            {
                var resultS = _process.GetDisciplineLessonSubgroup(new DisciplineLessonSubgroupBindingModel { DisciplineId = _dId.Value, StudentGroupId = new Guid(comboBoxStudentGroups.SelectedValue.ToString()),
                Semester = _semester});
                if (!resultS.Succeeded)
                {
                    Program.PrintErrorMessage("При загрузке подгрупп возникла ошибка: ", resultS.Errors);
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
