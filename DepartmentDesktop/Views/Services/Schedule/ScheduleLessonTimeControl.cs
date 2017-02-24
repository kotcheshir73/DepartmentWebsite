using System;
using System.Text;
using System.Windows.Forms;
using DepartmentService.IServices;
using DepartmentService.BindingModels;

namespace DepartmentDesktop.Views.Services.Schedule
{
    public partial class ScheduleLessonTimeControl : UserControl
    {
        private readonly IScheduleLessonTimeService _service;

        public ScheduleLessonTimeControl(IScheduleLessonTimeService service)
        {
            InitializeComponent();
            _service = service;
        }

        public void LoadData()
        {
            var list = _service.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel());
            if (list == null)
            {
                MessageBox.Show("Список пуст!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            dataGridViewList.DataSource = list;
            if (dataGridViewList.Columns.Count > 0)
            {
                dataGridViewList.Columns[0].Visible = false;
                dataGridViewList.Columns[1].Visible = false;
                dataGridViewList.Columns[2].HeaderText = "Название";
                dataGridViewList.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewList.Columns[3].HeaderText = "Дата начала";
                dataGridViewList.Columns[3].Width = 200;
                dataGridViewList.Columns[4].HeaderText = "Дата окончания";
                dataGridViewList.Columns[4].Width = 200;
                dataGridViewList.Columns[5].Visible = false;
                dataGridViewList.Columns[6].Visible = false;
            }
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            var form = new ScheduleLessonTimeForm(_service);
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
                var form = new ScheduleLessonTimeForm(_service, id);
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
                        var result = _service.DeleteScheduleLessonTime(new ScheduleLessonTimeGetBindingModel { Id = id });
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
