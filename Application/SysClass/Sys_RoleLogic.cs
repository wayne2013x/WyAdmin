using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SysClass
{
    using System.Data;
    using System.Collections;
    using Common;
    using Application.Class;
    using Entity.SysClass;
    using DataFramework;

    public class Sys_RoleLogic : BaseLogic<Sys_Role>
    {
        #region  增、删、改、查

        /// <summary>
        /// 数据源
        /// </summary>
        /// <param name="Query"></param>
        /// <param name="Page"></param>
        /// <param name="Rows"></param>
        /// <returns></returns>
        public PagingEntity GetDataSource(Hashtable Query, int Page, int Rows)
        {
            var _Query = db
                .Query<Sys_Role>()
                .WhereIF(!string.IsNullOrEmpty(Query["Role_Name"].ToStr()), (a) => a.Role_Name.Contains(Query["Role_Name"].ToStr()));

            if (string.IsNullOrEmpty(Query["sortName"].ToStr()))
            {
                _Query.OrderBy((a) => new { a.Role_Num });
            }
            else
            {
                _Query.OrderBy((a) => Query["sortName"].ToStr() + " " + Query["sortOrder"].ToStr());//前端自动排序
            }

            var IQuery = _Query.Select(a => new { a.Role_Num, a.Role_Name, a.Role_Remark, a.Role_CreateTime, _ukid = a.Role_ID });

            return this.GetPagingEntity(IQuery, Page, Rows, new Sys_Role());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <param name="_Sys_Function_List"></param>
        /// <returns></returns>
        public string Save(Sys_Role model)
        {
            db.Commit(() =>
            {
                if (model.Role_ID.ToGuid().Equals(Guid.Empty))
                {
                    model.Role_ID = db.Insert(model).ToGuid();
                    if (model.Role_ID == Guid.Empty)
                        throw new MessageBox(this.ErrorMessge);
                }
                else
                {
                    if (!db.UpdateById(model)) throw new MessageBox(this.ErrorMessge);
                }

            });

            return model.Role_ID.ToGuidStr();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Ids"></param>
        public void Delete(string Ids)
        {
            db.Commit(() =>
            {
                Ids.DeserializeObject<List<Guid>>().ForEach(item =>
                {
                    var _Sys_RoleM = db.FindById<Sys_Role>(item);
                    if (_Sys_RoleM.Role_IsDelete == 2) throw new MessageBox("该信息无法删除！");
                    db.DeleteById<Sys_Role>(item);
                });
            });
        }

        /// <summary>
        /// 表单数据加载
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Dictionary<string, object> LoadForm(Guid Id)
        {
            var _Sys_Role = db.FindById<Sys_Role>(Id);

            var di = this.EntityToDictionary(new Dictionary<string, object>()
            {
                {"_Sys_Role",_Sys_Role},
                {"status",1}
            });

            return di;
        }

        #endregion


    }
}
