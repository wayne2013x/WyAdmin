using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppHtml.Control
{
    using BaseControl;

    public class Html
    {

        /// <summary>
        /// 将匿名对象转换为字典
        /// </summary>
        /// <param name="Attribute"></param>
        /// <returns></returns>
        public Dictionary<string, string> ObjectToDictionary(object Attribute)
        {
            var di = new Dictionary<string, string>();

            Type ty = Attribute.GetType();

            var fields = ty.GetProperties().ToList();

            foreach (var item in fields)
            {
                var Name = "";
                if (item.Name.Contains("_"))
                    Name = item.Name.Replace("_", "-");
                else
                    Name = item.Name;

                di.Add(Name, item.GetValue(Attribute).ToString());
            }

            return di;
        }

        /// <summary>
        /// Input
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Name"></param>
        /// <param name="Placeholder"></param>
        /// <param name="Col"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public string Input(string Title, string Name, object Attribute = null, int Col = 6)
        {
            //<div class="col-sm-6 col-md-4">
            //    <h4 class="example-title">会员名称</h4>
            //    <input type="text" class="form-control" name="Member_Name" data-bind="value:Member_Name" placeholder="请输入 会员名称" />
            //</div>

            var H4 = PageControl.H4(new Dictionary<string, string> { 
                {"class","example-title"}
            }, Title);

            var _Input_Attribute = new Dictionary<string, string>();
            _Input_Attribute.Add("type", "text");
            _Input_Attribute.Add("class", "form-control");
            _Input_Attribute.Add("name", Name);
            _Input_Attribute.Add("data-bind", "value:" + Name);
            _Input_Attribute.Add("placeholder", "请输入 " + Title);

            if (Attribute != null)
            {
                foreach (var item in ObjectToDictionary(Attribute))
                {
                    _Input_Attribute[item.Key] = item.Value;
                }
            }

            var Input = PageControl.Input(_Input_Attribute);

            var _Div_Attribute = new Dictionary<string, string>();
            _Div_Attribute.Add("class", "col-sm-" + Col);
            return PageControl.Div(_Div_Attribute, H4 + Input);
        }

        /// <summary>
        /// Input
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Attribute"></param>
        /// <param name="Col"></param>
        /// <returns></returns>
        public string Input(string Title, object Attribute, int Col = 6)
        {
            //<div class="col-sm-6 col-md-4">
            //    <h4 class="example-title">会员名称</h4>
            //    <input type="text" class="form-control" name="Member_Name" data-bind="value:Member_Name" placeholder="请输入 会员名称" />
            //</div>

            var H4 = PageControl.H4(new Dictionary<string, string> { 
                {"class","example-title"}
            }, Title);
            var Input = PageControl.Input(ObjectToDictionary(Attribute));

            var _Div_Attribute = new Dictionary<string, string>();
            _Div_Attribute.Add("class", "col-sm-" + Col);
            return PageControl.Div(_Div_Attribute, H4 + Input);
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Name"></param>
        /// <param name="FuncOpetion"></param>
        /// <param name="Placeholder"></param>
        /// <param name="Attribute"></param>
        /// <param name="Col"></param>
        /// <returns></returns>
        public string Select(string Title, string Name, Func<string> FuncOpetion, object Attribute = null, int Col = 6)
        {
            //<div class="col-sm-6 col-md-4">
            //    <h4 class="example-title">性别</h4>
            //    <select class="form-control" name="Member_Sex" data-bind="value:Member_Sex">
            //        <option value="">==请选择 性别==</option>
            //        @foreach (var item in sexList)
            //        {
            //            <option value="@item">@item</option>
            //        }
            //    </select>
            //</div>

            var Options = "<option value=\"\">==请选择 " + Title + "==</option>";
            Options += FuncOpetion();

            var H4 = PageControl.H4(new Dictionary<string, string> { 
                {"class","example-title"}
            }, Title);

            var _Select_Attribute = new Dictionary<string, string>();
            _Select_Attribute.Add("class", "form-control");
            _Select_Attribute.Add("name", Name);
            _Select_Attribute.Add("data-bind", "value:" + Name);

            if (Attribute != null)
            {
                foreach (var item in ObjectToDictionary(Attribute))
                {
                    _Select_Attribute[item.Key] = item.Value;
                }
            }

            var Select = PageControl.Select(_Select_Attribute, Options);

            var _Div_Attribute = new Dictionary<string, string>();
            _Div_Attribute.Add("class", "col-sm-" + Col);
            return PageControl.Div(_Div_Attribute, H4 + Select);
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Attribute"></param>
        /// <param name="FuncOpetion"></param>
        /// <param name="Col"></param>
        /// <returns></returns>
        public string Select(string Title, object Attribute, Func<string> FuncOpetion, int Col = 6)
        {
            //<div class="col-sm-6 col-md-4">
            //    <h4 class="example-title">性别</h4>
            //    <select class="form-control" name="Member_Sex" data-bind="value:Member_Sex">
            //        <option value="">==请选择 性别==</option>
            //        @foreach (var item in sexList)
            //        {
            //            <option value="@item">@item</option>
            //        }
            //    </select>
            //</div>

            var Options = "==请选择 " + Title + "==";
            Options += FuncOpetion();

            var H4 = PageControl.H4(new Dictionary<string, string> { 
                {"class","example-title"}
            }, Title);

            var Select = PageControl.Select(ObjectToDictionary(Attribute), Options);

            var _Div_Attribute = new Dictionary<string, string>();
            _Div_Attribute.Add("class", "col-sm-" + Col);
            return PageControl.Div(_Div_Attribute, H4 + Select);
        }

        /// <summary>
        /// 查找带回
        /// </summary>
        /// <param name="Title"></param>
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
        public string FindBack(string Title, string Text, string ID, string Url, string FindClick, string RemoveClick, int Col = 6, string Placeholder = null, bool KO = true, bool Readonly = true)
        {
            //<div class="col-sm-6">
            //    <h4 class="example-title">用户名称</h4>
            //    <div class="input-group">
            //        <input type="text" class="form-control" name="User_Name" data-bind="value:User_Name" placeholder="请选择 用户名称" readonly="readonly" />
            //        <input type="text" class="form-control" style="display:none" name="Member_UserID" data-bind="value:Member_UserID" />
            //        <span class="input-group-btn">
            //            <button type="button" class="btn btn-outline btn-default" onclick="admin.findBack.open('/Admin/User?findback=Multiple','请选择 用户名称',function(row){
            //        App.FindBack.CallBack(row,'User')
            //    });">
            //                <i class="fa fa-search"></i>
            //            </button><button type="button" class="btn btn-outline btn-default" onclick="App.FindBack.CallBack(null,'User');"><i class="fa fa-close"></i></button>
            //        </span>
            //    </div>
            //</div>

            var ReadonlyHtml = "";
            if (Readonly)
            {
                ReadonlyHtml += " readonly=\"readonly\" ";
            }

            var _Text_KoHtml = "";
            var _ID_KoHtml = "";
            if (KO)
            {
                _Text_KoHtml = " data-bind=\"value:" + Text + "\"";
                _ID_KoHtml = " data-bind=\"value:" + ID + "\"";
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"col-sm-" + Col + "\">");
            sb.Append("    <h4 class=\"example-title\">" + Title + "</h4>");
            sb.Append("    <div class=\"input-group\">");
            sb.Append("        <input type=\"text\" class=\"form-control\" name=\"" + Text + "\" " + _Text_KoHtml + " placeholder=\"" + Placeholder + "\" " + ReadonlyHtml + "/>");
            sb.Append("        <input type=\"text\" class=\"form-control hidden-xs-up\" name=\"" + ID + "\" " + _ID_KoHtml + "/>");
            sb.Append("        <span class=\"input-group-btn\">");
            sb.Append("            <button type=\"button\" class=\"btn btn-outline btn-default\" onclick=\"admin.findBack.open('" + Url + "','" + Placeholder + "',function(row){ " + FindClick + " });\">");
            sb.Append("                <i class=\"fa fa-search\"></i>");
            sb.Append("            </button><button type=\"button\" class=\"btn btn-outline btn-default\" onclick=\"" + RemoveClick + "\"><i class=\"fa fa-close\"></i></button>");
            sb.Append("        </span>");
            sb.Append("    </div>");
            sb.Append("</div>");
            return sb.ToString();
        }

        /// <summary>
        /// 上传 图片 控件
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Name"></param>
        /// <param name="Col"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <returns></returns>
        public string UploadImage(string Title, string Name, string Tips, int Col, bool IsUpBtn)
        {
            

            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"col-sm-" + Col + "\">");
            sb.Append("    <h4 class=\"example-title\">" + Title + "</h4>");
            sb.Append("    <div class=\"wy-upfile\">");
            sb.Append("        <div class=\"wy-upfile-item\" " + (IsUpBtn ? "" : "style='height:200px;'") + ">");
            sb.Append("            <img height=\"" + (IsUpBtn ? "140" : "186") + "\" data-bind=\"visible:" + Name + ",attr:{'src':" + Name + "}\" />");
            sb.Append("        </div>");
            if (IsUpBtn)
            {
                sb.Append("        <div class=\"wy-upfile-shade\" onclick=\"$('input[name=" + Name + "]').click();\">" + Tips + "</div>");
                sb.Append("        <input type=\"file\" accept=\"image/*\" name=\"" + Name + "\" class=\"hide\" onchange=\"if(this.files[0])model." + Name + "(admin.getObjectUrl(this.files[0]));\">");
            }

            sb.Append("    </div>");
            sb.Append("</div>");
            return sb.ToString().Trim();
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Name"></param>
        /// <param name="DownloadName"></param>
        /// <param name="Placeholder"></param>
        /// <param name="Attribute"></param>
        /// <param name="Col"></param>
        /// <returns></returns>
        public string UploadFile(string Title, string Name, string DownloadName, int Col, string Tips)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"col-sm-" + Col + "\">");
            sb.Append("    <h4 class=\"example-title\">" + Title + "</h4>");
            sb.Append("    <div class=\"wy-upfile\">");
            sb.Append("        <div class=\"wy-upfile-item\">");

            if (string.IsNullOrEmpty(DownloadName))
            {
                sb.Append("            <a data-bind=\"visible:" + Name + ",attr:{'href':" + Name + ",'title':" + Name + "},text:" + Name + "\" target=\"_blank\"></a>");
            }
            else
            {
                sb.Append("            <a data-bind=\"visible:" + Name + ",attr:{'href':" + Name + ",'title':" + Name + ",'download':" + DownloadName + "},text:" + Name + "\" target=\"_blank\"></a>");
            }

            sb.Append("        </div>");
            sb.Append("        <div class=\"wy-upfile-shade\" onclick=\"$('input[name=" + Name + "]').click();\">" + Tips + "</div>");
            sb.Append("        <input type=\"file\" class=\"hide\" name=\"" + Name + "\" onchange=\"if (this.files[0]) { model." + Name + "(this.files[0].name); $(this).parent().find('.wy-upfile-item a').removeAttr('href') }\" />");
            sb.Append("    </div>");
            sb.Append("</div>");
            return sb.ToString().Trim();
        }

        /// <summary>
        /// 上传 图片 控件(多文件)
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Name"></param>
        /// <param name="Col"></param>
        /// <param name="EventChange"></param>
        /// <param name="Tips"></param>
        /// <returns></returns>
        public string UploadImageMultiple(string Title, string Name, int Col, string EventChange, string Tips)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"col-sm-" + Col + "\">");
            sb.Append("    <h4 class=\"example-title\">" + Title + "</h4>");
            sb.Append("    <div class=\"wy-upfile\">");
            sb.Append("        <div class=\"wy-upfile-item\">");
            sb.Append("            <!--<img height=\"140\" data-bind=\"visible:" + Name + ",attr:{'src':" + Name + "}\" />-->");
            sb.Append("        </div>");
            sb.Append("        <div class=\"wy-upfile-shade\" onclick=\"$('input[name=" + Name + "]').click();\">" + Tips + "</div>");
            sb.Append("        <input type=\"file\" accept=\"image/*\" name=\"" + Name + "\" class=\"hide\" onchange=\"" + EventChange + "\" multiple=\"multiple\">");
            sb.Append("    </div>");
            sb.Append("</div>");
            return sb.ToString().Trim();
        }

        /// <summary>
        /// 上传文件(多文件)
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Name"></param>
        /// <param name="DownloadName"></param>
        /// <param name="Col"></param>
        /// <param name="EventChange"></param>
        /// <param name="Tips"></param>
        /// <returns></returns>
        public string UploadFileMultiple(string Title, string Name, int Col, string EventChange, string Tips)
        {
            //<div class="col-sm-6">
            //    <h4 class="example-title">文件路径</h4>
            //    <div class="wy-upfile">
            //        <div class="wy-upfile-item">
            //            <a data-bind="visible:Member_FilePath,attr:{'href':Member_FilePath,'title':Member_FilePath},text:Member_FilePath" target="_blank"></a>
            //            <a data-bind="visible:Member_FilePath,attr:{'href':Member_FilePath,'title':Member_FilePath},text:Member_FilePath" target="_blank"></a>
            //            <a data-bind="visible:Member_FilePath,attr:{'href':Member_FilePath,'title':Member_FilePath},text:Member_FilePath" target="_blank"></a>
            //        </div>
            //        <div class="wy-upfile-shade" onclick="$('input[name=Member_FilePath]').click();">请点击此处选则文件</div>
            //        <input type="file" class="hide" name="Member_FilePath" onchange="if (this.files[0]) { model.Member_FilePath(this.files[0].name); $(this).parent().find('.wy-upfile-item a').removeAttr('href') }" />
            //    </div>
            //</div>

            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"col-sm-" + Col + "\">");
            sb.Append("    <h4 class=\"example-title\">" + Title + "</h4>");
            sb.Append("    <div class=\"wy-upfile\">");
            sb.Append("        <div class=\"wy-upfile-item\">");
            sb.Append("            <!--<a data-bind=\"visible:" + Name + ",attr:{'href':" + Name + ",'title':" + Name + "},text:" + Name + "\" target=\"_blank\"></a>-->");
            sb.Append("        </div>");
            sb.Append("        <div class=\"wy-upfile-shade\" onclick=\"$('input[name=" + Name + "]').click();\">" + Tips + "</div>");
            sb.Append("        <input type=\"file\" class=\"hide\" name=\"" + Name + "\" onchange=\"" + EventChange + "\" multiple=\"multiple\" />");
            sb.Append("    </div>");
            sb.Append("</div>");
            return sb.ToString().Trim();
        }

        /// <summary>
        /// UEditor 编辑器
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Name"></param>
        /// <param name="Col"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <returns></returns>
        public string UEditor(string Title, string Name, int Col = 6, string Width = "100%", string Height = "300px")
        {
            //<div class="col-sm-12">
            //    <h4 class="example-title">介绍</h4>
            //    <script id="Member_Introduce" type="text/plain" style="width:100%;height:300px;">
            //    </script>
            //</div>

            var H4 = PageControl.H4(new Dictionary<string, string> { 
                {"class","example-title"}
            }, Title);

            var _Script = PageControl.Script(new Dictionary<string, string>()
            {
                {"id",Name},
                {"type","text/plain"},
                {"style","width:"+Width+";height:"+Height+";"}
            }, "");

            var _Div_Attribute = new Dictionary<string, string>();
            _Div_Attribute.Add("class", "col-sm-" + Col);
            return PageControl.Div(_Div_Attribute, H4 + _Script);
        }

        /// <summary>
        /// Textarea 大文本框
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Name"></param>
        /// <param name="Col"></param>
        /// <param name="Rows"></param>
        /// <param name="Cols"></param>
        /// <returns></returns>
        public string Textarea(string Title, string Name, int Col, object Attribute, string Placeholder)
        {
            //<div class="col-sm-6">
            //    <h4 class="example-title">备注：</h4>
            //    <textarea class="form-control" data-bind="value:WebSiteLabel_Remark" placeholder="请输入备注" rows="5"></textarea>
            //</div>

            var H4 = PageControl.H4(new Dictionary<string, string> { 
                {"class","example-title"}
            }, Title);

            var _Textarea_Attribute = new Dictionary<string, string>();
            _Textarea_Attribute.Add("class", "form-control");
            _Textarea_Attribute.Add("name", Name);
            _Textarea_Attribute.Add("data-bind", "value:" + Name);
            _Textarea_Attribute.Add("Placeholder", (string.IsNullOrEmpty(Placeholder) ? "请输入 " + Title : Placeholder));

            if (Attribute != null)
            {
                foreach (var item in ObjectToDictionary(Attribute))
                {
                    _Textarea_Attribute[item.Key] = item.Value;
                }
            }

            var Textarea = PageControl.Textarea(_Textarea_Attribute);

            var _Div_Attribute = new Dictionary<string, string>();
            _Div_Attribute.Add("class", "col-sm-" + Col);
            return PageControl.Div(_Div_Attribute, H4 + Textarea);
        }



































    }
}
