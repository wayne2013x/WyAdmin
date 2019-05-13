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

    public class UpdateAnalysis<T>
    {

        public SQL Create(MemberInitExpression _MemberInitExpression, Expression<Func<T, bool>> Where, Analysis analysis)
        {
            var _SQL = new SQL();
            List<string> _Set = new List<string>();

            //修正Where 表达式树结构
            var _Table = Parser.GetTableInfo(typeof(T));
            var _TableName = _Table.TableName;

            if (Where != null && Where.Parameters[0].Name != _Table.TableName)
            {
                var _Set_Parameter = Expression.Parameter(typeof(T), _TableName);
                Where = Expression.Lambda<Func<T, bool>>(Where.Body, _Set_Parameter);
            }

            _TableName = DbSettings.KeywordHandle(_TableName);

            foreach (MemberAssignment item in _MemberInitExpression.Bindings)
            {
                //检测有无忽略字段
                if (_Table.Fields.FirstOrDefault(w => w.IsIgnore && w.FieldName == item.Member.Name) != null)
                    continue;

                var _Name = item.Member.Name;

                if (_Table.KeyFieldName == _Name && _Table.KeyFieldInfo.IsIdentity)//如果主键自增
                    continue;

                var _Count = _SQL.Parameter.Count;

                var _Key = "@" + _Name + "_" + _Count;

                _Set.Add(_Name + "=" + _Key);

                var _Val = Parser.Eval(item.Expression);

                _SQL.Parameter.Add(new DbParam() { ParameterName = _Key, Value = _Val });
            }

            _SQL.Code.AppendFormat("UPDATE {0} SET {1} WHERE 1=1 ", _TableName, string.Join(",", _Set));

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
