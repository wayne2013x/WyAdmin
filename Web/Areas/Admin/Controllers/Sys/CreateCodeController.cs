using Application.SysClass;
using DataFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Admin.Controllers.Sys
{
    public class CreateCodeController : BaseController
    {
        // 根据表 生成 代码
        // GET: /SysManage/CreateCode/
        public Sys_CreateCodeLogic _Logic = new Sys_CreateCodeLogic();

        protected override void Init()
        {
            this.MenuID = "Z-160";
        }

        public override ActionResult Index()
        {
            ViewData["DbSetCode"] = _Logic.CreateDbSetCode();
            return base.Index();
        }

        /// <summary>
        /// 获取数据库中所有的表和字段
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetZTreeAllTable()
        {
            return this.Success(new { status = 1, value = _Logic.GetZTreeAllTable() });
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(FormCollection fc)
        {
            var Type = fc["ClassType"];
            var Url = (fc["Url"] == null ? Server.MapPath("/Content/CreateFile") : fc["Url"]);
            var Str = fc["Str"];
            var Table = fc["Table"];
            var isall = fc["isall"].ToBool();
            var Template = Server.MapPath("/Content/Template/");

            _Logic.Save(Type, Url, Template, Str, isall, Table);
            return this.Success();
        }



    }
}