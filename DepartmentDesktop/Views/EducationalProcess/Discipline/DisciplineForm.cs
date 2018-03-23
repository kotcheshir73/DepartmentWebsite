using DepartmentDesktop.Models;
using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.Helpers;
using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace DepartmentDesktop.Views.EducationalProcess.Discipline
{
    public partial class DisciplineForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineService _service;

        private readonly IEducationalProcessService _processE;

        private Guid? _id = null;

        public DisciplineForm(IDisciplineService service, IEducationalProcessService processE, Guid? id = null)
        {
            InitializeComponent();
            _service = service;
            _processE = processE;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void DisciplineForm_Load(object sender, EventArgs e)
        {
            var resultDB = _service.GetDisciplineBlocks(new DisciplineBlockGetBindingModel { });
            if (!resultDB.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке блоков дисциплин возникла ошибка: ", resultDB.Errors);
                return;
            }

            var resultAY = _service.GetAcademicYears(new AcademicYearGetBindingModel { });
            if (!resultAY.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке академических годов возникла ошибка: ", resultAY.Errors);
                return;
            }

            var resultSD = _service.GetSeasonDaties(new SeasonDatesGetBindingModel { });
            if (!resultSD.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке семестров возникла ошибка: ", resultAY.Errors);
                return;
            }

            comboBoxDisciplineBlock.ValueMember = "Value";
            comboBoxDisciplineBlock.DisplayMember = "Display";
            comboBoxDisciplineBlock.DataSource = resultDB.Result.List
                .Select(d => new { Value = d.Id, Display = d.Title }).ToList();
            comboBoxDisciplineBlock.SelectedItem = null;

            comboBoxAcademicYear.ValueMember = "Value";
            comboBoxAcademicYear.DisplayMember = "Display";
            comboBoxAcademicYear.DataSource = resultAY.Result.List
                .Select(d => new { Value = d.Id, Display = d.Title }).ToList();
            comboBoxAcademicYear.SelectedItem = null;

            comboBoxSeasonDate.ValueMember = "Value";
            comboBoxSeasonDate.DisplayMember = "Display";
            comboBoxSeasonDate.DataSource = resultSD.Result.List
                .Select(d => new { Value = d.Id, Display = d.Title }).ToList();
            comboBoxSeasonDate.SelectedItem = null;

            if (_id.HasValue)
            {
                LoadData();

                LoadSettingsAcademicPlaRecords();
                LoadSettingsSchedule();
            }
        }

        private void LoadData()
        {
            var result = _service.GetDiscipline(new DisciplineGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            if (string.IsNullOrEmpty(entity.DisciplineShortName))
            {
                entity.DisciplineShortName = ScheduleHelper.CalcShortDisciplineName(entity.DisciplineName);
            }

            textBoxTitle.Text = entity.DisciplineName;
            textBoxDisciplineShortName.Text = entity.DisciplineShortName;
            comboBoxDisciplineBlock.SelectedValue = entity.DisciplineBlockId;
        }

        private bool CheckFill()
        {
            if (string.IsNullOrEmpty(textBoxTitle.Text))
            {
                return false;
            }
            if (comboBoxDisciplineBlock.SelectedValue == null)
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
                    result = _service.CreateDiscipline(new DisciplineRecordBindingModel
                    {
                        DisciplineName = textBoxTitle.Text,
                        DisciplineShortName = textBoxDisciplineShortName.Text,
                        DisciplineBlockId = new Guid(comboBoxDisciplineBlock.SelectedValue.ToString())
                    });
                }
                else
                {
                    result = _service.UpdateDiscipline(new DisciplineRecordBindingModel
                    {
                        Id = _id.Value,
                        DisciplineName = textBoxTitle.Text,
                        DisciplineShortName = textBoxDisciplineShortName.Text,
                        DisciplineBlockId = new Guid(comboBoxDisciplineBlock.SelectedValue.ToString())
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

        private void comboBoxAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxAcademicYear.SelectedValue != null && _id.HasValue)
            {
                standartControlAcademicPlanRecords.LoadPage();
            }
        }

        private void comboBoxSeasonDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSeasonDate.SelectedValue != null && _id.HasValue)
            {
                standartControlSchedule.LoadPage();
            }
        }

        /// <summary>
        /// Настройка контрола для вывода учебного плана по дисциплине
        /// </summary>
        private void LoadSettingsAcademicPlaRecords()
        {
            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "EducationDirectionCipher", Title = "Направление", Width = 100, Visible = true },
                new ColumnConfig { Name = "Semester", Title = "Семестры", Width = 100, Visible = true },
                new ColumnConfig { Name = "KindOfLoad", Title = "Вид нагрузки", Width = 200, Visible = true },
                new ColumnConfig { Name = "Hours", Title = "Часы", Width = 100, Visible = true }
            };

            List<string> hideToolStripButtons = new List<string> { "toolStripButtonAdd", "toolStripButtonUpd", "toolStripButtonDel", "toolStripDropDownButtonMoves" };

            standartControlAcademicPlanRecords.Configurate(columns, hideToolStripButtons, 20);

            standartControlAcademicPlanRecords.GetPageAddEvent(LoadAcademicPlanRecords);
        }

        /// <summary>
        /// Вывод страницы учебного плана по дисциплине
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private int LoadAcademicPlanRecords(int pageNumber, int pageSize)
        {
            var result = _processE.GetAcademicPlanRecordsForDiscipline(new AcademicPlanRecrodsForDiciplineBindingModel
            {
                AcademicYearId = new Guid(comboBoxAcademicYear.SelectedValue.ToString()),
                DisciplineId = _id.Value,
                PageNumber = pageNumber,
                PageSize = pageSize
            });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                return -1;
            }
            standartControlAcademicPlanRecords.GetDataGridViewRows.Clear();
            foreach (var res in result.Result.List)
            {
                standartControlAcademicPlanRecords.GetDataGridViewRows.Add(
                     res.Id,
                     res.EducationDirectionCipher,
                     res.Semester,
                     res.KindOfLoad,
                     res.Hours
                );
            }
            return result.Result.MaxCount;
        }

        private void LoadSettingsSchedule()
        {
            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "Type", Title = "Type", Width = 100, Visible = false },
                new ColumnConfig { Name = "Date", Title = "Дата", Width = 200, Visible = true },
                new ColumnConfig { Name = "LessonType", Title = "Тип занятия", Width = 100, Visible = true },
                new ColumnConfig { Name = "Classroom", Title = "Аудитория", Width = 100, Visible = true },
                new ColumnConfig { Name = "Lecturer", Title = "Преподаватель", Width = 200, Visible = true },
                new ColumnConfig { Name = "Group", Title = "Группа", Width = 100, Visible = true }
            };

            List<string> hideToolStripButtons = new List<string> { "toolStripButtonAdd", "toolStripButtonUpd", "toolStripButtonDel", "toolStripDropDownButtonMoves" };

            standartControlSchedule.Configurate(columns, hideToolStripButtons, 20);

            standartControlSchedule.GetPageAddEvent(LoadSchedule);
        }

        /// <summary>
        /// Вывод страницы учебного плана по дисциплине
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private int LoadSchedule(int pageNumber, int pageSize)
        {
            var result = _processE.GetScheduleRecordsForDiciplinePageViewModel(new ScheduleRecordsForDiciplineBindingModel
            {
                SeasonDateId = new Guid(comboBoxSeasonDate.SelectedValue.ToString()),
                DisciplineId = _id.Value,
                PageNumber = pageNumber,
                PageSize = pageSize
            });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                return -1;
            }
            standartControlSchedule.GetDataGridViewRows.Clear();
            foreach (var res in result.Result.List)
            {
                standartControlSchedule.GetDataGridViewRows.Add(
                     res.Id,
                     res.Type,
                     res.Date,
                     res.LessonType,
                     res.LessonClassroom,
                     res.LessonLecturer,
                     res.LessonGroup
                );
            }
            return result.Result.MaxCount;
        }
    }
}
