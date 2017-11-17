using DepartmentDesktop.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DepartmentDesktop.Controllers
{
	public partial class StandartControl : UserControl
	{
		public StandartControl()
		{
			InitializeComponent();
		}

		public void Configurate(List<ColumnConfig> columns, List<string> showToolStripButton)
		{
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

			var elemets = toolStripMenu.Items;
			foreach (ToolStripItem button in toolStripMenu.Items)
			{
				if (showToolStripButton.Contains(button.Name))
				{
					button.Visible = false;
					switch (button.Name)
					{
						case "toolStripButtonUpd":
							toolStripSeparator1.Visible = false;
							break;
						case "toolStripButtonDel":
							toolStripSeparator2.Visible = false;
							break;
						case "toolStripButtonRef":
							toolStripSeparator3.Visible = false;
							break;
						case "toolStripDropDownButtonMoves":
							toolStripSeparator4.Visible = false;
							break;
					}
				}
			}
		}

		public void ToolStripButtonAddEventClickAddEvent(EventHandler ev)
		{
			toolStripButtonAdd.Click += ev;
		}

		public void ToolStripButtonUpdEventClickAddEvent(EventHandler ev)
		{
			toolStripButtonUpd.Click += ev;
		}

		public void ToolStripButtonDelEventClickAddEvent(EventHandler ev)
		{
			toolStripButtonDel.Click += ev;
		}

		public void ToolStripButtonRefEventClickAddEvent(EventHandler ev)
		{
			toolStripButtonRef.Click += ev;
		}

		public void DataGridViewListEventKeyDownAddEvent(KeyEventHandler ev)
		{
			dataGridViewList.KeyDown += ev;
		}

		public void DataGridViewListEventCellDoubleClickAddEvent(DataGridViewCellEventHandler ev)
		{
			dataGridViewList.CellDoubleClick += ev;
		}

		public DataGridViewSelectedRowCollection GetDataGridViewSelectedRows { get { return dataGridViewList.SelectedRows; } }

		public DataGridViewRowCollection GetDataGridViewRows { get { return dataGridViewList.Rows; } }
	}
}
