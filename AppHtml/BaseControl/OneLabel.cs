using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppHtml.BaseControl
{
    /// <summary>
    /// 单一 标签 例如 ：<input />
    /// </summary>
    public class OneLabel : BaseClass
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Name"></param>
        /// <param name="_Attribute"></param>
        public OneLabel(string _Name, Dictionary<string, string> _Attribute)
        {
            this.Name = _Name;
            this.Attribute = _Attribute;
        }

        public string GetHtml()
        {
            return "<" + this.Name + " " + this.GetAttributeString() + " />";
        }



    }
}
