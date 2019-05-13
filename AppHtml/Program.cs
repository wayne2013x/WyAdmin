using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppHtml.BaseControl;
using Entity.SysClass;
using System.Linq.Expressions;

namespace AppHtml
{
    


    class Program
    {
        static void Main(string[] args)
        {
            System.Diagnostics.Stopwatch s = new System.Diagnostics.Stopwatch();
            s.Start();

            //
            Console.WriteLine(" UI.Input<Sys_UserM>(item => item.User_LoginName); >> " + UI.Input<Sys_User>(item => item.User_LoginName) + "\r\n");
            Console.WriteLine(" UI.InputCustom<Sys_UserM>(item => item.User_LoginName, 6, new { 我是自定义属性 = \"123\" }); >> " + UI.InputCustom<Sys_User>(item => item.User_LoginName, 6, new { 我是自定义属性 = "123" }) + "\r\n");


            Console.WriteLine(" 耗时：" + (s.ElapsedMilliseconds * 0.001) + " s");
            Console.ReadKey();

        }
    }
}
