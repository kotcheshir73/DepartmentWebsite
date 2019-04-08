using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using ControlsAndForms.Messangers;
using ControlsAndForms.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Tools;
using Unity;

namespace BaseControlsAndForms.Discipline
{
    public partial class FormDiscipline : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineService _service;

        private readonly IProcess _processE;

        private Guid? _id = null;

        public FormDiscipline(IDisciplineService service, IProcess processE, Guid? id = null)
        {
            InitializeComponent();
            _service = service;
            _processE = processE;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void FormDiscipline_Load(object sender, EventArgs e)
        {
            var resultDB = _service.GetDisciplineBlocks(new DisciplineBlockGetBindingModel { });
            if (!resultDB.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке блоков дисциплин возникла ошибка: ", resultDB.Errors);
                return;
            }

            comboBoxDisciplineBlock.ValueMember = "Value";
            comboBoxDisciplineBlock.DisplayMember = "Display";
            comboBoxDisciplineBlock.DataSource = resultDB.Result.List
                .Select(d => new { Value = d.Id, Display = d.Title }).ToList();
            comboBoxDisciplineBlock.SelectedItem = null;

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
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            if (string.IsNullOrEmpty(entity.DisciplineShortName))
            {
                // TODO
                //entity.DisciplineShortName = ScheduleHelper.CalcShortDisciplineName(entity.DisciplineName);
            }

            textBoxTitle.Text = entity.DisciplineName;
            textBoxDisciplineShortName.Text = entity.DisciplineShortName;
            comboBoxDisciplineBlock.SelectedValue = entity.DisciplineBlockId;
            textBoxDisciplineBlueAsteriskName.Text = entity.DisciplineBlueAsteriskName;
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
            if (string.IsNullOrEmpty(textBoxDisciplineBlueAsteriskName.Text))
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
                    result = _service.CreateDiscipline(new DisciplineSetBindingModel
                    {
                        DisciplineName = textBoxTitle.Text,
                        DisciplineShortName = textBoxDisciplineShortName.Text,
                        DisciplineBlockId = new Guid(comboBoxDisciplineBlock.SelectedValue.ToString()),
                        DisciplineBlueAsteriskName = textBoxDisciplineBlueAsteriskName.Text
                    });
                }
                else
                {
                    result = _service.UpdateDiscipline(new DisciplineSetBindingModel
                    {
                        Id = _id.Value,
                        DisciplineName = textBoxTitle.Text,
                        DisciplineShortName = textBoxDisciplineShortName.Text,
                        DisciplineBlockId = new Guid(comboBoxDisciplineBlock.SelectedValue.ToString()),
                        DisciplineBlueAsteriskName = textBoxDisciplineBlueAsteriskName.Text
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
        }
    }
}