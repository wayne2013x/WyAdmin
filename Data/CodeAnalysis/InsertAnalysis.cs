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

    public class InsertAnalysis<T>
    {

        public Tuple<SQL, object> Create(MemberInitExpression _MemberInitExpression, string LastInsertId, Analysis analysis)
        {
            var _SQL = new SQL();
            var _Table = Parser.GetTableInfo(typeof(T));
            List<string> _Cols = new List<string>();
            List<string> _Values = new List<string>();

            object _KeyId = null;

            var _TableName = DbSettings.KeywordHandle(_Table.TableName);

            foreach (MemberAssignment item in _MemberInitExpression.Bindings)
            {
                //检测有无忽略字段
                if (_Table.Fields.FirstOrDefault(w => w.IsIgnore && w.FieldName == item.Member.Name) != null)
                    continue;

                var _Name = item.Member.Name;
                var _Count = _SQL.Parameter.Count;

                var _Val = Parser.Eval(item.Expression);

                if (_Table.KeyFieldName == _Name)
                {
                    //如果主键自增
                    if (_Table.KeyFieldInfo.IsIdentity)
                    {
                        if (analysis._DbContextType != DbContextType.Oracle)
                            continue;
                        //MY_SEQ.NEXTVAL
                        _Val = "MY_SEQ.NEXTVAL";
                    }
                    else
                    {
                        _KeyId = this.CheckKey(_Table, _Val);
                        _Val = _KeyId;
                    }
                }

                _Cols.Add(_Name);

                _Values.Add("@" + _Name + "_" + _Count);

                _SQL.Parameter.Add(new DbParam()
                {
                    ParameterName = "@" + _Name + "_" + _Count,
                    Value = _Val
                });
            }

            _SQL.Code.AppendFormat("INSERT INTO {0} ({1}) VALUES ({2}) {3}", _TableName, string.Join(",", _Cols), string.Join(",", _Values), LastInsertId);

            return new Tuple<SQL, object>(_SQL, _KeyId);
        }

        private object CheckKey(Entity_Table _Entity_Table, object Value)
        {
            var _Identity = _Entity_Table.KeyFieldInfo.IsIdentity;
            var _FieldType = _Entity_Table.KeyFieldInfo.FieldType;

            if ((_FieldType == typeof(Guid) || _FieldType == typeof(Guid?)))
            {
                if (Value == null || Guid.Parse(Value.ToString()) == Guid.Empty)
                    return Guid.NewGuid();

                return Value;
            }
            return null;
        }

    }
}
