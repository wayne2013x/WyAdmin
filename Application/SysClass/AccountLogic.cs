using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SysClass
{
    using Common;
    using Application.Class;
    using Entity.SysClass;
    using DataFramework;

    public class AccountLogic : BaseLogic<Sys_User>
    {

        /// <summary>
        /// 账号检查
        /// </summary>
        /// <param name="uName"></param>
        /// <param name="uPwd"></param>
        /// <param name="loginCode"></param>
        public void Checked(string uName, string uPwd, string loginCode)
        {
            if (string.IsNullOrEmpty(uName))
                throw new MessageBox("请输入用户名");
            if (string.IsNullOrEmpty(uPwd))
                throw new MessageBox("请输入密码");
            if (string.IsNullOrEmpty(loginCode))
                throw new MessageBox("请输入验证码");

            var _Sys_User = db.Find<Sys_User>(w => w.User_LoginName == uName);

            if (_Sys_User.User_ID.ToGuid() == Guid.Empty)
                throw new MessageBox("用户不存在");
            if (_Sys_User.User_Pwd.ToStr().Trim() != uPwd)//Tools.MD5Encrypt(userpwd)))//
                throw new MessageBox("密码错误");
            string code = Tools.GetCookie("loginCode");
            if (string.IsNullOrEmpty(code))
                throw new MessageBox("验证码失效");
            if (!code.ToLower().Equals(loginCode.ToLower()))
                throw new MessageBox("验证码不正确");

            var _Sys_UserRole = db.Find<Sys_UserRole>(w => w.UserRole_UserID == _Sys_User.User_ID);
            var _Sys_Role = db.Find<Sys_Role>(w => w.Role_ID == _Sys_UserRole.UserRole_RoleID);

            var _Account = new Account();
            _Account.RoleID = _Sys_Role.Role_ID.ToGuid();
            _Account.UserID = _Sys_User.User_ID.ToGuid();
            _Account.UserName = _Sys_User.User_Name;
            //如果是超级管理员 帐户
            _Account.IsSuperManage = _Sys_Role.Role_ID == AppConfig.Admin_RoleID.ToGuid();

            Tools.SetSession("Account", _Account);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="oldpwd"></param>
        /// <param name="newpwd"></param>
        /// <param name="newlypwd"></param>
        public void ChangePwd(string oldpwd, string newpwd, string newlypwd)
        {
            if (string.IsNullOrEmpty(oldpwd)) throw new MessageBox("旧密码不能为空");
            if (string.IsNullOrEmpty(newpwd)) throw new MessageBox("新密码不能为空");
            if (string.IsNullOrEmpty(newlypwd)) throw new MessageBox("重复新密码不能为空");
            if (newpwd != newlypwd) throw new MessageBox("两次密码不一致");
            if (this.Get(_Account.UserID).User_Pwd != oldpwd) throw new MessageBox("旧密码不正确");
            db.Update<Sys_User>(w => w.User_ID == _Account.UserID,() => new Sys_User
            {
                User_Pwd = newlypwd
            });
        }

    }
}
