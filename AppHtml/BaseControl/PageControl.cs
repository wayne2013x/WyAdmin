using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppHtml.BaseControl
{
    public static class PageControl
    {

        public static string Input(Dictionary<string, string> attr)
        {
            return new OneLabel(DomName.input, attr).GetHtml();
        }

        public static string Img(Dictionary<string, string> attr)
        {
            return new OneLabel(DomName.img, attr).GetHtml();
        }

        public static string Textarea(Dictionary<string, string> attr)
        {
            return new TwoLabel(DomName.textarea, attr).GetHtml();
        }

        public static string Select(Dictionary<string, string> attr, string options)
        {
            return new TwoLabel(DomName.select, attr, options).GetHtml();
        }

        public static string Opeion(Dictionary<string, string> attr, string text)
        {
            return new TwoLabel(DomName.option, attr, text).GetHtml();
        }

        public static string Div(Dictionary<string, string> attr, string text)
        {
            return new TwoLabel(DomName.div, attr, text).GetHtml();
        }

        public static string Button(Dictionary<string, string> attr, string text)
        {
            return new TwoLabel(DomName.button, attr, text).GetHtml();
        }

        public static string H4(Dictionary<string, string> attr, string text)
        {
            return new TwoLabel(DomName.h4, attr, text).GetHtml();
        }

        public static string I(Dictionary<string, string> attr, string text)
        {
            return new TwoLabel(DomName.i, attr, text).GetHtml();
        }

        public static string Span(Dictionary<string, string> attr, string text)
        {
            return new TwoLabel(DomName.span, attr, text).GetHtml();
        }

        public static string Script(Dictionary<string, string> attr, string text)
        {
            return new TwoLabel(DomName.script, attr, text).GetHtml();
        }

        public static string A(Dictionary<string, string> attr, string text)
        {
            return new TwoLabel(DomName.a, attr, text).GetHtml();
        }








    }
}
