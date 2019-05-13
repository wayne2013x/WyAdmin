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
    /// Orderby 语法解析
    /// </summary>
    public class OrderByAnalysis
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
                var value = Parser.Eval(_LambdaExpression.Body);
                this.AddCode(_Sql, value);
            }
            else if (_Body is NewExpression)//如果是匿名对象
            {
                var body = (_Body as NewExpression);
                var values = body.Arguments;
                var member = body.Members;

                var column = new List<string>();
                var list_member = member.ToList();
                foreach (var item in values)
                {
                    if (item is MemberExpression)
                    {
                        var it = item as MemberExpression;
                        //检查是否有别名
                        var DisplayName = list_member[values.IndexOf(item)].Name;

                        var _Alias = this.AddAlias((it.Expression as ParameterExpression).Name, _Sql);

                        var _MemberName = it.Member.Name;

                        var _MemberName1 = DbSettings.KeywordHandle(_MemberName);

                        if (DisplayName == _MemberName)
                            column.Add(_Alias + _MemberName1);
                        else
                        {
                            if (!DisplayName.ToLower().Contains("asc") && !DisplayName.ToLower().Contains("desc"))
                                throw new DbFrameException("ORDER BY 语法错误 请使用 asc 或者 desc 关键字");
                            column.Add(_Alias + _MemberName1 + " " + (DisplayName.Contains("desc") ? "DESC" : "ASC"));
                        }
                    }
                    else if (item is ConstantExpression)
                    {
                        var it = item as ConstantExpression;
                        var val = it.Value.ToString();
                        //检查是否有别名 ''
                        var DisplayName = list_member[values.IndexOf(item)].Name;
                        if (!string.IsNullOrEmpty(DisplayName) && DisplayName.StartsWith("SqlString"))//判断别名是否 有 SqlString 关键字
                        {
                            column.Add(val);
                        }
                    }
                    else if (item.Type == typeof(string))
                    {
                        //检查是否有别名 ''
                        var value = Parser.Eval(item).ToString();
                        var DisplayName = list_member[values.IndexOf(item)].Name;
                        if (!string.IsNullOrEmpty(DisplayName) && DisplayName.StartsWith("SqlString"))
                        {
                            column.Add(value);
                        }
                    }
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
                throw new DbFrameException(" ORDER BY 无法解析的表达式！");
            }

        }

        public void CreateDESC(SQL _Sql, LambdaExpression _LambdaExpression)
        {
            var _Body = _LambdaExpression.Body;
            if (_Body is MemberExpression)
            {
                var _MemberExpression = _Body as MemberExpression;
                var _Alias = this.AddAlias((_MemberExpression.Expression as ParameterExpression).Name, _Sql);
                var _MemberName = DbSettings.KeywordHandle(_MemberExpression.Member.Name);
                this.AddCode(_Sql, _Alias + _MemberName + " DESC ");
            }
            else
            {
                throw new DbFrameException(" ORDER BY DESC 无法解析的表达式，更多语法请使用 ORDER BY。");
            }
        }

        private void AddCode(SQL _Sql, object Codes)
        {
            if (Codes == null) throw new DbFrameException(" ORDER BY 参数不能为空字符串或者Null ");

            _Sql.Code_OrderBy.Append(Codes);//记录 Order By
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
