using ControlsAndForms.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ControlsAndForms.Controls
{
    public partial class StandartControl : UserControl
	{
		/// <summary>
		/// Событие, вызываемое при загрузке данных на datagrid
		/// </summary>
		private event Func<int, int, int> _getPage;

		/// <summary>
		/// Номер страницы, которая сейчас выведена в таблице
		/// </summary>
		private int _currentPage = 1;

		/// <summary>
		/// Максимальное количество элементов, выводимых на странице
		/// </summary>
		private int _countElementsOnPage = 20;

		/// <summary>
		/// Количество страниц
		/// </summary>
		private int _countPages = 1;

		public StandartControl()
		{
			InitializeComponent();
			toolStripButtonRef.Click += (object sender, EventArgs e) => { LoadPage(); };
		}

		/// <summary>
		/// Конфингуратор контрола
		/// </summary>
		/// <param name="columns">Конфигурация колонок для datagrid</param>
		/// <param name="showToolStripButton">Список названий кнопок, которые надо скрыть</param>
		/// <param name="countElementsOnPage">Максимальное количество элементов, выводимых на странице (по умолчанию - 10)</param>
		public void Configurate(List<ColumnConfig> columns, List<string> showToolStripButton, int? countElementsOnPage = null, Dictionary<string, string> controlOnMoveElem = null)
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
					Width = column.Width ?? 0,
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

            if (controlOnMoveElem != null)
            {
                foreach (var elem in controlOnMoveElem)
                {
                    ToolStripMenuItem item = new ToolStripMenuItem { Text = elem.Value, Name = elem.Key };
                    toolStripDropDownButtonMoves.DropDownItems.Add(item);
                }
            }
		}

		/// <summary>
		/// Привязка к событию загрузки данных на datagrid
		/// </summary>
		/// <param name="ev"></param>
		public void GetPageAddEvent(Func<int, int, int> ev)
		{
			_getPage += ev;
		}

		/// <summary>
		/// Привязка обработчика для добавления нового элемента
		/// </summary>
		/// <param name="ev"></param>
		public void ToolStripButtonAddEventClickAddEvent(EventHandler ev)
		{
			toolStripButtonAdd.Click += ev;
		}

		/// <summary>
		/// Привязка обработчика для редактирования элемента
		/// </summary>
		/// <param name="ev"></param>
		public void ToolStripButtonUpdEventClickAddEvent(EventHandler ev)
		{
			toolStripButtonUpd.Click += ev;
		}

		/// <summary>
		/// Привязка обработчика для удаления элемента
		/// </summary>
		/// <param name="ev"></param>
		public void ToolStripButtonDelEventClickAddEvent(EventHandler ev)
		{
			toolStripButtonDel.Click += ev;
		}

        public void ToolStripButtonMoveEventClickAddEvent(string controlElementName, EventHandler ev)
        {
            ToolStripItemCollection controls = toolStripDropDownButtonMoves.DropDownItems;
            foreach (ToolStripItem control in controls)
            {
                if (control.Name == controlElementName)
                {
                    control.Click += ev;
                }
            }
        }

		/// <summary>
		/// Привязка обработчика нажатия клавиш на datagrid
		/// </summary>
		/// <param name="ev"></param>
		public void DataGridViewListEventKeyDownAddEvent(KeyEventHandler ev)
		{
			dataGridViewList.KeyDown += ev;
		}

		/// <summary>
		/// Привязка обработчика двойного клика мыши по строке datagrid
		/// </summary>
		/// <param name="ev"></param>
		public void DataGridViewListEventCellDoubleClickAddEvent(DataGridViewCellEventHandler ev)
		{
			dataGridViewList.CellDoubleClick += ev;
		}

		/// <summary>
		/// Вызов события загрузки данных на datagrid
		/// </summary>
		public void LoadPage()
		{
			if (_getPage != null)
			{
				_countPages = _getPage(_currentPage - 1, _countElementsOnPage);
				toolStripTextBoxPage.Text = _currentPage.ToString();
				toolStripLabelCountPages.Text = string.Format("из {0}", _countPages);
			}
		}

		/// <summary>
		/// Получение списка выбранныз строк (для редактирования или удаления)
		/// </summary>
		public DataGridViewSelectedRowCollection GetDataGridViewSelectedRows { get { return dataGridViewList.SelectedRows; } }

		/// <summary>
		/// Получение доступа к строка datagrid для заполнения
		/// </summary>
		public DataGridViewRowCollection GetDataGridViewRows { get { return dataGridViewList.Rows; } }

		/// <summary>
		/// Переход на страницу назад
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ToolStripButtonBefore_Click(object sender, EventArgs e)
		{
			_currentPage--;
			LoadPage();
		}

		/// <summary>
		/// Переход на страницу вперед
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ToolStripButtonNext_Click(object sender, EventArgs e)
		{
			_currentPage++;
			LoadPage();
		}

		/// <summary>
		/// Ввод номера страницы для отображения
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ToolStripTextBoxPage_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
			{
                int tempPage = 0;

                if (int.TryParse(toolStripTextBoxPage.Text, out tempPage))
				{
					_currentPage = tempPage;
					LoadPage();
				}
			}
		}
	}
}
