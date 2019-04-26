using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Tools;
using Unity;

namespace AcademicYearControlsAndForms.IndividualPlanKindOfWork
{
    public partial class FormIndividualPlanKindOfWork : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IIndividualPlanKindOfWorkService _service;

        private Guid _iptId;

        public FormIndividualPlanKindOfWork(IIndividualPlanKindOfWorkService service, Guid iptId, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
            _iptId = iptId;
        }

        protected override bool LoadComponents()
        {
            if (_iptId == null)
            {
                MessageBox.Show("Не указан заголовок", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            var resultIPT = _service.GetIndividualPlanTitles(new IndividualPlanTitleGetBindingModel { Id = _iptId });
            if (!resultIPT.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке заголовков возникла ошибка: ", resultIPT.Errors);
                return false;
            }

            comboBoxIndividualPlanTitle.ValueMember = "Value";
            comboBoxIndividualPlanTitle.DisplayMember = "Display";
            comboBoxIndividualPlanTitle.DataSource = resultIPT.Result.List
                .Select(x => new { Value = x.Id, Display = x.Title }).ToList();
            comboBoxIndividualPlanTitle.SelectedValue = _iptId;

            return true;
        }

        protected override void LoadData()
        {
            var result = _service.GetIndividualPlanKindOfWork(new IndividualPlanKindOfWorkGetBindingModel { Id = _id });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            comboBoxIndividualPlanTitle.SelectedValue = entity.IndividualPlanTitleId;
            textBoxOrder.Text = entity.Order.ToString();
            textBoxName.Text = entity.Name;
            textBoxTimeNormDescription.Text = entity.TimeNormDescription;
        }

        protected override bool CheckFill()
        {
            if (comboBoxIndividualPlanTitle.SelectedValue == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxOrder.Text))
            {
                return false;
            }
            else if (!int.TryParse(textBoxOrder.Text, out int order))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxTimeNormDescription.Text))
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
                result = _service.CreateIndividualPlanKindOfWork(new IndividualPlanKindOfWorkSetBindingModel
                {
                    IndividualPlanTitleId = new Guid(comboBoxIndividualPlanTitle.SelectedValue.ToString()),
                    Order = Convert.ToInt32(textBoxOrder.Text),
                    Name = textBoxName.Text,
                    TimeNormDescription = textBoxTimeNormDescription.Text
                });
            }
            else
            {
                result = _service.UpdateIndividualPlanKindOfWork(new IndividualPlanKindOfWorkSetBindingModel
                {
                    Id = _id.Value,
                    IndividualPlanTitleId = new Guid(comboBoxIndividualPlanTitle.SelectedValue.ToString()),
                    Order = Convert.ToInt32(textBoxOrder.Text),
                    Name = textBoxName.Text,
                    TimeNormDescription = textBoxTimeNormDescription.Text
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