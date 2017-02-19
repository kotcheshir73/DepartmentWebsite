using System;
using System.Windows.Forms;
using DepartmentService.IServices;
using DepartmentService.BindingModels;

namespace DepartmentDesktop.Views.EducationalProcess.SeasonDates
{
    public partial class SeasonDatesControl : UserControl
    {
        private readonly ISeasonDatesService _service;

        public SeasonDatesControl(ISeasonDatesService service)
        {
            InitializeComponent();
            _service = service;
        }

        public void LoadData()
        {
            var list = _service.GetSeasonDaties();
            if (list == null)
            {
                MessageBox.Show("Список пуст!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            dataGridViewList.DataSource = list;
            if (dataGridViewList.Columns.Count > 0)
            {
                dataGridViewList.Columns[0].Visible = false;
                dataGridViewList.Columns[1].HeaderText = "Название";
                dataGridViewList.Columns[1].Width = 150;
                dataGridViewList.Columns[2].HeaderText = "Нач. сем.";
                dataGridViewList.Columns[2].Width = 150;
                dataGridViewList.Columns[3].HeaderText = "Кон. сем.";
                dataGridViewList.Columns[3].Width = 150;
                dataGridViewList.Columns[4].HeaderText = "Нач. зач.";
                dataGridViewList.Columns[4].Width = 150;
                dataGridViewList.Columns[5].HeaderText = "Кон. зач.";
                dataGridViewList.Columns[5].Width = 150;
                dataGridViewList.Columns[6].HeaderText = "Нач. экз.";
                dataGridViewList.Columns[6].Width = 150;
                dataGridViewList.Columns[7].HeaderText = "Кон. экз.";
                dataGridViewList.Columns[7].Width = 150;
                dataGridViewList.Columns[8].HeaderText = "Нач. пр.";
                dataGridViewList.Columns[8].Width = 150;
                dataGridViewList.Columns[9].HeaderText = "Кон. пр.";
                dataGridViewList.Columns[9].Width = 150;
            }
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            var form = new SeasonDatesForm(_service);
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
                var form = new SeasonDatesForm(_service, id);
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
                        var res = _service.DeleteSeasonDates(new SeasonDatesGetBindingModel { Id = id });
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
            }
        }

        private void toolStripButtonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
