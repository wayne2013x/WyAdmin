using Application;
using Common;
using DataFramework;
using Entity;
using Entity.SysClass;
using System;
using System.Collections;
using System.Web.Mvc;
using Web.Attribute;

namespace Web.Areas.Admin.Controllers.Base
{
    namespace Web.Areas.Admin.Controllers.Base
    {

        public class MemberController : BaseController
        {
            // 会员信息管理
            // GET: /Admin/Member/

            protected override void Init()
            {
                this.MenuID = "A-100";
                this.PrintTitle = "我是一个 打印标题！";
            }

            MemberLogic _Logic = new MemberLogic();

            #region  查询数据列表
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
            #endregion  查询数据列表

            #region  基本操作，增删改查
            /// <summary>
            /// 保存
            /// </summary>
            /// <returns></returns>
            [HttpPost, AopCheckEntity, ValidateInput(false)]
            public ActionResult Save(Member model, string UserIDList)
            {
                //判断是否有文件上传上来
                var files = Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    var key = files.GetKey(i);
                    var filesObj = files[i];
                    if (key == "Member_Photo")
                    {
                        this.HandleUpFile(filesObj, new string[] { ".jpg", ".gif", ".png" }, null, (_Path) =>
                        {
                            model.Member_Photo = _Path;
                        });
                    }
                    if (key == "Member_FilePath")
                    {
                        this.HandleUpFile(filesObj, null, null, (_Path) =>
                        {
                            model.Member_FilePath = _Path;
                        });
                    }
                }

                this.KeyID = _Logic.Save(model);
                return this.Success(new { status = 1, ID = this.KeyID });
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
            /// EXCEL 导入数据
            /// </summary>
            /// <param name="ProjectTypeID"></param>
            /// <returns></returns>
            public ActionResult ExcelToDb()
            {
                try
                {
                    var hpfb = Request.Files[0];
                    this.HandleUpFile(hpfb, new string[] { ".xlsx", ".xls" }, (file) =>
                    {
                        if (file == null || file.ContentLength < 1) throw new MessageBox("请选择文件");
                    });

                    _Logic.ExcelToDb(hpfb.InputStream, (errorMsg) =>
                    {
                        if (!string.IsNullOrEmpty(errorMsg.ToStr()))
                        {
                            throw new MessageBox(errorMsg.ToString().Replace("\r\n", "<br />").Trim());
                        }
                    });

                    return this.Success();
                }
                catch (Exception ex)
                {
                    throw new MessageBox(ex.Message.Replace("\r\n", "<br />").Trim());
                }
            }



        }
    }
}