using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AppHtml.Control;
using System.Linq.Expressions;
using DataFramework.Class;

namespace AppHtml
{
    

    public class UI
    {
        public static Html _Html = new Html();
        private static KOViewModelControl _KOViewModelControl = new KOViewModelControl();

        /// <summary>
        /// 解析表达式
        /// </summary>
        /// <param name="Field"></param>
        /// <returns></returns>
        private static dynamic AnalysisExpression<T>(Expression<Func<T, dynamic>> Field)
            where T : class, new()
        {
            var Name = "";
            var Title = "";

            var body = Field.Body;

            MemberExpression member = null;

            if (body is UnaryExpression)
            {
                var _UnaryExpression = body as UnaryExpression;
                member = _UnaryExpression.Operand as MemberExpression;
            }
            else if (body is ConstantExpression)
            {
                //var _ConstantExpression = body as ConstantExpression;

            }
            else if (body is MethodCallExpression)
            {
                //var _ConstantExpression = body as MethodCallExpression;
            }
            else if (body is MemberExpression)
            {
                member = body as MemberExpression;
            }



            if (member == null) throw new Exception("语法错误!");

            Name = member.Member.Name;

            var _FieldAttribute = member.Member.GetCustomAttributes(typeof(FieldAttribute), false).FirstOrDefault() as FieldAttribute;
            if (_FieldAttribute == null) throw new ArgumentNullException("属性 " + Name + " 找不到 自定义特性 ObjectRemarks.FieldAttribute ");

            Title = _FieldAttribute.Alias;

            return new { Name = Name, Title = Title };
        }

        /// <summary>
        /// Input 控件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Field"></param>
        /// <param name="Placeholder"></param>
        /// <param name="Attribute"></param>
        /// <param name="Col"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static MvcHtmlString Input<T>(Expression<Func<T, dynamic>> Field, int Col = 6, string Title = null, object Attribute = null)
            where T : class, new()
        {
            var Obj = AnalysisExpression(Field);

            if (string.IsNullOrEmpty(Title)) Title = Obj.Title;

            return MvcHtmlString.Create(_Html.Input(Title, Obj.Name, Attribute, Col));
        }

        /// <summary>
        /// Input 控件 自定义属性 方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Field"></param>
        /// <param name="Attribute"></param>
        /// <param name="Col"></param>
        /// <returns></returns>
        public static MvcHtmlString InputCustom<T>(Expression<Func<T, object>> Field, int Col = 6, object Attribute = null)
            where T : class, new()
        {
            var Obj = AnalysisExpression(Field);

            return MvcHtmlString.Create(_Html.Input(Obj.Title, Attribute, Col));
        }

        /// <summary>
        /// Slect 控件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Field"></param>
        /// <param name="FuncOpetion"></param>
        /// <param name="Col"></param>
        /// <param name="Attribute"></param>
        /// <returns></returns>
        public static MvcHtmlString Select<T>(Expression<Func<T, object>> Field, Func<string> FuncOpetion, int Col = 6, string Title = null, object Attribute = null)
            where T : class, new()
        {
            var Obj = AnalysisExpression(Field);

            if (string.IsNullOrEmpty(Title)) Title = Obj.Title;

            return MvcHtmlString.Create(_Html.Select(Title, Obj.Name, FuncOpetion, Attribute, Col));
        }

        /// <summary>
        /// Slect 控件 自定义属性 方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Field"></param>
        /// <param name="Attribute"></param>
        /// <param name="FuncOpetion"></param>
        /// <param name="Col"></param>
        /// <returns></returns>
        public static MvcHtmlString SelectCustom<T>(Expression<Func<T, object>> Field, Func<string> FuncOpetion, int Col = 6, object Attribute = null)
        where T : class, new()
        {
            var Obj = AnalysisExpression(Field);

            return MvcHtmlString.Create(_Html.Select(Obj.Title, Attribute, FuncOpetion, Col));
        }

