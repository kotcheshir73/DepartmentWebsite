using System;
using System.Windows.Forms;

namespace TicketViews.Controls
{
    public partial class StandartElementControl : UserControl
    {
        private event Action _onSetId;

        private Guid? _id;

        /// <summary>
        /// Идентификатор выбранной записи
        /// </summary>
        public Guid? Id
        {
            get { return _id; }
            set { _id = value; _onSetId?.Invoke(); }
        }

        public StandartElementControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Привязка обработчика при указании идентификатора
        /// </summary>
        /// <param name="ev"></param>
        public void OnSetIdAddEvent(Action ev)
        {
            _onSetId += ev;
        }

        /// <summary>
        /// Привязка обработчика для вызова поиска
        /// </summary>
        /// <param name="ev"></param>
        public void ButtonSelectClickAddEvent(EventHandler ev)
        {
            buttonSelect.Click += ev;
        }

        /// <summary>
        /// Отчистка выбранного значения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClear_Click(object sender, EventArgs e)
        {
            _id = null;
            textBox.Text = "";
        }
    }
}