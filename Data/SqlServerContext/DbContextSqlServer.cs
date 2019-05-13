
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataFramework.SqlServerContext
{
    using CodeAnalysis;
    using DataFramework.Interface;
    using DataFramework.Abstract;
    using Achieve;
    using System.Data;
    using DataFramework.Class;

    /// <summary>
    /// SqlServer 实现
    /// </summary>
    public class DbContextSqlServer : DataFramework.Context.BaseDb
    {

        public DbContextSqlServer(PagingMode pagingMode = PagingMode.ROW_NUMBER)
        {
            _PagingMode = pagingMode;        
            DbSettings.KeywordHandle = (Keyword) => "[" + Keyword + "]";
            this.Ado = new AdoAchieve(this.ConnectionString);
            this.analysis = new Analysis(this.Ado, LastInsertId, DbContextType.SqlServer);
        }

        public DbContextSqlServer(string _ConnectionString, PagingMode pagingMode = PagingMode.ROW_NUMBER) : base(_ConnectionString)
        {
            _PagingMode = pagingMode;
            DbSettings.KeywordHandle = (Keyword) => "[" + Keyword + "]";
            this.Ado = new AdoAchieve(this.ConnectionString);
            this.analysis = new Analysis(this.Ado, LastInsertId, DbContextType.SqlServer);
        }

        protected new string LastInsertId = "SELECT @@IDENTITY;";//"SELECT SCOPE_IDENTITY();";

        /// <summary>
        /// 分页方式
        /// </summary>
        public static PagingMode _PagingMode { get; set; }

        /// <summary>
        /// 设置默认连接字符串
        /// </summary>
        /// <param name="_ConnectionString"></param>
        public static void SetDefaultConnectionString(string _ConnectionString)
        {
            DbSettings.DefaultConnectionString = _ConnectionString;
        }

    }

    /// <summary>
    /// 分页模式
    /// </summary>
    public enum PagingMode
    {
        /// <summary>
        /// sqlserver 常规分页
        /// </summary>
        ROW_NUMBER,
        /// <summary>
        /// sqlserver 2012 以上支持
        /// </summary>
        OFFSET

    }




}
