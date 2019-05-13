using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
namespace DataFramework
{
    //
    using System.Linq.Expressions;
    using System.Reflection;
    using DataFramework.Class;
    using Dapper;

    public static class ConvertExtension
    {
        /// <summary>
        /// string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToStr<T>(this T value)
        {
            try
            {
                return value.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// int
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt32<T>(this T value)
        {
            if (value == null) return 0;

            int result = 0;

            if (!Int32.TryParse(value.ToStr(), out result))
                return 0;

            return result;
        }

        /// <summary>
        /// float
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static float ToFloat<T>(this T value)
        {
            if (value == null) return 0;

            float result = 0;

            if (!float.TryParse(value.ToStr(), out result))
                return 0;

            return result;
        }

        /// <summary>
        /// double
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ToDouble<T>(this T value)
        {
            if (value == null) return 0;

            double result = 0;

            if (!double.TryParse(value.ToStr(), out result))
                return 0;

            return result;
        }

        /// <summary>
        /// decimal
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal ToDecimal<T>(this T value)
        {
            if (value == null) return 0;

            decimal result = 0;

            if (!decimal.TryParse(value.ToStr(), out result))
                return 0;

            return result;
        }

        /// <summary>
        /// Guid
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Guid ToGuid<T>(this T value)
        {
            if (value == null) return Guid.Empty;

            Guid result = Guid.Empty;

            if (!Guid.TryParse(value.ToStr(), out result))
                return Guid.Empty;

            return result;
        }

        /// <summary>
        /// Guid?
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Guid? ToGuidNull<T>(this T value)
        {
            if (value == null) return null;

            Guid result = Guid.Empty;

            if (!Guid.TryParse(value.ToStr(), out result))
                return null;

            return result;
        }

        /// <summary>
        /// GuidString
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToGuidStr<T>(this T value)
        {
            return value.ToGuid().ToStr();
        }

        /// <summary>
        /// DateTime
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ToDateTime<T>(this T value)
        {
            if (value == null) return DateTime.MinValue;

            DateTime result = DateTime.MinValue;

            if (!DateTime.TryParse(value.ToStr(), out result))
                return DateTime.MinValue;

            return result;
        }

        /// <summary>
        /// DateTime?
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime? ToDateTimeNull<T>(this T value)
        {
            if (value == null) return null;

            DateTime result = DateTime.MinValue;

            if (!DateTime.TryParse(value.ToStr(), out result))
                return null;

            return result;
        }

        /// <summary>
        /// 格式的 时间 字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="FormatStr"></param>
        /// <returns></returns>
        public static string ToDateTimeFormat<T>(this T value, string FormatStr = "yyyy-MM-dd")
        {
            var datetime = value.ToDateTime();
            if (datetime.ToShortDateString() == DateTime.MinValue.ToShortDateString())
                return String.Empty;
            else
                return datetime.ToString(FormatStr);
        }

        /// <summary>
        /// bool
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ToBool<T>(this T value)
        {
            if (value == null) return false;

            bool result = false;

            if (!bool.TryParse(value.ToStr(), out result))
                return false;

            return result;
        }

        /// <summary>
        /// byte[]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] ToBytes<T>(this T value)
        {
            try
            {
                return value as byte[];
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name=”timeStamp”></param>
        /// <returns></returns>
        public static DateTime ToTime<T>(this int timeStamp)
        {
            var dtStart = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime); return dtStart.Add(toNow);
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name=”time”></param>
        /// <returns></returns>
        public static int ToTimeInt<T>(this DateTime time)
        {
            var startTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            return (int)((time) - startTime).TotalSeconds;
        }

        /// <summary>
        /// DataRow 转换 实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static T ToEntity<T>(this DataRow dr) where T : class, new()
        {
            var _Entity = Parser.CreateInstance<T>();
            var list = _Entity.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            if (list.Length == 0) throw new Exception("找不到任何 公共属性！");

            foreach (var item in list)
            {
                string AttrName = item.Name;
                foreach (DataColumn dc in dr.Table.Columns)
                {
                    if (AttrName != dc.ColumnName) continue;
                    if (dr[dc.ColumnName] != DBNull.Value) item.SetValue(_Entity, dr[dc.ColumnName], null);
                }
            }
            return _Entity;
        }

        /// <summary>
        /// 将 datatable 转换为 list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable table) where T : class, new()
        {
            var list = new List<T>();

            var _Entity = Parser.CreateInstance<T>();
            var propertyInfo = _Entity.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            foreach (DataRow dr in table.Rows)
            {
                _Entity = Parser.CreateInstance<T>();
                foreach (var item in propertyInfo)
                {
                    string AttrName = item.Name;
                    foreach (DataColumn dc in dr.Table.Columns)
                    {
                        if (AttrName != dc.ColumnName) continue;
                        if (dr[dc.ColumnName] != DBNull.Value)
                            item.SetValue(_Entity, dr[dc.ColumnName], null);
                        else
                            item.SetValue(_Entity, null, null);
                    }
                }
                list.Add(_Entity);
            }
            return list;
        }

        /// <summary>
        /// datatable 转换为 List<Dictionary<string,object>>
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<Dictionary<string, object>> ToList(this DataTable table)
        {
            var list = new List<Dictionary<string, object>>();
            var dic = new Dictionary<string, object>();
            foreach (DataRow dr in table.Rows)
            {
                if (dic != null) dic = new Dictionary<string, object>();
                foreach (DataColumn dc in table.Columns)
                {
                    if (dc.DataType == typeof(DateTime))
                        dic.Add(dc.ColumnName, dr[dc.ColumnName].ToDateTimeFormat("yyyy-MM-dd HH:mm:ss"));
                    else
                        dic.Add(dc.ColumnName, dr[dc.ColumnName]);
                }
                list.Add(dic);
            }
            return list;
        }

        /// <summary>
        /// IDataReader 转换为 DataTable
        /// </summary>
        /// <param name="_IDataReader"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(this IDataReader _IDataReader)
        {
            DataTable dt = new DataTable();
            dt.Load(_IDataReader);
            return dt;
        }

        /// <summary>
        /// 将匿名对象转换为字典
        /// </summary>
        /// <param name="Attribute"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ToDictionary<T>(this T Attribute) where T : class, new()
        {
            var di = new Dictionary<string, object>();

            Type ty = Attribute.GetType();

            var fields = ty.GetProperties().ToList();

            foreach (var item in fields) di.Add(item.Name, item.GetValue(Attribute).ToString());

            return di;
        }


        /************sql****************/
        /// <summary>
        /// 在 拉姆达表达式 where 表达式中使用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool In<T>(this T obj, List<T> array)
        {
            return true;
        }

        /// <summary>
        /// 在 拉姆达表达式 where 表达式中使用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool NotIn<T>(this T obj, List<T> array)
        {
            return true;
        }

        /// <summary>
        /// 在 拉姆达表达式 where 表达式中使用 模糊查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="value">例如：%张三%</param>
        /// <returns></returns>
        public static bool Like<T>(this T obj, string value)
        {
            return true;
        }

        /// <summary>
        /// 一般在Where 条件中使用 例如 : w.User_CreateTime.SqlString("convert(varchar(50),@F,23)")   注意： 字符串中的 @F 代表当前 字段字符串 User_CreateTime
        /// 
        /// 一般用来支持这种语法》CONVERT(varchar(100), GETDATE(), 23)    -- 2006-05-16 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SqlString<T>(this T obj, string value)
        {
            return true;
        }

        /// <summary>
        /// 一般在Where 条件中使用 例如 : w.User_CreateTime.SqlString("convert(varchar(50),@F,23)")   注意： 字符串中的 @F 代表当前 字段字符串 User_CreateTime
        /// 
        /// 可以做比较 则使用该函数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T SqlStringCompare<T>(this T obj, string value)
        {
            return default(T);
        }

    }



    public class Parser
    {
        /// <summary>
        /// 将 Model 转换为 MemberInitExpression 类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static MemberInitExpression ModelToMemberInitExpression<T>(T Model)
        {
            var proInfo = Parser.GetPropertyInfos(typeof(T));

            var list = new List<MemberBinding>();

            foreach (var item in proInfo) list.Add(Expression.Bind(item, Expression.Constant(item.GetValue(Model), item.PropertyType)));

            var newExpr = Expression.New(typeof(T));

            return Expression.MemberInit(newExpr, list);
        }

        /// <summary>
        /// 获取 PropertyInfo 集合
        /// </summary>
        /// <param name="_type"></param>
        /// <param name="_bindingFlags"></param>
        /// <returns></returns>
        public static PropertyInfo[] GetPropertyInfos(Type _type, BindingFlags _bindingFlags = (BindingFlags.Instance | BindingFlags.Public))
        {
            return _type.GetProperties(_bindingFlags);
        }

        /// <summary>
        /// 创建 对象实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CreateInstance<T>()
        {
            var _Type = typeof(T);
            if (_Type.IsValueType || typeof(T) == typeof(string))
                return default(T);
            return (T)Activator.CreateInstance(_Type);
        }

        /// <summary>
        /// 获取 对象 中 某个属性得 标记
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_type"></param>
        /// <param name="_name"></param>
        /// <returns></returns>
        public static T GetAttribute<T>(Type _type, string _name) where T : Attribute
        {
            return Parser.GetPropertyInfo(_type, _name).GetCustomAttribute(typeof(T)) as T;
        }

        /// <summary>
        /// 获取 PropertyInfo 对象
        /// </summary>
        /// <param name="_type"></param>
        /// <param name="_name"></param>
        /// <returns></returns>
        public static PropertyInfo GetPropertyInfo(Type _type, string _name)
        {
            return _type.GetProperty(_name);
        }

        /// <summary>
        /// 获取 TableAttribute
        /// </summary>
        /// <param name="_type"></param>
        /// <returns></returns>
        public static TableAttribute GetTableAttribute(Type _type)
        {
            return (TableAttribute)Attribute.GetCustomAttributes(_type, true).Where(item => item is TableAttribute).FirstOrDefault();
        }

        /// <summary>
        /// 获取表信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_Entity"></param>
        /// <returns></returns>
        public static Entity_Table GetTableInfo<T>(T _Entity)
        {
            return TableInfo.Get(_Entity);
        }

        /// <summary>
        /// 获取表信息
        /// </summary>
        /// <param name="_Type"></param>
        /// <returns></returns>
        public static Entity_Table GetTableInfo(Type _Type)
        {
            return TableInfo.Get(_Type);
        }

        /// <summary>
        /// Eval
        /// </summary>
        /// <param name="_Expression"></param>
        /// <returns></returns>
        public static object Eval(Expression _Expression)
        {
            var cast = Expression.Convert(_Expression, typeof(object));
            return Expression.Lambda<Func<object>>(cast).Compile().Invoke();
        }

        /// <summary>
        /// 获取 DynamicParameters 对象
        /// </summary>
        /// <param name="dbParams"></param>
        /// <returns></returns>
        public static DynamicParameters GetDynamicParameters(List<DbParam> dbParams)
        {
            var _DynamicParameters = new DynamicParameters();
            foreach (var item in dbParams) _DynamicParameters.Add(item.ParameterName, item.Value);
            return _DynamicParameters;
        }

        /// <summary>
        /// 根据实体对象 的 ID 创建 Expression<Func<T, bool>> 表达式树
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_KeyName"></param>
        /// <param name="_KeyValue"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> WhereById<T>(string _KeyName, object _KeyValue, string _ParName = "_Where_Parameter")
        {
            //创建 Where Lambda表达式树
            var _Type = typeof(T);
            var _Where_Parameter = Expression.Parameter(_Type, _ParName);
            var _Property = _Type.GetProperty(_KeyName);

            ConstantExpression _Right = Expression.Constant(_KeyValue);

            if (_KeyValue == null)
            {
                if (_Property.PropertyType == typeof(Guid)) _KeyValue = Guid.Empty;

                if (_Property.PropertyType == typeof(int)) _KeyValue = Int32.MinValue;
            }

            try
            {
                if (_Property.PropertyType == typeof(Guid)) _Right = Expression.Constant(_KeyValue, typeof(Guid));

                if (_Property.PropertyType == typeof(int)) _Right = Expression.Constant(_KeyValue, typeof(int));

                if (_Property.PropertyType == typeof(Guid?)) _Right = Expression.Constant(_KeyValue, typeof(Guid?));

                if (_Property.PropertyType == typeof(int?)) _Right = Expression.Constant(_KeyValue, typeof(int?));
            }
            catch (Exception ex)
            {
                if (_Property.PropertyType != _KeyValue.GetType())
                    throw new DbFrameException("请将主键值 转换为 正确的类型值！");
                else
                    throw ex;
            }

            var _Left = Expression.Property(_Where_Parameter, _Property);
            var _Where_Body = Expression.Equal(_Left, _Right);
            return Expression.Lambda<Func<T, bool>>(_Where_Body, _Where_Parameter);
        }



    }


    public class DbSettings
    {

        /// <summary>
        /// 默认连接字符串
        /// </summary>
        public static string DefaultConnectionString { get; set; }

        /// <summary>
        /// 关键字处理 函数
        /// </summary>
        public static Func<string, string> KeywordHandle;

    }

    /// <summary>
    /// 多表连接类型
    /// </summary>
    public enum EJoinType
    {
        /// <summary>
        /// join
        /// </summary>
        JOIN,

        /// <summary>
        /// 内连接 inner join
        /// </summary>
        INNER_JOIN,

        /// <summary>
        /// 左连接 left join
        /// </summary>
        LEFT_JOIN,

        /// <summary>
        /// 左外连接 left outer join
        /// </summary>
        LEFT_OUTER_JOIN,

        /// <summary>
        /// 右连接 right join
        /// </summary>
        RIGHT_JOIN,

        /// <summary>
        /// 右外连接 right outer join
        /// </summary>
        RIGHT_OUTER_JOIN,

        /// <summary>
        /// 全连接 full join
        /// </summary>
        FULL_JOIN,

        /// <summary>
        /// 全外连接 full outer join
        /// </summary>
        FULL_OUTER_JOIN,

        /// <summary>
        /// 交叉连接 cross join
        /// </summary>
        CROSS_JOIN
    }

    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum DbContextType
    {
        SqlServer,
        MySql,
        Oracle
    }


}
