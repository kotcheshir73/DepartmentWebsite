using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using ControlsAndForms.Messangers;
using ControlsAndForms.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;

namespace BaseControlsAndForms.Discipline
{
    public partial class ControlDiscipline : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineService _service;

        private readonly IProcess _processE;

        public ControlDiscipline(IDisciplineService service, IProcess processE)
		{
			InitializeComponent();
			_service = service;
            _processE = processE;

            List<ColumnConfig> columns = new List<ColumnConfig>
			{
				new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
				new ColumnConfig { Name = "DisciplineName", Title = "Название", Width = 600, Visible = true },
                new ColumnConfig { Name = "DisciplineShortName", Title = "Краткое название", Width = 200, Visible = true },
                new ColumnConfig { Name = "DisciplineBlockTitle", Title = "Блок", Width = 200, Visible = true },
                new ColumnConfig { Name = "DisciplineBlueAsteriskName", Title = "Синоним для синей звездочки", Width = 300, Visible = true }
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

		public void LoadData()
		{
			standartControl.LoadPage();
		}

		private int LoadRecords(int pageNumber, int pageSize)
		{
			var result = _service.GetDisciplines(new DisciplineGetBindingModel { PageNumber = pageNumber, PageSize = pageSize } );
			if (!result.Succeeded)
			{
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				return -1;
			}
			standartControl.GetDataGridViewRows.Clear();
			foreach (var res in result.Result.List)
			{
				standartControl.GetDataGridViewRows.Add(
					res.Id,
					res.DisciplineName,
                    res.DisciplineShortName,
                    res.DisciplineBlockTitle,
                    res.DisciplineBlueAsteriskName
				);
			}
			return result.Result.MaxCount;
		}

		private void AddRecord()
		{
            var form = Container.Resolve<FormDiscipline>(
                new ParameterOverrides
                {
                    { "id", Guid.Empty }
                }
                .OnType<FormDiscipline>());
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
                var form = Container.Resolve<FormDiscipline>(
                    new ParameterOverrides
                    {
                        { "id", id }
                    }
                    .OnType<FormDiscipline>());
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
                        var result = _service.DeleteDiscipline(new DisciplineGetBindingModel { Id = id });
						if (!result.Succeeded)
						{
                            ErrorMessanger.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
						}
					}
					standartControl.LoadPage();
				}
			}
		}
	}
}