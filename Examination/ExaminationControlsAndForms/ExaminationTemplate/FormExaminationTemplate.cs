using BaseInterfaces.BindingModels;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using Enums;
using ExaminationControlsAndForms.ExaminationTemplateBlock;
using ExaminationControlsAndForms.ExaminationTemplateTicket;
using ExaminationControlsAndForms.TicketTemplate;
using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.Interfaces;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Tools;
using Unity;

namespace ExaminationControlsAndForms.ExaminationTemplate
{
    public partial class FormExaminationTemplate : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IExaminationTemplateService _service;

        public FormExaminationTemplate(IExaminationTemplateService service, ITicketTemplateService serviceTT, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
            controlTicketTemplateSearch.Service = serviceTT;
        }

        protected override bool LoadComponents()
        {
            var resultD = _service.GetDisciplines(new DisciplineGetBindingModel { });
            if (!resultD.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке дисциплин возникла ошибка: ", resultD.Errors);
                return false;
            }

            var resultED = _service.GetEducationDirections(new EducationDirectionGetBindingModel { });
            if (!resultED.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке направлений возникла ошибка: ", resultED.Errors);
                return false;
            }

            foreach (var elem in Enum.GetValues(typeof(Semesters)))
            {
                comboBoxSemester.Items.Add(elem.ToString());
            }

            comboBoxDiscipline.ValueMember = "Value";
            comboBoxDiscipline.DisplayMember = "Display";
            comboBoxDiscipline.DataSource = resultD.Result.List
                .Select(d => new { Value = d.Id, Display = d.DisciplineName }).ToList();
            comboBoxDiscipline.SelectedItem = null;

            comboBoxEducationDirection.ValueMember = "Value";
            comboBoxEducationDirection.DisplayMember = "Display";
            comboBoxEducationDirection.DataSource = resultED.Result.List
                .Select(ed => new { Value = ed.Id, Display = ed.Cipher + " " + ed.Title }).ToList();
            comboBoxEducationDirection.SelectedItem = null;

            return true;
        }

        protected override void LoadData()
        {
            if (tabPageRecords.Controls.Count == 0)
            {
                var control = Container.Resolve<ControlExaminationTemplateBlock>();
                control.Dock = DockStyle.Fill;
                tabPageRecords.Controls.Add(control);
            }
            (tabPageRecords.Controls[0] as ControlExaminationTemplateBlock).LoadData(_id.Value);

            if (tabPageTickets.Controls.Count == 0)
            {
                var control = Container.Resolve<ControlExaminationTemplateTicket>();
                control.Dock = DockStyle.Fill;
                tabPageTickets.Controls.Add(control);
            }
            (tabPageTickets.Controls[0] as ControlExaminationTemplateTicket).LoadData(_id.Value);

            var result = _service.GetExaminationTemplate(new ExaminationTemplateGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            comboBoxDiscipline.SelectedValue = entity.DisciplineId;
            comboBoxEducationDirection.SelectedValue = entity.EducationDirectionId;
            if (!string.IsNullOrEmpty(entity.Semester))
            {
                comboBoxSemester.SelectedIndex = comboBoxSemester.Items.IndexOf(entity.Semester);
            }
            textBoxExaminationTemplateName.Text = entity.ExaminationTemplateName;
            controlTicketTemplateSearch.Id = entity.TicketTemplateId;
        }

        protected override bool CheckFill()
        {
            labelDiscipline.ForeColor = SystemColors.ControlText;
            if (comboBoxDiscipline.SelectedValue == null)
            {
                labelDiscipline.ForeColor = Color.Red;
                return false;
            }
            return true;
        }

        protected override bool Save()
        {
            ResultService result;
            if (!_id.HasValue)
            {
                result = _service.CreateExaminationTemplate(new ExaminationTemplateSetBindingModel
                {
                    DisciplineId = new Guid(comboBoxDiscipline.SelectedValue.ToString()),
                    EducationDirectionId = comboBoxEducationDirection.SelectedValue != null ? new Guid(comboBoxEducationDirection.SelectedValue.ToString()) : (Guid?)null,
                    Semester = string.IsNullOrEmpty(comboBoxSemester.Text) ? (Semesters?)null : (Semesters)Enum.Parse(typeof(Semesters), comboBoxSemester.Text),
                    ExaminationTemplateName = textBoxExaminationTemplateName.Text,
                    TicketTemplateId = controlTicketTemplateSearch.Id
                });
            }
            else
            {
                result = _service.UpdateExaminationTemplate(new ExaminationTemplateSetBindingModel
                {
                    Id = _id.Value,
                    DisciplineId = new Guid(comboBoxDiscipline.SelectedValue.ToString()),
                    EducationDirectionId = comboBoxEducationDirection.SelectedValue != null ? new Guid(comboBoxEducationDirection.SelectedValue.ToString()) : (Guid?)null,
                    Semester = string.IsNullOrEmpty(comboBoxSemester.Text) ? (Semesters?)null : (Semesters)Enum.Parse(typeof(Semesters), comboBoxSemester.Text),
                    ExaminationTemplateName = textBoxExaminationTemplateName.Text,
                    TicketTemplateId = controlTicketTemplateSearch.Id
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

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxExaminationTemplateName.Text = string.Format("{0}{1}{2}", comboBoxDiscipline.Text,
                string.IsNullOrEmpty(comboBoxEducationDirection.Text) ? "" : string.Format(" {0}", comboBoxEducationDirection.Text),
                string.IsNullOrEmpty(comboBoxSemester.Text) ? "" : string.Format(" {0}", comboBoxSemester.Text));
        }
    }
}