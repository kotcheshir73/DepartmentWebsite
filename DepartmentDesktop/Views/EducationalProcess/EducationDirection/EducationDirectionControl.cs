using System;
using System.Windows.Forms;
using DepartmentService.IServices;
using DepartmentService.BindingModels;
using System.Text;

namespace DepartmentDesktop.Views.EducationalProcess.EducationDirection
{
    public partial class EducationDirectionControl : UserControl
    {
        private readonly IEducationDirectionService _service;

        public EducationDirectionControl(IEducationDirectionService service)
        {
            InitializeComponent();
            _service = service;
        }

        public void LoadData()
        {
            var list = _service.GetEducationDirections();
            if (list == null)
            {
                MessageBox.Show("Список пуст!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            dataGridViewList.DataSource = list;
            if (dataGridViewList.Columns.Count > 0)
            {
                dataGridViewList.Columns[0].Visible = false;
                dataGridViewList.Columns[1].HeaderText = "Шифр";
                dataGridViewList.Columns[1].Width = 100;
                dataGridViewList.Columns[2].HeaderText = "Название";
                dataGridViewList.Columns[2].Width = 150;
                dataGridViewList.Columns[3].HeaderText = "Описание";
                dataGridViewList.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            var form = new EducationDirectionForm(_service);
            if(form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void toolStripButtonUpd_Click(object sender, EventArgs e)
        {
            if(dataGridViewList.SelectedRows.Count == 1)
            {
                long id = Convert.ToInt64(dataGridViewList.SelectedRows[0].Cells[0].Value);
                var form = new EducationDirectionForm(_service, id);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void toolStripButtonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewList.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = 0; i < dataGridViewList.SelectedRows.Count; ++i)
                    {
                        long id = Convert.ToInt64(dataGridViewList.SelectedRows[i].Cells[0].Value);
                        var result = _service.DeleteEducationDirection(new EducationDirectionGetBindingModel { Id = id });
                        if (result.Succeeded)
                        {
                            LoadData();
                        }
                        else
                        {
                            StringBuilder strRes = new StringBuilder();
                            foreach (var err in result.Errors)
                            {
                                strRes.Append(string.Format("{0} : {1}\r\n", err.Key, err.Value));
                            }
                            MessageBox.Show("При сохранении возникла ошибка: " + strRes.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void toolStripButtonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
