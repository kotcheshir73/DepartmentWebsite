using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools;
using Unity;

namespace AcademicYearControlsAndForms.IndividualPlanNIRScientificArticle
{
    public partial class FormIndividualPlanNIRScientificArticle : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IIndividualPlanNIRScientificArticleService _service;

        private Guid _ipId;

        public FormIndividualPlanNIRScientificArticle(IIndividualPlanNIRScientificArticleService service, Guid ipId, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
            _ipId = ipId;
        }

        protected override bool LoadComponents()
        {
            if (_ipId == null)
            {
                MessageBox.Show("Не указан индивидуальный план", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            var resultIP = _service.GetIndividualPlans(new IndividualPlanGetBindingModel { Id = _ipId });
            if (!resultIP.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке индивидуальных планов возникла ошибка: ", resultIP.Errors);
                return true;
            }

            comboBoxIndividualPlan.ValueMember = "Value";
            comboBoxIndividualPlan.DisplayMember = "Display";
            comboBoxIndividualPlan.DataSource = resultIP.Result.List
                .Select(x => new { Value = x.Id, Display = x.AcademicYearsTitle + " - " + x.LecturerName }).ToList();
            comboBoxIndividualPlan.SelectedValue = _ipId;

            return true;
        }

        protected override void LoadData()
        {
            var result = _service.GetIndividualPlanNIRScientificArticle(new IndividualPlanNIRScientificArticleGetBindingModel { Id = _id });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            comboBoxIndividualPlan.SelectedValue = entity.IndividualPlanId;
            textBoxOrder.Text = entity.Order.ToString();
            textBoxName.Text = entity.Name;
            textBoxTypeOfPublication.Text = entity.TypeOfPublication;
            textBoxVolume.Text = entity.Volume.ToString();
            textBoxPublishing.Text = entity.Publishing;
            textBoxYear.Text = entity.Year.ToString();
            textBoxStatus.Text = entity.Status;
        }

        protected override bool CheckFill()
        {
            if (comboBoxIndividualPlan.SelectedValue == null)
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
            if (string.IsNullOrEmpty(textBoxTypeOfPublication.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxVolume.Text))
            {
                return false;
            }
            else if (!double.TryParse(textBoxVolume.Text, out double volume))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxPublishing.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxYear.Text))
            {
                return false;
            }
            else if (!int.TryParse(textBoxYear.Text, out int year))
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
                result = _service.CreateIndividualPlanNIRScientificArticle(new IndividualPlanNIRScientificArticleSetBindingModel
                {
                    IndividualPlanId = new Guid(comboBoxIndividualPlan.SelectedValue.ToString()),
                    Order = Convert.ToInt32(textBoxOrder.Text),
                    Name = textBoxName.Text,
                    TypeOfPublication = textBoxTypeOfPublication.Text,
                    Volume = Convert.ToDouble(textBoxVolume.Text),
                    Publishing = textBoxPublishing.Text,
                    Year = Convert.ToInt32(textBoxYear.Text),
                    Status = textBoxStatus.Text
                });
            }
            else
            {
                result = _service.UpdateIndividualPlanNIRScientificArticle(new IndividualPlanNIRScientificArticleSetBindingModel
                {
                    Id = _id.Value,
                    IndividualPlanId = new Guid(comboBoxIndividualPlan.SelectedValue.ToString()),
                    Order = Convert.ToInt32(textBoxOrder.Text),
                    Name = textBoxName.Text,
                    TypeOfPublication = textBoxTypeOfPublication.Text,
                    Volume = Convert.ToDouble(textBoxVolume.Text),
                    Publishing = textBoxPublishing.Text,
                    Year = Convert.ToInt32(textBoxYear.Text),
                    Status = textBoxStatus.Text
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