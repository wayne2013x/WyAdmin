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

    public class JoinAnalysis
    {


        public void Create(SQL _Sql, LambdaExpression _LambdaExpression, EJoinType _EJoinType,Analysis analysis)
        {
            if (_LambdaExpression.Body is BinaryExpression)
            {
                var body = (_LambdaExpression.Body as BinaryExpression);

                var _Parameters = _LambdaExpression.Parameters;

                var _Parameter = _Parameters[_Parameters.Count - 1];

                var _Alias = DbSettings.KeywordHandle(_Parameter.Name);

                var _TabName = DbSettings.KeywordHandle(_Parameter.Type.Name);

                _Sql.Code_Join.AppendFormat(" {0} {1} AS {2} ON ", _EJoinType.ToString().Replace("_", " "), _TabName, _Alias);

                var _New_Sql = new SQL();
                _New_Sql.Parameter = _Sql.Parameter;

                analysis.CreateWhere(_LambdaExpression, _New_Sql);

                _Sql.Code_Join.Append(_New_Sql.Code_Where);

                _Sql.Parameter = _New_Sql.Parameter;

                _Sql.Alias[_Alias] = _TabName;
            }
        }





    }
}
