using DepartmentDesktop.Views.EducationalProcess.Student;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using Microsoft.Practices.Unity;
using System;
using System.Text;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.EducationalProcess.StudentGroup
{
    public partial class StudentGroupStudentsControl : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IStudentGroupService _service;

		private readonly IStudentService _serviceS;

		private readonly IStudentMoveService _serviceSM;

		private Guid _studentGroupId;

        public StudentGroupStudentsControl(IStudentGroupService service, IStudentService serviceS, IStudentMoveService serviceSM)
        {
            InitializeComponent();
            _service = service;
			_serviceS = serviceS;
			_serviceSM = serviceSM;
		}

        public void LoadData(Guid studentGroupId)
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
                dataGridViewList.Columns[0].Visible = false;
                dataGridViewList.Columns[1].HeaderText = "Номер зачетки";
                dataGridViewList.Columns[1].Width = 150;
                dataGridViewList.Columns[2].HeaderText = "Фамилия";
                dataGridViewList.Columns[2].Width = 150;
                dataGridViewList.Columns[3].HeaderText = "Имя";
                dataGridViewList.Columns[3].Width = 150;
                dataGridViewList.Columns[4].HeaderText = "Отчество";
                dataGridViewList.Columns[4].Width = 150;
                dataGridViewList.Columns[5].Visible = false;
                dataGridViewList.Columns[6].Visible = false;
                dataGridViewList.Columns[7].Visible = false;
                dataGridViewList.Columns[8].HeaderText = "Описание";
                dataGridViewList.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void toolStripButtonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewList.SelectedRows.Count == 1)
            {
                Guid id = new Guid(dataGridViewList.SelectedRows[0].Cells[0].Value.ToString());
                var form = Container.Resolve<StudentForm>(
                    new ParameterOverrides
                    {
                        { "id", id }
                    }
                    .OnType<StudentForm>());
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
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "doc|*.doc|docx|*.docx"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var result = _serviceSM.LoadStudentsFromFile(new StudentLoadDocBindingModel { Id = _studentGroupId, FileName = dialog.FileName });
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
            var form = Container.Resolve<StudentGroupEnrollmentForm>(
                new ParameterOverrides
                {
                    { "id", _studentGroupId }
                }
                .OnType<StudentGroupEnrollmentForm>());
            if (form.ShowDialog() == DialogResult.OK)
			{
				LoadData(_studentGroupId);
			}
		}

		private void transferStudentsToolStripMenuItem_Click(object sender, EventArgs e)
		{
            var form = Container.Resolve<StudentGroupTransferForm>(
                new ParameterOverrides
                {
                    { "id", _studentGroupId }
                }
                .OnType<StudentGroupTransferForm>());
            if (form.ShowDialog() == DialogResult.OK)
			{
				LoadData(_studentGroupId);
			}
		}

		private void deductionStudentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<StudentGroupDeductionForm>(
                new ParameterOverrides
                {
                    { "id", _studentGroupId }
                }
                .OnType<StudentGroupDeductionForm>());
			if (form.ShowDialog() == DialogResult.OK)
			{
				LoadData(_studentGroupId);
			}
		}

		private void toAcademStudentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<StudentGroupToAcademForm>(
                new ParameterOverrides
                {
                    { "id", _studentGroupId }
                }
                .OnType<StudentGroupToAcademForm>());
			if (form.ShowDialog() == DialogResult.OK)
			{
				LoadData(_studentGroupId);
			}
		}
	}
}
