using ControlsAndForms.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ControlsAndForms.Controls
{
    public partial class StandartSearchControl : UserControl
    {
        /// <summary>
        /// Событие, вызываемое при загрузке данных на datagrid
        /// </summary>
        private event Action _getPage;

        /// <summary>
        /// Событие, вызываемое при выборе элемента
        /// </summary>
        private event Action<Guid> _selectElement;

        /// <summary>
        /// Словарь со словами для поиска
        /// </summary>
        private Dictionary<string, string> _searchWords;

        /// <summary>
        /// Получение списка выбранныз строк (для редактирования или удаления)
        /// </summary>
        public DataGridViewSelectedRowCollection GetDataGridViewSelectedRows { get { return dataGridViewList.SelectedRows; } }

        /// <summary>
        /// Словарь со словами для поиска
        /// </summary>
        public Dictionary<string, string> SearchWords { get { return _searchWords; } }

        /// <summary>
        /// Получение доступа к строка datagrid для заполнения
        /// </summary>
        public DataGridViewRowCollection GetDataGridViewRows { get { return dataGridViewList.Rows; } }

        public StandartSearchControl()
        {
            InitializeComponent();
            _searchWords = new Dictionary<string, string>();
        }

        /// <summary>
        /// Конфингуратор контрола
        /// </summary>
        /// <param name="columns">Конфигурация колонок для datagrid</param>
        public void Configurate(List<ColumnConfig> columns)
        {
            dataGridViewList.Columns.Clear();
            panelSearch.Controls.Clear();
            int x = 0;
            int tabIndex = 0;
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

                if(column.Width.HasValue && column.Width > 0 && column.Visible)
                {
                    TextBox textBox = new TextBox
                    {
                        Location = new Point(1 + x, 2),
                        Name = string.Format("textBox{0}", column.Name),
                        Size = new Size(column.Width.Value, 20),
                        TabIndex = tabIndex++,
                        Tag = column.Name
                    };
                    textBox.KeyDown += TextBoxKeyDown;
                    panelSearch.Controls.Add(textBox);
                    _searchWords.Add(column.Name, "");
                    x += column.Width ?? 0;
                }
            }
        }

        /// <summary>
        /// Привязка к событию загрузки данных на datagrid
        /// </summary>
        /// <param name="ev"></param>
        public void GetPageAddEvent(Action ev)
        {
            _getPage += ev;
            _getPage?.Invoke();
        }

        /// <summary>
        /// Привязка к событию загрузки данных на datagrid
        /// </summary>
        /// <param name="ev"></param>
        public void SelectElementAddEvent(Action<Guid> ev)
        {
            _selectElement += ev;
        }

        /// <summary>
        /// Вызов события загрузки данных на datagrid
        /// </summary>
        public void LoadPage()
        {
            _getPage?.Invoke();
        }

        private void TextBoxKeyDown(object sender, KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            _searchWords[textBox.Tag.ToString()] = textBox.Text;
            _getPage?.Invoke();
        }

        private void DataGridViewList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(dataGridViewList.SelectedRows.Count > 0)
            {
                var id = new Guid(dataGridViewList.SelectedRows[0].Cells[0].Value.ToString());
                _selectElement?.Invoke(id);
            }
        }

        private void ToolStripButtonSelect_Click(object sender, EventArgs e)
        {
            if (dataGridViewList.SelectedRows.Count > 0)
            {
                var id = new Guid(dataGridViewList.SelectedRows[0].Cells[0].Value.ToString());
                _selectElement?.Invoke(id);
            }
        }
    }
}