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

    public class Sys_MenuLogic : BaseLogic<Sys_Menu>
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
                .Query<Sys_Menu>()
                .Join<Sys_Menu>((a, b) => a.Menu_ParentID == b.Menu_ID)
                .WhereIF(string.IsNullOrEmpty(Query["Menu_ID"].ToStr()), (a, b) => a.Menu_ParentID == null)
                .WhereIF(!string.IsNullOrEmpty(Query["Menu_ID"].ToStr()), (a, b) => a.Menu_ParentID == Query["Menu_ID"].ToGuid())
                .WhereIF(!string.IsNullOrEmpty(Query["Menu_Name"].ToStr()), (a, b) => a.Menu_Name.Contains(Query["Menu_Name"].ToStr()));

            if (string.IsNullOrEmpty(Query["sortName"].ToStr()))
            {
                _Query.OrderBy((a, b) => new { a.Menu_Num });
            }
            else
            {
                _Query.OrderBy((a, b) => Query["sortName"].ToStr() + " " + Query["sortOrder"].ToStr());//前端自动排序
            }

            var IQuery = _Query.Select((a, b) => new
            {
                a.Menu_Name,
                a.Menu_Url,
                父级菜单 = b.Menu_Name,
                a.Menu_Num,
                a.Menu_Icon,
                SqlString = "case when a.Menu_IsShow=2 then '隐藏' else '显示' end Menu_IsShow",
                a.Menu_CreateTime,
                _ukid = a.Menu_ID
            });

            return this.GetPagingEntity(IQuery, Page, Rows, new Sys_Menu());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <param name="_Sys_Function_List"></param>
        /// <returns></returns>
        public string Save(Sys_Menu model, string _Sys_Function_List)
        {
            model.Menu_IsShow = 1;

            db.Commit(() =>
            {
                if (model.Menu_ID.ToGuid() == Guid.Empty)
                {
                    model.Menu_ID = db.Insert(model).ToGuid();
                    if (model.Menu_ID.ToGuid().Equals(Guid.Empty))
                        throw new MessageBox(this.ErrorMessge);
                }
                else
                {
                    if (!db.UpdateById(model)) throw new MessageBox(this.ErrorMessge);
                }

                //删除菜单的功能
                db.Delete<Sys_MenuFunction>(w => w.MenuFunction_MenuID == model.Menu_ID.ToGuid());

                _Sys_Function_List.DeserializeObject<List<Sys_Function>>().ForEach(item =>
                {
                    db.Insert<Sys_MenuFunction>(new Sys_MenuFunction
                    {
                        MenuFunction_MenuID = model.Menu_ID.ToGuid(),
                        MenuFunction_FunctionID = item.Function_ID.ToGuid(),
                    });
                });

            });
            return model.Menu_ID.ToGuidStr();
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
                    //删除菜单的功能
                    db.Delete<Sys_MenuFunction>(w => w.MenuFunction_MenuID == item);

                    db.DeleteById<Sys_Menu>(item);
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
            var _Sys_Menu = db.FindById<Sys_Menu>(Id);

            var _Parent_Menu = db.FindById<Sys_Menu>(_Sys_Menu.Menu_ParentID);

            var Menu_Power = db
                .FindList<Sys_MenuFunction>(item => item.MenuFunction_MenuID == Id)
                .Select(w => w.MenuFunction_FunctionID);

            var di = this.EntityToDictionary(new Dictionary<string, object>()
            {
                {"_Sys_Menu",_Sys_Menu},
                {"pname",_Parent_Menu.Menu_Name.ToStr()},
                {"Menu_Power",Menu_Power},
                {"status",1}
            });
            return di;
        }

        #endregion


        #region  创建系统左侧菜单

        /// <summary>
        /// 根据角色ID 获取菜单
        /// </summary>
        /// <returns></returns>
        public List<Sys_Menu> GetMenuByRoleID()
        {
            var sql = @"select * from Sys_Menu where 1=1 and Menu_IsShow=1 order by Menu_Num asc";

            if (!this._Account.IsSuperManage)
            {
                var _roleid = this._Account.RoleID.ToGuid();
                sql = @"
                                    select * from (

                                    select Menu_ID, a.Menu_Num, Menu_Name, Menu_Url, Menu_Icon, a.Menu_ParentID, Menu_CreateTime 
                                    from (select * from Sys_Menu where 1=1 and Menu_Url is null or Menu_Url='') a
                                     join (
	                                    select Menu_Num,Menu_ParentID
		                                    from [dbo].[Sys_RoleMenuFunction] 
		                                    join Sys_Menu on Menu_ID=RoleMenuFunction_MenuID and RoleMenuFunction_RoleID='" + _roleid + @"'
		                                    group by RoleMenuFunction_MenuID,RoleMenuFunction_RoleID,Menu_Num,Menu_ParentID
                                    ) b on charindex(a.Menu_Num,b.Menu_Num)>0 and a.Menu_ID=b.Menu_ParentID
                                    union
                                    select Menu_ID, Menu_Num, Menu_Name, Menu_Url, Menu_Icon, Menu_ParentID, Menu_CreateTime 
                                    from Sys_Menu x
                                    join (
	                                    select RoleMenuFunction_MenuID,RoleMenuFunction_RoleID 
		                                    from [dbo].[Sys_RoleMenuFunction] 
		                                    group by RoleMenuFunction_MenuID,RoleMenuFunction_RoleID
                                    ) y on x.Menu_ID=y.RoleMenuFunction_MenuID and y.RoleMenuFunction_RoleID='" + _roleid + @"'

                                    ) tab
                                    order by tab.Menu_Num asc
                                ";
            }
            return db.QueryBySql<Sys_Menu>(sql).ToList();
        }

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="_StringBuilder"></param>
        public void CreateMenus(Guid Id, StringBuilder _StringBuilder)
        {
            var _Sys_Menu_List = this.GetMenuByRoleID();

            var _Parent_List = new List<Sys_Menu>();
            if (Id == Guid.Empty)
                _Parent_List = _Sys_Menu_List.Where(w => w.Menu_ParentID == null || w.Menu_ParentID == Guid.Empty).ToList();
            else
                _Parent_List = _Sys_Menu_List.Where(w => w.Menu_ParentID == Id).ToList();

            if (_Parent_List.Count > 0)
            {
                if (Id == Guid.Empty)
                {
                    _StringBuilder.Append("<ul class=\"metismenu\" id=\"menu\">");

                    _StringBuilder.Append("<li><a href=\"javascript:void(0)\" wy-router-href=\"/Admin/Home/Main/\" wy-router-text=\"首页\" class=\"has-first-menu\"><i class=\"icon icon-home fa-lg\"></i>&nbsp;&nbsp;<span>首页</span></a></li>");

                }
                else
                    _StringBuilder.Append("<ul aria-expanded=\"false\">");

                foreach (var item in _Parent_List)
                {
                    var _Child_List = _Sys_Menu_List.FindAll(w => w.Menu_ParentID != null && w.Menu_ParentID == item.Menu_ID);

                    if (_Child_List.Count > 0)
                    {
                        _StringBuilder.Append("<li>");

                        if (Id == Guid.Empty)
                        {
                            _StringBuilder.Append(string.Format("<a class=\"has-arrow has-first-menu\" href=\"javascript:void(0)\" aria-expanded=\"false\"><i class=\"{0} fa-lg\"></i>&nbsp;&nbsp;<span>{1}</span></a>", item.Menu_Icon, item.Menu_Name));
                        }
                        else
                        {
                            _StringBuilder.Append(string.Format("<a class=\"has-arrow\" href=\"javascript:void(0)\" aria-expanded=\"false\"><i class=\"{0}\"></i>&nbsp;&nbsp;<span>{1}</span></a>", item.Menu_Icon, item.Menu_Name));
                        }

                        this.CreateMenus(item.Menu_ID, _StringBuilder);
                        _StringBuilder.Append("</li>");
                    }
                    else
                    {
                        _StringBuilder.Append("<li>");
                        if (Id == Guid.Empty)
                        {
                            _StringBuilder.Append(string.Format("<a href=\"javascript:void(0);var url='{0}';\" wy-router-href=\"{0}\" wy-router-text=\"{2}\" class=\"has-first-menu\"><i class=\"{1}\"></i>&nbsp;&nbsp;<span>{2}</span></a>", item.Menu_Url, item.Menu_Icon, item.Menu_Name));
                        }
                        else
                        {
                            _StringBuilder.Append(string.Format("<a href=\"javascript:void(0);var url='{0}';\" wy-router-href=\"{0}\" wy-router-text=\"{2}\" ><i class=\"{1}\"></i>&nbsp;&nbsp;<span>{2}</span></a>", item.Menu_Url, item.Menu_Icon, item.Menu_Name));
                        }

                        _StringBuilder.Append("</li>");
                    }
                }

                _StringBuilder.Append("</ul>");

            }

        }

        #endregion  左侧菜单

        /// <summary>
        /// 获取 菜单功能 树
        /// </summary>
        /// <returns></returns>
        public List<object> GetMenuZTree()
        {
            var list = new List<object>();
            var _Sys_Menu_List = db.Query<Sys_Menu>().OrderBy(item => new { item.Menu_Num }).ToList();
            var _Sys_Function_List = db.Query<Sys_Function>().OrderBy(item => new { item.Function_Num }).ToList();
            var _Sys_MenuFunction_List = db.Query<Sys_MenuFunction>().OrderBy(item => new { item.MenuFunction_CreateTime }).ToList();
            //遍历菜单
            foreach (var item in _Sys_Menu_List)
            {
                list.Add(new
                {
                    id = item.Menu_ID,
                    name = item.Menu_Name + "(" + item.Menu_Num + ")",
                    pId = item.Menu_ParentID,
                    @checked = false,
                    chkDisabled = true
                });
                //判断本次菜单底下是否还有子菜单
                if (_Sys_Menu_List.Count(w => w.Menu_ParentID == item.Menu_ID) == 0)
                {
                    //遍历功能
                    foreach (var _Function in _Sys_Function_List)
                    {
                        //判断是否 该菜单下 是否勾选了 该功能
                        var _Sys_MenuFunction_Count = _Sys_MenuFunction_List.Count(w =>
                        w.MenuFunction_FunctionID == _Function.Function_ID &&
                        w.MenuFunction_MenuID == item.Menu_ID);

                        list.Add(new
                        {
                            id = _Function.Function_ID,
                            name = _Function.Function_Name,
                            pId = item.Menu_ID,
                            @checked = _Sys_MenuFunction_Count > 0,
                            chkDisabled = true
                        });
                    }
                }
            }
            return list;
        }




    }
}
