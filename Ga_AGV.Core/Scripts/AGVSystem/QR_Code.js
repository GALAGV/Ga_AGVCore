$(function () {
    //1.初始化Table
    var oTable = new TableInit();
    oTable.Init();

    //查询
    $("#btn_query").click(function () {
        var rexNum = /^[0-9]*$/; //数字
        if (!rexNum.test($('#query_qrId').val())) {
            toastr.error('格式输入不正确');
            return;
        }
        $("#tb_report").bootstrapTable('destroy'); //先要将table销毁，否则会保留上次加载的内容
        oTable.Init();
        $("#tb_report").bootstrapTable('refresh');
    });

    //新增
    $("#btn_add").click(function () {
        $("#myModalLabel").text("新增");
        $("#myModal").find(".form-control").val("");
        $('#myModal').modal();
    });

    //修改
    $("#btn_edit").click(function () {
        var row = $("#tb_report").bootstrapTable('getSelections');
        if (row.length == 1) {
            $("#myModalLabel").text("修改");
            $("#txt_qrId").val(row[0].qrId);
            $("#txt_qrInfo").val(row[0].qrInfo);
            $("#txt_qrX").val(row[0].qrX);
            $("#txt_qrY").val(row[0].qrY);
            $("#txt_qrStatus").val(row[0].qrStatus);
            $("#txt_qrRemark").val(row[0].qrRemark);
            $('#myModal').modal();
        }
        else {
            toastr.error("请选中一行后修改！");
        }
    });

    //批量删除
    $('#btn_delete').click(function () {
        var data = $("#tb_report").bootstrapTable('getSelections');
        if (data.length > 0) {
            bootbox.confirm({
                message: "确认删除吗？",
                buttons: {
                    confirm: {
                        label: '确认',
                        className: 'btn-primary'
                    },
                    cancel: {
                        label: '取消',
                        className: 'btn-default'
                    }
                },
                callback: function () {
                    submit("/api/AGVSystem/DelQRCode", data);
                }
            });
        } else {
            toastr.error("请选中一行后删除！");
        }
    });

    //模态窗体 提交
    $("#btn_submit").click(function () {
        var data = {
            qrId: $("#txt_qrId").val(),
            qrInfo: $("#txt_qrInfo").val(),
            qrX: $("#txt_qrX").val(),
            qrY: $("#txt_qrY").val(),
            qrStatus: $("#txt_qrStatus").val(),
            qrRemark: $("#txt_qrRemark").val()
        };
        console.log("QR_Code" + $("#myModalLabel").text());
        if ($("#myModalLabel").text() == "新增") {
            if (!Verify())
                return;
            submit("/api/AGVSystem/AddQRCode", data);
        } else if ($("#myModalLabel").text() == "修改") {
            if (!Verify())
                return;
            submit("/api/AGVSystem/UpQRCode", data);
        }
    });

    //提交方法
    function submit(url, data) {
        $.ajax({
            url: url,
            type: "post",
            data: JSON.stringify(data),
            contentType: 'application/json',
            success: function (data) {
                if (data.Success) {
                    $('#myModal').modal('toggle'); //关闭模态窗体
                    $("#tb_report").bootstrapTable('refresh');
                    toastr.success(data.Message);
                }
                else {
                    $("#tb_report").bootstrapTable('refresh');
                    toastr.error(data.Message);
                }
            },
            error: function (e) {
                toastr.error(e.Message);
            }
        });
    }

    //验证
    function Verify() {
        if ($('#txt_qrInfo').val() == "" || $('#txt_qrX').val() == "" || $('#txt_qrY').val() == "" || $('#txt_qrStatus').val() == "") {
            toastr.error('不能为空！！！');
            return false;
        }
        var rexNum = /^[0-9]*$/; //数字
        console.log($('#txt_qrX').val()); console.log($('#txt_qrY').val());
        if (!rexNum.test($('#txt_qrX').val() || $('#txt_qrY').val())) {
            toastr.error('坐标值输入不正确！！！');
            return false;
        }
        return true;
    }
});

