using System;
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
            var result = _service.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel());
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				return;
			}
			dataGridViewList.DataSource = result.Result;
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
							Program.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
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
