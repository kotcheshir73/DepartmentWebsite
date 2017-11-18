using DepartmentDesktop.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DepartmentDesktop.Controllers
{
	public partial class StandartControl : UserControl
	{
		private event Func<int, int, int> _getPage;

		private int _currentPage = 1;

		private int _countElementsOnPage = 10;

		private int _countPages = 1;

		public StandartControl()
		{
			InitializeComponent();
			toolStripButtonRef.Click += (object sender, EventArgs e) => { LoadPage(); };
		}

		public void Configurate(List<ColumnConfig> columns, List<string> showToolStripButton, int? countElementsOnPage = null)
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

			if (countElementsOnPage.HasValue)
			{
				_countElementsOnPage = countElementsOnPage.Value;
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

		public void DataGridViewListEventKeyDownAddEvent(KeyEventHandler ev)
		{
			dataGridViewList.KeyDown += ev;
		}

		public void DataGridViewListEventCellDoubleClickAddEvent(DataGridViewCellEventHandler ev)
		{
			dataGridViewList.CellDoubleClick += ev;
		}

		public void GetPageAddEvenet(Func<int, int, int> ev)
		{
			_getPage += ev;
		}

		public DataGridViewSelectedRowCollection GetDataGridViewSelectedRows { get { return dataGridViewList.SelectedRows; } }

		public DataGridViewRowCollection GetDataGridViewRows { get { return dataGridViewList.Rows; } }

		public void LoadPage()
		{
			if (_getPage != null)
			{
				_countPages = _getPage(_currentPage - 1, _countElementsOnPage);
				toolStripTextBoxPage.Text = _currentPage.ToString();
				toolStripLabelCountPages.Text = string.Format("из {0}", _countPages);
			}
		}

		private void toolStripButtonBefore_Click(object sender, EventArgs e)
		{
			_currentPage--;
			LoadPage();
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
			_currentPage++;
			LoadPage();
		}

		private void toolStripTextBoxPage_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
			{
				if (int.TryParse(toolStripTextBoxPage.Text, out int tempPage))
				{
					_currentPage = tempPage;
					LoadPage();
				}
			}
		}
	}
}
