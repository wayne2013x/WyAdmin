using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFramework.CodeAnalysis
{
    //
    using System.Linq.Expressions;
    using DataFramework.Abstract;
    using DataFramework.Class;
    using Dapper;
    using DataFramework.Interface;

    public class Analysis : IAnalysis
    {
        protected string LastInsertId { get; }

        protected AbstractAdo Ado { get; }

        public DbContextType _DbContextType { get; set; }

        public Analysis(AbstractAdo abstractAdo, string lastInsertId, DbContextType dbContextType)
        {
            this.Ado = abstractAdo;
            this.LastInsertId = lastInsertId;
            this._DbContextType = dbContextType;
        }

        /// <summary>
        /// Insert 解析并执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_MemberInitExpression"></param>
        /// <param name="LastInsertId"></param>
        /// <returns></returns>
        public object Insert<T>(MemberInitExpression _MemberInitExpression)
        {
            var _Sql_Tuple = new InsertAnalysis<T>().Create(_MemberInitExpression, LastInsertId, this);

            var _Sql = _Sql_Tuple.Item1;
            object _KeyId = null;
            //如果开启了 Commit 状态
            if (Ado.CommitState)
                _KeyId = Ado._DbConnection.ExecuteScalar(_Sql.Code.ToString(), _Sql.GetDynamicParameters(), Ado._DbTransaction);
            else
                _KeyId = Ado.ExecuteScalar(_Sql.Code.ToString(), _Sql.GetDynamicParameters());
            if (_KeyId == null)
                return _Sql_Tuple.Item2;
            else
                return _KeyId;
        }

        /// <summary>
        /// Update 解析并执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_MemberInitExpression"></param>
        /// <param name="Where"></param>
        /// <param name="Ado"></param>
        /// <returns></returns>
        public bool Update<T>(MemberInitExpression _MemberInitExpression, Expression<Func<T, bool>> Where)
        {
            var _Sql = new UpdateAnalysis<T>().Create(_MemberInitExpression, Where, this);
            return Execute(_Sql);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Where"></param>
        /// <param name="Ado"></param>
        /// <returns></returns>
        public bool Delete<T>(Expression<Func<T, bool>> Where)
        {
            var _Sql = new DeleteAnalysis<T>().Create(Where, this);
            return Execute(_Sql);
        }

        /// <summary>
        /// 修改 删除 执行
        /// </summary>
        /// <param name="Ado"></param>
        /// <param name="_Sql"></param>
        /// <returns></returns>
        private bool Execute(SQL _Sql)
        {
            int _Count = 0;
            //如果开启了 Commit 状态
            if (Ado.CommitState)
                _Count = Ado._DbConnection.Execute(_Sql.Code.ToString(), _Sql.GetDynamicParameters(), Ado._DbTransaction);
            else
                _Count = Ado.Execute(_Sql.Code.ToString(), _Sql.GetDynamicParameters());

            return _Count > 0;
        }

        /// <summary>
        /// 获取 Count 
        /// </summary>
        /// <param name="Ado"></param>
        /// <param name="_Sql"></param>
        public int Count(SQL _Sql)
        {
            var _New_Sql = new CountAnalysis().Create(_Sql);
            Analysis.ToSql(_New_Sql, Ado.CommitState);
            return Ado.ExecuteScalar<int>(_New_Sql.Code.ToString(), _Sql.GetDynamicParameters());
        }

        /// <summary>
        /// Max
        /// </summary>
        /// <param name="_LambdaExpression"></param>
        /// <param name="Ado"></param>
        /// <param name="_Sql"></param>
        /// <returns></returns>
        public T Max<T>(LambdaExpression _LambdaExpression, SQL _Sql)
        {
            var _New_Sql = new Max_Min_Sum_Analysis().CreateMax(_LambdaExpression, _Sql);
            Analysis.ToSql(_New_Sql, Ado.CommitState);
            return Ado.ExecuteScalar<T>(_New_Sql.Code.ToString(), _Sql.GetDynamicParameters());
        }

        /// <summary>
        /// Max
        /// </summary>
        /// <param name="_LambdaExpression"></param>
        /// <param name="Ado"></param>
        /// <param name="_Sql"></param>
        /// <returns></returns>
        public T Min<T>(LambdaExpression _LambdaExpression, SQL _Sql)
        {
            var _New_Sql = new Max_Min_Sum_Analysis().CreateMin(_LambdaExpression, _Sql);
            Analysis.ToSql(_New_Sql, Ado.CommitState);
            return Ado.ExecuteScalar<T>(_New_Sql.Code.ToString(), _Sql.GetDynamicParameters());
        }

        /// <summary>
        /// Max
        /// </summary>
        /// <param name="_LambdaExpression"></param>
        /// <param name="Ado"></param>
        /// <param name="_Sql"></param>
        /// <returns></returns>
        public T Sum<T>(LambdaExpression _LambdaExpression, SQL _Sql)
        {
            var _New_Sql = new Max_Min_Sum_Analysis().CreateSum(_LambdaExpression, _Sql);
            Analysis.ToSql(_New_Sql, Ado.CommitState);
            return Ado.ExecuteScalar<T>(_New_Sql.Code.ToString(), _Sql.GetDynamicParameters());
        }



















































        /// <summary>
        /// 创建 Query 对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_LambdaExpression"></param>
        /// <param name="_Sql"></param>
        public void CreateQuery<T>(SQL _Sql)
        {
            new QueryAnalysis<T>().Create(_Sql);
        }

        /// <summary>
        /// 创建 GroupBy 对象
        /// </summary>
        /// <param name="_LambdaExpression"></param>
        /// <param name="_Sql"></param>
        public void CreateGroupBy(LambdaExpression _LambdaExpression, SQL _Sql)
        {
            new GroupByAnalysis().Create(_Sql, _LambdaExpression);
        }

        /// <summary>
        /// 创建 OrderBy 对象
        /// </summary>
        /// <param name="_LambdaExpression"></param>
        /// <param name="_Sql"></param>
        public void CreateOrderBy(LambdaExpression _LambdaExpression, SQL _Sql)
        {
            new OrderByAnalysis().Create(_Sql, _LambdaExpression);
        }

        /// <summary>
        /// 创建 OrderBy  desc 对象
        /// </summary>
        /// <param name="_LambdaExpression"></param>
        /// <param name="_Sql"></param>
        public void CreateOrderByDESC(LambdaExpression _LambdaExpression, SQL _Sql)
        {
            new OrderByAnalysis().CreateDESC(_Sql, _LambdaExpression);
        }

        /// <summary>
        /// 创建 Select 对象
        /// </summary>
        /// <param name="_LambdaExpression"></param>
        /// <param name="_Sql"></param>
        public void CreateSelect(LambdaExpression _LambdaExpression, SQL _Sql)
        {
            new SelectAnalysis().Create(_Sql, _LambdaExpression);
        }

        /// <summary>
        /// 创建 Where 对象
        /// </summary>
        /// <param name="_LambdaExpression"></param>
        /// <param name="_Sql"></param>
        public void CreateWhere(LambdaExpression _LambdaExpression, SQL _Sql)
        {
            new WhereAnalysis(_LambdaExpression, _Sql);
        }

        /// <summary>
        /// 创建 Join 对象
        /// </summary>
        /// <param name="_LambdaExpression"></param>
        /// <param name="_Sql"></param>
        /// <param name="_EJoinType"></param>
        public void CreateJoin(LambdaExpression _LambdaExpression, SQL _Sql, EJoinType _EJoinType)
        {
            new JoinAnalysis().Create(_Sql, _LambdaExpression, _EJoinType, this);
        }

        /// <summary>
        /// 创建 TOP 对象
        /// </summary>
        /// <param name="Top"></param>
        /// <param name="_Sql"></param>
        public void CreateTop(int Top, SQL _Sql)
        {
            new TopAnalysis().Create(_Sql, Top, this);
        }

        /// <summary>
        /// 创建 Distinct 对象
        /// </summary>
        /// <param name="_Sql"></param>
        public void CreateDistinct(SQL _Sql)
        {
            new DistinctAnalysis().Create(_Sql);
        }

        /// <summary>
        /// 创建 分页 对象
        /// </summary>
        /// <param name="_Sql"></param>
        /// <param name="PageNumber"></param>
        /// <param name="PageSize"></param>
        public void CreateTakePage(SQL _Sql, int PageNumber, int PageSize)
        {
            new TakePageAnalysis().Create(_Sql, PageNumber, PageSize, this);
        }

        /// <summary>
        /// SQL 对象中 sql 语句 转换为String
        /// </summary>
        /// <param name="_Sql"></param>
        /// <returns></returns>
        public static string SqlToString(SQL _Sql)
        {
            if (string.IsNullOrEmpty(_Sql.Code.ToString()))
            {
                ToSql(_Sql, false);
            }

            var _SqlCode = _Sql.Code.ToString();

            if (string.IsNullOrEmpty(_SqlCode)) return null;

            if (_Sql.Parameter == null) return _SqlCode;

            foreach (var item in _Sql.Parameter)
            {
                _SqlCode = _SqlCode.Replace("@" + item.ParameterName, item.Value == null ? null : "'" + item.Value.ToString() + "' ");
            }

            return _SqlCode;
        }

        /// <summary>
        /// 将 SQL 对象 中的 代码 组装
        /// </summary>
        /// <param name="_Sql"></param>
        public static void ToSql(SQL _Sql, bool _WithNolock)
        {
            //组装Sql
            _Sql.Code.Clear();

            //ROWS FETCH 分页
            var _Code_TakePage_String = _Sql.Code_TakePage.ToString();

            if (!string.IsNullOrEmpty(_Code_TakePage_String) && !_Code_TakePage_String.Contains("#SqlString#"))
            {
                _Sql.Code_OrderBy.Append(_Code_TakePage_String);
            }

            //组装
            _Sql.Code.AppendFormat(" SELECT {0} FROM {1}", _Sql.Code_Column, _Sql.Code_FromTab);

            if (!string.IsNullOrEmpty(_Sql.Code_Join.ToString()))
            {
                _Sql.Code.AppendFormat(" {0}", _Sql.Code_Join);
            }

            if (!string.IsNullOrEmpty(_Sql.Code_Where.ToString()))
            {
                _Sql.Code.AppendFormat(" WHERE 1=1 {0}", _Sql.Code_Where);
            }

            if (!string.IsNullOrEmpty(_Sql.Code_OrderBy.ToString()))
            {
                _Sql.Code.AppendFormat(" ORDER BY {0}", _Sql.Code_OrderBy);
            }

            if (!string.IsNullOrEmpty(_Sql.Code_GroupBy.ToString()))
            {
                _Sql.Code.AppendFormat(" GROUP BY {0}", _Sql.Code_GroupBy);
            }

            //ROW_NUMBER 分页

            if (!string.IsNullOrEmpty(_Code_TakePage_String) && _Code_TakePage_String.Contains("#SqlString#"))
            {
                _Code_TakePage_String = _Code_TakePage_String.Replace("#SqlString#", _Sql.Code.ToString());

                _Sql.Code.Clear().Append(_Code_TakePage_String);
            }

            //如果开启事务 则禁止 查询锁表
            //if (_WithNolock)
            //{
            //    _Sql.Code.Replace("(# WITH(NOLOCK) #)", " WITH(NOLOCK) ");
            //}
            //else
            //{
            //    _Sql.Code.Replace("(# WITH(NOLOCK) #)", "");
            //}

        }

    }


}
