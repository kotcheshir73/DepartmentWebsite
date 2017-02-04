using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;

namespace DepartmentDesktop.Models
{
    public class WordTemplate
    {
        private PageSetup _pageSetup;

        private List<WordParagraphSetup> _paragraphs;

        public PageSetup PageSetup { get { return _pageSetup; } }

        public WordTemplate(PageSetup pageSetup)
        {
            _pageSetup = pageSetup;
            _paragraphs = new List<WordParagraphSetup>();
        }
    }

    public class WordParagraphSetup
    {
        private Font _font;

        private string _text;

        private ParagraphFormat _paragraphFormat;

        public Font Font { set { _font = value; } get { return _font; } }

        public string Text { set { _text = value; } get { return _text; } }

        public ParagraphFormat ParagraphFormat { set { _paragraphFormat = value; } get { return _paragraphFormat; } }


    }
}
