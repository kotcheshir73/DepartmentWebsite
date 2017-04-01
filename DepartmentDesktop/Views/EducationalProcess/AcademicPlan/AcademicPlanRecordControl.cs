using System;
using System.Windows.Forms;
using DepartmentService.IServices;
using DepartmentService.BindingModels;
using System.Collections.Generic;
using DepartmentDesktop.Models;

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

			List<ColumnConfig> columns = new List<ColumnConfig>
			{
				new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
				new ColumnConfig { Name = "Disciplne", Title = "Дисциплина", Width = 200, Visible = true },
				new ColumnConfig { Name = "KindOfLoad", Title = "Вид нагрузки", Width = 150, Visible = true },
				new ColumnConfig { Name = "Semester", Title = "Семестр", Width = 150, Visible = true },
				new ColumnConfig { Name = "Hours", Title = "Часы", Width = 100, Visible = true }
			};
			dataGridViewList.Columns.Clear();
			foreach (var column in columns)
			{
				dataGridViewList.Columns.Add(new DataGridViewTextBoxColumn
				{
					HeaderText = column.Title,
					Name = string.Format("Column{0}", column.Name),
					ReadOnly = true,
					Visible = column.Visible,
					Width = column.Width.HasValue ? column.Width.Value : 0,
					AutoSizeMode = column.Width.HasValue ? DataGridViewAutoSizeColumnMode.None : DataGridViewAutoSizeColumnMode.Fill
				});
			}
		}

		public void LoadData(long apId)
		{
			_apId = apId;
			LoadRecords();
		}

		private void LoadRecords()
		{
			var result = _service.GetAcademicPlanRecords(new AcademicPlanRecordGetBindingModel { AcademicPlanId = _apId });
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				return;
			}
			dataGridViewList.Rows.Clear();
			foreach (var res in result.Result)
			{
				dataGridViewList.Rows.Add(
					res.Id,
					res.Disciplne,
					res.KindOfLoad,
					res.Semester,
					res.Hours
				);
			}
		}

		private void AddRecord()
		{
			var form = new AcademicPlanRecordForm(_service, _apId);
			if (form.ShowDialog() == DialogResult.OK)
			{
				LoadRecords();
			}
		}

		private void UpdRecord()
		{
			if (dataGridViewList.SelectedRows.Count == 1)
			{
				long id = Convert.ToInt64(dataGridViewList.SelectedRows[0].Cells[0].Value);
				var form = new AcademicPlanRecordForm(_service, _apId, id);
				if (form.ShowDialog() == DialogResult.OK)
				{
					LoadRecords();
				}
			}
		}

		private void DelRecord()
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
					LoadRecords();
				}
			}
		}

		private void toolStripButtonAdd_Click(object sender, EventArgs e)
		{
			AddRecord();
		}

		private void toolStripButtonUpd_Click(object sender, EventArgs e)
		{
			UpdRecord();
		}

		private void toolStripButtonDel_Click(object sender, EventArgs e)
		{
			DelRecord();
		}

		private void toolStripButtonRef_Click(object sender, EventArgs e)
		{
			LoadRecords();
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

		private void dataGridViewList_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Insert:
					AddRecord();
					break;
				case Keys.Enter:
					UpdRecord();
					break;
				case Keys.Delete:
					DelRecord();
					break;
			}
		}

		private void dataGridViewList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			UpdRecord();
		}
	}
}
