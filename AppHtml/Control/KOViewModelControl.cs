using DataFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AppHtml.Control
{

    public class KOViewModelControl
    {
        public StringBuilder ScriptStr(params object[] Models)
        {
            var sb = new StringBuilder();
            foreach (var item in Models)
            {
                if (item is string)
                {
                    this.GetByString(item.ToStr(), sb);
                }
                else
                {
                    this.GetByModel(item, sb);
                }
            }
            return sb;
        }

        private StringBuilder Get<T>(T Model, string[] More, StringBuilder sb)
        {
            this.GetByModel(Model, sb);

            if (More != null)
            {
                More.ToList().ForEach(item =>
                {
                    this.GetByString(item, sb);
                });
            }
            return sb;
        }

        private StringBuilder GetByModel<T>(T Model, StringBuilder sb)
        {
            Parser.GetPropertyInfos(Model.GetType()).ToList().ForEach(item =>
            {
                sb.Append("this." + item.Name + "=ko.observable('');");
            });
            return sb;
        }

        private StringBuilder GetByString(string More, StringBuilder sb)
        {
            sb.Append("this." + More + "=ko.observable('');");
            return sb;
        }




    }
}