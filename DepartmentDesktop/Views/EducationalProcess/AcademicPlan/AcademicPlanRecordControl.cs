using System;
using System.Windows.Forms;
using DepartmentService.IServices;
using DepartmentService.BindingModels;

namespace DepartmentDesktop.Views.EducationalProcess.AcademicPlan
{
	public partial class AcademicPlanRecordControl : UserControl
	{
		private readonly IAcademicPlanRecordService _service;

		private long _apId;

		public AcademicPlanRecordControl(IAcademicPlanRecordService service)
		{
			InitializeComponent();
			_service = service;
		}

		public void LoadData(long apId)
		{
			_apId = apId;
			var result = _service.GetAcademicPlanRecords(new AcademicPlanRecordGetBindingModel { AcademicPlanId = _apId });
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
				dataGridViewList.Columns[2].Visible = false;
				dataGridViewList.Columns[3].HeaderText = "Дисциплина";
				dataGridViewList.Columns[3].Width = 200;
				dataGridViewList.Columns[4].Visible = false;
				dataGridViewList.Columns[5].HeaderText = "Вид нагрузки";
				dataGridViewList.Columns[5].Width = 150;
				dataGridViewList.Columns[6].HeaderText = "Семестр";
				dataGridViewList.Columns[6].Width = 150;
				dataGridViewList.Columns[7].HeaderText = "Часы";
				dataGridViewList.Columns[7].Width = 100;
			}
		}

		private void toolStripButtonAdd_Click(object sender, EventArgs e)
		{
			var form = new AcademicPlanRecordForm(_service, _apId);
			if (form.ShowDialog() == DialogResult.OK)
			{
				LoadData(_apId);
			}
		}

		private void toolStripButtonUpd_Click(object sender, EventArgs e)
		{
			if (dataGridViewList.SelectedRows.Count == 1)
			{
				long id = Convert.ToInt64(dataGridViewList.SelectedRows[0].Cells[0].Value);
				var form = new AcademicPlanRecordForm(_service, _apId, id);
				if (form.ShowDialog() == DialogResult.OK)
				{
					LoadData(_apId);
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
						var result = _service.DeleteAcademicPlanRecord(new AcademicPlanRecordGetBindingModel { Id = id });
						if (!result.Succeeded)
						{
							Program.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
						}
					}
					LoadData(_apId);
				}
			}
		}

		private void toolStripButtonRef_Click(object sender, EventArgs e)
		{
			LoadData(_apId);
		}

		private void loadFromXMLToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "xml|*.xml";
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				var result = _service.LoadFromXMLAcademicPlanRecord(new AcademicPlanRecordLoadFromXMLBindingModel
				{
					Id = _apId,
					FileName = dialog.FileName
				});
				if (result.Succeeded)
				{
					LoadData(_apId);
				}
				else
				{
					Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				}
			}
		}
	}
}
