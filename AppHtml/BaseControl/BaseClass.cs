using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppHtml.BaseControl
{
    using System.Linq.Expressions;

    /// <summary>
    /// 标签库 基础类
    /// </summary>
    public class BaseClass
    {
        /// <summary>
        /// 标签名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 属性
        /// </summary>
        public Dictionary<string, string> Attribute { get; set; }

        /// <summary>
        /// 获取属性 字符串
        /// </summary>
        /// <param name="_Attribute"></param>
        /// <returns></returns>
        public string GetAttributeString()
        {
            if (Attribute == null) throw new ArgumentNullException("标签库 基础类 Attribute 不能为 Null");

            var str = "";

            foreach (var item in Attribute)
            {
                str += item.Key + "=\"" + item.Value + "\" ";
            }

            return str;
        }





    }
}
