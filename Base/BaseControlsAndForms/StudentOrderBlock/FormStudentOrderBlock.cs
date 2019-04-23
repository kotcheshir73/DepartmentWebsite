using BaseControlsAndForms.StudentOrderBlockStudent;
using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using Enums;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Tools;
using Unity;

namespace BaseControlsAndForms.StudentOrderBlock
{
    public partial class FormStudentOrderBlock : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IStudentOrderBlockService _service;

        private Guid? _soId = null;

        public FormStudentOrderBlock(IStudentOrderBlockService service, Guid? soId = null, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
            _soId = soId;
        }

        private void FormStudentOrderBlock_Load(object sender, EventArgs e)
        {
            var resultSO = _service.GetStudentOrders(new StudentOrderGetBindingModel { Id = _soId });
            if (!resultSO.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке приказов возникла ошибка: ", resultSO.Errors);
                return;
            }

            comboBoxStudentOrder.ValueMember = "Value";
            comboBoxStudentOrder.DisplayMember = "Display";
            comboBoxStudentOrder.DataSource = resultSO.Result.List
                .Select(d => new { Value = d.Id, Display = d.StudentOrderType }).ToList();
            comboBoxStudentOrder.SelectedItem = null;

            foreach (var elem in Enum.GetValues(typeof(StudentOrderType)))
            {
                comboBoxStudentOrderType.Items.Add(elem.ToString());
            }
            comboBoxStudentOrderType.SelectedIndex = 0;

            var resultED = _service.GetEducationDirections(new EducationDirectionGetBindingModel { });
            if (!resultED.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке направлений возникла ошибка: ", resultED.Errors);
                return;
            }

            comboBoxEducationDirection.ValueMember = "Value";
            comboBoxEducationDirection.DisplayMember = "Display";
            comboBoxEducationDirection.DataSource = resultED.Result.List
                .Select(d => new { Value = d.Id, Display = d.Cipher }).ToList();
            comboBoxEducationDirection.SelectedItem = null;

            StandartForm_Load();
        }

        protected override void LoadData()
        {
            if (tabPageRecords.Controls.Count == 0)
            {
                var control = Container.Resolve<ControlStudentOrderBlockStudent>();
                control.Dock = DockStyle.Fill;
                tabPageRecords.Controls.Add(control);
            }
            (tabPageRecords.Controls[0] as ControlStudentOrderBlockStudent).LoadData(_id.Value, null);

            var result = _service.GetStudentOrderBlock(new StudentOrderBlockGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            comboBoxStudentOrder.SelectedValue = entity.StudentOrderId;
            comboBoxStudentOrderType.SelectedIndex = comboBoxStudentOrderType.Items.IndexOf(entity.StudentOrderType);
            if (entity.EducationDirectionId.HasValue)
            {
                comboBoxEducationDirection.SelectedValue = entity.EducationDirectionId;
            }
        }

        private bool CheckFill()
        {
            if (comboBoxStudentOrder.SelectedValue == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(comboBoxStudentOrderType.Text))
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
                    result = _service.CreateStudentOrderBlock(new StudentOrderBlockSetBindingModel
                    {
                        StudentOrderId = new Guid(comboBoxStudentOrder.SelectedValue.ToString()),
                        StudentOrderType = comboBoxStudentOrderType.Text,
                        EducationDirectionId = comboBoxEducationDirection.SelectedValue != null ? new Guid(comboBoxEducationDirection.SelectedValue.ToString()) : (Guid?)null
                    });
                }
                else
                {
                    result = _service.UpdateStudentOrderBlock(new StudentOrderBlockSetBindingModel
                    {
                        Id = _id.Value,
                        StudentOrderId = new Guid(comboBoxStudentOrder.SelectedValue.ToString()),
                        StudentOrderType = comboBoxStudentOrderType.Text,
                        EducationDirectionId = comboBoxEducationDirection.SelectedValue != null ? new Guid(comboBoxEducationDirection.SelectedValue.ToString()) : (Guid?)null
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