
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataFramework.Abstract
{
    using System.Data;
    using DataFramework.Class;
    using DataFramework.Interface;

    public abstract class AbstractDb : IDb//, IDisposable
    {
        protected string ConnectionString { get; set; }
        protected AbstractAdo Ado { get; set; }

        public AbstractDb()
        {
            if (string.IsNullOrEmpty(DbSettings.DefaultConnectionString))
                throw new DbFrameException("请在您的程序启动时 调用 SetDefaultConnectionString(string _ConnectionString) 函数 设置默认连接字符串！");

            this.ConnectionString = DbSettings.DefaultConnectionString;
        }
        public AbstractDb(string _ConnectionString)
        {
            if (string.IsNullOrEmpty(_ConnectionString))
                throw new DbFrameException("传入参数 _ConnectionString 不能为空！");

            this.ConnectionString = _ConnectionString;
        }
        /// <summary>
        /// 最后一次 插入 ID 这里一般设置 字符串即可
        /// </summary>
        protected string LastInsertId = string.Empty;
        /*Insert*/
        public abstract object Insert<T>(T Entity);
        public abstract object Insert<T>(Expression<Func<T>> Entity);
        /*Update*/
        public abstract bool Update<T>(Expression<Func<T, bool>> Where, T Entity);
        public abstract bool Update<T>(Expression<Func<T, bool>> Where, Expression<Func<T>> Entity);
        public abstract bool UpdateById<T>(T Entity);
        /*Delete*/
        public abstract bool Delete<T>(Expression<Func<T, bool>> Where);
        public abstract bool DeleteById<T>(object Id);
        /*Select*/
        public abstract IQuery<T> Query<T>();
        public abstract IQuery<T> Query<T>(Expression<Func<T, bool>> Where);
        public abstract bool Commit(Action _Action);
        public abstract T Find<T>(Expression<Func<T, bool>> Where);
        public abstract T FindById<T>(object Id);
        public abstract List<T> FindList<T>(Expression<Func<T, bool>> Where);
        /*Select By Sql String */
        public abstract int Execute(string SqlStr, object Param);
        public abstract T ExecuteScalar<T>(string SqlStr, object Param);
        public abstract object ExecuteScalar(string SqlStr, object Param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        public abstract IEnumerable<T> QueryBySql<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);
        public abstract DataTable QueryDataTable(string SqlStr, object Param);
        public abstract T QuerySingleOrDefault<T>(string SqlStr, object Param);
        public abstract T QueryFirstOrDefault<T>(string SqlStr, object Param);
        public abstract IDataReader ExecuteReader(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

    }

}
