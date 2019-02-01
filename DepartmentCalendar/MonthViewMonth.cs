using System;
using System.Drawing;

namespace DepartmentWebsite
{
    /// <summary>
    /// Представляет отображаемый месяц
    /// </summary>
    public class MonthViewMonth
    {
        private DateTime _date;
        private Rectangle monthNameBounds;
        private Rectangle[] dayNamesBounds;
        private MonthViewDay[] days;
        private string[] _dayHeaders;
        private Point _location;
        private MonthView _monthview;

        /// <summary>
        /// Получает границы
        /// </summary>
        public Rectangle Bounds
        {
            get { return new Rectangle(Location, Size); }
        }

        /// <summary>
        /// Получает компонент календаря
        /// </summary>
        public MonthView MonthView
        {
            get { return _monthview; }
        }

        /// <summary>
        /// Получает позицию календаря
        /// </summary>
        public Point Location
        {
            get { return _location; }
        }

        /// <summary>
        /// Получает размер
        /// </summary>
        public Size Size
        {
            get { return MonthView.MonthSize; }
        }

        /// <summary>
        /// Получает или задает множество дней календаря
        /// </summary>
        public MonthViewDay[] Days
        {
            get { return days; }
            set { days = value; }
        }

        /// <summary>
        /// Получает или задает границы названия дня.
        /// </summary>
        public Rectangle[] DayNamesBounds
        {
            get { return dayNamesBounds; }
            set { dayNamesBounds = value; }
        }

        /// <summary>
        /// Получает или задает заголовки дней
        /// </summary>
        public string[] DayHeaders
        {
            get { return _dayHeaders; }
            set { _dayHeaders = value; }
        }

        /// <summary>
        /// Получает или задает границы названия месяца.
        /// </summary>
        public Rectangle MonthNameBounds
        {
            get { return monthNameBounds; }
            set { monthNameBounds = value; }
        }

        /// <summary>
        /// Получает или задает дату первого дня в месяце
        /// </summary>
        public DateTime Date
        {
            get { return _date; }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса MonthViewMonth.
        /// </summary>
        internal MonthViewMonth(MonthView monthView, DateTime date)
        {
            if (date.Day != 1)
            {
                date = new DateTime(date.Year, date.Month, 1);
            }


            _monthview = monthView;
            _date = date;

            int preDays = (new int[] { 0, 1, 2, 3, 4, 5, 6 })[(int)date.DayOfWeek] - (int)MonthView.FirstDayOfWeek;
            days = new MonthViewDay[6 * 7];
            DateTime curDate = date.AddDays(preDays);
            DayHeaders = new string[7];

            for (int i = 0; i < days.Length; i++)
            {
                days[i] = new MonthViewDay(this, curDate);

                if (i < 7)
                {
                    DayHeaders[i] = curDate.ToString("ddd").Substring(0, 2);
                }

                curDate = curDate.AddDays(1);
            }
        }

        /// <summary>
        /// Устанавливает значение свойства Location
        /// </summary>
        internal void SetLocation(Point location)
        {

            int startX = location.X;
            int startY = location.Y;
            int curX = startX;
            int curY = startY;

            _location = location;

            monthNameBounds = new Rectangle(location, new Size(Size.Width, MonthView.DaySize.Height));

            dayNamesBounds = new Rectangle[7];
            curY = location.Y + MonthView.DaySize.Height;
            for (int i = 0; i < dayNamesBounds.Length; i++)
            {
                DayNamesBounds[i] = new Rectangle(curX, curY, MonthView.DaySize.Width, MonthView.DaySize.Height);

                curX += MonthView.DaySize.Width;
            }

            curX = startX;
            curY = startY + MonthView.DaySize.Height * 2;

            for (int i = 0; i < Days.Length; i++)
            {
                Days[i].SetBounds(new Rectangle(new Point(curX, curY), MonthView.DaySize));

                curX += MonthView.DaySize.Width;

                if ((i + 1) % 7 == 0)
                {
                    curX = startX;
                    curY += MonthView.DaySize.Height;
                }
            }

        }
    }
}