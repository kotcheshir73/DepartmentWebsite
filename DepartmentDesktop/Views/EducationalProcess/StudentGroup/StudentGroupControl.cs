using System;
using System.Windows.Forms;
using DepartmentService.IServices;
using DepartmentService.BindingModels;

namespace DepartmentDesktop.Views.EducationalProcess.StudentGroup
{
	public partial class StudentGroupControl : UserControl
	{
		private readonly IStudentGroupService _service;

		private readonly IStudentService _serviceS;

		public StudentGroupControl(IStudentGroupService service, IStudentService serviceS)
		{
			InitializeComponent();
			_service = service;
			_serviceS = serviceS;
		}

		public void LoadData()
		{
			var result = _service.GetStudentGroups();
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
				dataGridViewList.Columns[2].HeaderText = "Шифр";
				dataGridViewList.Columns[2].Width = 100;
				dataGridViewList.Columns[3].HeaderText = "Группа";
				dataGridViewList.Columns[3].Width = 100;
				dataGridViewList.Columns[4].HeaderText = "Курс";
				dataGridViewList.Columns[4].Width = 150;
				dataGridViewList.Columns[5].HeaderText = "Количество студентов";
				dataGridViewList.Columns[5].Width = 150;
				dataGridViewList.Columns[6].HeaderText = "План по студентам";
				dataGridViewList.Columns[6].Width = 150;
				dataGridViewList.Columns[7].HeaderText = "Количество подгрупп";
				dataGridViewList.Columns[7].Width = 150;
				dataGridViewList.Columns[8].Visible = false;
				dataGridViewList.Columns[9].Visible = false;
				dataGridViewList.Columns[10].HeaderText = "Староста";
				dataGridViewList.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
				dataGridViewList.Columns[11].HeaderText = "Куратор";
				dataGridViewList.Columns[11].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			}
		}

		private void toolStripButtonAdd_Click(object sender, EventArgs e)
		{
			var form = new StudentGroupForm(_service, _serviceS);
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
				var form = new StudentGroupForm(_service, _serviceS, id);
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
						var result = _service.DeleteStudentGroup(new StudentGroupGetBindingModel { Id = id });
						if (!result.Succeeded)
						{
							Program.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
						}
					}
					LoadData();
				}
			}
		}

		private void toolStripButtonRef_Click(object sender, EventArgs e)
		{
			LoadData();
		}
	}
}
