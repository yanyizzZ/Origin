
var daterangepicker_zh_CN = {
    "format": "YYYY-MM-DD",
    "separator": " - ",
    "applyLabel": "应用",
    "cancelLabel": "取消",
    "fromLabel": "从",
    "toLabel": "到",
    "customRangeLabel": "自定义",
    "weekLabel": "周",
    "daysOfWeek": [
        "日",
        "一",
        "二",
        "三",
        "四",
        "五",
        "六"
    ],
    "monthNames": [
        "一月",
        "二月",
        "三月",
        "四月",
        "五月",
        "六月",
        "七月",
        "八月",
        "九月",
        "十月",
        "十一月",
        "十二月"
    ],
    "firstDay": 1
};


var daterangepickerWithTime_zh_CN = {
    "format": "YYYY-MM-DD HH:mm",
    "separator": " - ",
    "applyLabel": "应用",
    "cancelLabel": "取消",
    "fromLabel": "从",
    "toLabel": "到",
    "customRangeLabel": "自定义",
    "weekLabel": "周",
    "daysOfWeek": [
        "日",
        "一",
        "二",
        "三",
        "四",
        "五",
        "六"
    ],
    "monthNames": [
        "一月",
        "二月",
        "三月",
        "四月",
        "五月",
        "六月",
        "七月",
        "八月",
        "九月",
        "十月",
        "十一月",
        "十二月"
    ],
    "firstDay": 1
};

var default_ranges_zh_CN = {
    '今天': [moment(), moment()],
    '昨天': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
    '最近一周': [moment().subtract(6, 'days'), moment()],
    '最近一月': [moment().subtract(1, 'month'), moment()],
    '这个月': [moment().startOf('month'), moment().endOf('month')],
    '上个月': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')],
    '今年': [moment().startOf('year'), moment().endOf('year')],
    '去年': [moment().subtract(1, 'year').startOf('year'), moment().subtract(1, 'year').endOf('year')]
}