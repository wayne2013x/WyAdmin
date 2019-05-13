
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFramework.Context
{
    using DataFramework.Abstract;
    using DataFramework.Class;
    using DataFramework.CodeAnalysis;
    using DataFramework.Interface;
    using System.Data;
    using System.Linq.Expressions;

    public class BaseDb : AbstractDb
    {
        public string ErrorMessge { get; set; } = "操作失败";

        protected Analysis analysis { get; set; }

        public BaseDb()
        {

        }

        public BaseDb(string _ConnectionString) : base(_ConnectionString)
        {

        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public override object Insert<T>(T Entity)
        {
            return analysis.Insert<T>(Parser.ModelToMemberInitExpression(Entity));
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public override object Insert<T>(Expression<Func<T>> Entity)
        {
            return analysis.Insert<T>(Entity.Body as MemberInitExpression);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Entity"></param>
        /// <param name="Where"></param>
        /// <returns></returns>
        public override bool Update<T>(Expression<Func<T, bool>> Where, T Entity)
        {
            return analysis.Update<T>(Parser.ModelToMemberInitExpression(Entity), Where);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Entity"></param>
        /// <param name="Where"></param>
        /// <returns></returns>
        public override bool Update<T>(Expression<Func<T, bool>> Where, Expression<Func<T>> Entity)
        {
            return analysis.Update<T>(Entity.Body as MemberInitExpression, Where);
        }

        /// <summary>
        /// UpdateById
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public override bool UpdateById<T>(T Entity)
        {
            var _Table = Parser.GetTableInfo(Entity);
            var _TableName = _Table.TableName;
            var _KeyInfo = _Table.KeyFieldInfo;
            var Where = Parser.WhereById<T>(_KeyInfo.FieldName, _KeyInfo.Value, _TableName);
            return analysis.Update<T>(Parser.ModelToMemberInitExpression(Entity), Where);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Where"></param>
        /// <returns></returns>
        public override bool Delete<T>(Expression<Func<T, bool>> Where)
        {
            return analysis.Delete(Where);
        }

        /// <summary>
        /// DeleteById
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Id"></param>
        /// <returns></returns>
        public override bool DeleteById<T>(object Id)
        {
            var _Table = Parser.GetTableInfo(typeof(T));
            var _TableName = _Table.TableName;
            var Where = Parser.WhereById<T>(_Table.KeyFieldName, Id, _TableName);
            return analysis.Delete(Where);
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <param name="_Action"></param>
        /// <returns></returns>
        public override bool Commit(Action _Action)
        {
            return this.Ado.Commit(_Action);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Alias">表别名</param>
        /// <returns></returns>
        public override IQuery<T> Query<T>()
        {
            var _Sql = new SQL();
            return new SqlServerContext.Achieve.QueryAchieve<T>(_Sql, this.Ado, this.analysis);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Where"></param>
        /// <returns></returns>
        public override IQuery<T> Query<T>(Expression<Func<T, bool>> Where)
        {
            return this.Query<T>().Where(Where);
        }

        public override T Find<T>(Expression<Func<T, bool>> Where)
        {
            return this.Query<T>(Where).Frist<T>();
        }

        public override T FindById<T>(object Id)
        {
            var _ParName = typeof(T).Name.ToLower();
            var _KeyName = Parser.GetTableInfo(typeof(T)).KeyFieldName;

            return this.Query<T>(Parser.WhereById<T>(_KeyName, Id, _ParName)).Frist<T>();
        }

        public override List<T> FindList<T>(Expression<Func<T, bool>> Where)
        {
            var _List_T = this.Query<T>(Where).ToList<T>();
            if (_List_T == null)
                return new List<T>();
            return _List_T.ToList();
        }

        /* Ado 执行 Sql 语句*/
        public override int Execute(string SqlStr, object Param)
        {
            return this.Ado.Execute(SqlStr, Param);
        }

        public override T ExecuteScalar<T>(string SqlStr, object Param)
        {
            return this.Ado.ExecuteScalar<T>(SqlStr, Param);
        }

        public override object ExecuteScalar(string SqlStr, object Param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return this.Ado.ExecuteReader(SqlStr, Param, transaction, commandTimeout, commandType);
        }

        public override IEnumerable<T> QueryBySql<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return this.Ado.QueryBySql<T>(sql, param, transaction, buffered, commandTimeout, commandType);
        }

        public override DataTable QueryDataTable(string SqlStr, object Param)
        {
            return this.Ado.QueryDataTable(SqlStr, Param);
        }

        public override T QuerySingleOrDefault<T>(string SqlStr, object Param)
        {
            return this.Ado.QuerySingleOrDefault<T>(SqlStr, Param);
        }

        public override T QueryFirstOrDefault<T>(string SqlStr, object Param)
        {
            return this.Ado.QueryFirstOrDefault<T>(SqlStr, Param);
        }

        public override IDataReader ExecuteReader(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return this.Ado.ExecuteReader(sql, param, transaction, commandTimeout, commandType);
        }


    }
}
