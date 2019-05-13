using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.SysClass
{
    public class Account : Class.BaseClass
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid RoleID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserID { get; set; }
        /// <summary>
        /// 当前登录人 是否是 超级管理员
        /// </summary>
        public bool IsSuperManage { get; set; }

        public Account()
        {
            this.IsSuperManage = false;
        }




    }
}
