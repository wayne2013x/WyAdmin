
//表单类

var adminForm = {
    createFormData: function (Data) {
        // FormData 对象
        var form = new FormData();
        var data = ko.toJS(model);
        for (item in data) {
            form.append(item, data[item]);// 可以增加表单数据
        }
        for (item in Data) {
            form.append(item, Data[item]);
        }
        return form;
    },
    load: function (options) {
        var defaults = {
            KeyId: "",
            url: "",
            data: null,
            success: null,
            callBack: null,
            title: null
        };
        var options = $.extend({}, defaults, options);

        var winIndex = admin.getLayerIframeIndex();
        if (!options.title) {
            if (options.KeyId)
                top.$("#layui-layer" + winIndex + ">.layui-layer-title").text(options.title ? options.title : "修改");
            else
                top.$("#layui-layer" + winIndex + ">.layui-layer-title").text(options.title ? options.title : "添加");
        } else {
            top.$("#layui-layer" + winIndex + ">.layui-layer-title").text(options.title);
        }

        //初始化表单
        admin.ajax({
            url: options.url,
            data: options.data ? options.data : { ID: options.KeyId },
            loading: true,
            success: function (r) {
                if (r.status == 1) {
                    if (options.success) {
                        options.success(r);
                    } else {
                        admin.koMapping(r, model, "model");
                    }
                    if (options.callBack)
                        options.callBack(r);
                }
            }
        });


    },
    Save: function (options) {
        var defaults = {
            isClose: false,
            url: "",
            data: null,
            success: null,
            callBack: null,
            msg: null,
            isupfile: false,
        };
        var options = $.extend({}, defaults, options);
        admin.ajax({
            url: options.url,
            data: (options.data ? options.data : ko.toJS(model)),
            isupfile: options.isupfile,
            loading: true,
            success: function (r) {
                if (r.status == 1) {
                    if (options.success) {
                        options.success(r);
                    }
                    else {
                        admin.msg(options.msg ? options.msg : "操作成功！", "成功");
                        adminForm.refreshParent(options.isClose);
                        if (!options.isClose) adminForm.resetUrl(r);
                    }
                    if (options.callBack != null)
                        options.callBack(r);
                }
            }
        });


    },
    //刷新 父级
    refreshParent: function (IsClose) {
        var parentName = admin.getParentFrameName();
        if (!top.window.frames[parentName]) {
            parentName = admin.getWinIframe();
        }
        try {
            if (parentName) {
                top.window.frames[parentName].App.Refresh();
            }
        } catch (e) {

        }

        if (IsClose) {
            admin.layer.close(admin.getLayerIframeIndex());
        }
    },
    //刷新重置URL
    resetUrl: function (r) {
        //开启遮罩层
        admin.loading.start();
        var url = window.location.href;
        if (r) {
            if (url.indexOf('?ID') == -1 && url.indexOf('&ID') == -1) {
                if (url.indexOf('?') != -1) {
                    url = url + '&ID=' + r.ID + '';
                }
                else {
                    url = url + '?ID=' + r.ID + '';
                }
            }
            else {
                var obj = adminForm.getRequest();
                if (!obj["ID"]) {
                    url = url.replace("ID=", "ID=" + r.ID);
                }
            }
        }
        else {
            var obj = adminForm.getRequest();
            if (obj) {
                url = url.substring(0, url.indexOf('?'));
                url = url + '?1=1';
                for (var item in obj) {
                    if (item != 'ID' && item != '1') {
                        url = url + '&' + item + '=' + obj[item] + '';
                    }
                }
            }
        }
        top.$("iframe[name=" + admin.getIframeName() + "]").attr("src", url).on("load", function () {
            admin.loading.end();
        });
        return url;
    },
    getRequest: function () {
        var url = location.search; //获取url中"?"符后的字串
        var theRequest = new Object();
        if (url.indexOf("?") != -1) {
            var str = url.substr(1);
            strs = str.split("&");
            for (var i = 0; i < strs.length; i++) {
                theRequest[strs[i].split("=")[0]] = unescape(strs[i].split("=")[1]);
            }
        }
        return theRequest;
    }




};