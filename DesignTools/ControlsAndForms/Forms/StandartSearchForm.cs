using ControlsAndForms.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ControlsAndForms.Forms
{
    public partial class StandartSearchForm : Form
    {
        /// <summary>
        /// Словарь со словами для поиска
        /// </summary>
        protected Dictionary<string, string> _searchWords;

        protected Guid _selectedId;

        public Guid SelectedId { get { return _selectedId; } }

        public StandartSearchForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Конфингуратор контрола
        /// </summary>
        /// <param name="columns">Конфигурация колонок для datagrid</param>
        protected void Configurate(List<ColumnConfig> columns)
        {
            _searchWords = new Dictionary<string, string>();
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

                if (column.Width.HasValue && column.Width > 0 && column.Visible)
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

        protected virtual void LoadRecords() { }

        /// <summary>
        /// Вызов события выбора элемента
        /// </summary>
        /// <param name="id"></param>
        private void Select(Guid id)
        {
            _selectedId = id;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void TextBoxKeyDown(object sender, KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            _searchWords[textBox.Tag.ToString()] = textBox.Text;
            LoadRecords();
        }

        private void DataGridViewList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridViewList.SelectedRows.Count > 0)
            {
                var id = new Guid(dataGridViewList.SelectedRows[0].Cells[0].Value.ToString());
                Select(id);
            }
        }

        private void ToolStripButtonSelect_Click(object sender, EventArgs e)
        {
            if (dataGridViewList.SelectedRows.Count > 0)
            {
                var id = new Guid(dataGridViewList.SelectedRows[0].Cells[0].Value.ToString());
                Select(id);
            }
        }
    }
}
