using BaseControlsAndForms.StudentOrderBlock;
using BaseControlsAndForms.StudentOrderBlockStudent;
using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using Enums;
using System;
using System.Windows.Forms;
using Tools;
using Unity;

namespace BaseControlsAndForms.StudentOrder
{
    public partial class FormStudentOrder : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IStudentOrderService _service;

        public FormStudentOrder(IStudentOrderService service, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
        }

        private void FormStudentOrder_Load(object sender, EventArgs e)
        {
            foreach (var elem in Enum.GetValues(typeof(StudentOrderType)))
            {
                comboBoxStudentOrderType.Items.Add(elem.ToString());
            }
            comboBoxStudentOrderType.SelectedIndex = 0;

            StandartForm_Load(sender, e);
        }

        protected override void LoadData()
        {
            if (tabPageRecords.Controls.Count == 0)
            {
                var control = Container.Resolve<ControlStudentOrderBlock>();
                control.Dock = DockStyle.Fill;
                tabPageRecords.Controls.Add(control);
            }
            (tabPageRecords.Controls[0] as ControlStudentOrderBlock).LoadData(_id.Value);

            if (tabPageStudents.Controls.Count == 0)
            {
                var control = Container.Resolve<ControlStudentOrderBlockStudent>();
                control.Dock = DockStyle.Fill;
                tabPageStudents.Controls.Add(control);
            }
            (tabPageStudents.Controls[0] as ControlStudentOrderBlockStudent).LoadData(null, _id.Value);

            var result = _service.GetStudentOrder(new StudentOrderGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            textBoxOrderNumber.Text = entity.OrderNumber;
            dateTimePickerOrderDate.Value = entity.OrderDate;
            comboBoxStudentOrderType.SelectedIndex = comboBoxStudentOrderType.Items.IndexOf(entity.StudentOrderType);
        }

        private bool CheckFill()
        {
            if (string.IsNullOrEmpty(textBoxOrderNumber.Text))
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
                    result = _service.CreateStudentOrder(new StudentOrderSetBindingModel
                    {
                        OrderNumber = textBoxOrderNumber.Text,
                        OrderDate = dateTimePickerOrderDate.Value,
                        StudentOrderType = comboBoxStudentOrderType.Text
                    });
                }
                else
                {
                    result = _service.UpdateStudentOrder(new StudentOrderSetBindingModel
                    {
                        Id = _id.Value,
                        OrderNumber = textBoxOrderNumber.Text,
                        OrderDate = dateTimePickerOrderDate.Value,
                        StudentOrderType = comboBoxStudentOrderType.Text
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