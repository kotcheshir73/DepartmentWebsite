using AcademicYearControlsAndForms.DisciplineTimeDistributionClassroom;
using AcademicYearControlsAndForms.DisciplineTimeDistributionRecord;
using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using BaseInterfaces.BindingModels;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Tools;
using Unity;

namespace AcademicYearControlsAndForms.DisciplineTimeDistribution
{
    public partial class FormDisciplineTimeDistribution : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineTimeDistributionService _service;

        private Guid? _aprId;

        public FormDisciplineTimeDistribution(IDisciplineTimeDistributionService service, Guid? aprId, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
            _aprId = aprId;
        }

        private void FormDisciplineTimeDistribution_Load(object sender, EventArgs e)
        {
            var resultAPR = _service.GetAcademicPlanRecords(new AcademicPlanRecordGetBindingModel { Id = _aprId });
            if (!resultAPR.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке записей учебных планов возникла ошибка: ", resultAPR.Errors);
                return;
            }

            var resultSG = _service.GetStudentGroups(new StudentGroupGetBindingModel { });
            if (!resultSG.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке групп возникла ошибка: ", resultSG.Errors);
                return;
            }

            comboBoxAcademicPlanRecord.ValueMember = "Value";
            comboBoxAcademicPlanRecord.DisplayMember = "Display";
            comboBoxAcademicPlanRecord.DataSource = resultAPR.Result.List
                .Select(ap => new { Value = ap.Id, Display = ap.Disciplne }).ToList();

            comboBoxStudentGroup.ValueMember = "Value";
            comboBoxStudentGroup.DisplayMember = "Display";
            comboBoxStudentGroup.DataSource = resultSG.Result.List
                .Select(d => new { Value = d.Id, Display = d.GroupName }).ToList();
            comboBoxStudentGroup.SelectedItem = null;

            StandartForm_Load();
        }

        protected override void LoadData()
        {
            if (tabPageRecords.Controls.Count == 0)
            {
                var control = Container.Resolve<ControlDisciplineTimeDistributionRecord>();
                control.Dock = DockStyle.Fill;
                tabPageRecords.Controls.Add(control);
            }
            (tabPageRecords.Controls[0] as ControlDisciplineTimeDistributionRecord).LoadData(_id.Value);

            if (tabPageClassrooms.Controls.Count == 0)
            {
                var control = Container.Resolve<ControlDisciplineTimeDistributionClassroom>();
                control.Dock = DockStyle.Fill;
                tabPageClassrooms.Controls.Add(control);
            }
            (tabPageClassrooms.Controls[0] as ControlDisciplineTimeDistributionClassroom).LoadData(_id.Value);

            var result = _service.GetDisciplineTimeDistribution(new DisciplineTimeDistributionGetBindingModel { Id = _id });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            comboBoxAcademicPlanRecord.SelectedValue = entity.AcademicPlanRecordId;
            comboBoxStudentGroup.SelectedValue = entity.StudentGroupId;
            textBoxComment.Text = entity.Comment;
            textBoxCommentWishesOfTeacher.Text = entity.CommentWishesOfTeacher;
        }

        private bool CheckFill()
        {
            if (comboBoxAcademicPlanRecord.SelectedValue == null)
            {
                return false;
            }
            if (comboBoxStudentGroup.SelectedValue == null)
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
                    result = _service.CreateDisciplineTimeDistribution(new DisciplineTimeDistributionSetBindingModel
                    {
                        AcademicPlanRecordId = new Guid(comboBoxAcademicPlanRecord.SelectedValue.ToString()),
                        StudentGroupId = new Guid(comboBoxStudentGroup.SelectedValue.ToString()),
                        Comment = textBoxComment.Text,
                        CommentWishesOfTeacher = textBoxCommentWishesOfTeacher.Text
                    });
                }
                else
                {
                    result = _service.UpdateDisciplineTimeDistribution(new DisciplineTimeDistributionSetBindingModel
                    {
                        Id = _id.Value,
                        AcademicPlanRecordId = new Guid(comboBoxAcademicPlanRecord.SelectedValue.ToString()),
                        StudentGroupId = new Guid(comboBoxStudentGroup.SelectedValue.ToString()),
                        Comment = textBoxComment.Text,
                        CommentWishesOfTeacher = textBoxCommentWishesOfTeacher.Text
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