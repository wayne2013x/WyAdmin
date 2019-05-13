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
    using System.Collections;
    using System.Linq.Expressions;

    public class WhereAnalysis
    {
        public WhereAnalysis(Expression _Expression, SQL _Sql)
        {
            if (_Expression is LambdaExpression)
            {
                new WhereAnalysis((_Expression as LambdaExpression).Body, _Sql);
            }
            else if (_Expression is BinaryExpression)
            {
                this.DealBinaryExpression(_Expression as BinaryExpression, _Sql);
            }
            else if (_Expression is MemberExpression)
            {
                this.DealMemberExpression(_Expression as MemberExpression, _Sql);
            }
            else if (_Expression is ConstantExpression)
            {
                this.DealConstantExpression(_Expression as ConstantExpression, _Sql);
            }
            else if (_Expression is UnaryExpression)
            {
                this.DealUnaryExpression(_Expression as UnaryExpression, _Sql);
            }
            else if (_Expression is ParameterExpression)
            {
                this.DealParameterExpression(_Expression as ParameterExpression, _Sql);
            }
            else if (_Expression is NewArrayExpression)
            {
                this.DealNewArrayExpression(_Expression as NewArrayExpression, _Sql);
            }
            else if (_Expression is MethodCallExpression)
            {
                this.DealMethodCallExpression(_Expression as MethodCallExpression, _Sql);
            }
            else
            {
                throw new DbFrameException("无法解析的表达式！");
            }
        }

        private void DealBinaryExpression(BinaryExpression _Expression, SQL _Sql)
        {
            //检查括号
            this.CheckBrackets(_Expression.ToString(), _Sql, (_AddBrackets) =>
            {
                //左边
                this.CheckBrackets(_Expression.Left.ToString(), _Sql, (_AddBrackets_Left) =>
                {
                    new WhereAnalysis(_Expression.Left, _Sql);
                });

                _Sql.Code_Where.Append(this.GetOperatorStr(_Expression.NodeType));

                //右边
                this.CheckBrackets(_Expression.Right.ToString(), _Sql, (_AddBrackets_Right) =>
                {
                    new WhereAnalysis(_Expression.Right, _Sql);
                });
            });
        }

        private void DealMemberExpression(MemberExpression _Expression, SQL _Sql)
        {
            if (_Expression.Expression is ParameterExpression)
            {
                new WhereAnalysis(_Expression.Expression, _Sql);
                var _MemberName = DbSettings.KeywordHandle(_Expression.Member.Name);
                _Sql.Code_Where.Append(_MemberName);
            }
            else
            {
                if (_Expression.Expression == null)
                {
                    Eval(_Expression, _Sql);
                }
                else
                {
                    var typeName = _Expression.Expression.GetType().Name;
                    if (typeName == "TypedParameterExpression")
                        _Sql.Code_Where.Append(DbSettings.KeywordHandle(_Expression.Member.Name));
                    else
                        Eval(_Expression, _Sql);
                }

            }

        }

        private void DealConstantExpression(ConstantExpression _Expression, SQL _Sql)
        {
            this.Eval(_Expression, _Sql);
        }

        private void DealUnaryExpression(UnaryExpression _Expression, SQL _Sql)
        {
            new WhereAnalysis(_Expression.Operand, _Sql);
        }

        private void DealParameterExpression(ParameterExpression _Expression, SQL _Sql)
        {
            var _TabName = (_Expression as ParameterExpression).Name;
            _Sql.Code_Where.Append(this.AddAlias(_TabName, _Sql));
        }

        private void DealNewArrayExpression(NewArrayExpression _Expression, SQL _Sql)
        {
            StringBuilder tmpstr = new StringBuilder();
            foreach (Expression ex in _Expression.Expressions)
            {
                tmpstr.Append("'" + this.Eval(ex) + "'");
                tmpstr.Append(",");
            }
            this.AddParameter(tmpstr.ToString(0, tmpstr.Length - 1), _Sql);
        }

        private void DealMethodCallExpression(MethodCallExpression _Expression, SQL _Sql)
        {
            if (_Expression.Arguments.Count > 0)
            {
                var _Alias = string.Empty;
                var _MemberName = string.Empty;

                if (_Expression.Object == null)
                {
                    var _Member = (_Expression.Arguments[0] as MemberExpression);
                    var _MethodName = _Expression.Method.Name;
                    switch (_Expression.Method.Name)
                    {
                        case "Like":
                            _Alias = this.AddAlias((_Member.Expression as ParameterExpression).Name, _Sql);
                            _MemberName = DbSettings.KeywordHandle(_Member.Member.Name);
                            _Sql.Code_Where.Append(_Alias + _MemberName);
                            _Sql.Code_Where.Append(" LIKE ");
                            new WhereAnalysis(_Expression.Arguments[1], _Sql);
                            break;
                        case "In":
                            _Alias = this.AddAlias((_Member.Expression as ParameterExpression).Name, _Sql);
                            _MemberName = DbSettings.KeywordHandle(_Member.Member.Name);
                            _Sql.Code_Where.Append(_Alias + _MemberName);
                            _Sql.Code_Where.Append(" IN ( ");
                            new WhereAnalysis(_Expression.Arguments[1], _Sql);
                            _Sql.Code_Where.Append(" ) ");
                            break;
                        case "NotIn":
                            _Alias = this.AddAlias((_Member.Expression as ParameterExpression).Name, _Sql);
                            _MemberName = DbSettings.KeywordHandle(_Member.Member.Name);
                            _Sql.Code_Where.Append(_Alias + _MemberName);
                            _Sql.Code_Where.Append(" NOT IN ( ");
                            new WhereAnalysis(_Expression.Arguments[1], _Sql);
                            _Sql.Code_Where.Append(" ) ");
                            break;
                        case "SqlString":
                        case "SqlStringCompare":
                            _Alias = this.AddAlias((_Member.Expression as ParameterExpression).Name, _Sql);
                            _MemberName = DbSettings.KeywordHandle(_Member.Member.Name);
                            if (_Expression.Arguments[1] is ConstantExpression)
                            {
                                var _Code = (_Expression.Arguments[1] as ConstantExpression).Value;
                                if (_Code != null)
                                {
                                    _Code = _Code.ToString().Replace("@F", _Alias + _MemberName);
                                }
                                _Sql.Code_Where.Append(_Code);
                            }
                            else if (_Expression.Arguments[1] is BinaryExpression)
                            {
                                var _BinaryExpression = _Expression.Arguments[1] as BinaryExpression;

                                _Sql.Code_Where.Append(" ");
                                if (_BinaryExpression.NodeType == ExpressionType.Add)//如果是拼接字符串
                                {
                                    UnaryExpression cast = Expression.Convert(_BinaryExpression, typeof(object));
                                    var _Code = Expression.Lambda<Func<object>>(cast).Compile().Invoke();
                                    _Sql.Code_Where.Append(_Code.ToString().Replace("@F", _Alias + _MemberName));
                                }
                                else
                                {
                                    //这里将左右两边拼接
                                    this.SplitJoint(_BinaryExpression, _Sql, _Alias + _MemberName);
                                }
                            }
                            break;
                        default:
                            Eval(_Expression, _Sql);
                            break;
                    }
                }
                else if (_Expression.Object != null)
                {
                    dynamic _Member = _Expression.Object;
                    switch (_Expression.Method.Name)
                    {
                        case "StartsWith":
                            _Alias = this.AddAlias(_Member.Expression.Name, _Sql);
                            _MemberName = DbSettings.KeywordHandle(_Member.Member.Name);
                            _Sql.Code_Where.Append(_Alias + _MemberName);
                            _Sql.Code_Where.Append(" LIKE ");
                            new WhereAnalysis(_Expression.Arguments[0], _Sql);
                            _Sql.Code_Where.Append(" + '%' ");
                            break;
                        case "Contains":
                            _Alias = this.AddAlias(_Member.Expression.Name, _Sql);
                            _MemberName = DbSettings.KeywordHandle(_Member.Member.Name);
                            _Sql.Code_Where.Append(_Alias + _MemberName);
                            _Sql.Code_Where.Append(" LIKE '%' + ");
                            new WhereAnalysis(_Expression.Arguments[0], _Sql);
                            _Sql.Code_Where.Append(" + '%' ");
                            break;
                        case "EndsWith":
                            _Alias = this.AddAlias(_Member.Expression.Name, _Sql);
                            _MemberName = DbSettings.KeywordHandle(_Member.Member.Name);
                            _Sql.Code_Where.Append(_Alias + _MemberName);
                            _Sql.Code_Where.Append(" LIKE '%' + ");
                            new WhereAnalysis(_Expression.Arguments[0], _Sql);
                            break;
                        case "Equals":
                            _Alias = this.AddAlias(_Member.Expression.Name, _Sql);
                            _MemberName = DbSettings.KeywordHandle(_Member.Member.Name);
                            _Sql.Code_Where.Append(_Alias + _MemberName);
                            _Sql.Code_Where.Append(" = ");
                            new WhereAnalysis(_Expression.Arguments[0], _Sql);
                            break;
                        default:
                            Eval(_Expression, _Sql);
                            break;
                    }
                }
            }
            else
            {
                Eval(_Expression, _Sql);
            }

        }

        private string GetOperatorStr(ExpressionType _ExpressionType)
        {
            switch (_ExpressionType)
            {
                case ExpressionType.Or:
                case ExpressionType.OrElse: return " OR ";
                case ExpressionType.And:
                case ExpressionType.AndAlso: return " AND ";
                case ExpressionType.GreaterThan: return " > ";
                case ExpressionType.GreaterThanOrEqual: return " >= ";
                case ExpressionType.LessThan: return " < ";
                case ExpressionType.LessThanOrEqual: return " <= ";
                case ExpressionType.Equal: return " = ";
                case ExpressionType.NotEqual: return " <> ";
                case ExpressionType.Add: return " + ";
                case ExpressionType.Subtract: return " - ";
                case ExpressionType.Multiply: return " * ";
                case ExpressionType.Divide: return " / ";
                case ExpressionType.Modulo: return " % ";
                default: throw new DbFrameException("无法解析的表达式！");
            }
        }

        private void Eval(Expression _Expression, SQL _Sql)
        {
            UnaryExpression cast = Expression.Convert(_Expression, typeof(object));
            var obj = Expression.Lambda<Func<object>>(cast).Compile().Invoke();
            if (obj == null)
            {
                this.AddParameter(obj, _Sql);
            }
            else
            {
                var type = obj.GetType();
                if (type.Name == "List`1") //list集合
                {
                    var list = obj as IEnumerable;
                    var index = 0;
                    foreach (var item in list)
                    {
                        this.AddParameter(item, _Sql);
                        index = _Sql.Code_Where.Length;
                        _Sql.Code_Where.Append(",");
                    }
                    _Sql.Code_Where.Remove(index, 1);
                }
                else
                    this.AddParameter(obj, _Sql);
            }
        }

        /// <summary>
        /// 计算值
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public object Eval(Expression expression)
        {
            UnaryExpression cast = Expression.Convert(expression, typeof(object));
            var obj = Expression.Lambda<Func<object>>(cast).Compile().Invoke();
            return obj != null ? GetValueFormat(obj) : obj;
        }


        /// <summary>
        /// 对某些值 特殊处理
        /// </summary>
        /// <param name="obj"></param>
        private object GetValueFormat(object obj)
        {
            var type = obj.GetType();
            if (type.Name == "List`1") //list集合
            {
                var data = new List<string>();
                var list = obj as IEnumerable;
                string sql = string.Empty;
                foreach (var item in list)
                {
                    if (item == null) continue;
                    data.Add(item.ToString());
                }
                sql = string.Join(",", data);
                return sql;
            }
            return obj;
        }

        /// <summary>
        /// 字符串拼接
        /// </summary>
        /// <param name="_BinaryExpression"></param>
        /// <param name="_ParserArgs"></param>
        /// <param name="_Name"></param>
        private void SplitJoint(BinaryExpression _BinaryExpression, SQL _Sql, string _Name)
        {
            var _Left = _BinaryExpression.Left;
            var _Right = _BinaryExpression.Right;

            if (_BinaryExpression.NodeType != ExpressionType.Add) throw new DbFrameException("无法解析的表达式！");

            if (_Left is ConstantExpression && _Left.Type == typeof(string))
            {
                var _ConstantExpression = _Left as ConstantExpression;
                if (_ConstantExpression.Value != null)
                {
                    _Sql.Code_Where.Append(_ConstantExpression.Value.ToString().Replace("@F", _Name));
                }
            }

            //如果右边还有 BinaryExpression 树类型 递归下去
            if (_Right is BinaryExpression)
            {
                this.SplitJoint(_Right as BinaryExpression, _Sql, _Name);
            }
            else if (_Right is ConstantExpression)
            {
                var _ConstantExpression = _Right as ConstantExpression;
                if (_ConstantExpression.Value != null)
                {
                    _Sql.Code_Where.Append(_ConstantExpression.Value.ToString().Replace("@F", _Name));
                }
            }
            else
            {
                this.Eval(_Right, _Sql);
            }
        }

        /// <summary>
        /// 检查是否有 括号
        /// </summary>
        /// <param name="_Expression"></param>
        /// <param name="_ParserArgs"></param>
        /// <param name="_Action"></param>
        private void CheckBrackets(string _Str, SQL _Sql, Action<bool> _Action)
        {
            //检查是否有括号
            var _AddBrackets = !string.IsNullOrEmpty(_Str) && _Str.Length > 5 && _Str.StartsWith("((") && _Str.EndsWith("))");

            if (_AddBrackets) _Sql.Code_Where.Append(" (");

            _Action(_AddBrackets);

            if (_AddBrackets) _Sql.Code_Where.Append(" )");
        }

        /// <summary> 
        /// 追加参数
        /// </summary>
        private void AddParameter(object obj, SQL _Sql)
        {
            if (obj == null || obj == DBNull.Value)
            {
                _Sql.Code_Where.Append("NULL");
                _Sql.Code_Where.Replace(" = NULL", " IS NULL ");
                _Sql.Code_Where.Replace(" <> NULL", " IS NOT NULL ");
            }
            else
            {
                string name = "Param_" + _Sql.Parameter.Count;
                _Sql.Parameter.Add(new DbParam() { ParameterName = name, Value = obj });
                _Sql.Code_Where.Append('@');
                _Sql.Code_Where.Append(name);
            }
        }

        /// <summary>
        /// 追加别名
        /// </summary>
        private string AddAlias(string Alias, SQL _Sql)
        {
            if (_Sql.IsAlias)
                return DbSettings.KeywordHandle(Alias) + ".";
            return "";
        }

    }
}
