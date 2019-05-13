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
    public class MenuFunctionController : BaseController
    {
        // 菜单功能
        // GET: /ManageSys/MenuFunction/
        public Sys_MenuLogic _Logic = new Sys_MenuLogic();

        protected override void Init()
        {
            this.MenuID = "Z-130";
        }

        public override ActionResult Info()
        {
            var _List = db
              .Query<Sys_Function>()
              .OrderBy(item => new { item.Function_Num })
              .ToList();
            return View(_List);
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
        public ActionResult Save(Sys_Menu model, string _Sys_Function_List)
        {
            this.KeyID = _Logic.Save(model, _Sys_Function_List);
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

        /// <summary>
        /// 获取菜单和功能树
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMenuAndFunctionTree()
        {
            return Success(new
            {
                status = 1,
                value = _Logic.GetMenuZTree()
            });
        }


    }
}