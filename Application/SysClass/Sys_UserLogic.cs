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

    public class Sys_UserLogic : BaseLogic<Sys_User>
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
                .Query<Sys_User>()
                .Join<Sys_UserRole>((a, b) => a.User_ID == b.UserRole_UserID)
                .Join<Sys_Role>((a, b, c) => b.UserRole_RoleID == c.Role_ID)
                .WhereIF(!string.IsNullOrEmpty(Query["User_Name"].ToStr()), (a, b, c) => a.User_Name.Contains(Query["User_Name"].ToStr()))
                .WhereIF(!string.IsNullOrEmpty(Query["User_LoginName"].ToStr()), (a, b, c) => a.User_LoginName.Contains(Query["User_LoginName"].ToStr()));

            if (string.IsNullOrEmpty(Query["sortName"].ToStr()))
            {
                _Query.OrderBy((a, b, c) => new { desc = a.User_CreateTime });
            }
            else
            {
                _Query.OrderBy((a, b, c) => Query["sortName"].ToStr() + " " + Query["sortOrder"].ToStr());//前端自动排序
            }

            var IQuery = _Query.Select((a, b, c) => new { a.User_Name, a.User_LoginName, a.User_Email, c.Role_Name, a.User_CreateTime, _ukid = a.User_ID });

            return this.GetPagingEntity(IQuery, Page, Rows, new Sys_User(), new Sys_Role());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <param name="_Sys_Function_List"></param>
        /// <returns></returns>
        public string Save(Sys_User model, Sys_UserRole _Sys_UserRole)
        {
            db.Commit(() =>
            {
                if (model.User_ID.ToGuid() == Guid.Empty)
                {
                    if (string.IsNullOrEmpty(model.User_Pwd))
                        model.User_Pwd = "123"; //Tools.MD5Encrypt("123456");
                    else
                        model.User_Pwd = model.User_Pwd;//Tools.MD5Encrypt(model.cUsers_LoginPwd);

                    model.User_ID = db.Insert(model).ToGuid();
                    if (model.User_ID.ToGuid().Equals(Guid.Empty))
                        throw new MessageBox(this.ErrorMessge);
                    //用户角色
                    _Sys_UserRole.UserRole_UserID = model.User_ID;
                    _Sys_UserRole.UserRole_ID = db.Insert(_Sys_UserRole).ToGuid();
                    if (_Sys_UserRole.UserRole_ID == Guid.Empty)
                        throw new MessageBox(this.ErrorMessge);
                }
                else
                {
                    //如果 密码字段为空，则不修改该密码
                    if (string.IsNullOrEmpty(model.User_Pwd))
                    {
                        db.Update<Sys_User>(w => w.User_ID == model.User_ID, () => new Sys_User
                        {
                            User_ID = model.User_ID,
                            User_Email = model.User_Email,
                            User_IsDelete = model.User_IsDelete,
                            User_LoginName = model.User_LoginName,
                            User_Name = model.User_Name
                        });
                    }
                    else
                    {
                        if (!db.UpdateById(model)) throw new MessageBox(this.ErrorMessge);
                    }

                    //用户角色
                    db.Update<Sys_UserRole>(w => w.UserRole_UserID == model.User_ID, () => new Sys_UserRole
                    {
                        UserRole_RoleID = _Sys_UserRole.UserRole_RoleID
                    });
                }

            });

            return model.User_ID.ToGuidStr();
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
                    var _Sys_User = db.FindById<Sys_User>(item);
                    if (_Sys_User.User_IsDelete == 2) throw new MessageBox("该信息无法删除！");
                    db.Delete<Sys_UserRole>(w => w.UserRole_UserID == item);
                    db.DeleteById<Sys_User>(item);
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
            var _Sys_User = db.FindById<Sys_User>(Id);
            var _Sys_UserRole = db.Find<Sys_UserRole>(w => w.UserRole_UserID == Id);
            var _Sys_Role = db.FindById<Sys_Role>(_Sys_UserRole.UserRole_RoleID);

            var di = this.EntityToDictionary(new Dictionary<string, object>()
            {
                {"_Sys_User",_Sys_User},
                {"_Sys_Role",_Sys_Role},
                {"status",1}
            });

            //重要字段移除 不能传递给页面
            if (di.ContainsKey("User_Pwd")) di.Remove("User_Pwd");

            return di;
        }

        #endregion




    }
}
