using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using ControlsAndForms.Forms;
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
    public partial class FormDiscipline : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineService _service;

        private readonly IProcess _processE;

        public FormDiscipline(IDisciplineService service, IProcess processE, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
            _processE = processE;
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

            StandartForm_Load();
        }

        protected override void LoadData()
        {
            var result = _service.GetDiscipline(new DisciplineGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

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

        protected override bool Save()
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
    }
}