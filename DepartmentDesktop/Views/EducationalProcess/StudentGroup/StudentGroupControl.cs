using DepartmentDesktop.Models;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.EducationalProcess.StudentGroup
{
	public partial class StudentGroupControl : UserControl
	{
		private readonly IStudentGroupService _service;

		private readonly IStudentService _serviceS;

		private readonly IStudentMoveService _serviceSM;

		public StudentGroupControl(IStudentGroupService service, IStudentService serviceS, IStudentMoveService serviceSM)
		{
			InitializeComponent();
			_service = service;
			_serviceS = serviceS;
			_serviceSM = serviceSM;

			List<ColumnConfig> columns = new List<ColumnConfig>
			{
				new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
				new ColumnConfig { Name = "EducationDirectionCipher", Title = "Шифр", Width = 100, Visible = true },
				new ColumnConfig { Name = "GroupName", Title = "Группа", Width = 150, Visible = true },
				new ColumnConfig { Name = "Kurs", Title = "Курс", Width = 80, Visible = true },
				new ColumnConfig { Name = "CountStudents", Title = "Количество студентов", Width = 150, Visible = true },
				new ColumnConfig { Name = "Steward", Title = "Староста", Width = 200, Visible = true },
				new ColumnConfig { Name = "Curator", Title = "Куратор", Width = 200, Visible = true }
			};

			List<string> hideToolStripButtons = new List<string> { "toolStripDropDownButtonMoves" };

			standartControl.Configurate(columns, hideToolStripButtons, 20);

			standartControl.GetPageAddEvent(LoadRecords);
			standartControl.ToolStripButtonAddEventClickAddEvent((object sender, EventArgs e) => { AddRecord(); });
			standartControl.ToolStripButtonUpdEventClickAddEvent((object sender, EventArgs e) => { UpdRecord(); });
			standartControl.ToolStripButtonDelEventClickAddEvent((object sender, EventArgs e) => { DelRecord(); });
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
			standartControl.LoadPage();
		}

		private int LoadRecords(int pageNumber, int pageSize)
		{
			var result = _service.GetStudentGroups(new StudentGroupGetBindingModel { PageNumber = pageNumber, PageSize = pageSize });
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				return -1;
			}
			standartControl.GetDataGridViewRows.Clear();
			foreach (var res in result.Result.List)
			{
				standartControl.GetDataGridViewRows.Add(
					res.Id,
					res.EducationDirectionCipher,
					res.GroupName,
					Math.Log(res.Course, 2.0) + 1,
					res.CountStudents,
					res.Steward,
					res.Curator
				);
			}
			return result.Result.MaxCount;
		}

		private void AddRecord()
		{
			var form = new StudentGroupForm(_service, _serviceS, _serviceSM);
			if (form.ShowDialog() == DialogResult.OK)
			{
				standartControl.LoadPage();
			}
		}

		private void UpdRecord()
		{
			if (standartControl.GetDataGridViewSelectedRows.Count == 1)
			{
				long id = Convert.ToInt64(standartControl.GetDataGridViewSelectedRows[0].Cells[0].Value);
				var form = new StudentGroupForm(_service, _serviceS, _serviceSM, id);
				if (form.ShowDialog() == DialogResult.OK)
				{
					standartControl.LoadPage();
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
						long id = Convert.ToInt64(standartControl.GetDataGridViewSelectedRows[i].Cells[0].Value);
						var result = _service.DeleteStudentGroup(new StudentGroupGetBindingModel { Id = id });
						if (!result.Succeeded)
						{
							Program.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
						}
					}
					standartControl.LoadPage();
				}
			}
		}
	}
}
