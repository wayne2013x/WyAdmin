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

    public class Sys_RoleMenuFunctionLogic : BaseLogic<Sys_RoleMenuFunction>
    {

        #region  增、删、改、查

        #endregion

        /// <summary>
        /// 获取角色菜单功能树
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public List<object> GetRoleMenuFunctionZTree(Guid RoleId)
        {
            var list = new List<object>();
            var _Sys_Menu_List = db
              .Query<Sys_Menu>()
              .OrderBy(item => new { item.Menu_Num })
              .ToList();
            var _Sys_Function_List = db
                .Query<Sys_Function>()
                .OrderBy(item => new { item.Function_Num })
                .ToList();
            var _Sys_MenuFunction_List = db
                .Query<Sys_MenuFunction>()
                .OrderBy(item => new { item.MenuFunction_CreateTime })
                .ToList();
            var _Sys_RoleMenuFunction = db.Query<Sys_RoleMenuFunction>().ToList();

            foreach (var item in _Sys_Menu_List)
            {
                list.Add(new
                {
                    id = item.Menu_ID,
                    name = item.Menu_Name + "(" + item.Menu_Num + ")",
                    pId = item.Menu_ParentID,
                    tag = "Menu",
                    @checked = false
                });
                //判断是否为末级菜单
                if (_Sys_Menu_List.Count(w => w.Menu_ParentID == item.Menu_ID) == 0)
                {
                    //遍历 菜单拥有的功能
                    var _SysMenuFunctionList = _Sys_MenuFunction_List.ToList().FindAll(w => w.MenuFunction_MenuID == item.Menu_ID);
                    foreach (var _MenuFunction in _SysMenuFunctionList)
                    {
                        //得到功能信息
                        var _Function = _Sys_Function_List.ToList().Find(w => w.Function_ID == _MenuFunction.MenuFunction_FunctionID);
                        
                        //判断该角色 对应的菜单和功能是否存在
                        var _Count = _Sys_RoleMenuFunction.Count(w =>
                          w.RoleMenuFunction_RoleID == RoleId &&
                          w.RoleMenuFunction_MenuID == item.Menu_ID &&
                          w.RoleMenuFunction_FunctionID == _MenuFunction.MenuFunction_FunctionID);

                        list.Add(new
                        {
                            id = _MenuFunction.MenuFunction_FunctionID,
                            name = _Function.Function_Name,
                            pId = item.Menu_ID,
                            tag = "MenuFunction",
                            @checked = _Count > 0
                        });
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 保存角色功能树
        /// </summary>
        /// <param name="Sys_RoleMenuFunction_List"></param>
        /// <param name="RoleId"></param>
        public void SaveFunction(string Sys_RoleMenuFunction_List, Guid RoleId)
        {
            db.Commit(() =>
            {
                db.Delete<Sys_RoleMenuFunction>(w => w.RoleMenuFunction_RoleID == RoleId);
                Sys_RoleMenuFunction_List.DeserializeObject<List<Sys_RoleMenuFunction>>().ForEach(item =>
                {
                    db.Insert(item);
                });
            });
        }



    }
}
