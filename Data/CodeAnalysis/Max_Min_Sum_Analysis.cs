using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFramework.CodeAnalysis
{
    using DataFramework.Class;
    using System.Linq.Expressions;

    public class Max_Min_Sum_Analysis
    {
        public SQL CreateMax(LambdaExpression _LambdaExpression, SQL _Sql)
        {
            if (_LambdaExpression == null) throw new DbFrameException(" Max 参数不能为空字符串或者Null ");

            var _Body = _LambdaExpression.Body;
            if (_Body is MemberExpression)
            {
                var _MemberExpression = _Body as MemberExpression;
                var _Alias = this.AddAlias((_MemberExpression.Expression as ParameterExpression).Name, _Sql);
                var _MemberName = DbSettings.KeywordHandle(_MemberExpression.Member.Name);

                return this.AddCode(_Sql, " MAX(" + _Alias + _MemberName + ") ");
            }
            else
            {
                throw new DbFrameException(" MAX 无法解析的表达式。");
            }
        }

        public SQL CreateMin(LambdaExpression _LambdaExpression, SQL _Sql)
        {
            if (_LambdaExpression == null) throw new DbFrameException(" Min 参数不能为空字符串或者Null ");

            var _Body = _LambdaExpression.Body;
            if (_Body is MemberExpression)
            {
                var _MemberExpression = _Body as MemberExpression;
                var _Alias = this.AddAlias((_MemberExpression.Expression as ParameterExpression).Name, _Sql);
                var _MemberName = DbSettings.KeywordHandle(_MemberExpression.Member.Name);

                return this.AddCode(_Sql, " MIN(" + _Alias + _MemberName + ") ");
            }
            else
            {
                throw new DbFrameException(" MIN 无法解析的表达式。");
            }
        }

        public SQL CreateSum(LambdaExpression _LambdaExpression, SQL _Sql)
        {
            if (_LambdaExpression == null) throw new DbFrameException(" Sum 参数不能为空字符串或者Null ");

            var _Body = _LambdaExpression.Body;
            if (_Body is MemberExpression)
            {
                var _MemberExpression = _Body as MemberExpression;
                var _Alias = this.AddAlias((_MemberExpression.Expression as ParameterExpression).Name, _Sql);
                var _MemberName = DbSettings.KeywordHandle(_MemberExpression.Member.Name);

                return this.AddCode(_Sql, " SUM(" + _Alias + _MemberName + ") ");
            }
            else
            {
                throw new DbFrameException(" SUM 无法解析的表达式。");
            }
        }

        private SQL AddCode(SQL _Sql, string Codes)
        {
            var _New_Sql = new SQL();
            _New_Sql.Parameter = _Sql.Parameter;

            _New_Sql.Code_Column.Clear().Append(Codes);
            _New_Sql.Code_FromTab = _Sql.Code_FromTab;
            _New_Sql.Code_GroupBy = _Sql.Code_GroupBy;
            _New_Sql.Code_Join = _Sql.Code_Join;
            _New_Sql.Code_OrderBy = _Sql.Code_OrderBy;
            _New_Sql.Code_Where = _Sql.Code_Where;
            //_New_Sql.Code_TakePage = _Sql.Code_TakePage;

            return _New_Sql;
        }

        /// <summary>
        /// 追加别名
        /// </summary>
        private string AddAlias(string Alias, SQL _Sql)
        {
            if (_Sql.IsAlias)
                return DbSettings.KeywordHandle(Alias) + ".";
            return string.Empty;
        }


    }


}
