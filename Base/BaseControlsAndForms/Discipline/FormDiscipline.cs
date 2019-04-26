using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using System;
using System.Data;
using System.Linq;
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

        protected override bool LoadComponents()
        {
            var resultDB = _service.GetDisciplineBlocks(new DisciplineBlockGetBindingModel { });
            if (!resultDB.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке блоков дисциплин возникла ошибка: ", resultDB.Errors);
                return false;
            }

            comboBoxDisciplineBlock.ValueMember = "Value";
            comboBoxDisciplineBlock.DisplayMember = "Display";
            comboBoxDisciplineBlock.DataSource = resultDB.Result.List
                .Select(d => new { Value = d.Id, Display = d.Title }).ToList();
            comboBoxDisciplineBlock.SelectedItem = null;

            return true;
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

        protected override bool CheckFill()
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
    }
}