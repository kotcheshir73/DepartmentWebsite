using BaseControlsAndForms.StudentOrder;
using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using ControlsAndForms.Messangers;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;

namespace BaseControlsAndForms.Services
{
    public partial class FormStudentOrdersShow : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IProcess _process;

        private Guid _id;

        public FormStudentOrdersShow(IProcess process, Guid id)
        {
            InitializeComponent();
            _process = process;
            _id = id;
        }

        private void FormStudentOrdersShow_Load(object sender, EventArgs e)
        {
            var result = _process.StudentOrderShow(new StudentOrdersShowBindingModel { Id = _id });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            dataGridView.Rows.Clear();

            foreach(var elem in result.Result)
            {
                dataGridView.Rows.Add(new object[] { elem.Id, elem.OrderNumber, elem.OrderDate.ToLongDateString(), elem.StudentOrderType, elem.StudentOrderBlockType, elem.StudentGromFrom, elem.StudentGroupTo });
            }
        }

        private void DataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(dataGridView.SelectedRows.Count > 0)
            {
                foreach(DataGridViewRow row in dataGridView.SelectedRows)
                {
                    Guid id = new Guid(row.Cells[0].Value.ToString());
                    var form = Container.Resolve<FormStudentOrder>(
                        new ParameterOverrides
                        {
                            { "id", id }
                        }
                        .OnType<FormStudentOrder>());
                    form.Show();
                }
            }
        }
    }
}