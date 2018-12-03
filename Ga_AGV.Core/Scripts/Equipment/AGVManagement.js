
$(function () {

    toastr.options = {
        showDuration: "300",                                      // 显示动画的时间
        hideDuration: "1300",                                     //  消失的动画时间
        timeOut: "1000",                                             //  自动关闭超时时间 
        extendedTimeOut: "1000",                                  //  加长展示时间
        showEasing: "swing",                                     //  显示时的动画缓冲方式
        hideEasing: "linear",                                       //   消失时的动画缓冲方式
        showMethod: "fadeIn",                                   //   显示时的动画方式
        hideMethod: "fadeOut"                                   //   消失时的动画方式
    };

    //1.初始化Table
    var oTable = new TableInit();
    oTable.Init();

    //查询
    $("#btn_query").click(function () {
        var rexNum = /^[0-9]*$/; //验证数字
        if (!rexNum.test($('#AGVNumber').val())) {
            toastr.error('AGV编号格式输入不正确');
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
                callback: function (result) {
                    if (result) {
                        $.ajax({
                            type: 'post',
                            url: '/api/agvlist/deletelist',
                            contentType: 'application/json',
                            data: JSON.stringify(data),
                            datatype: 'json',
                            success: function (res) {
                                if (res.Success) {
                                    toastr.success(res.Message);
                                    $("#tb_report").bootstrapTable('refresh');//刷新表格
                                }
                                else {
                                    toastr.error(res.Message);
                                }
                            },
                            error: function (e) {
                                toastr.error(e.responseText);
                            }
                        });
                    }
                }
            });

        } else {
            toastr.error("请选中一行后删除！");
        }

    });


    //编辑
    $("#btn_edit").click(function () {
        var a = $("#tb_report").bootstrapTable('getSelections');
        if (a.length == 1) {
            console.log(a[0]);
            $("#myModalLabel").text("修改");
            $("#myModal").find(".form-control").val("");
            $('#agvNum').val(a[0].agvNum);
            $('#agvSerialNum').val(a[0].agvSerialNum);
            $('#agvIp').val(a[0].agvIp);
            $('#agvPort').val(a[0].agvPort);
            $('#agvId').val(a[0].agvId);
            $('#myModal').modal('toggle');

        }
        else {
            toastr.error("请选中一行后修改！");
        }
    });

    //添加/编辑提交
    $('#btn_AGVsubmit').click(function () {
        if ($("#myModalLabel").text() == "新增") {
            AGVedit('/api/agvlist/Addagv');
        } else {
            AGVedit('/api/agvlist/editagv');
        }
    });

    //编辑/添加
    function AGVedit(url) {
        if (!Verify())
            return;

        $.ajax({
            type: 'post',
            url: url,
            contentType: 'application/json',
            data: JSON.stringify({ "agvId": $('#agvId').val(), "agvNum": $('#agvNum').val(), "agvIp": $('#agvIp').val(), "agvPort": $('#agvPort').val(), "agvSerialNum": $('#agvSerialNum').val() }),
            datatype: 'json',
            success: function (res) {
                if (res.Success) {
                    $('#myModal').modal('toggle'); //关闭Modal窗口
                    toastr.success(res.Message)
                    $("#tb_report").bootstrapTable('refresh');//刷新表格
                }
                else {
                    toastr.error(res.Message);
                }
            },
            error: function (e) {
                toastr.error(e.responseText);
            }
        });
    }

    //表单验证
    function Verify() {
        if ($('#agvNum').val() == "" || $('#agvIp').val() == "" || $('#agvPort').val() == "" || $('#agvSerialNum').val() == "") {
            toastr.error('必填项不能为空！！！');
            return false;
        }
        var rexip = /^\d+\.\d+\.\d+\.\d+$/;//验证IP
        var rexNum = /^[0-9]*$/; //验证数字
        if (!rexip.test($('#agvIp').val())) {
            toastr.error('IP地址格式不正确');
            return false;
        }
        if (!rexNum.test($('#agvNum').val()) || !rexNum.test($('#agvPort').val())) {
            toastr.error('AGV编号或端口号格式输入错误');
            return false;
        }
        return true;
    }

});

