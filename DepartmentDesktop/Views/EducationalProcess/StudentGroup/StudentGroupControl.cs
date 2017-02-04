using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DepartmentService.IServices;
using DepartmentService.BindingModels;

namespace DepartmentDesktop.Views.EducationalProcess.StudentGroup
{
    public partial class StudentGroupControl : UserControl
    {
        private readonly IStudentGroupService _service;

        public StudentGroupControl(IStudentGroupService service)
        {
            InitializeComponent();
            _service = service;
        }

        public void LoadData()
        {
            var list = _service.GetStudentGroups();
            if (list == null)
            {
                MessageBox.Show("Информация", "Список пуст!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            dataGridViewList.DataSource = list;
            if (dataGridViewList.Columns.Count > 0)
            {
                dataGridViewList.Columns[0].Visible = false;
                dataGridViewList.Columns[1].Visible = false;
                dataGridViewList.Columns[2].HeaderText = "Шифр";
                dataGridViewList.Columns[2].Width = 150;
                dataGridViewList.Columns[3].HeaderText = "Группа";
                dataGridViewList.Columns[3].Width = 150;
                dataGridViewList.Columns[4].HeaderText = "Курс";
                dataGridViewList.Columns[4].Width = 150;
                dataGridViewList.Columns[5].HeaderText = "Количество студентов";
                dataGridViewList.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            var form = new StudentGroupForm(_service);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void toolStripButtonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewList.SelectedRows.Count == 1)
            {
                long id = Convert.ToInt64(dataGridViewList.SelectedRows[0].Cells[0].Value);
                var form = new StudentGroupForm(_service, id);
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
                long id = Convert.ToInt64(dataGridViewList.SelectedRows[0].Cells[0].Value);
                var res = _service.DeleteStudentGroup(new StudentGroupGetBindingModel { Id = id });
                if (res.Succeeded)
                {
                    LoadData();
                }
                else
                {
                    MessageBox.Show("", "При сохранении возникла ошибка: " + res.Errors["error"], MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
