using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFramework.CodeAnalysis
{
    //
    using System.Linq.Expressions;
    using DataFramework.Class;

    public class DeleteAnalysis<T>
    {
        public SQL Create(Expression<Func<T, bool>> Where, Analysis analysis)
        {
            var _SQL = new SQL();

            //修正Where 表达式树结构
            var _Table = Parser.GetTableInfo(typeof(T));
            var _TableName = _Table.TableName;

            if (Where != null && Where.Parameters[0].Name != _Table.TableName)
            {
                var _Set_Parameter = Expression.Parameter(typeof(T), _TableName);
                Where = Expression.Lambda<Func<T, bool>>(Where.Body, _Set_Parameter);
            }

            _TableName = DbSettings.KeywordHandle(_TableName);

            _SQL.Code.AppendFormat("DELETE FROM {0} WHERE 1=1 ", _TableName);

            if (Where != null)
            {
                _SQL.IsAlias = false;

                analysis.CreateWhere(Where, _SQL);

                _SQL.Code.AppendFormat("AND {0}", _SQL.Code_Where);
            }

            _SQL.Code.Append(";");

            return _SQL;
        }

    }
}
