using System;
using System.Drawing;

namespace DepartmentWebsite
{
    /// <summary>
    /// Представляет отображаемые дни месяца
    /// </summary>
    public class MonthViewDay
    {
        Rectangle _bounds;
        private DateTime _date;
        private MonthViewMonth _month;
        private MonthView _monthView;

        /// <summary>
        /// Привязка к компоненту календаря
        /// </summary>
        public MonthView MonthView
        {
            get { return _monthView; }
            set { _monthView = value; }
        }

        /// <summary>
        /// Привязка к месяцу
        /// </summary>
        public MonthViewMonth Month
        {
            get { return _month; }
        }

        /// <summary>
        /// Получает обводку для дня
        /// </summary>
        public Rectangle Bounds
        {
            get { return _bounds; }
        }

        /// <summary>
        /// Получает текущую дату
        /// </summary>
        public DateTime Date
        {
            get { return _date; }
        }

        /// <summary>
        /// Получает или задает, выбрана ли дата
        /// </summary>
        public bool Selected
        {
            get { return Date >= MonthView.SelectionStart && Date <= MonthView.SelectionEnd; }
        }

        /// <summary>
        /// Возвращает, если день неактивен
        /// </summary>
        public bool Grayed
        {
            get { return Month.Date.Month != Date.Month; }
        }

        /// <summary>
        /// Возвращает значение, указывающее, отображается ли экземпляр дня в календаре
        /// </summary>
        public bool Visible
        {
            get
            {
                return !(Grayed && (Date > MonthView.ViewStart && Date < MonthView.ViewEnd));
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса MonthViewDay.
        /// </summary>
        internal MonthViewDay(MonthViewMonth month, DateTime date)
        {
            _month = month;
            _monthView = month.MonthView;
            _date = date;


        }
        /// <summary>
        /// Устанавливает значение свойства "Bounds"
        /// </summary>
        internal void SetBounds(Rectangle bounds)
        {
            _bounds = bounds;
        }
    }
}
