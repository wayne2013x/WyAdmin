using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFramework.Abstract
{
    using System.Data;
    using Interface;

    public abstract class AbstractAdo : IAdo
    {
        public AbstractAdo(string ConnectionString)
        {
            this.CommitState = false;
            this._ConnectionString = ConnectionString;
        }
        public bool CommitState { get; set; }
        public string _ConnectionString { get; set; }
        public IDbConnection _DbConnection { get; set; }
        public IDbTransaction _DbTransaction { get; set; }        
        public abstract bool Commit(Action _Action);
        public abstract int Execute(string SqlStr, object Param);
        public abstract IDataReader ExecuteReader(string SqlStr, object Param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        public abstract T ExecuteScalar<T>(string SqlStr, object Param);
        public abstract IDbConnection GetDbConnection();
        public abstract IEnumerable<T> QueryBySql<T>(string SqlStr, object Param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);
        public abstract DataTable QueryDataTable(string SqlStr, object Param);
        public abstract T QueryFirstOrDefault<T>(string SqlStr, object Param);
        public abstract T QuerySingleOrDefault<T>(string SqlStr, object Param);
        public abstract object ExecuteScalar(string SqlStr, object Param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
    }
}
