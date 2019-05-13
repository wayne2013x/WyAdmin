using Application.SysClass;
using DataFramework;
using Entity.SysClass;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Attribute;

namespace Web.Areas.Admin.Controllers.Sys
{
    public class UserController : BaseController
    {
        // 用户管理
        // GET: /ManageSys/User/
        public Sys_UserLogic _Logic = new Sys_UserLogic();

        protected override void Init()
        {
            this.MenuID = "Z-100";
        }

        #region  基本操作，增删改查

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        [NonAction]
        public override PagingEntity GetPagingEntity(Hashtable query, int page = 1, int rows = 20)
        {
            //获取列表
            return _Logic.GetDataSource(query, page, rows);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        [HttpPost, AopCheckEntity]
        public ActionResult Save(Sys_User model, string Role_ID)
        {
            this.KeyID = _Logic.Save(model, new Sys_UserRole() { UserRole_RoleID = Role_ID.ToGuid() });
            return this.Success(new
            {
                status = 1,
                ID = this.KeyID
            });
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(string Ids)
        {
            _Logic.Delete(Ids);
            return this.Success();
        }

        /// <summary>
        /// 查询根据ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LoadForm(string ID)
        {
            return this.Success(_Logic.LoadForm(ID.ToGuid()));
        }
        #endregion  基本操作，增删改查

    }

}