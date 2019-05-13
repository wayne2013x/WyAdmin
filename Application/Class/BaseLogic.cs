using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Class
{
    using System.Data;
    using System.Data.SqlClient;
    using System.Data.Common;
    using Entity.SysClass;
    using Entity.Class;
    using Common;
    using System.Reflection;
    using System.Linq.Expressions;
    using DataFramework;
    using DataFramework.Class;
    using DataFramework.SqlServerContext;
    using DataFramework.Interface;
    using Dapper;

    public class BaseLogic<T>
    {
        protected string ErrorMessge = "操作失败！";

        /// <summary>
        /// 登录 信息 对象
        /// </summary>
        protected Account _Account = new Account();

        public BaseLogic()
        {
            _Account = this.GetSession<Account>("Account");
        }

        protected DbContextSqlServer db = new DbContextSqlServer();

        public void SetSession(string key, object value)
        {
            Tools.SetSession(key, value);
        }

        public TResult GetSession<TResult>(string key)
        {
            return Tools.GetSession<TResult>(key);
        }

        /// <summary>
        /// 将多个实体组合成为一个 字典类型
        /// </summary>
        /// <param name="di"></param>
        /// <returns></returns>
        public Dictionary<string, object> EntityToDictionary(Dictionary<string, object> di)
        {
            Dictionary<string, object> r = new Dictionary<string, object>();
            foreach (var item in di)
            {
                if (item.Value is Entity.Class.BaseClass)
                {
                    Parser.GetPropertyInfos(item.Value.GetType()).ToList().ForEach(pi =>
                    {
                        if (pi.GetValue(item.Value, null) == null)
                            r.Add(pi.Name, null);
                        else
                        {
                            if (pi.PropertyType == typeof(DateTime))
                                r.Add(pi.Name, pi.GetValue(item.Value, null).ToDateTimeFormat("yyyy-MM-dd HH:mm:ss"));
                            else
                                r.Add(pi.Name, pi.GetValue(item.Value, null));
                        }
                    });
                }
                else
                {
                    r.Add(item.Key, item.Value);
                }
            }
            return r;
        }

        /// <summary>
        /// 分页查询 通过 存储过程 PROC_SPLITPAGE
        /// </summary>
        /// <param name="SqlStr"></param>
        /// <param name="Page"></param>
        /// <param name="Rows"></param>
        /// <param name="Param"></param>
        /// <param name="ArryEntity"></param>
        /// <returns></returns>
        private PagingEntity GetPagingEntity(string SqlStr, int Page, int Rows, DbParameter[] Param, params BaseClass[] ArryEntity)
        {
            var _PagingEntity = new PagingEntity();

            //解析参数
            foreach (var item in Param)
            {
                SqlStr = SqlStr.Replace("@" + item.ParameterName, item.Value == null ? null : "'" + item.Value + "' ");
            }

            var _DynamicParameters = new DynamicParameters();
            _DynamicParameters.Add("@SQL", SqlStr, DbType.String, ParameterDirection.Input);
            _DynamicParameters.Add("@PAGE", Page, DbType.Int32, ParameterDirection.Input);
            _DynamicParameters.Add("@PAGESIZE", Rows, DbType.Int32, ParameterDirection.Input);
            _DynamicParameters.Add("@PAGECOUNT", 0, DbType.Int32, ParameterDirection.Output);
            _DynamicParameters.Add("@RECORDCOUNT", 0, DbType.Int32, ParameterDirection.Output);

            var _IDataReader = db.ExecuteReader("PROC_SPLITPAGE", _DynamicParameters, null, 30, CommandType.StoredProcedure);
            //将 IDataReader 对象转换为 DataSet 
            DataSet _DataSet = new AdoExtend.HZYDataSet();
            _DataSet.Load(_IDataReader, LoadOption.OverwriteChanges, null, new DataTable[] { });

            if (_DataSet.Tables.Count == 2)
            {
                var _Table = _DataSet.Tables[1];
                var _Total = _DynamicParameters.Get<int>("@RECORDCOUNT");
                _PagingEntity.Table = _Table;
                _PagingEntity.Counts = _Total;
                _PagingEntity.PageCount = (_Total / Rows);
                _PagingEntity.List = _Table.ToList();

                this.SetHeaderJson(_PagingEntity, ArryEntity);
            }

            return _PagingEntity;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="Iquery"></param>
        /// <param name="Page"></param>
        /// <param name="Rows"></param>
        /// <param name="ArryEntity"></param>
        /// <returns></returns>
        public PagingEntity GetPagingEntity(IQuery Iquery, int Page, int Rows, params BaseClass[] ArryEntity)
        {
            var _Count = Iquery.Count();

            var _TakePage = Iquery.TakePage(Page, Rows);

            var _PagingEntity = new PagingEntity();

            var _Table = _TakePage.ToTable();

            if (_Table.Columns.Contains("ROWID"))
                _Table.Columns.RemoveAt(0);

            _PagingEntity.Table = _Table;
            _PagingEntity.Counts = _Count;
            _PagingEntity.PageCount = (_Count / Rows);
            _PagingEntity.List = _Table.ToList();

            this.SetHeaderJson(_PagingEntity, ArryEntity);

            return _PagingEntity;
        }

        /// <summary>
        /// 生产表头 Json 对象
        /// </summary>
        /// <param name="_PagingEntity"></param>
        /// <param name="ArryEntity"></param>
        private void SetHeaderJson(PagingEntity _PagingEntity, params BaseClass[] ArryEntity)
        {
            var dic = new Dictionary<string, object>();
            var list = new List<PropertyInfo>();
            var colNames = new List<Dictionary<string, string>>();
            ArryEntity.ToList().ForEach(item =>
            {
                //将所有实体里面的属性放入list中
                item.GetType().GetProperties().ToList().ForEach(p =>
                {
                    list.Add(p);
                });
            });
            foreach (DataColumn dc in _PagingEntity.Table.Columns)
            {
                dic = new Dictionary<string, object>();
                var col = new Dictionary<string, string>();
                var pro = list.Find(item => item.Name.Equals(dc.ColumnName));

                dic["field"] = dc.ColumnName;
                dic["align"] = "left";
                dic["sortable"] = true;
                if (pro == null)
                {
                    dic["title"] = dc.ColumnName;
                    dic["visible"] = !dc.ColumnName.Equals("_ukid");
                    col.Add(dc.ColumnName, dc.ColumnName);
                }
                else
                {
                    //获取有特性标记的属性【获取字段别名（中文名称）】
                    var FiledConfig = pro.GetCustomAttribute(typeof(FieldAttribute)) as FieldAttribute;
                    if (FiledConfig != null)
                    {
                        dic["title"] = (FiledConfig.Alias == "" ? dc.ColumnName : FiledConfig.Alias);
                        dic["visible"] = true;
                        col.Add(dc.ColumnName, dic["title"].ToStr());
                    }
                }
                _PagingEntity.ColNames.Add(col);
                _PagingEntity.ColModel.Add(dic);
            }
        }

        public T Get(object Id)
        {
            return db.FindById<T>(Id);
        }

        public T Get(Expression<Func<T, bool>> _Where)
        {
            return db.Find(_Where);
        }



    }
}
