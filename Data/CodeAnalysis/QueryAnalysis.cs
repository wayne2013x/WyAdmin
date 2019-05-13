using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFramework.CodeAnalysis
{
    //
    using DataFramework;
    using DataFramework.Class;
    using System.Linq.Expressions;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QueryAnalysis<T>
    {

        public void Create(SQL _Sql)
        {
            var _Table = Parser.GetTableInfo(typeof(T));

            var _TableName = DbSettings.KeywordHandle(_Table.TableName);

            //var _Alias = DbSettings.KeywordHandle(_TableName);

            //_Sql.Alias[_Alias] = _TableName;//记录用到的表名 与 别名

            _Sql.Code_Column.Clear().Append("*");//记录列

            _Sql.Code_FromTab.AppendFormat(_TableName);//select * from Member with(nolock)


        }

    }
}