        /// <summary>
        /// 查找带回 控件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <param name="Text"></param>
        /// <param name="ID"></param>
        /// <param name="Url"></param>
        /// <param name="FindClick"></param>
        /// <param name="RemoveClick"></param>
        /// <param name="Col"></param>
        /// <param name="Placeholder"></param>
        /// <param name="KO"></param>
        /// <param name="Readonly"></param>
        /// <returns></returns>
        public static MvcHtmlString FindBack<T, T1>(Expression<Func<T, object>> Text, Expression<Func<T1, object>> ID, string Url, string FindClick, string RemoveClick, int Col = 6, string Title = null, string Placeholder = null, bool KO = true, bool Readonly = true)
            where T : class, new()
            where T1 : class, new()
        {
            var Obj = AnalysisExpression(Text);
            var Obj1 = AnalysisExpression(ID);

            if (string.IsNullOrEmpty(Title))
                Title = Obj.Title;

            var _New_Placeholder = (string.IsNullOrEmpty(Placeholder) ? "请选择 " + Title : Placeholder);

            return MvcHtmlString.Create(_Html.FindBack(Title, Obj.Name, Obj1.Name, Url, FindClick, RemoveClick, Col, _New_Placeholder, KO, Readonly));
        }

        /// <summary>
        /// 上传图片 控件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Field"></param>
        /// <param name="Col"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <returns></returns>
        public static MvcHtmlString UploadImage<T>(Expression<Func<T, object>> Field, int Col = 6, bool IsUpBtn = true, string Tips = "请点击此处选择图片文件")
         where T : class, new()
        {
            var Obj = AnalysisExpression(Field);

            return MvcHtmlString.Create(_Html.UploadImage(Obj.Title, Obj.Name, Tips, Col, IsUpBtn));
        }

        /// <summary>
        /// 上传文件 控件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Field"></param>
        /// <param name="Col"></param>
        /// <param name="Attribute"></param>
        /// <returns></returns>
        public static MvcHtmlString UploadFile<T>(Expression<Func<T, object>> Field, int Col = 6, string DownloadName = null, string Tips = "请点击此处选择文件")
            where T : class, new()
        {
            var Obj = AnalysisExpression(Field);

            return MvcHtmlString.Create(_Html.UploadFile(Obj.Title, Obj.Name, DownloadName, Col, Tips));
        }

        /// <summary>
        /// 上传图片 控件(多文件)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Field"></param>
        /// <param name="Col"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <returns></returns>
        public static MvcHtmlString UploadImageMultiple<T>(Expression<Func<T, object>> Field, string EventChange, int Col = 6, string Tips = "请点击此处选则图片文件")
            where T : class, new()
        {
            var Obj = AnalysisExpression(Field);

            return MvcHtmlString.Create(_Html.UploadImageMultiple(Obj.Title, Obj.Name, Col, EventChange, Tips));
        }

        /// <summary>
        /// 上传文件 控件(多文件)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Field"></param>
        /// <param name="Col"></param>
        /// <param name="Attribute"></param>
        /// <returns></returns>
        public static MvcHtmlString UploadFileMultiple<T>(Expression<Func<T, object>> Field, string EventChange, int Col = 6, string Tips = "请点击此处选则文件")
            where T : class, new()
        {
            var Obj = AnalysisExpression(Field);

            return MvcHtmlString.Create(_Html.UploadFileMultiple(Obj.Title, Obj.Name, Col, EventChange, Tips));
        }

        /// <summary>
        /// UEditor 编辑器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Field"></param>
        /// <param name="Col"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <returns></returns>
        public static MvcHtmlString UEditor<T>(Expression<Func<T, object>> Field, int Col = 6, string Title = null, string Width = "100%", string Height = "300px")
            where T : class, new()
        {

            var Obj = AnalysisExpression(Field);

            if (string.IsNullOrEmpty(Title)) Title = Obj.Title;

            return MvcHtmlString.Create(_Html.UEditor(Title, Obj.Name, Col, Width, Height));
        }

        /// <summary>
        /// Textarea 大文本框
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Field"></param>
        /// <param name="Col"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <returns></returns>
        public static MvcHtmlString Textarea<T>(Expression<Func<T, object>> Field, int Col = 6, string Title = null, object Attribute = null, string Placeholder = null)
            where T : class, new()
        {
            var Obj = AnalysisExpression(Field);

            if (string.IsNullOrEmpty(Title)) Title = Obj.Title;

            return MvcHtmlString.Create(_Html.Textarea(Title, Obj.Name, Col, Attribute, Placeholder));

        }


        /// <summary>
        /// 创建 KO 实体
        /// </summary>
        /// <returns></returns>
        public static MvcHtmlString CreateKOViewModel(params object[] Models)
        {
            return MvcHtmlString.Create(_KOViewModelControl.ScriptStr(Models).ToString());
        }





    }
}
