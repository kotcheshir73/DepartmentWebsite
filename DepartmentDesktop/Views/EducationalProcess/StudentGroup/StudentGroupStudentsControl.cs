using System;
using System.Text;
using System.Windows.Forms;
using DepartmentService.IServices;
using DepartmentService.BindingModels;

namespace DepartmentDesktop.Views.EducationalProcess.StudentGroup
{
    public partial class StudentGroupStudentsControl : UserControl
    {
        private readonly IStudentGroupService _service;

		private readonly IStudentService _serviceS;

		private long _studentGroupId;

        public StudentGroupStudentsControl(IStudentGroupService service, IStudentService serviceS)
        {
            InitializeComponent();
            _service = service;
			_serviceS = serviceS;

		}

        public void LoadData(long studentGroupId)
        {
            _studentGroupId = studentGroupId;
            var result = _serviceS.GetStudents(new StudentGetBindingModel { StudentGroupId = studentGroupId });
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				return;
			}
			dataGridViewList.DataSource = result.Result;
			if (dataGridViewList.Columns.Count > 0)
            {
                dataGridViewList.Columns[0].HeaderText = "Номер зачетки";
                dataGridViewList.Columns[0].Width = 150;
                dataGridViewList.Columns[1].HeaderText = "Фамилия";
                dataGridViewList.Columns[1].Width = 150;
                dataGridViewList.Columns[2].HeaderText = "Имя";
                dataGridViewList.Columns[2].Width = 150;
                dataGridViewList.Columns[3].HeaderText = "Отчество";
                dataGridViewList.Columns[3].Width = 150;
                dataGridViewList.Columns[4].Visible = false;
                dataGridViewList.Columns[5].Visible = false;
                dataGridViewList.Columns[6].Visible = false;
                dataGridViewList.Columns[7].HeaderText = "Описание";
                dataGridViewList.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void toolStripButtonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewList.SelectedRows.Count == 1)
            {
                string id = dataGridViewList.SelectedRows[0].Cells[0].Value.ToString();
                var form = new Student.StudentForm(_serviceS, id);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData(_studentGroupId);
                }
            }
        }

        private void toolStripButtonRef_Click(object sender, EventArgs e)
        {
            LoadData(_studentGroupId);
        }

        private void loadListOfStudentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "doc|*.doc|docx|*.docx";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var result = _serviceS.LoadStudentsFromFile(new StudentLoadDocBindingModel { Id = _studentGroupId, FileName = dialog.FileName });
                if (result.Succeeded)
                {
                    LoadData(_studentGroupId);
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

		private void enrollmentStudentsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var form = new StudentGroupEnrollmentForm(_service, _serviceS, _studentGroupId);
			if(form.ShowDialog() == DialogResult.OK)
			{
				LoadData(_studentGroupId);
			}
		}

		private void transferStudentsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var form = new StudentGroupTransferForm(_service, _serviceS, _studentGroupId);
			if (form.ShowDialog() == DialogResult.OK)
			{
				LoadData(_studentGroupId);
			}
		}
	}
}
