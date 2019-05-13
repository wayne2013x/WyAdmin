using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppHtml.BaseControl
{
    public class TwoLabel : BaseClass
    {
        /// <summary>
        /// 标签内部 元素
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 成对标签
        /// </summary>
        /// <param name="_Name">标签名称</param>
        /// <param name="_Attribute">标签属性</param>
        public TwoLabel(string _Name, Dictionary<string, string> _Attribute, string _Text = "")
        {
            this.Name = _Name;
            this.Attribute = _Attribute;
            this.Text = _Text;
        }

        public string GetHtml()
        {
            return "<" + this.Name + " " + this.GetAttributeString() + ">" + Text + "</" + this.Name + ">";
        }




    }
}
