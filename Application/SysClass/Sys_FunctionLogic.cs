using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SysClass
{
    using System.Collections;
    using Common;
    using Application.Class;
    using Entity.SysClass;
    using DataFramework;

    public class Sys_FunctionLogic : BaseLogic<Sys_Function>
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
            var IQuery = db
                .Query<Sys_Function>()
                .WhereIF(!string.IsNullOrEmpty(Query["Function_Name"].ToStr()), (a) => a.Function_Name.Contains(Query["Function_Name"].ToStr()));

            if (string.IsNullOrEmpty(Query["sortName"].ToStr()))
            {
                IQuery.OrderBy((a) => new { a.Function_Num });
            }
            else
            {
                IQuery.OrderBy((a) => Query["sortName"].ToStr() + " " + Query["sortOrder"].ToStr());//前端自动排序
            }

            IQuery.Select(a => new { a.Function_Num, a.Function_Name, a.Function_ByName, a.Function_CreateTime, _ukid = a.Function_ID });

            return this.GetPagingEntity(IQuery, Page, Rows, new Sys_Function());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <param name="_Sys_Function_List"></param>
        /// <returns></returns>
        public string Save(Sys_Function model)
        {
            db.Commit(() =>
            {
                if (model.Function_ID.ToGuid() == Guid.Empty)
                {
                    model.Function_ID = db.Insert(model).ToGuid();
                    if (model.Function_ID.ToGuid() == Guid.Empty)
                        throw new MessageBox(this.ErrorMessge);
                }
                else
                {
                    if (!db.UpdateById(model)) throw new MessageBox(this.ErrorMessge);
                }
            });

            return model.Function_ID.ToGuidStr();
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
                    db.DeleteById<Sys_Function>(item);
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
            var _Sys_Function = db.FindById<Sys_Function>(Id);
            var di = this.EntityToDictionary(new Dictionary<string, object>()
            {
                {"_Sys_Function",_Sys_Function},
                {"status",1}
            });

            return di;
        }

        #endregion
    }
}
