using System;
using System.Windows.Forms;
using DepartmentService.IServices;
using DepartmentService.BindingModels;
using System.Text;

namespace DepartmentDesktop.Views.EducationalProcess.StreamingLesson
{
    public partial class StreamingLessonControl : UserControl
    {
        private readonly IStreamingLessonService _service;

        public StreamingLessonControl(IStreamingLessonService service)
        {
            InitializeComponent();
            _service = service;
        }

        public void LoadData()
        {
            var list = _service.GetStreamingLessons();
            if (list == null)
            {
                MessageBox.Show("Список пуст!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            dataGridViewList.DataSource = list;
            if (dataGridViewList.Columns.Count > 0)
            {
                dataGridViewList.Columns[0].Visible = false;
                dataGridViewList.Columns[1].HeaderText = "Список групп";
                dataGridViewList.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewList.Columns[2].HeaderText = "Описание";
                dataGridViewList.Columns[2].Width = 150;
            }
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            var form = new StreamingLessonForm(_service);
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
                var form = new StreamingLessonForm(_service, id);
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
                        var result = _service.DeleteStreamingLesson(new StreamingLessonGetBindingModel { Id = id });
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
