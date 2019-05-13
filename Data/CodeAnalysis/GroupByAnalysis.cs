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

    public class GroupByAnalysis
    {

        public void Create(SQL _Sql, LambdaExpression _LambdaExpression)
        {
            var _Body = _LambdaExpression.Body;
            if (_Body is ConstantExpression)//如果是字符串
            {
                var value = (_Body as ConstantExpression).Value;
                this.AddCode(_Sql, value);
            }
            else if (_Body.Type == typeof(string))//如果是字符串
            {
                var value = Parser.Eval(_Body);
                this.AddCode(_Sql, value);
            }
            else if (_Body is NewExpression)//如果是匿名对象
            {
                var body = (_Body as NewExpression);
                var values = body.Arguments;

                var column = new List<string>();
                foreach (MemberExpression item in values)
                {
                    var it = item as MemberExpression;
                    var _Alias = AddAlias((it.Expression as ParameterExpression).Name, _Sql);
                    var _MemberName = DbSettings.KeywordHandle(it.Member.Name);
                    column.Add(_Alias + _MemberName);
                }
                this.AddCode(_Sql, string.Join(",", column));
            }
            else if (_Body is MemberExpression)
            {
                var _MemberExpression = _Body as MemberExpression;
                var _Alias = this.AddAlias((_MemberExpression.Expression as ParameterExpression).Name, _Sql);
                var _MemberName = DbSettings.KeywordHandle(_MemberExpression.Member.Name);
                this.AddCode(_Sql, _Alias + _MemberName);
            }
            else
            {
                throw new DbFrameException(" GROUP BY 无法解析的表达式！");
            }
        }

        private void AddCode(SQL _Sql, object Codes)
        {
            if (Codes == null)
                throw new DbFrameException(" GROUP BY 参数不能为空字符串或者Null ");

            _Sql.Code_GroupBy.Append(Codes);
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
