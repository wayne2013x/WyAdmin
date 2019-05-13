





var adminList = {
    dom: {
        panelSearch: $('#panelSearch'),
        btnAdd: $('*[name=add]'),
        btnEdit: $('*[name=edit]'),
        btnDelete: $('*[name=delete]'),
        findBack: admin.getQueryString("findback"),
    },
    bootStrapTableObj: null,
    bootStrapTableIndex: 0,
    bootStrapTable: function (options) {
        var defaults = {
            dom: "#bootStrapTable",
            height: $(window).height() - 90,
            striped: true,//隔行 渐变色
            method: "post",
            classes: "table table-hover",
            url: "",
            idField: "_ukid",
            contentType: "application/x-www-form-urlencoded", //"multipart/form-data",//"application/json",
            dataType: "json",
            pageSize: 25,
            pageNumber: 1,
            pageList: [20, 30, 100, 1000],
            sidePagination: "server",
            paginationPreText: "上页",
            paginationNextText: "下页",
            pagination: true,
            showColumns: false,
            detailView: false,
            clickToSelect: true,
            sortable: true,//是否启用排序
            silentSort: true,
            sortStable: true,
            //sortName: "_ukid",//定义排序列,通过url方式获取数据填写字段名，否则填写下标 
            sortOrder: "asc",//定义排序方式 'asc' 或者 'desc'
            maintainSelected: true,
            columns: [],
            data: [],
            queryParams: null,
            onClickRow: null,
            onDblClickRow: null,
            onCheck: null,
            onCheckAll: null,
            onLoadSuccess: null,
        };
        var options = $.extend({}, defaults, options);

        adminList.bootStrapTableObj = $(options.dom);
        //拼接 一下表格的默认 列
        var columns_def = [{
            field: 'number',
            title: '',
            width: '35px',
            align: 'center',
            formatter: function (value, row, index) {
                var page = adminList.bootStrapTableObj.bootstrapTable("getOptions");
                return page.pageSize * (page.pageNumber - 1) + index + 1;
                //return index + 1;
            }
        }, {
            checkbox: true,
            field: 'ck',
        }];
        for (var i = 0; i < options.columns.length; i++) {
            columns_def.push(options.columns[i]);
        }
        options.columns = columns_def;

        var jsonConfig = {
            height: options.height,
            striped: options.striped,
            method: options.method,
            contentType: options.contentType,
            dataType: options.dataType,
            classes: options.classes,
            url: options.url,
            columns: options.columns,
            sidePagination: options.sidePagination,
            paginationPreText: options.paginationPreText,
            paginationNextText: options.paginationNextText,
            data: options.data,
            queryParamsType: 'limit_123',
            undefinedText: '',
            idField: options.idField,
            pageSize: options.pageSize,
            pageNumber: options.pageNumber,
            pageList: options.pageList,
            pagination: options.pagination,
            showColumns: options.showColumns,
            detailView: options.detailView,
            clickToSelect: options.clickToSelect,
            sortable: options.sortable,
            silentSort: options.silentSort,
            sortName: options.sortName,
            sortOrder: options.sortOrder,
            maintainSelected: options.maintainSelected,
            onLoadSuccess: options.onLoadSuccess,
            responseHandler: function (res) {
                //res.total = res.records;//这里为了兼容以前的 表格插件  
                return res;
            },
            queryParams: function (params) {
                params = {
                    rows: params.pageSize,   //页面大小
                    page: params.pageNumber,  //页码
                    sortName: params.sortName,  //排序列名
                    sortOrder: params.sortOrder//排位命令（desc，asc）
                };
                //将检索的信息放入进去
                var datas = adminList.dom.panelSearch.find('form').serialize().split("&");
                for (var i = 0; i < datas.length; i++) {
                    params[datas[i].split("=")[0]] = decodeURI(datas[i].split("=")[1]);
                }
                if (options.queryParams) {
                    params = options.queryParams(params);
                    //var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
                    //    pageSize: params.limit,   //页面大小
                    //    pageNumber: params.pageNumber,  //页码
                    //    minSize: $("#leftLabel").val(),
                    //    maxSize: $("#rightLabel").val(),
                    //    minPrice: $("#priceleftLabel").val(),
                    //    maxPrice: $("#pricerightLabel").val(),
                    //    Cut: Cut,
                    //    Color: Color,
                    //    Clarity: Clarity,
                    //    sort: params.sort,  //排序列名
                    //    sortOrder: params.order//排位命令（desc，asc）
                    //};
                }
                //console.log(params);
                return params;
            },
            onClickRow: function (row, dom, field) {
                adminList.bootStrapTableObj.bootstrapTable('uncheckAll');
                if (options.onClickRow != null) {
                    options.onClickRow(row, dom, field);
                } else {
                    adminList.bootStrapTableIndex = dom.data('index'); //记录选中的id 用来 防止 info页保存后 列表刷新不能记住选中的数据
                    adminList.bootStrapTableIndex = row._ukid;
                    adminList.btnControl();
                }
            },
            onDblClickRow: function (row, dom, field) {

                if (options.onDblClickRow && adminList.dom.findBack) {
                    options.onDblClickRow(row, dom, field);
                } else {
                    if (adminList.dom.findBack) {
                        adminList.findBack();
                    }
                }
            },
            onCheck: function (row, dom) {//选中复选框
                if (options.onCheck) options.onCheck(row);
                adminList.btnControl();
            },
            onUncheck: function () {//取消复选框
                adminList.btnControl();
            },
            onCheckAll: function (row) {//选中所有复选框
                if (options.onCheckAll) options.onCheckAll(row);
                adminList.btnControl();
            },
            onUncheckAll: function () {//取消选中所有复选框
                adminList.btnControl();
            },
            onLoadSuccess: function () {
                //加载完成 检测一下是否有选中的行id 如果有将行 设置为选中状态
                if (adminList.bootStrapTableIndex) {
                    adminList.bootStrapTableObj.bootstrapTable("checkBy", { field: "_ukid", values: [adminList.bootStrapTableIndex] });
                }
                if (options.onLoadSuccess) options.onLoadSuccess();
            }
        };
        //生成表格
        adminList.bootStrapTableObj.bootstrapTable(jsonConfig);

        // Add responsive   表格自适应
        $(window).bind('resize', function () {
            adminList.bootStrapTableObj.bootstrapTable('resetView', { height: $(window).height() - 90 });
        });

        return adminList.bootStrapTableObj;
    },
    //得到 用户选中得行
    selectRows: function () {
        var rows = adminList.bootStrapTableObj.bootstrapTable('getSelections');
        return rows;
    },
    //按钮控制
    btnControl: function () {
        var rows = adminList.selectRows();
        if (rows.length > 0) {

            if (rows.length == 1) {
                adminList.dom.btnEdit.removeAttr('disabled');
                adminList.dom.btnDelete.removeAttr('disabled');
            } else {
                adminList.dom.btnEdit.attr('disabled', true);
                adminList.dom.btnDelete.removeAttr('disabled');
            }

        } else {
            adminList.dom.btnEdit.attr('disabled', true);
            adminList.dom.btnDelete.attr('disabled', true);
        }
    },
    //查询面板
    panelSearch: function () {
        adminList.dom.panelSearch.toggle("fast");
    },
    //检索
    search: function () {
        adminList.refresh();
    },
    //重置检索信息
    resetSearch: function () {
        adminList.dom.panelSearch.find("form")[0].reset();
        adminList.refresh();
    },
    //刷新
    refresh: function (data) {
        //refresh 刷新表格
        if (data) {
            adminList.bootStrapTableObj.bootstrapTable('refresh', {
                query: data
            });
        } else {
            adminList.bootStrapTableObj.bootstrapTable('refresh');
        }
        //检查一下 按钮控制状态
        setTimeout(function () {
            adminList.btnControl();
        }, 300);
    },
    //打开 表单页
    form: function (options) {
        options.success = function (layero) {
            top.$(layero).find("iframe").attr("data-parentiframename", options.parentIframeName);
        }
        admin.openWindow(options);
    },
    //删除数据
    delete: function (url, callBack) {
        var rows = adminList.selectRows();
        if (rows.length == 0) {
            return admin.msg("请选择要移除的数据");
        }
        admin.confirm("确认删除?", function (index) {
            var json = [];
            for (var i = 0; i < rows.length; i++) {
                json.push(rows[i]._ukid);
            }
            admin.ajax({
                url: url,
                data: { Ids: JSON.stringify(json) },
                success: function (r) {
                    if (r.status == 1) {
                        if (callBack) {
                            callBack();
                        }
                        admin.msg("操作成功！");
                        admin.layer.close(index);
                    }
                }
            });
        }, function () {

        });
    },
    //导出excel
    exportExcel: function (url, data) {
        $(event.srcElement).attr("href", url + "?" + adminList.dom.panelSearch.find('form').serialize() + (data ? data : ""));
    },
    //打印
    print: function (url, data) {
        $(event.srcElement).attr("href", url + "?" + adminList.dom.panelSearch.find('form').serialize() + (data ? data : ""));
    },
    findBack: function () {
        var value = adminList.dom.findBack;
        admin.findBack.close(adminList.selectRows());
    }









};
