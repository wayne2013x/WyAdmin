using Application.SysClass;
using DataFramework;
using Entity.SysClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Admin.Controllers.Sys
{
    public class RoleFunctionController : BaseController
    {
        // 角色功能
        // GET: /ManageSys/RoleFunction/
        public Sys_RoleMenuFunctionLogic _Logic = new Sys_RoleMenuFunctionLogic();

        protected override void Init()
        {
            this.MenuID = "Z-140";
        }

        public override ActionResult Index()
        {
            var _List = db
                         .Query<Sys_Role>()
                         .OrderBy(item => new { item.Role_Num })
                         .ToList();
            return View(_List);
        }
        #region  基本操作，增删改查

        /// <summary>
        /// 获取角色菜单功能
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetRoleMenuFunctionTree(string RoleId)
        {
            return this.Success(new
            {
                status = 1,
                value = _Logic.GetRoleMenuFunctionZTree(RoleId.ToGuid())
            });
        }

        /// <summary>
        /// 保存角色功能
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(string Sys_RoleMenuFunction_List, string RoleId)
        {
            _Logic.SaveFunction(Sys_RoleMenuFunction_List, RoleId.ToGuid());
            return this.Success();
        }
        #endregion 基本操作，增删改查
    }
}