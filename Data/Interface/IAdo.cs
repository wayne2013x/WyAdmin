using System;
using System.Collections.Generic;

namespace DataFramework.Interface
{
    using System.Data;

    public interface IAdo
    {
        /// <summary>
        /// 连接对象
        /// </summary>
        /// <returns></returns>
        //IDbConnection GetDbConnection();

        /// <summary>
        /// 执行 Insert Delete Update
        /// </summary>
        /// <param name="SqlStr"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        int Execute(string SqlStr, object Param);
        /// <summary>
        /// 执行 Insert Delete Update 并且 返回 值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SqlStr"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        T ExecuteScalar<T>(string SqlStr, object Param);
        /// <summary>
        /// 执行 Insert Delete Update 并且 返回 值
        /// </summary>
        /// <param name="SqlStr"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        object ExecuteScalar(string SqlStr, object Param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
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
        IEnumerable<T> QueryBySql<T>(string SqlStr, object Param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);
        /// <summary>
        /// 执行查询 得到 DataTable
        /// </summary>
        /// <param name="SqlStr"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        DataTable QueryDataTable(string SqlStr, object Param);
        /// <summary>
        /// 查询单行单列数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SqlStr"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        T QuerySingleOrDefault<T>(string SqlStr, object Param);
        /// <summary>
        /// 查询单行 数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SqlStr"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        T QueryFirstOrDefault<T>(string SqlStr, object Param);
        /// <summary>
        /// ExecuteReader
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        IDataReader ExecuteReader(string SqlStr, object Param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        /// <summary>
        /// 提交事务
        /// </summary>
        /// <param name="_Action"></param>
        /// <returns></returns>
        bool Commit(Action _Action);

    }
}
