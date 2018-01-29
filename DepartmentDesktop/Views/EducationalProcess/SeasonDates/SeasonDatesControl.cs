using System;
using System.Windows.Forms;
using DepartmentService.IServices;
using DepartmentService.BindingModels;
using System.Collections.Generic;
using DepartmentDesktop.Models;

namespace DepartmentDesktop.Views.EducationalProcess.SeasonDates
{
	public partial class SeasonDatesControl : UserControl
	{
		private readonly ISeasonDatesService _service;

        private Guid _ayId;

        public SeasonDatesControl(ISeasonDatesService service)
		{
			InitializeComponent();
			_service = service;

			List<ColumnConfig> columns = new List<ColumnConfig>
			{
				new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
				new ColumnConfig { Name = "Title", Title = "Название", Width = 200, Visible = true },
				new ColumnConfig { Name = "DateBeginFirstHalf", Title = "Нач. 1 периода", Width = 150, Visible = true },
				new ColumnConfig { Name = "DateEndFirstHalf", Title = "Кон. 1 периода", Width = 150, Visible = true },
                new ColumnConfig { Name = "DateBeginSecondHalf", Title = "Нач. 2 периода", Width = 150, Visible = true },
                new ColumnConfig { Name = "DateEndSecondHalf", Title = "Кон. 2 периода", Width = 150, Visible = true },
                new ColumnConfig { Name = "DateBeginOffset", Title = "Нач. зач.", Width = 150, Visible = true },
				new ColumnConfig { Name = "DateEndOffset", Title = "Кон. зач.", Width = 150, Visible = true },
				new ColumnConfig { Name = "DateBeginExamination", Title = "Нач. экз.", Width = 150, Visible = true },
				new ColumnConfig { Name = "DateEndExamination", Title = "Кон. экз.", Width = 150, Visible = true },
				new ColumnConfig { Name = "DateBeginPractice", Title = "Нач. пр.", Width = 150, Visible = true },
				new ColumnConfig { Name = "DateEndPractice", Title = "Кон. пр.", Width = 150, Visible = true }
			};

			List<string> hideToolStripButtons = new List<string> { "toolStripDropDownButtonMoves" };

			standartControl.Configurate(columns, hideToolStripButtons);

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

		public void LoadData(Guid ayId)
		{
            _ayId = ayId;
            standartControl.LoadPage();
		}

		private int LoadRecords(int pageNumber, int pageSize)
		{
			var result = _service.GetSeasonDaties(new SeasonDatesGetBindingModel { AcademicYearId = _ayId, PageNumber = pageNumber, PageSize = pageSize });
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
					res.Title,
					res.DateBeginFirstHalfSemester,
					res.DateEndFirstHalfSemester,
                    res.DateBeginSecondHalfSemester,
                    res.DateEndSecondHalfSemester,
					res.DateBeginOffset,
					res.DateEndOffset,
					res.DateBeginExamination,
					res.DateEndExamination,
					res.DateBeginPractice,
					res.DateEndPractice
				);
			}
			return result.Result.MaxCount;
		}

		private void AddRecord()
		{
			var form = new SeasonDatesForm(_service, ayId: _ayId);
			if (form.ShowDialog() == DialogResult.OK)
			{
				standartControl.LoadPage();
			}
		}

		private void UpdRecord()
		{
			if (standartControl.GetDataGridViewSelectedRows.Count == 1)
			{
                Guid id = new Guid(standartControl.GetDataGridViewSelectedRows[0].Cells[0].Value.ToString());
                var form = new SeasonDatesForm(_service, ayId: _ayId, id: id);
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
                        Guid id = new Guid(standartControl.GetDataGridViewSelectedRows[i].Cells[0].Value.ToString());
                        var result = _service.DeleteSeasonDates(new SeasonDatesGetBindingModel { Id = id });
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
