var admin = {
    layer: window.top.layer,
    init: function () {
        admin.setPower();
        admin.ajaxFilter();
        admin.onError();
        //layer 扩展皮肤
        layer.config({ extend: '../retina.css' });
        jQuery(function () {

        });
    },
    ajaxFilter: function () {
        jQuery(document).ajaxStart(function () {

        });
        jQuery(document).ajaxSend(function () {

        });
        jQuery(document).ajaxSuccess(function () {

        });
        jQuery(document).ajaxComplete(function (event, xhr, options) {
            var text = xhr.responseText;
            try {
                var json = JSON.parse(text);

                //信息提示 = 10,
                //登录超时 = 20,
                //手动处理 = 30,
                //程序异常 = 50

                switch (json.status) {
                    case 10:
                        admin.msg(json.msg, "警告");
                        break;
                    case 20:
                        //alert('登录超时！系统将退出重新登录！');
                        top.window.location = json.msg;
                        //admin.confirm("登录超时！是否退出系统重新登录？", function () { top.window.location = json.msg }, function () { }, "登录超时！");
                        break;
                    case 50:
                        admin.ajax({
                            type: "post",
                            url: "/Admin/Error/Index",
                            data: json,
                            dataType: "html",
                            success: function (h) {
                                $('html').empty().html(h);
                            }
                        });
                        break;
                };


            } catch (e) {
                //throw new Error(e.message);
            }
        });
        jQuery(document).ajaxError(function (event, xhr, options, exc) {
            //event - 包含 event 对象
            //xhr - 包含 XMLHttpRequest 对象
            //options - 包含 AJAX 请求中使用的选项
            //exc - 包含 JavaScript exception

            admin.loading.end();

            var text = xhr.responseText;
            var json = JSON.parse(text);
            if (json.status !== 10 && json.status !== 20 && json.status !== 30 && json.status !== 50) {
                throw new Error("ajax请求错误!");
            }

        });
        jQuery(document).ajaxStop(function () {

        });
    },
    //异常捕获
    onError: function () {
        window.onerror = function (msg, url, line, col, error) {
            //alert(msg);
        }
    },
    //消息框
    alert: function (content, type, title) {
        if (type == "警告")
            return admin.layer.alert(content, { icon: 0, offset: "t", title: title || "警告" });
        else if (type == "成功")
            return admin.layer.alert(content, { icon: 1, offset: "t", title: title || "成功" });
        else if (type == "错误")
            return admin.layer.alert(content, { icon: 2, offset: "t", title: title || "错误" });
        else
            return admin.layer.alert(content, { offset: "t", title: title || "提醒" });
    },
    //提示框
    msg: function (content, type, time) {
        if (type == "警告")
            return admin.layer.msg(content, { icon: 0, offset: "t", time: time || 10 * 1000 });
        else if (type == "成功")
            return admin.layer.msg(content, { icon: 1, offset: "t", time: time || 10 * 1000 });
        else if (type == "错误")
            return admin.layer.msg(content, { icon: 2, offset: "t", time: time || 10 * 1000 });
        else
            return admin.layer.msg(content, { offset: "t", time: time || 10 * 1000 });
    },
    //询问框
    confirm: function (content, yes, cancel, title) {
        admin.layer.confirm(content, { icon: 3, offset: "t", title: title || "提醒" }, yes, cancel);
    },
    //加载
    loading: {
        index: 0,
        start: function () {
            this.index = admin.layer.load(0, { shade: [0.1, "#fff"], time: 600 * 60 * 1000 });
        },
        end: function () {
            var _this = this;
            admin.layer.close(_this.index);
        }
    },
    //Ajax请求
    ajax: function (options) {
        var defaults = {
            type: "post",
            url: "",
            dataType: "json",
            data: {},
            success: null,
            async: true,
            isupfile: false,//是否 开启上传文件
            loading: false
        };
        var options = $.extend(defaults, options);
        if (!options.url)
            return false;
        if (options.loading)
            admin.loading.start();

        var ajaxOptions = {
            type: options.type,
            url: options.url,
            dataType: options.dataType,
            data: options.data,
            async: options.async,
            success: function (r) {
                if (options.loading)
                    admin.loading.end();
                options.success(r);
            },
            beforeSend: function () {
            },
            complete: function () {

            }
        };

        if (options.isupfile) {
            ajaxOptions.processData = false;
            ajaxOptions.contentType = false;
            ajaxOptions.cache = false;
        }

        $.ajax(ajaxOptions);
    },
    //根据键取地址栏中的值
    getQueryString: function (key) {
        var reg = new RegExp("(^|&)" + key + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return r[2]; return "";
    },
    //建立一個可存取到該file的url  用于上传图片，，可通过该地址浏览图片
    getObjectUrl: function (file) {
        var url = "";
        if (window.createObjectURL != undefined) { // basic
            url = window.createObjectURL(file);
        } else if (window.URL != undefined) { // mozilla(firefox)
            url = window.URL.createObjectURL(file);
        } else if (window.webkitURL != undefined) { // webkit or chrome
            url = window.webkitURL.createObjectURL(file);
        }
        return url;
    },
    //将图片对象转换为 base64
    readFile: function (obj, callBack) {
        var file = obj.files[0];
        var resVal;
        //判断类型是不是图片  
        if (!/image\/\w+/.test(file.type)) {
            alert("请确保文件为图像类型");
            return false;
        }
        var reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = function (e) {
            //alert(this.result); //就是base64  
            resVal = this.result;
            if (callBack) callBack(resVal);
            //return resVal;
        }

    },
    setSession: function (key, value) {
        window.top.localStorage.setItem(key, JSON.stringify(value));
    },
    getSession: function (key) {
        return JSON.parse(window.top.localStorage.getItem(key));
    },
    koMapping: function (json, obj, objName) {
        //json 映射到 ko 实体中去
        var code = "";
        for (var prop in json) {
            if (obj.hasOwnProperty(prop)) {
                code += objName + "." + prop + "(";

                var value = json[prop];
                if (value) {
                    if ((typeof value == 'string') && value.constructor == String) {
                        value = value.replace(/\r/g, "\\r").replace(/\n/g, "\\n").replace(/\'/g, "\\'");
                        //判断是否 是这种格式 /Date(1539187200000)/
                        if (value.indexOf('/Date(') >= 0 && value.lastIndexOf(')/') >= 0) {
                            value = new Date(parseInt(value.substring(6, value.lastIndexOf(')/')))).Format('yyyy-MM-dd hh:mm');
                        }
                    }
                    code += "'" + value + "'";
                }

                code += "); ";
            }
        }
        if (code) eval(code);
    },
    vModelEmpty: function (obj, objName) {
        //重置ko 的 实体
        var code = "";
        for (item in obj) {
            code += objName + "." + item + "(''); ";
        }
        if (code) eval(code);
    },
    getParentFrameName: function () {
        return top.$("#" + admin.getIframeName()).attr("data-parentiframename");
    },
    //获取系统框架 中 打开的 iframe 对象
    getWinIframe: function () {
        return top.$("Content>.wy-navTab-li.active").attr("name");
    },
    //获取 layer iframe 窗口的 index
    getLayerIframeIndex: function () {
        return admin.layer.getFrameIndex(window.name);
    },
    getIframeName: function () {
        return "layui-layer-iframe" + admin.getLayerIframeIndex();
    },
    //根据Iframe的name 获取Ifram对象 dx对象可不用传递
    getIframeObj: function (name, dx) {
        var f;
        if (dx == "" || dx == null || dx == undefined) {
            dx = "top"; f = eval(dx + ".frames");
        }
        else {
            f = eval(dx);
        }
        for (var i = 0; i < f.length; i++) {
            if (f[i].length > 0) {
                return admin.getIframeObj(name, dx + ".frames['" + f[i].name + "']");
            }
            else {
                if (f[i].name == name) {
                    return dx + ".frames['" + name + "']";
                }
            }
        }
    },
    //执行某个Iframe中的函数 funName：函数名（参数1，参数2，...）  iframeName：iframe名字
    exFunction: function (funName, iframeName) {
        try {
            eval(admin.getIframeObj(iframeName) + "." + funName + ";");
            return true;
        } catch (e) {
            throw new Error(e.message);
            return false;
        }
    },
    //查找带回
    findBack: {
        close: function (row) {
            var findback = admin.getQueryString("findback");
            if (findback) {
                admin.setSession('findBackJson', row);
                admin.layer.close(admin.getLayerIframeIndex());
            }
        },
        open: function (content, title, callBack, w, h, isFull, parentIframeName) {
            admin.openWindow({
                content: content,
                title: title || "查找带回",
                IsFull: isFull,
                width: w ? w : "1200px",
                height: h ? h : "1200px",
                btn: false,
                success: function (layero) {
                    $(layero).find("iframe").attr("data-parentiframename", parentIframeName || admin.getIframeName());
                },
                end: function () {
                    var findBackJson = admin.getSession("findBackJson");
                    if (callBack && findBackJson && findBackJson.length > 0) {
                        callBack(findBackJson);
                    }
                    admin.setSession("findBackJson", []);
                }
            });
        }
    },
    //打开 iframe 窗口
    openWindow: function (options) {
        var defaults = {
            id: "",
            url: "",
            title: "系统窗口",
            width: "1200px",
            height: "1200px",
            btn: ["保存", "关闭"],
            btnClass: ['btn btn-primary', 'btn btn-danger'],
            shade: [0.1, "#fff"],
            parentIframeName: "",
            callBack: null,
            IsFull: false,
            success: null,
            end: null,
        };
        var options = $.extend({}, defaults, options);
        var _width = top.$("html").width() > parseInt(options.width.replace('px', '')) ? options.width : top.$("html").width() + 'px';
        var _height = top.$("html").height() > parseInt(options.height.replace('px', '')) ? options.height : top.$("html").height() + 'px';

        var json = $.extend({}, {
            id: options.id,
            type: 2,
            shade: options.shade,
            title: options.title,
            fix: false,
            area: [_width, _height],
            content: options.url,
            shift: 0,
            btn: options.btn,
            btnClass: options.btnClass,
            maxmin: true,
            anim: 2,
            //zIndex: Lay.zIndex, //重点1
            success: options.success,
            yes: function () {
                options.callBack(options.id);
            }, cancel: function () {
                return true;
            }, end: function () {
                options.end();
            }
        }, options);

        var index = admin.layer.open(json);
        if (options.IsFull) {
            admin.layer.full(index);
        }

    },
    setPower: function () {
        try {
            if (PowerModel) {
                for (var item in PowerModel) {
                    if (!PowerModel[item]) {
                        $("*[data-power=" + item + "]").remove();
                    }
                }
            }
        } catch (e) {

        }
    }
};

//admin 框架初始化
admin.init();

// 对Date的扩展，将 Date 转化为指定格式的String
// 月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符， 
// 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字) 
// 例子： 
// (new Date()).Format("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423 
// (new Date()).Format("yyyy-M-d h:m:s.S")      ==> 2006-7-2 8:9:4.18 
Date.prototype.Format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}
