using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentService.BindingModels;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TicketServiceInterfaces.BindingModels;
using TicketServiceInterfaces.Interfaces;
using TicketViews.Views.ExaminationTemplateBlock;
using TicketViews.Views.ExaminationTemplateTicket;
using TicketViews.Views.TicketTemplate;
using Unity;
using Unity.Attributes;

namespace TicketViews.Views.ExaminationTemplate
{
    public partial class ExaminationTemplateForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IExaminationTemplateService _service;

        private Guid? _id = null;

        public ExaminationTemplateForm(IExaminationTemplateService service, Guid? id = null)
        {
            InitializeComponent();
            _service = service;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void ExaminationTemplateForm_Load(object sender, EventArgs e)
        {
            var resultD = _service.GetDisciplines(new DisciplineGetBindingModel { });
            if (!resultD.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке дисциплин возникла ошибка: ", resultD.Errors);
                return;
            }

            var resultED = _service.GetEducationDirections(new EducationDirectionGetBindingModel { });
            if (!resultED.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке направлений возникла ошибка: ", resultED.Errors);
                return;
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


            if (_id.HasValue)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            if (tabPageRecords.Controls.Count == 0)
            {
                var control = Container.Resolve<ExaminationTemplateBlockControl>();
                control.Dock = DockStyle.Fill;
                tabPageRecords.Controls.Add(control);
            }
            (tabPageRecords.Controls[0] as ExaminationTemplateBlockControl).LoadData(_id.Value);

            if (tabPageTickets.Controls.Count == 0)
            {
                var control = Container.Resolve<ExaminationTemplateTicketControl>();
                control.Dock = DockStyle.Fill;
                tabPageTickets.Controls.Add(control);
            }
            (tabPageTickets.Controls[0] as ExaminationTemplateTicketControl).LoadData(_id.Value);

            if (tabPageTicketTemplate.Controls.Count == 0)
            {
                var control = Container.Resolve<TicketTemplateControl>();
                control.Dock = DockStyle.Fill;
                tabPageTicketTemplate.Controls.Add(control);
            }
            (tabPageTicketTemplate.Controls[0] as TicketTemplateControl).LoadData(_id.Value);

            var result = _service.GetExaminationTemplate(new ExaminationTemplateGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
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
        }

        private bool CheckFill()
        {
            labelDiscipline.ForeColor = SystemColors.ControlText;
            if (comboBoxDiscipline.SelectedValue == null)
            {
                labelDiscipline.ForeColor = Color.Red;
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
                    result = _service.CreateExaminationTemplate(new ExaminationTemplateSetBindingModel
                    {
                        DisciplineId = new Guid(comboBoxDiscipline.SelectedValue.ToString()),
                        EducationDirectionId = comboBoxEducationDirection.SelectedValue != null ? new Guid(comboBoxEducationDirection.SelectedValue.ToString()) : (Guid?)null,
                        Semester = string.IsNullOrEmpty(comboBoxSemester.Text) ? (Semesters?)null : (Semesters)Enum.Parse(typeof(Semesters), comboBoxSemester.Text),
                        ExaminationTemplateName = textBoxExaminationTemplateName.Text
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
                        ExaminationTemplateName = textBoxExaminationTemplateName.Text
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

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                MessageBox.Show("Сохранение прошло успешно", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
        }

        private void ButtonSaveAndClose_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxExaminationTemplateName.Text = string.Format("{0}{1}{2}", comboBoxDiscipline.Text, 
                string.IsNullOrEmpty(comboBoxEducationDirection.Text) ? "" : string.Format(" {0}", comboBoxEducationDirection.Text),
                string.IsNullOrEmpty(comboBoxSemester.Text) ? "" : string.Format(" {0}", comboBoxSemester.Text));
        }
    }
}