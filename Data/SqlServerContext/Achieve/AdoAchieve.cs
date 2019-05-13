using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFramework.SqlServerContext.Achieve
{
    //
    using Dapper;
    using System.Data;
    using System.Data.SqlClient;
    using DataFramework.Abstract;

    public class AdoAchieve : AbstractAdo
    {

        public AdoAchieve(string ConnectionString) : base(ConnectionString)
        {

        }

        /// <summary>
        /// 数据库 连接对象
        /// </summary>
        /// <returns></returns>
        public override IDbConnection GetDbConnection()
        {
            return new SqlConnection(this._ConnectionString);
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <param name="_Action"></param>
        /// <returns></returns>
        public override bool Commit(Action _Action)
        {
            _DbConnection = this.GetDbConnection();
            if (_DbConnection.State == ConnectionState.Closed) _DbConnection.Open();
            using (_DbTransaction = _DbConnection.BeginTransaction())
            {
                try
                {
                    //事务 状态设置 开
                    this.CommitState = true;

                    _Action?.Invoke();

                    _DbTransaction.Commit();
                    //事务 状态设置 关
                    this.CommitState = false;
                    return true;
                }
                catch (Exception ex)
                {
                    _DbTransaction.Rollback();
                    //事务 状态设置 关
                    this.CommitState = false;
                    throw ex;
                }
                finally
                {
                    //事务 状态设置 关
                    this.CommitState = false;

                    _DbConnection.Close();
                }
            }
        }

        /// <summary>
        /// 执行 Insert Delete Update
        /// </summary>
        /// <param name="SqlStr"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public override int Execute(string SqlStr, object Param)
        {
            return this.GetDbConnection().Execute(SqlStr, Param);
        }

        /// <summary>
        /// 执行 Insert Delete Update 并且 返回 值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SqlStr"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public override T ExecuteScalar<T>(string SqlStr, object Param)
        {
            return this.GetDbConnection().ExecuteScalar<T>(SqlStr, Param);
        }

        /// <summary>
        /// 执行 Insert Delete Update 并且 返回 值
        /// </summary>
        /// <param name="SqlStr"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public override object ExecuteScalar(string SqlStr, object Param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return this.GetDbConnection().ExecuteScalar(SqlStr, Param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// IDataReader 可执行存储过程
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public override IDataReader ExecuteReader(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return this.GetDbConnection().ExecuteReader(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public override IEnumerable<T> QueryBySql<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (typeof(T).Name == new Dictionary<string, object>().GetType().Name)
            {
                return (IEnumerable<T>)(this.QueryDataTable(sql, param).ToList());
            }
            else
            {
                return this.GetDbConnection().Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
            }
        }

        /// <summary>
        /// 获取 DataTable
        /// </summary>
        /// <param name="SqlStr"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public override DataTable QueryDataTable(string SqlStr, object Param)
        {
            IDataReader _IDataReader = null;
            if (Param == null)
                _IDataReader = this.GetDbConnection().ExecuteReader(SqlStr);
            else
                _IDataReader = this.GetDbConnection().ExecuteReader(SqlStr, Param);
            return _IDataReader.ToDataTable();
        }

        /// <summary>
        /// 查询单行单列数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SqlStr"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public override T QueryFirstOrDefault<T>(string SqlStr, object Param)
        {
            return this.GetDbConnection().QuerySingleOrDefault<T>(SqlStr, Param);
        }

        /// <summary>
        /// 查询单行 数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SqlStr"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public override T QuerySingleOrDefault<T>(string SqlStr, object Param)
        {
            return this.GetDbConnection().QueryFirstOrDefault<T>(SqlStr, Param);
        }


    }
}
