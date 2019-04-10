﻿using ControlsAndForms.Messangers;
using ControlsAndForms.Models;
using ScheduleInterfaces.BindingModels;
using ScheduleInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;

namespace ScheduleControlsAndForms.StreamingLesson
{
    public partial class ControlStreamingLesson : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IStreamingLessonService _service;

		public ControlStreamingLesson(IStreamingLessonService service)
		{
			InitializeComponent();
			_service = service;

			List<ColumnConfig> columns = new List<ColumnConfig>
			{
				new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
				new ColumnConfig { Name = "IncomingGroups", Title = "Список групп", Width = 400, Visible = true },
				new ColumnConfig { Name = "StreamName", Title = "Описание", Width = 100, Visible = true }
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
			var result = _service.GetStreamingLessons(new StreamingLessonGetBindingModel { PageNumber = pageNumber, PageSize = pageSize });
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
					res.IncomingGroups,
					res.StreamName
				);
            }
            return result.Result.MaxCount;
        }

		private void AddRecord()
		{
            var form = Container.Resolve<FormStreamingLesson>(
                new ParameterOverrides
                {
                    { "id", Guid.Empty }
                }
                .OnType<FormStreamingLesson>());
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
                var form = Container.Resolve<FormStreamingLesson>(
                    new ParameterOverrides
                    {
                        { "id", id }
                    }
                    .OnType<FormStreamingLesson>());
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
                        var result = _service.DeleteStreamingLesson(new StreamingLessonGetBindingModel { Id = id });
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