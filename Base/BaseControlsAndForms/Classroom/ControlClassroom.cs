using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using ControlsAndForms.Messangers;
using ControlsAndForms.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;

namespace BaseControlsAndForms.Classroom
{
    public partial class ControlClassroom : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IClassroomService _service;

		public ControlClassroom(IClassroomService service)
		{
			InitializeComponent();
			_service = service;
			
			List<ColumnConfig> columns = new List<ColumnConfig>
			{
				new ColumnConfig { Name = "Id", Title = "Id", Width = 150, Visible = false },
                new ColumnConfig { Name = "Number", Title = "Аудитория", Width = 150, Visible = true },
                new ColumnConfig { Name = "ClassroomType", Title = "Тип", Width = 100, Visible = true },
				new ColumnConfig { Name = "Capacity", Title = "Вместимость", Width = 150, Visible = true },
                new ColumnConfig { Name = "NotUseInSchedule", Title = "Вне расписания", Width = 150, Visible = true }
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
			var result = _service.GetClassrooms(new ClassroomGetBindingModel { PageNumber = pageNumber, PageSize = pageSize });
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
                    res.Number,
                    res.ClassroomType,
					res.Capacity,
                    (res.NotUseInSchedule ? "Да" : "Нет")
				);
			}
			return result.Result.MaxCount;
		}

		private void AddRecord()
		{
            var form = Container.Resolve<FormClassroom>(
                new ParameterOverrides
                {
                    { "id", Guid.Empty }
                }
                .OnType<FormClassroom>());
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
                var form = Container.Resolve<FormClassroom>(
                    new ParameterOverrides
                    {
                        { "id", id }
                    }
                    .OnType<FormClassroom>());
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
                        var result = _service.DeleteClassroom(new ClassroomGetBindingModel { Id = id });
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