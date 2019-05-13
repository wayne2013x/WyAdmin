using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFramework.Class
{
    public class TableAttribute : Attribute
    {

        public string TableName = string.Empty;

        public TableAttribute(string _TableName)
        {
            this.TableName = _TableName;
        }

    }
}
