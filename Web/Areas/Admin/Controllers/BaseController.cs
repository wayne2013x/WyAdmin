using Common;
using DataFramework;
using DataFramework.Class;
using DataFramework.SqlServerContext;
using Entity.SysClass;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Attribute;

namespace Web.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [AopActionFilter()]
    public class BaseController : Controller
    {
        //
        // GET: /Admin/Base/
        protected DbContextSqlServer db = new DbContextSqlServer();
        protected List<SQL> li = new List<SQL>();

        /// <summary>
        /// 主键ID
        /// </summary>
        public string KeyID { get; set; }

        /// <summary>
        /// 菜单ID
        /// </summary>
        public string MenuID { get; set; }

        /// <summary>
        /// 是否执行权限逻辑
        /// </summary>
        public bool IsExecutePowerLogic { get; set; }

        /// <summary>
        /// 打印标题
        /// </summary>
        public string PrintTitle { get; set; }

        /// <summary>
        /// 帐户 信息 对象
        /// </summary>
        protected Account _Account = new Account();

        protected virtual void Init() { }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            this.IsExecutePowerLogic = true;
            base.Initialize(requestContext);
            this._Account = Tools.GetSession<Account>("Account");
            this.Init();
        }


        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult Info()
        {
            return View();
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            if (!filterContext.HttpContext.Request.IsAjaxRequest() && this._Account != null)
            {
                this.PowerLogic(filterContext);

            }
        }

        private void PowerLogic(ActionExecutedContext filterContext)
        {
            string ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string ActionName = filterContext.ActionDescriptor.ActionName;
            string Area = filterContext.RouteData.DataTokens["area"].ToStr();

            //判断是否执行权限逻辑
            if (this.IsExecutePowerLogic)
            {
                var _func_list = db.Query<Sys_Function>().OrderBy(item => new { item.Function_Num }).ToList();
                var _power_list = new Dictionary<string, object>();
                //这里得判断一下是否是查找带回调用页面
                string findback = filterContext.HttpContext.Request.QueryString["findback"].ToStr();

                if (string.IsNullOrEmpty(findback))
                {
                    //dynamic model = new ExpandoObject();
                    if (string.IsNullOrEmpty(MenuID))
                    {
                        throw new MessageBox("区域(" + Area + "),控制器(" + ControllerName + "):的程序中缺少菜单ID");
                    }

                    var _Menu = db.Find<Sys_Menu>(w => w.Menu_Num == MenuID);
                    if (!_Menu.Menu_Url.ToStr().StartsWith("/" + Area + "/" + ControllerName + "/"))
                    {
                        throw new MessageBox("区域(" + Area + "),控制器(" + ControllerName + "):的程序中缺少菜单ID与该页面不匹配");
                    }

                    var _role_menu_func_list = db.Query<Sys_RoleMenuFunction>().ToList();
                    var _menu_func_list = db.Query<Sys_MenuFunction>().ToList();

                    if (!_Account.IsSuperManage)
                    {
                        _power_list = new Dictionary<string, object>();
                        _func_list.ForEach(item =>
                        {
                            var ispower = _role_menu_func_list.FindAll(x =>
                                x.RoleMenuFunction_RoleID == _Account.RoleID &&
                                x.RoleMenuFunction_MenuID == _Menu.Menu_ID &&
                                x.RoleMenuFunction_FunctionID == item.Function_ID);

                            _power_list.Add(item.Function_ByName, (ispower.Count > 0));

                        });
                    }
                    else
                    {
                        _func_list.ForEach(item =>
                        {
                            _power_list.Add(item.Function_ByName, true);
                            //var ispower = _menu_func_list.FindAll(x => x.uMenuFunction_MenuID == Tools.ToGuid(MenuID) && x.uMenuFunction_FunctionID == item.uFunction_ID);
                            //if (ispower.Count > 0)
                            //    _power_list.Add(item.cFunction_ByName, true);
                            //else
                            //    _power_list.Add(item.cFunction_ByName, false);
                        });
                    }
                }
                else
                {
                    _power_list = new Dictionary<string, object>();
                    _func_list.ForEach(item =>
                    {
                        _power_list.Add(item.Function_ByName, false);
                    });
                    _power_list["Have"] = true;
                    _power_list["Search"] = true;
                }
                filterContext.Controller.ViewData["PowerModel"] = _power_list.SerializeObject();
            }

            this.ViewData["thisWindowName"] = $"adminIframe-{Request.Path}{Request.QueryString}";
            this.ViewData["formWindowName"] = $"Form_{Request.Path.ToStr().Replace("/", "")}";
        }


        /// <summary>
        /// 列表页接口
        /// </summary>
        /// <param name="fc"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult GetDataSource(FormCollection fc, int page = 1, int rows = 20)
        {
            var hs = this.GetUrlQueryString(Request.UrlReferrer.Query);
            foreach (var item in hs.Keys)
            {
                if (!fc.AllKeys.Contains(item.ToString()))
                    fc.Add(item.ToString(), hs[item.ToString()].ToStr());
            }

            var query = this.FormCollectionToHashtable(fc);

            var pe = this.GetPagingEntity(query, page, rows);

            return Success(new
            {
                status = 1,
                column = pe.ColModel,
                rows = pe.List,
                page = page,
                total = pe.Counts,
                pageCount = pe.PageCount
            });
        }



        /// <summary>
        /// 数据源
        /// </summary>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [NonAction]
        public virtual PagingEntity GetPagingEntity(Hashtable query, int page = 1, int rows = 20)
        {
            return new PagingEntity();
        }


        /// <summary>
        /// 打印
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult Print(FormCollection fc)
        {
            TempData["Title"] = PrintTitle;
            foreach (var item in Request.QueryString.Keys)
            {
                if (!fc.AllKeys.Contains(item.ToString()))
                    fc.Add(item.ToString(), Request.QueryString[item.ToString()].ToStr());
            }

            var query = this.FormCollectionToHashtable(fc);

            var pe = GetPagingEntity(query, 1, int.MaxValue);
            return View("~/Areas/Admin/Views/Print/Index.cshtml", pe);
        }

        /// <summary>
        /// 导出EXCEL
        /// </summary>
        /// <param name="fc"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult ExportExcel(FormCollection fc, int page = 1, int rows = int.MaxValue)
        {
            foreach (var item in Request.QueryString.Keys)
            {
                if (!fc.AllKeys.Contains(item.ToString()))
                    fc.Add(item.ToString(), Request.QueryString[item.ToString()].ToStr());
            }

            var query = this.FormCollectionToHashtable(fc);

            var pe = GetPagingEntity(query, page, rows);
            return File(DBToExcel(pe), Tools.GetFileContentType[".xls"], Guid.NewGuid().ToString() + ".xls");
        }


        /// <summary>
        /// 表数据转换为EXCEL
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        [NonAction]
        public virtual byte[] DBToExcel(PagingEntity pe)
        {
            DataTable dt = pe.Table;
            var list = pe.ColModel;
            HSSFWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet();

            //填充表头
            IRow dataRow = sheet.CreateRow(0);
            foreach (DataColumn column in dt.Columns)
            {
                if (column.ColumnName.Equals("_ukid"))
                    continue;
                foreach (var item in list)
                {
                    if (column.ColumnName.Equals(item["field"].ToStr()))
                    {
                        dataRow.CreateCell(column.Ordinal).SetCellValue(item["title"].ToStr());
                    }
                }
            }

            //填充内容
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataRow = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Columns[j].ColumnName.Equals("_ukid"))
                        continue;
                    dataRow.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                }
            }

            //保存
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// 将  FormCollection  转换为  哈希表
        /// </summary>
        /// <returns></returns>
        [NonAction]
        protected System.Collections.Hashtable FormCollectionToHashtable(FormCollection fc)
        {
            System.Collections.Hashtable hashtable = new System.Collections.Hashtable();
            if (fc != null)
                fc.AllKeys.ToList().ForEach(item =>
                {
                    hashtable.Add(item, HttpUtility.UrlDecode(fc[item]));
                });
            return hashtable;
        }




        /// <summary>
        /// 根据地址字符串获取参数
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        [NonAction]
        public Dictionary<string, object> GetUrlQueryString(string Url)
        {
            var di = new Dictionary<string, object>();
            if (Url.Contains("?"))
            {
                Url = Url.Substring(Url.IndexOf("?") + 1);
                string[] str;
                if (Url.Contains("&"))
                {
                    str = Url.Split('&');
                    foreach (var item in str)
                    {
                        if (item.Contains("="))
                        {
                            di.Add(item.Split('=')[0], (item.Split('=')[1] == "null") ? null : item.Split('=')[1]);
                        }
                    }
                }
                else
                {
                    if (Url.Contains("="))
                    {
                        str = Url.Split('=');
                        di.Add(str[0], str[1]);
                    }
                }
            }
            return di;
        }

        /// <summary>
        /// 返回 Json 信息
        /// </summary>
        /// <param name="_object"></param>
        /// <returns></returns>
        public JsonResult Success(object _object = null)
        {
            if (_object == null)
                return Json(new { status = 1 }, JsonRequestBehavior.DenyGet);
            return Json(_object, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 处理上传文件
        /// </summary>
        /// <param name="_HttpPostedFileBase"></param>
        /// <param name="Format">文件格式</param>
        /// <param name="Check">执行前 验证回调</param>
        /// <param name="CallBack">如果有回调则保存 否则不保存</param>
        public void HandleUpFile(HttpPostedFileBase _HttpPostedFileBase, string[] Format, Action<HttpPostedFileBase> Check = null, Action<string> CallBack = null)
        {
            if (Check != null) Check(_HttpPostedFileBase);

            string ExtensionName = Path.GetExtension(_HttpPostedFileBase.FileName).ToLower().Trim();//获取后缀名

            if (Format != null && !Format.Contains(ExtensionName.ToLower()))
            {
                throw new MessageBox("请上传后缀名为：" + string.Join("、", Format) + " 格式的文件");
            }

            if (CallBack != null)
            {
                if (!System.IO.Directory.Exists(Server.MapPath("/Content/UpFile/")))
                    System.IO.Directory.CreateDirectory(Server.MapPath("/Content/UpFile/"));
                string filePath = "/Content/UpFile/" + Guid.NewGuid() + ExtensionName;
                _HttpPostedFileBase.SaveAs(Server.MapPath(filePath));

                CallBack(filePath);
            }
        }





    }
}