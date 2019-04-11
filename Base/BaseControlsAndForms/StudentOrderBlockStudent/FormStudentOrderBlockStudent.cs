using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Tools;
using Unity;

namespace BaseControlsAndForms.StudentOrderBlockStudent
{
    public partial class FormStudentOrderBlockStudent : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IStudentOrderBlockStudentService _service;

        private Guid? _sobId = null;

        public FormStudentOrderBlockStudent(IStudentOrderBlockStudentService service, Guid? sobId = null, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
            _sobId = sobId;
        }

        private void FormStudentOrderBlockStudent_Load(object sender, EventArgs e)
        {
            var resultSOB = _service.GetStudentOrderBlocks(new StudentOrderBlockGetBindingModel { Id = _sobId });
            if (!resultSOB.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке блоков приказа возникла ошибка: ", resultSOB.Errors);
                return;
            }

            comboBoxStudentOrderBlock.ValueMember = "Value";
            comboBoxStudentOrderBlock.DisplayMember = "Display";
            comboBoxStudentOrderBlock.DataSource = resultSOB.Result.List
                .Select(d => new { Value = d.Id, Display = d.StudentOrderType }).ToList();
            comboBoxStudentOrderBlock.SelectedValue = _sobId;


            var resultS = _service.GetStudents(new StudentGetBindingModel { });
            if (!resultS.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке студентов возникла ошибка: ", resultS.Errors);
                return;
            }

            comboBoxStudent.ValueMember = "Value";
            comboBoxStudent.DisplayMember = "Display";
            comboBoxStudent.DataSource = resultS.Result.List
                .Select(d => new { Value = d.Id, Display = d.FullName }).ToList();
            comboBoxStudent.SelectedItem = null;


            var resultSG = _service.GetStudentGroups(new StudentGroupGetBindingModel { });
            if (!resultSG.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке дисциплин возникла ошибка: ", resultSG.Errors);
                return;
            }

            comboBoxStudentGroupFrom.ValueMember = "Value";
            comboBoxStudentGroupFrom.DisplayMember = "Display";
            comboBoxStudentGroupFrom.DataSource = resultSG.Result.List
                .Select(d => new { Value = d.Id, Display = d.GroupName }).ToList();
            comboBoxStudentGroupFrom.SelectedItem = null;

            comboBoxStudentGroupTo.ValueMember = "Value";
            comboBoxStudentGroupTo.DisplayMember = "Display";
            comboBoxStudentGroupTo.DataSource = resultSG.Result.List
                .Select(d => new { Value = d.Id, Display = d.GroupName }).ToList();
            comboBoxStudentGroupTo.SelectedItem = null;

            StandartForm_Load();
        }

        protected override void LoadData()
        {
            var result = _service.GetStudentOrderBlockStudent(new StudentOrderBlockStudentGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;
            
            comboBoxStudentOrderBlock.SelectedValue = entity.StudentOrderBlockId;
            comboBoxStudent.SelectedValue = entity.StudentId;
            if(entity.StudentGroupFromId.HasValue)
            {
                comboBoxStudentGroupFrom.SelectedValue = entity.StudentGroupFromId;
            }
            if (entity.StudentGroupToId.HasValue)
            {
                comboBoxStudentGroupTo.SelectedValue = entity.StudentGroupToId;
            }
            textBoxInfo.Text = entity.Info;
        }

        private bool CheckFill()
        {
            if (comboBoxStudentOrderBlock.SelectedValue == null)
            {
                return false;
            }
            if (comboBoxStudent.SelectedValue == null)
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
                    result = _service.CreateStudentOrderBlockStudent(new StudentOrderBlockStudentSetBindingModel
                    {
                        StudentOrderBlockId = new Guid(comboBoxStudentOrderBlock.SelectedValue.ToString()),
                        StudentId = new Guid(comboBoxStudent.SelectedValue.ToString()),
                        StudentGroupFromId = comboBoxStudentGroupFrom.SelectedValue != null? new Guid(comboBoxStudentGroupFrom.SelectedValue.ToString()) : (Guid?)null,
                        StudentGroupToId = comboBoxStudentGroupTo.SelectedValue != null ? new Guid(comboBoxStudentGroupTo.SelectedValue.ToString()) : (Guid?)null,
                        Info = textBoxInfo.Text
                    });
                }
                else
                {
                    result = _service.UpdateStudentOrderBlockStudent(new StudentOrderBlockStudentSetBindingModel
                    {
                        Id = _id.Value,
                        StudentOrderBlockId = new Guid(comboBoxStudentOrderBlock.SelectedValue.ToString()),
                        StudentId = new Guid(comboBoxStudent.SelectedValue.ToString()),
                        StudentGroupFromId = comboBoxStudentGroupFrom.SelectedValue != null ? new Guid(comboBoxStudentGroupFrom.SelectedValue.ToString()) : (Guid?)null,
                        StudentGroupToId = comboBoxStudentGroupTo.SelectedValue != null ? new Guid(comboBoxStudentGroupTo.SelectedValue.ToString()) : (Guid?)null,
                        Info = textBoxInfo.Text
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