var TableInit = function () {
    var oTableInit = new Object();
    //初始化Table
    oTableInit.Init = function () {
        $('#tb_report').bootstrapTable({
            url: '/api/agvlist/agv',             //请求后台的URL（*）
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
            search: false,                       //是否显示表格搜索，此搜索是客户端搜索，不会进服务端
            strictSearch: true,
            showColumns: true,                  //是否显示所有的列
            showRefresh: true,                  //是否显示刷新按钮
            minimumCountColumns: 2,             //最少允许的列数
            clickToSelect: true,                //是否启用点击选中行
            //height: 500,                      //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
            uniqueId: "agvId",                     //每一行的唯一标识，一般为主键列
            showToggle: true,                   //是否显示详细视图和列表视图的切换按钮
            cardView: false,                    //是否显示详细视图
            detailView: false,

            columns: [
                {
                    checkbox: true
                },
                {
                    field: 'agvNum',
                    title: 'AGV编号',
                    align: 'center'
                }, {
                    field: 'agvIp',
                    title: 'IP',
                    align: 'center'
                }, {
                    field: 'agvPort',
                    title: '端口号',
                    align: 'center'
                }, {
                    field: 'agvOnLineTime',
                    title: '最近上线时间',
                    align: 'center'
                }, {
                    field: 'agvOffLineTime',
                    title: '最近离线时间',
                    align: 'center'
                },
                {
                    field: "agvId", title: "操作", width: 200, align: "center", formatter: function (value, row, index) {
                        agvInfo[index] = row;
                        var strHtml = '<a class="btn btn-xs btn-primary btn-update"  onclick="control(' + index + ')"><i class="fa fa-bolt"></i>&nbsp;控制</a>&nbsp;&nbsp;' +
                            '<a class="btn btn-xs btn-danger btn-remove" onclick="agvdelete(' + row.agvId + ')"><i class="fa fa-trash-o"></i>&nbsp;删除</a>';
                        return strHtml;
                    }, edit: false
                }
            ]
        });
    };

    //得到查询的参数
    oTableInit.queryParams = function (params) {
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            limit: params.limit,   //页面大小
            offset: params.offset,  //页码
            agvNum: $('#AGVNumber').val()
        };
        return temp;
    };
    return oTableInit;
};

var agvInfo = new Array();//AGV信息
var Index;//AGV下标
var iCount; //状态更新定时器

function control(agvIndex) {
    $("#AGVtitle").text("AGV控制");
    var agvNu;
    if (agvInfo[agvIndex].agvNum < 10) {
        agvNu = "00" + agvInfo[agvIndex].agvNum;
    } else if (agvInfo[agvIndex].agvNum < 99 && agvInfo[agvIndex].agvNum >= 10) {
        agvNu = "0" + agvInfo[agvIndex].agvNum;
    } else {
        agvNu = agvInfo[agvIndex].agvNum;
    }
    $('#agvNums').html(agvNu);
    $('#agvNu').html(agvInfo[agvIndex].agvNum + '号');
    Index = agvIndex;
    agvstatic();
    $('#AGVControl').modal();
    clearInterval(iCount);
    iCount= setInterval(function () { agvstatic(); },500);
}

//停止更新状态
function agvclose() {
    clearInterval(iCount);
}

//查询AGV状态
function agvstatic() {
    $.ajax({
        type: 'post',
        url: '/api/agvlist/agvState',
        contentType: 'application/json',
        data: JSON.stringify(agvInfo[Index]),
        datatype: 'json',
        success: function (res) {
            if (res.Success) {
                if (res.Message != "正常") {
                    $('#Message').css("color", "red");
                } else {
                    $('#Message').css("color", "#139656");
                }
                if (res.agvIsRunning != "在线") {
                    $('#agvIsRunning').css("color", "red");
                    $("#agvimg").attr("src", "/image/agv-off.gif");
                    $("#agvsNumer").html("离线");
                } else {
                    $('#agvIsRunning').css("color", "#139656");
                    $("#agvimg").attr("src", "/image/agv-on.gif");
                    $("#agvsNumer").html("在线");
                }
                $('#agvFirmware').html(res.agvFirmware);
                $('#qrcode').html(res.qrcode);
                $('#agvIsRunning').html(res.agvIsRunning);
                $('#PBS').html(res.PBS);
                $('#agvHolder').html(res.agvHolder);

                $('#Message').html(res.Message);
                var point = chart1.series[0].points[0];
                point.update(res.agvspeed);
                var point2 = chart2.series[0].points[0];
                point2.update(res.voltage);
            }
        },
        error: function (e) {
            toastr.error(e.responseText);
            agvclose();
        }
    });
}


//启动AGV
function agvstarts() {
    agv('/api/agvlist/agvstart', agvInfo[Index]);
}

//停止AGV
function agvstop() {
    agv('/api/agvlist/agvstop', agvInfo[Index]);
}

//云台上升
function DeckRise() {
    agv('/api/agvlist/DeckRise', agvInfo[Index]);
}

//云台下降
function DeckDecline() {
    agv('/api/agvlist/DeckDecline', agvInfo[Index]);
}

