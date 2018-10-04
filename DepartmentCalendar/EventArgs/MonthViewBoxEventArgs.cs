using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace DepartmentWebsite
{
    /// <summary>
    /// Представляет данные о текстовом поле, которое будет отображаться в компоненте
    /// </summary>
    public class MonthViewBoxEventArgs
    {
        private Graphics _graphics;
        private Color _textColor;
        private Color _backgroundColor;
        private string _text;
        private Color _borderColor;
        private Rectangle _bounds;
        private Font _font;
        private TextFormatFlags _TextFlags;

        /// <summary>
        /// Получает или задает границы поля
        /// </summary>
        public Rectangle Bounds
        {
            get { return _bounds; }
        }

        /// <summary>
        /// Возвращает или задает шрифт текста. Если null, будет использоваться значение по умолчанию.
        /// </summary>
        public Font Font
        {
            get { return _font; }
            set { _font = value; }
        }

        /// <summary>
        /// Получает или задает объект Graphics, используемый для рисования
        /// </summary>
        public Graphics Graphics
        {
            get { return _graphics; }
        }

        /// <summary>
        /// Получает или задает цвет рамки
        /// </summary>
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; }
        }

        /// <summary>
        /// Получает или задает текст
        /// </summary>
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        /// <summary>
        /// Возвращает или задает цвет фона в поле
        /// </summary>
        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; }
        }

        /// <summary>
        /// Возвращает или задает цвет текста
        /// </summary>
        public Color TextColor
        {
            get { return _textColor; }
            set { _textColor = value; }
        }

        /// <summary>
        /// Флаги текста
        /// </summary>
        public TextFormatFlags TextFlags
        {
            get { return _TextFlags; }
            set { _TextFlags = value; }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса MonthViewBoxEventArgs.
        /// </summary>
        internal MonthViewBoxEventArgs(Graphics graphics, Rectangle bounds, string text, StringAlignment textAlign, Color textColor, Color backColor, Color borderColor)
        {
            _graphics = graphics;

            _graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            _graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            _graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            _graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

            _bounds = bounds;
            Text = text;
            TextColor = textColor;
            BackgroundColor = backColor;
            //BorderColor = Color.Red;
            BorderColor = borderColor;

            switch (textAlign)
            {
                case StringAlignment.Center:
                    TextFlags |= TextFormatFlags.HorizontalCenter;
                    break;

                case StringAlignment.Far:
                    TextFlags |= TextFormatFlags.Right;
                    break;

                case StringAlignment.Near:
                    TextFlags |= TextFormatFlags.Left;
                    break;


                default:
                    break;

            }

            TextFlags |= TextFormatFlags.VerticalCenter;
        }

        internal MonthViewBoxEventArgs(Graphics graphics, Rectangle bounds, string text, Color textColor)
            : this(graphics, bounds, text, StringAlignment.Center, textColor, Color.Empty, Color.Empty)
        { }

        internal MonthViewBoxEventArgs(Graphics graphics, Rectangle bounds, string text, Color textColor, Color backColor)
            : this(graphics, bounds, text, StringAlignment.Center, textColor, backColor, Color.Empty)
        { }

        internal MonthViewBoxEventArgs(Graphics graphics, Rectangle bounds, string text, StringAlignment textAlign, Color textColor, Color backColor)
            : this(graphics, bounds, text, textAlign, textColor, backColor, Color.Empty)
        { }

        internal MonthViewBoxEventArgs(Graphics graphics, Rectangle bounds, string text, StringAlignment textAlign, Color textColor)
            : this(graphics, bounds, text, textAlign, textColor, Color.Empty, Color.Empty)
        { }

    }
}
