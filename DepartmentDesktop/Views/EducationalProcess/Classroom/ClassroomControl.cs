using System;
using System.Windows.Forms;
using DepartmentService.IServices;
using DepartmentService.BindingModels;
using System.Collections.Generic;
using DepartmentDesktop.Models;

namespace DepartmentDesktop.Views.EducationalProcess.Classroom
{
	public partial class ClassroomControl : UserControl
	{
		private readonly IClassroomService _service;

		public ClassroomControl(IClassroomService service)
		{
			InitializeComponent();
			_service = service;
			
			List<ColumnConfig> columns = new List<ColumnConfig>
			{
				new ColumnConfig { Name = "Id", Title = "Аудитория", Width = 150, Visible = true },
				new ColumnConfig { Name = "ClassroomType", Title = "Тип", Width = 100, Visible = true },
				new ColumnConfig { Name = "Capacity", Title = "Вместимость", Width = 150, Visible = true }
			};

			List<string> hideToolStripButtons = new List<string> { "toolStripDropDownButtonMoves" };

			standartControl.Configurate(columns, hideToolStripButtons);

			standartControl.ToolStripButtonAddEventClickAddEvent((object sender, EventArgs e) => { AddRecord(); });
			standartControl.ToolStripButtonUpdEventClickAddEvent((object sender, EventArgs e) => { UpdRecord(); });
			standartControl.ToolStripButtonDelEventClickAddEvent((object sender, EventArgs e) => { DelRecord(); });
			standartControl.ToolStripButtonRefEventClickAddEvent((object sender, EventArgs e) => { LoadRecords(); });
			standartControl.DataGridViewListEventCellDoubleClickAddEvent((object sender, DataGridViewCellEventArgs e) => { UpdRecord(); });
			standartControl.DataGridViewListEventKeyDownAddEvent((object sender, KeyEventArgs e) => {
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
			});
		}

		public void LoadData()
		{
			LoadRecords();
		}

		private void LoadRecords()
		{
			var result = _service.GetClassrooms(new ClassroomGetBindingModel { });
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				return;
			}
			standartControl.GetDataGridViewRows.Clear();
			foreach (var res in result.Result.List)
			{
				standartControl.GetDataGridViewRows.Add(
					res.Id,
					res.ClassroomType,
					res.Capacity
				);
			}
		}

		private void AddRecord()
		{
			var form = new ClassroomForm(_service);
			if (form.ShowDialog() == DialogResult.OK)
			{
				LoadRecords();
			}
		}

		private void UpdRecord()
		{
			if (standartControl.GetDataGridViewSelectedRows.Count == 1)
			{
				string id = Convert.ToString(standartControl.GetDataGridViewSelectedRows[0].Cells[0].Value);
				var form = new ClassroomForm(_service, id);
				if (form.ShowDialog() == DialogResult.OK)
				{
					LoadRecords();
				}
			}
		}

		private void DelRecord()
		{
			if (standartControl.GetDataGridViewSelectedRows.Count > 0)
			{
				if (MessageBox.Show("Вы уверены, что хотите удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					for (int i = 0; i < standartControl.GetDataGridViewSelectedRows.Count; ++i)
					{
						string id = Convert.ToString(standartControl.GetDataGridViewSelectedRows[i].Cells[0].Value);
						var result = _service.DeleteClassroom(new ClassroomGetBindingModel { Id = id });
						if (!result.Succeeded)
						{
							Program.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
						}
					}
					LoadRecords();
				}
			}
		}
	}
}
