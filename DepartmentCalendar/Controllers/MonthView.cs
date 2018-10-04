using DepartmentWebsite.Controllers;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DepartmentWebsite
{
    /// <summary>
    /// Устанавливает календарь, на котором пользователь может выбрать дату
    /// </summary>
    [DefaultEvent("SelectionChanged")]
    public class MonthView
        : ContainerControl
    {
        private int _forwardMonthIndex;
        private MonthViewDay _lastHitted;
        private bool _mouseDown;
        private Size _daySize;
        private DateTime _selectionStart;
        private DateTime _selectionEnd;
        private DayOfWeek _weekStart;
        private DayOfWeek _workWeekStart;
        private DayOfWeek _workWeekEnd;
        private MonthViewSelection _selectionMode;
        private DateTime _viewStart;
        private Size _monthSize;
        private MonthViewMonth[] _months;
        private Padding _itemPadding;
        private Color _monthTitleColor;
        private Color _monthTitleColorInactive;
        private Color _monthTitleTextColor;
        private Color _monthTitleTextColorInactive;
        private Color _dayBackgroundColor;
        private Color _daySelectedBackgroundColor;
        private Color _dayTextColor;
        private Color _daySelectedTextColor;
        private Color _arrowsColor;
        private Color _dayGrayedText;
        private Color _todayBorderColor;
        private Rectangle _forwardButtonBounds;
        private bool _forwardButtonSelected;
        private Rectangle _backwardButtonBounds;
        private bool _backwardButtonSelected;
        private bool _mouseDoubleClicked;
        private bool _viewOneMonth;

        /// <summary>
        /// Вызывается при выборе даты
        /// </summary>
        public event EventHandler SelectionChanged;

        /// <summary>
        /// Размер квадрата выбранного дня
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size DaySize
        {
            get { return _daySize; }
        }

        /// <summary>
        /// Возвращает или задает первый день недели
        /// </summary>
        [DefaultValue(DayOfWeek.Sunday)]
        public DayOfWeek FirstDayOfWeek
        {
            get { return _weekStart; }
            set { _weekStart = value; }
        }

        /// <summary>
        /// Возвращает значение, указывающее, выбрана ли кнопка "Назад"
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool BackwardButtonSelected
        {
            get { return _backwardButtonSelected; }
        }

        /// <summary>
        /// Границы кнопки "Назад"
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle BackwardButtonBounds
        {
            get { return _backwardButtonBounds; }
        }

        /// <summary>
        /// Получает значение, указывающее, если выбрана кнопка "Вперед"
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ForwardButtonSelected
        {
            get { return _forwardButtonSelected; }
        }

        /// <summary>
        /// Границы кнопки "Вперед"
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle ForwardButtonBounds
        {
            get { return _forwardButtonBounds; }
        }

        /// <summary>
        /// Возвращает или задает внутреннее заполнение элементов (Дни, названия дней, месяцев)
        /// </summary>
        public Padding ItemPadding
        {
            get { return _itemPadding; }
            set { _itemPadding = value; }
        }

        /// <summary>
        /// Возвращает месяцы, отображаемые в календаре
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MonthViewMonth[] Months
        {
            get { return _months; }
        }

        /// <summary>
        /// Возвращает размер месяца целиком внутри MonthView
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size MonthSize
        {
            get { return _monthSize; }
        }

        /// <summary>
        /// Получает или задает начало выбора
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime SelectionStart
        {
            get { return _selectionStart; }
            set
            {
                _selectionStart = value;
                Invalidate();
                OnSelectionChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Получает или задает окончание выбора
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime SelectionEnd
        {
            get { return _selectionEnd; }
            set
            {
                _selectionEnd = value.Date.Add(new TimeSpan(23, 59, 59));
                Invalidate();
                OnSelectionChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Получает или задает режим выбора дат в MonthView
        /// </summary>
        [DefaultValue(MonthViewSelection.Manual)]
        public MonthViewSelection SelectionMode
        {
            get { return _selectionMode; }
            set { _selectionMode = value; }
        }

        /// <summary>
        /// Возвращает или задает дату первого отображаемого месяца
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime ViewStart
        {
            get { return _viewStart; }
            set { _viewStart = value; UpdateMonths(); Invalidate(); }
        }

        /// <summary>
        /// Получает последний день последнего месяца, показанного на экране.
        /// </summary>
        public DateTime ViewEnd
        {
            get
            {
                DateTime month = Months[Months.Length - 1].Date;
                return month.Date.AddDays(DateTime.DaysInMonth(month.Year, month.Month));
            }
        }

        /// <summary>
        /// Получает или задает фоновый цвет дня.
        /// </summary>
        public Color DayBackgroundColor
        {
            get { return _dayBackgroundColor; }
            set { _dayBackgroundColor = value; }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса "MonthView"/>.
        /// </summary>
        public MonthView()
        {
            SetStyle(ControlStyles.Opaque, true);
            DoubleBuffered = true;

            _selectionMode = MonthViewSelection.Manual;
            _workWeekStart = DayOfWeek.Monday;
            _workWeekEnd = DayOfWeek.Friday;
            _weekStart = DayOfWeek.Sunday;
            _viewStart = DateTime.Now;
            _itemPadding = new Padding(2);
            _monthTitleColor = SystemColors.ActiveCaption;
            _monthTitleColorInactive = SystemColors.InactiveCaption;
            _monthTitleTextColor = SystemColors.ActiveCaptionText;
            _monthTitleTextColorInactive = SystemColors.InactiveCaptionText;
            _dayBackgroundColor = Color.Empty;
            _daySelectedBackgroundColor = SystemColors.Highlight;
            _dayTextColor = SystemColors.WindowText;
            _daySelectedTextColor = SystemColors.HighlightText;
            _arrowsColor = SystemColors.Window;
            _dayGrayedText = SystemColors.GrayText;
            _todayBorderColor = Color.Maroon;

            _mouseDoubleClicked = false;
            _viewOneMonth = false;

            UpdateMonthSize();
            UpdateMonths();
        }

        /// <summary>
        /// Проверяет, выбран ли день в указанном месте
        /// </summary>
        public MonthViewDay HitTest(Point p)
        {
            for (int i = 0; i < Months.Length; i++)
            {
                if (Months[i].Bounds.Contains(p))
                {
                    for (int j = 0; j < Months[i].Days.Length; j++)
                    {
                        if (Months[i].Days[j].Bounds.Contains(p))
                        {
                            return Months[i].Days[j];
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Перемещение календаря на один месяц вперед
        /// </summary>
        public void GoForward()
        {
            ViewStart = ViewStart.AddMonths(1);
        }

        /// <summary>
        /// Перемещение календаря на один месяц назад
        /// </summary>
        public void GoBackward()
        {
            ViewStart = ViewStart.AddMonths(-1);
        }

        /// <summary>
        /// Устанавливает границы кнопки "Вперед".
        /// </summary>
        private void SetForwardButtonBounds(Rectangle bounds)
        {
            _forwardButtonBounds = bounds;
        }

        /// <summary>
        /// Устанавливает границы кнопки "Назад".
        /// </summary>
        private void SetBackwardButtonBounds(Rectangle bounds)
        {
            _backwardButtonBounds = bounds;
        }

        /// <summary>
        /// Устанавливает значение кнопки "Вперед" выбранной
        /// </summary>
        private void SetForwardButtonSelected(bool selected)
        {
            _forwardButtonSelected = selected;
        }

        /// <summary>
        /// Устанавливает значение кнопки "Назад" выбранной
        /// </summary>
        private void SetBackwardButtonSelected(bool selected)
        {
            _backwardButtonSelected = selected;
            Invalidate(BackwardButtonBounds);
        }

        /// <summary>
        /// Выбирает неделю, выделенную мышью
        /// </summary>
        private void SelectWeek(DateTime hit)
        {
            int preDays = (new int[] { 0, 1, 2, 3, 4, 5, 6 })[(int)hit.DayOfWeek] - (int)FirstDayOfWeek;

            _selectionStart = hit.AddDays(-preDays);
            SelectionEnd = SelectionStart.AddDays(6);
        }

        /// <summary>
        /// Выбирает рабочую неделю, выделенную мышью
        /// </summary>
        private void SelectWorkWeek(DateTime hit)
        {
            int preDays = (new int[] { 0, 1, 2, 3, 4, 5, 6 })[(int)hit.DayOfWeek] - (int)_workWeekStart;

            _selectionStart = hit.AddDays(-preDays);
            SelectionEnd = SelectionStart.AddDays(Math.Abs(_workWeekStart - _workWeekEnd));
        }

        /// <summary>
        /// Выбирает месяц, выделенный мышью
        /// </summary>
        private void SelectMonth(DateTime hit)
        {
            _selectionStart = new DateTime(hit.Year, hit.Month, 1);
            SelectionEnd = new DateTime(hit.Year, hit.Month, DateTime.DaysInMonth(hit.Year, hit.Month));
        }

        /// <summary>
        /// Рисует текстовый элемент
        /// </summary>
        private void DrawBox(MonthViewBoxEventArgs e)
        {
            if (!e.BackgroundColor.IsEmpty)
            {
                using (SolidBrush b = new SolidBrush(e.BackgroundColor))
                {
                    e.Graphics.FillRectangle(b, e.Bounds);
                }
            }

            if (!e.TextColor.IsEmpty && !string.IsNullOrEmpty(e.Text))
            {
                TextRenderer.DrawText(e.Graphics, e.Text, e.Font != null ? e.Font : Font, e.Bounds, e.TextColor, e.TextFlags);
            }

            if (!e.BorderColor.IsEmpty)
            {
                using (Pen p = new Pen(e.BorderColor))
                {
                    Rectangle r = e.Bounds;
                    r.Width--; r.Height--;
                    e.Graphics.DrawRectangle(p, r);
                }
            }
        }

        /// <summary>
        /// Обновляет размер месяца при отрисовке календаря
        /// </summary>
        private void UpdateMonthSize()
        {
            //Один день недели плюс 31 возможная цифра
            string[] strs = new string[7 + 31];
            int maxWidth = 0;
            int maxHeight = 0;

            for (int i = 0; i < 7; i++)
            {
                strs[i] = ViewStart.AddDays(i).ToString("ddd").Substring(0, 1);
            }

            for (int i = 7; i < strs.Length; i++)
            {
                strs[i] = (i - 6).ToString();
            }

            Font f = new Font(Font, FontStyle.Bold);

            for (int i = 0; i < strs.Length; i++)
            {
                Size s = TextRenderer.MeasureText(strs[i], f);

                if (_mouseDoubleClicked)
                {
                    //На один месяц
                    maxWidth = 120;
                    maxHeight = 65;

                }
                else
                {
                    maxWidth = Math.Max(s.Width, maxWidth);
                    maxHeight = Math.Max(s.Height, maxHeight);
                }
            }

            maxWidth += ItemPadding.Horizontal;
            maxHeight += ItemPadding.Vertical;

            _daySize = new Size(maxWidth, maxHeight);
            _monthSize = new Size(maxWidth * 7, maxHeight * 7 + maxHeight);
        }

        /// <summary>
        /// Обновление месяцев календаря
        /// </summary>
        private void UpdateMonths()
        {
            int gapping = 30;
            int calendars;

            int calendarsX = Convert.ToInt32(Math.Max(Math.Floor((double)ClientSize.Width / (double)(MonthSize.Width + gapping)), 1.0));
            int calendarsY = Convert.ToInt32(Math.Max(Math.Floor((double)ClientSize.Height / (double)(MonthSize.Height + gapping)), 1.0));
            if (_mouseDoubleClicked)
            {
                calendars = 1;
                _mouseDoubleClicked = false;
            }
            else
            {
                calendars = calendarsX * calendarsY;
            }

            int monthsWidth = (calendarsX * MonthSize.Width) + (calendarsX - 1) * gapping;
            int monthsHeight = (calendarsY * MonthSize.Height) + (calendarsY - 1) * gapping;
            int startX = (ClientSize.Width - monthsWidth) / 2;
            int startY = (ClientSize.Height - monthsHeight - gapping) / 2;
            int curX = startX;
            int curY = startY;
            _forwardMonthIndex = calendarsX - 1;

            _months = new MonthViewMonth[calendars];

            for (int i = 0; i < Months.Length; i++)
            {
                Months[i] = new MonthViewMonth(this, ViewStart.AddMonths(i));
                Months[i].SetLocation(new Point(curX, curY));

                curX += gapping + MonthSize.Width;

                if ((i + 1) % calendarsX == 0)
                {
                    curX = startX;
                    curY += gapping + MonthSize.Height;
                }
            }

            MonthViewMonth first = Months[0];

            SetBackwardButtonBounds(new Rectangle(first.Bounds.Left + ItemPadding.Left, first.Bounds.Top + ItemPadding.Top, DaySize.Height - ItemPadding.Horizontal, DaySize.Height - ItemPadding.Vertical));
            SetForwardButtonBounds(new Rectangle(first.Bounds.Right - ItemPadding.Right - BackwardButtonBounds.Width, first.Bounds.Top + ItemPadding.Top, BackwardButtonBounds.Width, BackwardButtonBounds.Height));
        }

        /// <summary>
        /// Вызывает событие System.Windows.Forms.Control.MouseDown
        /// </summary>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            //Сделать метод "Перерисовка компонента DescriptionBox (скорлл и n-ое количество DescriptionBox)"
            base.OnMouseDown(e);

            Focus();

            _mouseDown = true;

            MonthViewDay day = HitTest(e.Location);

            if (day != null)
            {
                switch (SelectionMode)
                {
                    case MonthViewSelection.Manual:
                    case MonthViewSelection.Day:
                        SelectionEnd = _selectionStart = day.Date;
                        break;
                    case MonthViewSelection.WorkWeek:
                        SelectWorkWeek(day.Date);
                        break;
                    case MonthViewSelection.Week:
                        SelectWeek(day.Date);
                        break;
                    case MonthViewSelection.Month:
                        SelectMonth(day.Date);
                        break;
                }
            }

            if (ForwardButtonSelected)
            {
                GoForward();
            }
            else if (BackwardButtonSelected)
            {
                GoBackward();
            }
        }

        protected void menuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem clickedItem = sender as ToolStripItem;
        }

        /// <summary>
        /// Вызывает событие System.Windows.Forms.Control.MouseMove
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (_mouseDown)
            {
                MonthViewDay day = HitTest(e.Location);

                if (day != null && day != _lastHitted)
                {
                    switch (SelectionMode)
                    {
                        case MonthViewSelection.Manual:
                            if (day.Date > SelectionStart)
                            {
                                SelectionEnd = day.Date;
                            }
                            else
                            {
                                SelectionStart = day.Date;
                            }
                            break;
                        case MonthViewSelection.Day:
                            SelectionEnd = _selectionStart = day.Date;
                            break;
                        case MonthViewSelection.WorkWeek:
                            SelectWorkWeek(day.Date);
                            break;
                        case MonthViewSelection.Week:
                            SelectWeek(day.Date);
                            break;
                        case MonthViewSelection.Month:
                            SelectMonth(day.Date);
                            break;
                    }
                    //Сделать метод "Перерисовка компонента DescriptionBox (скорлл и n-ое количество DescriptionBox)"
                    _lastHitted = day;
                }
            }

            if (ForwardButtonBounds.Contains(e.Location))
            {
                SetForwardButtonSelected(true);
            }
            else if (ForwardButtonSelected)
            {
                SetForwardButtonSelected(false);
            }

            if (BackwardButtonBounds.Contains(e.Location))
            {
                SetBackwardButtonSelected(true);
            }
            else if (BackwardButtonSelected)
            {
                SetBackwardButtonSelected(false);
            }
        }

        /// <summary>
        /// Вызывает событие MouseDoubleClick
        /// </summary>
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            _mouseDoubleClicked = true;

            MonthViewDay day = HitTest(e.Location);

            if (!_viewOneMonth)
            {
                if (day != null)
                {
                    ViewStart = day.Date;
                    _mouseDoubleClicked = true;
                    _viewOneMonth = true;
                }
            }
            else
            {
                if (day == null)
                {
                    _mouseDoubleClicked = false;
                    _viewOneMonth = false;
                }
                else
                {
                    using (Form form = new Form())
                    {
                        form.Text = "Создать событие";
                        form.Size = new Size(374, 444);

                        DescriptionBox createBox = new DescriptionBox(this);
                        form.Controls.Add(createBox);

                        form.ShowDialog();
                    }
                }
            }

            UpdateMonthSize();
            UpdateMonths();
            Invalidate();
        }

        /// <summary>
        /// Вызывает событие System.Windows.Forms.Control.MouseUp
        /// </summary>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            _mouseDown = false;
        }

        /// <summary>
        /// Вызывает событие System.Windows.Forms.Control.MouseWheel
        /// </summary>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (e.Delta < 0)
            {
                GoForward();
            }
            else
            {
                GoBackward();
            }
        }

        /// <summary>
        /// Отрисовка календаря
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.Clear(SystemColors.Window);

            for (int i = 0; i < Months.Length; i++)
            {
                if (Months[i].Bounds.IntersectsWith(e.ClipRectangle))
                {
                    //Заголовок месяца
                    string title = Months[i].Date.ToString("MMMM yyyy");

                    MonthViewBoxEventArgs evtTitle = new MonthViewBoxEventArgs(e.Graphics, Months[i].MonthNameBounds,
                        title,
                        Focused ? _monthTitleTextColor : _monthTitleTextColorInactive,
                        Focused ? _monthTitleColor : _monthTitleColorInactive);

                    DrawBox(evtTitle);


                    StringAlignment stringAlignment = StringAlignment.Far;
                    if (_viewOneMonth)
                        stringAlignment = StringAlignment.Near;
                    //Названия дней
                    for (int j = 0; j < Months[i].DayNamesBounds.Length; j++)
                    {
                        MonthViewBoxEventArgs evtDay = new MonthViewBoxEventArgs(e.Graphics, Months[i].DayNamesBounds[j], Months[i].DayHeaders[j],
                            stringAlignment, ForeColor, DayBackgroundColor);

                        DrawBox(evtDay);
                    }

                    if (Months[i].DayNamesBounds != null && Months[i].DayNamesBounds.Length != 0)
                    {
                        using (Pen p = new Pen(_monthTitleColor))
                        {
                            int y = Months[i].DayNamesBounds[0].Bottom;
                            e.Graphics.DrawLine(p, new Point(Months[i].Bounds.X, y), new Point(Months[i].Bounds.Right, y));
                        }
                    }

                    //Перечисление дней
                    foreach (MonthViewDay day in Months[i].Days)
                    {
                        MonthViewBoxEventArgs evtDay;

                        if (!day.Visible) continue;

                        if (_viewOneMonth)
                        {
                            evtDay = new MonthViewBoxEventArgs(e.Graphics, day.Bounds, day.Date.Day.ToString(),
                            StringAlignment.Near,
                            day.Grayed ? _dayGrayedText : (day.Selected ? _daySelectedTextColor : ForeColor),
                            day.Selected ? _daySelectedBackgroundColor : DayBackgroundColor);

                            evtDay.BorderColor = Color.Gray;
                        }
                        else
                        {
                            evtDay = new MonthViewBoxEventArgs(e.Graphics, day.Bounds, day.Date.Day.ToString(),
                            StringAlignment.Far,
                            day.Grayed ? _dayGrayedText : (day.Selected ? _daySelectedTextColor : ForeColor),
                            day.Selected ? _daySelectedBackgroundColor : DayBackgroundColor);
                        }

                        if (day.Date.Equals(DateTime.Now.Date))
                        {
                            evtDay.BorderColor = _todayBorderColor;
                        }

                        //Проходить по событиям в БД, менять у дней-событий BackgroundColor

                        if (day.Date.Equals(new DateTime(2019, 3, 8)) && !day.Selected)
                        {
                            evtDay.BackgroundColor = Color.Red;
                        }

                        DrawBox(evtDay);
                    }

                    //Стрелки-переходы по месяцам
                    if (i == 0)
                    {
                        Rectangle r = BackwardButtonBounds;
                        using (Brush b = new SolidBrush(_arrowsColor))
                        {
                            e.Graphics.FillPolygon(b, new Point[] {
                                new Point(r.Right, r.Top),
                                new Point(r.Right, r.Bottom - 1),
                                new Point(r.Left + r.Width / 2, r.Top + r.Height / 2),
                            });
                        }
                    }

                    if (i == _forwardMonthIndex)
                    {
                        Rectangle r = ForwardButtonBounds;
                        using (Brush b = new SolidBrush(_arrowsColor))
                        {
                            e.Graphics.FillPolygon(b, new Point[] {
                                new Point(r.X, r.Top),
                                new Point(r.X, r.Bottom - 1),
                                new Point(r.Left + r.Width / 2, r.Top + r.Height / 2),
                            });
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Отрисовка и обновление календаря в форме
        /// </summary>
        protected override void OnResize(EventArgs e)
        {
            UpdateMonths();
            Invalidate();
        }

        /// <summary>
        /// Вызывает событие SelectionChanged
        /// </summary>
        protected void OnSelectionChanged(EventArgs e)
        {
            if (SelectionChanged != null)
            {
                SelectionChanged(this, e);
            }
        }
    }
}