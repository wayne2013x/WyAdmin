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
    /// Select 解析
    /// </summary>
    public class SelectAnalysis
    {

        public void Create(SQL _Sql, LambdaExpression _LambdaExpression)
        {
            if (_LambdaExpression.Body is ConstantExpression)//如果是字符串
            {
                var body = (_LambdaExpression.Body as ConstantExpression);
                this.AddCode(_Sql, body.Value);
            }
            else if (_LambdaExpression.Body is MemberExpression)
            {
                var _MemberExpression = _LambdaExpression.Body as MemberExpression;
                if (_MemberExpression.Expression is ConstantExpression)
                {
                    var value = Parser.Eval(_LambdaExpression.Body);
                    this.AddCode(_Sql, value);
                }
                else if (_MemberExpression.Expression is ParameterExpression)
                {
                    var _ParameterExpression = _MemberExpression.Expression as ParameterExpression;
                    var _TabName = this.AddAlias(_ParameterExpression.Name, _Sql);
                    var _DisplayName = DbSettings.KeywordHandle(_MemberExpression.Member.Name);
                    var column = new List<string>();
                    column.Add(_TabName + _DisplayName + " AS " + _DisplayName);
                    this.AddCode(_Sql, string.Join(",", column));
                }
            }
            else if (_LambdaExpression.Body is NewExpression)//如果是匿名对象
            {
                var body = (_LambdaExpression.Body as NewExpression);
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
                        var DisplayName = DbSettings.KeywordHandle(list_member[values.IndexOf(item)].Name);
                        var _TabName = this.AddAlias((it.Expression as ParameterExpression).Name, _Sql);
                        column.Add(_TabName + DbSettings.KeywordHandle(it.Member.Name) + " AS " + DisplayName);
                    }
                    else if (item is ConstantExpression)
                    {
                        var it = item as ConstantExpression;
                        var val = it.Value;
                        //检查是否有别名 ''
                        var DisplayName = list_member[values.IndexOf(item)].Name;
                        if (!string.IsNullOrEmpty(DisplayName))
                        {
                            //判断别名是否 有 SqlString 关键字
                            if (DisplayName.StartsWith("SqlString"))
                            {
                                column.Add(val.ToString());
                            }
                            else
                            {
                                column.Add(DbSettings.KeywordHandle(val.ToString()) + " AS " + DbSettings.KeywordHandle(DisplayName));
                            }
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
                    else if (item is ParameterExpression)
                    {
                        this.AnalysisParameterExpression(item as ParameterExpression, column);
                    }
                }
                this.AddCode(_Sql, string.Join(",", column));
            }
            else if (_LambdaExpression.Body is ParameterExpression)
            {
                var column = new List<string>();
                this.AnalysisParameterExpression(_LambdaExpression.Body as ParameterExpression, column);
                this.AddCode(_Sql, string.Join(",", column));
            }
            else
            {
                throw new DbFrameException(" SELECT 语法不支持！");
            }

        }

        /// <summary>
        /// 替换 列
        /// </summary>
        /// <param name="_Sql"></param>
        /// <param name="Codes"></param>
        private void AddCode(SQL _Sql, object Codes)
        {
            if (Codes == null) return;

            if (string.IsNullOrEmpty(Codes.ToString()) || Codes.ToString().Contains("*")) return;

            _Sql.Code_Column.Clear().Append(Codes);//记录列

            //将 Select 到 From 这一段 先移除 然后上新的字符串
            //var SqlString = _Sql.Code.ToString();
            //var _StartIndex = 0;
            //var _EndIndex = SqlString.IndexOf("FROM");

            //_Sql.Code.Remove(_StartIndex, _EndIndex);

            //_Sql.Code.Insert(0, "SELECT " + Codes + " ");
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

        private void AnalysisParameterExpression(ParameterExpression _ParameterExpression, List<string> column)
        {
            var _PropertyInfos = Parser.GetPropertyInfos(_ParameterExpression.Type);
            var _Table = Parser.GetTableInfo(_ParameterExpression.Type);
            for (int i = 0; i < _PropertyInfos.Length; i++)
            {
                //检测有无忽略字段
                if (_Table.Fields.FirstOrDefault(w => !w.IsColumn && w.FieldName == _PropertyInfos[i].Name) != null)
                    continue;

                column.Add(_PropertyInfos[i].Name);
            };
        }

    }
}