//左转
function LeftTurn() {
    agv('/api/agvlist/LeftTurn', agvInfo[Index]);
}

//右转
function rightTurn() {
    agv('/api/agvlist/rightTurn', agvInfo[Index]);
}

//更改速度
function UpdateSpeed() {
    agvCom('/api/agvlist/UpdateSpeed', agvInfo[Index], $('#speed').val());
}

//更改速度
function UpdatePBS() {
    agvCom('/api/agvlist/UpdatePBS', agvInfo[Index], $('#PBS').val());
}

//急停
function Scram() {
    agv('/api/agvlist/Scram', agvInfo[Index]);
}

//手动自动
function ManualSelfMotion() {
    agv('/api/agvlist/ManualSelfMotion', agvInfo[Index]);
}

//复位
function Restoration() {
    agv('/api/agvlist/Restoration', agvInfo[Index]);
}


//AGV控制
function agv(agvurl, agvdata) {
    $.ajax({
        type: 'post',
        url: agvurl,
        contentType: 'application/json',
        data: JSON.stringify(agvdata),
        datatype: 'json',
        success: function (res) {
            if (res.Success) {
                toastr.success(res.Message);
            }
            else {
                toastr.error(res.Message);
            }
        },
        error: function (e) {
            toastr.error(e.responseText);
        }
    });
}

//AGV控制
function agvCom(agvurl, agvdata, agvValue) {
    $.ajax({
        type: 'post',
        url: agvurl,
        contentType: 'application/json',
        data: JSON.stringify({ "Data": agvdata, "Value": agvValue }),
        datatype: 'json',
        success: function (res) {
            if (res.Success) {
                toastr.success(res.Message);
            }
            else {
                toastr.error(res.Message);
            }
        },
        error: function (e) {
            toastr.error(e.responseText);
        }
    });
}


//删除AGV
function agvdelete(id) {
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
        callback: function (result) {
            if (result) {
                $.ajax({
                    type: 'post',
                    url: '/api/agvlist/deleteagv',
                    contentType: 'application/json',
                    data: JSON.stringify({ "agvId": id }),
                    datatype: 'json',
                    success: function (res) {
                        if (res.Success) {
                            toastr.success(res.Message);
                            $("#tb_report").bootstrapTable('refresh');//刷新表格
                        }
                        else {
                            toastr.error(res.Message);
                        }
                    },
                    error: function (e) {
                        toastr.error(e.responseText);
                    }
                });
            }
        }
    });
}

// 公共配置
Highcharts.setOptions({
    chart: {
        type: 'solidgauge'
    },
    title: null,
    pane: {
        center: ['50%', '85%'],
        size: '140%',
        startAngle: -90,
        endAngle: 90,
        background: {
            backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || '#EEE',
            innerRadius: '60%',
            outerRadius: '100%',
            shape: 'arc'
        }
    },
    tooltip: {
        enabled: false
    },
    yAxis: {
        stops: [
            [0.1, '#55BF3B'], // green
            [0.5, '#DDDF0D'], // yellow
            [0.9, '#DF5353'] // red
        ],
        lineWidth: 0,
        minorTickInterval: null,
        tickPixelInterval: 400,
        tickWidth: 0,
        title: {
            y: -70
        },
        labels: {
            y: 16
        }
    },
    plotOptions: {
        solidgauge: {
            dataLabels: {
                y: 5,
                borderWidth: 0,
                useHTML: true
            }
        }
    }
});
// 速度仪表
var chart1 = Highcharts.chart('container-speed', {
    yAxis: {
        min: 0,
        max: 100,
        title: {
            text: '速度'
        }
    },
    credits: {
        enabled: false
    },
    series: [{
        name: '速度',
        data: [80],
        dataLabels: {
            format: '<div style="text-align:center"><span style="font-size:25px;color:' +
                ((Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black') + '">{y}</span><br/>' +
                '<span style="font-size:12px;color:silver">m/min</span></div>'
        },
        tooltip: {
            valueSuffix: ' km/h'
        }
    }]
});
// 电量仪表
var chart2 = Highcharts.chart('container-rpm', {
    yAxis: {
        min: 0,
        max: 100,
        title: {
            text: '电压'
        }
    },
    series: [{
        name: '电压',
        data: [1],
        dataLabels: {
            format: '<div style="text-align:center"><span style="font-size:25px;color:' +
                ((Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black') + '">{y:.1f}</span><br/>' +
                '<span style="font-size:12px;color:silver">v</span></div>'
        },
        tooltip: {
            valueSuffix: ' revolutions/min'
        }
    }]
});