//function operateFormatter(value, row, index) {
//    return [
//        '<button type="button" class="RoleOfdelete btn btn-primary btn-sm" style="margin-right:15px;">修改</button>',
//        '<button type="button" class="RoleOfedit btn btn-primary btn-sm" style="margin-right:15px;">删除</button>'
//    ].join('');
//};

//window.operateEvents = {
//    'click .RoleOfdelete': function (e, value, row, index) {
//        $("#myModalLabel").text("修改");
//        $('#myModal').modal();
//        $("#txt_qrId").val(row.qrId);
//        $("#txt_qrInfo").val(row.qrInfo);
//        $("#txt_qrX").val(row.qrX);
//        $("#txt_qrY").val(row.qrY);
//        $("#txt_qrStatus").val(row.qrStatus);
//        $("#txt_qrRemark").val(row.qrRemark);
//    },
//    'click .RoleOfedit': function (e, value, row, index) {
//        $("#myModalLabel").text("删除");
//        $('#myModal').modal();
//        $("#txt_qrId").val(row.qrId);
//        $("#txt_qrInfo").val(row.qrInfo);
//        $("#txt_qrX").val(row.qrX);
//        $("#txt_qrY").val(row.qrY);
//        $("#txt_qrStatus").val(row.qrStatus);
//        $("#txt_qrRemark").val(row.qrRemark);
//    }
//};

var TableInit = function () {
    var oTableInit = new Object();
    //初始化Table
    oTableInit.Init = function () {
        $('#tb_report').bootstrapTable({
            url: '/api/AGVSystem/QRCodeShow',            //请求后台的URL（*）
            method: 'get',                      //请求方式（*）
            toolbar: '#toolbar',                //工具按钮用哪个容器
            striped: true,                      //是否显示行间隔色
            cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
            pagination: true,                   //是否显示分页（*）
            sortable: false,                    //是否启用排序
            sortOrder: "asc",                   //排序方式
            queryParams: oTableInit.queryParams,//传递参数（*）
            sidePagination: "server",           //分页方式：client客户端分页，server服务端分页（*）
            pageNumber: 1,                      //初始化加载第一页，默认第一页
            pageSize: 10,                       //每页的记录行数（*）
            pageList: [10, 25, 50, 100, 500],   //可供选择的每页的行数（*）
            search: false,                       //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
            strictSearch: true,
            showColumns: true,                  //是否显示所有的列
            showRefresh: true,                  //是否显示刷新按钮
            minimumCountColumns: 2,             //最少允许的列数
            clickToSelect: true,                //是否启用点击选中行
            //height: 500,                      //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
            uniqueId: "agvId",                  //每一行的唯一标识，一般为主键列
            showToggle: true,                   //是否显示详细视图和列表视图的切换按钮
            cardView: false,                    //是否显示详细视图
            detailView: false,

            columns: [{
                checkbox: true
            }, {
                field: 'qrId',
                title: '二维码编号',
                align: 'center'
            }, {
                field: 'qrInfo',
                title: '信息',
                align: 'center'
            }, {
                field: 'qrX',
                title: 'X轴',
                align: 'center'
            }, {
                field: 'qrY',
                title: 'Y轴',
                align: 'center'
            }, {
                field: 'qrStatus',
                title: '状态',
                align: 'center'
            }, {
                field: 'qrRemark',
                title: '备注',
                align: 'center'
            }
                //,{
                //    field: "operate",
                //    title: "操作",
                //    width: 200,
                //    align: "center",
                //    events: operateEvents,
                //    formatter: operateFormatter
                //    }
            ]
        });
    };
    //得到查询的参数
    oTableInit.queryParams = function (params) {
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            limit: params.limit,   //页面大小
            offset: params.offset,  //页码
            qrID: $("#query_qrId").val(),
            qrStatus: $("#query_qrStatus").val()
        };
        return temp;
    };
    return oTableInit;
};