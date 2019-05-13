using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    using System.Collections;
    using Common;
    using Application.Class;
    using Entity;
    using Entity.SysClass;
    using DataFramework;
    using NPOI.SS.UserModel;

    public class MemberLogic : BaseLogic<Sys_User>
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
                        .Query<Member>()
                        .Join<Sys_User>((a, b) => a.Member_UserID == b.User_ID)
                        .WhereIF(!string.IsNullOrEmpty(Query["Member_Name"].ToStr()), (a, b) => a.Member_Name.Contains(Query["Member_Name"].ToStr()))
                        .WhereIF(!string.IsNullOrEmpty(Query["User_Name"].ToStr()), (a, b) => b.User_Name.Contains(Query["User_Name"].ToStr()));

            if (string.IsNullOrEmpty(Query["sortName"].ToStr()))
            {
                //默认排序字段
                _Query.OrderBy((a, b) => new { desc = a.Member_Num });
            }
            else
            {
                //前端自动排序
                _Query.OrderBy((a, b) => Query["sortName"].ToStr() + " " + Query["sortOrder"].ToStr());
            }

            var IQuery = _Query.Select((a, b) => new { a.Member_Num, a.Member_Name, a.Member_Phone, a.Member_Sex, b.User_Name, a.Member_CreateTime, _ukid = a.Member_ID });

            return this.GetPagingEntity(IQuery, Page, Rows, new Member(), new Sys_User());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <param name="_Sys_Function_List"></param>
        /// <returns></returns>
        public string Save(Member model)
        {
            db.Commit(() =>
            {
                if (model.Member_ID.ToGuid() == Guid.Empty)
                {
                    model.Member_ID = db.Insert(model).ToGuid();
                    if (model.Member_ID.ToGuid() == Guid.Empty)
                        throw new MessageBox(this.ErrorMessge);
                }
                else
                {
                    if (!db.UpdateById(model)) throw new MessageBox(this.ErrorMessge);
                }
            });
            return model.Member_ID.ToGuidStr();
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
                    db.DeleteById<Member>(item);
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
            var _MemberM = db.FindById<Member>(Id);
            var _Sys_UserM = db.FindById<Sys_User>(_MemberM.Member_UserID);//db.FindSingle<Sys_User>(w => w.User_ID == _MemberM.Member_UserID);//
            var di = this.EntityToDictionary(new Dictionary<string, object>()
            {
                {"_MemberM",_MemberM},
                {"_Sys_UserM",_Sys_UserM},
                {"status",1}
            });

            if (di.ContainsKey("User_Pwd")) di.Remove("User_Pwd");
            //格式化 日期
            di["Member_Birthday"] = di["Member_Birthday"].ToDateTimeFormat();

            return di;
        }

        #endregion

        /// <summary>
        /// 导入excel 数据到数据库中
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="_Action"></param>
        public void ExcelToDb(System.IO.Stream fileStream, Action<string> _Action)
        {
            StringBuilder errorMsg = new StringBuilder();
            //excel工作表
            ISheet sheet = null;
            //根据文件流创建excel数据结构,NPOI的工厂类WorkbookFactory会自动识别excel版本，创建出不同的excel数据结构
            IWorkbook workbook = WorkbookFactory.Create(fileStream);
            sheet = workbook.GetSheetAt(0);
            IRow row = null;
            db.Commit(() =>
            {
                if (sheet.LastRowNum > 0)
                {
                    for (int i = 0; i <= sheet.LastRowNum; i++)
                    {
                        row = sheet.GetRow(i);
                        if (row == null) continue;
                        int rowNum = i + 1;
                        if (i > 0)//忽略表头
                        {
                            //var hymc = row.GetCell(0) == null ? "" : NPOIHelper.GetCellValue(row.GetCell(0)).ToStr();//用户名称
                            //var hydh = row.GetCell(1) == null ? "" : NPOIHelper.GetCellValue(row.GetCell(1)).ToStr();//用户电话
                            //var xb = row.GetCell(2) == null ? "" : NPOIHelper.GetCellValue(row.GetCell(2)).ToStr();//性别

                            ///**********开始你的逻辑部分 start***********/

                            //if (string.IsNullOrEmpty(hymc))
                            //{
                            //    errorMsg.Append(string.Format("第{0}行的会员名称不能为空", rowNum)); break;
                            //}

                            ////得到信息 写入数据库
                            //db.Insert<Member>(new Member
                            //{
                            //    Member_Name = hymc,
                            //    Member_Phone = hydh.ToInt32(),
                            //    Member_Sex = xb
                            //}, li);

                            //throw new MessageBox("这里只是做一个 例子！");

                            /**********开始你的逻辑部分 end***********/

                        }
                    }
                }
                else
                {
                    errorMsg.Append("未找到任何数据");
                }

            });

            _Action(errorMsg.ToString());

        }

    }
}
