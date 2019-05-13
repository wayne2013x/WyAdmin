using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFramework.Class
{
    public class TableInfo
    {

        public static Entity_Table Get<T>(T _Entity)
        {
            var _Entity_Table = new Entity_Table();
            var _TableAttr = Parser.GetTableAttribute(_Entity.GetType());
            _Entity_Table.TableName = _TableAttr == null ? _Entity.GetType().Name : _TableAttr.TableName;
            var _Fields = Parser.GetPropertyInfos(_Entity.GetType());
            foreach (var item in _Fields)
            {
                var _FieldAttribute = (Attribute.GetCustomAttribute(item, typeof(FieldAttribute)) as FieldAttribute);
                if (_FieldAttribute == null) continue;
                var _FieldInfo = new Entity_FieldInfo()
                {
                    Alias = _FieldAttribute.Alias,
                    FieldName = item.Name,
                    FieldType = item.PropertyType,
                    IsIdentity = _FieldAttribute.IsIdentity,
                    IsIgnore = _FieldAttribute.IsIgnore,
                    IsColumn = _FieldAttribute.IsColumn,
                    IsPrimaryKey = _FieldAttribute.IsPrimaryKey,
                    Value = item.GetValue(_Entity)
                };

                if (_FieldAttribute.IsPrimaryKey)
                {
                    _Entity_Table.KeyFieldName = _FieldInfo.FieldName;
                    _Entity_Table.KeyFieldInfo = _FieldInfo;
                }
                _Entity_Table.Fields.Add(_FieldInfo);
            }
            return _Entity_Table;
        }

        public static Entity_Table Get(Type _Type)
        {
            var _Entity_Table = new Entity_Table();
            var _TableAttr = Parser.GetTableAttribute(_Type);
            _Entity_Table.TableName = _TableAttr == null ? _Type.Name : _TableAttr.TableName;
            var _Fields = Parser.GetPropertyInfos(_Type);
            foreach (var item in _Fields)
            {
                var _FieldAttribute = (Attribute.GetCustomAttribute(item, typeof(FieldAttribute)) as FieldAttribute);
                if (_FieldAttribute == null) continue;
                var _FieldInfo = new Entity_FieldInfo()
                {
                    Alias = _FieldAttribute.Alias,
                    FieldName = item.Name,
                    FieldType = item.PropertyType,
                    IsIdentity = _FieldAttribute.IsIdentity,
                    IsIgnore = _FieldAttribute.IsIgnore,
                    IsColumn = _FieldAttribute.IsColumn,
                    IsPrimaryKey = _FieldAttribute.IsPrimaryKey
                };

                if (_FieldAttribute.IsPrimaryKey)
                {
                    _Entity_Table.KeyFieldName = _FieldInfo.FieldName;
                    _Entity_Table.KeyFieldInfo = _FieldInfo;
                }
                _Entity_Table.Fields.Add(_FieldInfo);
            }
            return _Entity_Table;
        }

    }

    /// <summary>
    /// 表信息
    /// </summary>
    public class Entity_Table
    {
        public Entity_Table()
        {
            this.Fields = new List<Entity_FieldInfo>();
        }
        public string TableName { get; set; }

        public string KeyFieldName { get; set; }

        public Entity_FieldInfo KeyFieldInfo { get; set; }

        public List<Entity_FieldInfo> Fields { get; set; }
    }

    /// <summary>
    /// 字段描述
    /// </summary>
    public class Entity_FieldInfo
    {
        /// <summary>
        /// 字段描述
        /// </summary>
        public string Alias = string.Empty;

        /// <summary>
        /// 是否为主键
        /// </summary>
        public bool IsPrimaryKey = false;

        /// <summary>
        /// 是否为自增
        /// </summary>
        public bool IsIdentity = false;

        /// <summary>
        /// 是否忽略字段【不对该字段 添加、修改 操作】
        /// </summary>
        public bool IsIgnore = false;

        /// <summary>
        /// 是否为数据库字段列
        /// </summary>
        public bool IsColumn = true;

        /// <summary>
        /// 字段类型
        /// </summary>
        public Type FieldType = typeof(Guid);

        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName = string.Empty;

        /// <summary>
        /// 字段值
        /// </summary>
        public object Value = null;

    }



}
