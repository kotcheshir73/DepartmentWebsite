using AcademicYearControlsAndForms.IndividualPlanKindOfWork;
using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using System;
using System.Windows.Forms;
using Tools;
using Unity;

namespace AcademicYearControlsAndForms.IndividualPlanTitle
{
    public partial class FormIndividualPlanTitle : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IIndividualPlanTitleService _service;

        public FormIndividualPlanTitle(IIndividualPlanTitleService service, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
        }

        private void FormIndividualPlanTitle_Load(object sender, EventArgs e)
        {
            StandartForm_Load();
        }

        protected override void LoadData()
        {
            if (tabPageRecords.Controls.Count == 0)
            {
                var control = Container.Resolve<ControlIndividualPlanKindOfWork>();
                control.Dock = DockStyle.Fill;
                tabPageRecords.Controls.Add(control);
            }
            (tabPageRecords.Controls[0] as ControlIndividualPlanKindOfWork).LoadData(_id.Value);

            var result = _service.GetIndividualPlanTitle(new IndividualPlanTitleGetBindingModel { Id = _id });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            textBoxOrder.Text = entity.Order.ToString();
            textBoxTitle.Text = entity.Title;
        }

        private bool CheckFill()
        {
            if (string.IsNullOrEmpty(textBoxTitle.Text))
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
            return true;
        }

        protected override bool Save()
        {
            if (CheckFill())
            {
                ResultService result;
                if (!_id.HasValue)
                {
                    result = _service.CreateIndividualPlanTitle(new IndividualPlanTitleSetBindingModel
                    {
                        Title = textBoxTitle.Text,
                        Order = Convert.ToInt32(textBoxOrder.Text)
                    });
                }
                else
                {
                    result = _service.UpdateIndividualPlanTitle(new IndividualPlanTitleSetBindingModel
                    {
                        Id = _id.Value,
                        Title = textBoxTitle.Text,
                        Order = Convert.ToInt32(textBoxOrder.Text)
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