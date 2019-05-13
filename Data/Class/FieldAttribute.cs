using System;
namespace DataFramework.Class
{

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class FieldAttribute : Attribute
    {
        public string Alias = string.Empty;
        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsPrimaryKey = false;
        /// <summary>
        /// 是否自增
        /// </summary>
        public bool IsIdentity = false;
        /// <summary>
        /// 是否忽略
        /// </summary>
        public bool IsIgnore = false;
        /// <summary>
        /// 是否为数据库字段列
        /// </summary>
        public bool IsColumn = true;
        /// <summary>
        /// 属性类型
        /// </summary>
        public Type FieldType = typeof(Guid);
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName = string.Empty;
        /// <summary>
        /// 字段描述
        /// </summary>
        /// <param name="_Alias">别名</param>
        public FieldAttribute(string _Alias)
        {
            this.Alias = _Alias;
            this.IsPrimaryKey = false;
            this.IsIdentity = false;
            this.IsIgnore = false;
            this.IsColumn = true;
            this.FieldType = null;
            this.FieldName = string.Empty;

        }
    }
}
