using System;
using System.Windows.Forms;
using DepartmentService.IServices;
using DepartmentService.BindingModels;

namespace DepartmentDesktop.Views.EducationalProcess.Classroom
{
    public partial class ClassroomControl : UserControl
    {
        private readonly IClassroomService _service;

        public ClassroomControl(IClassroomService service)
        {
            InitializeComponent();
            _service = service;
        }

        public void LoadData()
        {
            var list = _service.GetClassrooms();
            if (list == null)
            {
                MessageBox.Show("Список пуст!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            dataGridViewList.DataSource = list;
            if (dataGridViewList.Columns.Count > 0)
            {
                dataGridViewList.Columns[0].HeaderText = "Аудитория";
                dataGridViewList.Columns[0].Width = 150;
                dataGridViewList.Columns[1].HeaderText = "Тип";
                dataGridViewList.Columns[1].Width = 200;
                dataGridViewList.Columns[2].HeaderText = "Вместимость";
                dataGridViewList.Columns[2].Width = 150;
            }
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            var form = new ClassroomForm(_service);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void toolStripButtonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewList.SelectedRows.Count == 1)
            {
                string id = Convert.ToString(dataGridViewList.SelectedRows[0].Cells[0].Value);
                var form = new ClassroomForm(_service, id);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void toolStripButtonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewList.SelectedRows.Count == 1)
            {
                string id = Convert.ToString(dataGridViewList.SelectedRows[0].Cells[0].Value);
                var res = _service.DeleteClassroom(new ClassroomGetBindingModel { Id = id });
                if (res.Succeeded)
                {
                    LoadData();
                }
                else
                {
                    MessageBox.Show("При сохранении возникла ошибка: " + res.Errors["error"], "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
