//$(function () {
//    //输入文本，隐藏错误提示
//    $(":input").on('keypress', function () {
//        $(".text-message").hide();
//    });
//    $('input:checkbox').click(function () {
//        this.blur();
//        this.focus();
//        $(".text-message").hide();
//    });
//    $("select").on('change', function () {
//        $(".text-message").hide();
//    });
//});

//Ajax.BeginForm表单回调函数，ajax回调函数
function ajaxSuccess(result) {
    var message = result.message;
    var status = result.status;
    if (message) {
        $('.text-message').html(message).show().delay(3000).hide(0);
    } else {
        if (status === "success") {
            $('.text-message').html('操作成功！').show().delay(3000).hide(0);
            parent.$.fancybox.close();
            return;
        }
        if (status === "error") {
            $('.text-message').html('操作失败！').show().delay(3000).hide(0);
            return;
        }
        $('.text-message').html('操作异常！').show().delay(3000).hide(0);
        return;
    }
    if (status === "success") {
        parent.$.fancybox.close();
    }
    return;
}
function ajaxError(obj) {
    $('.text-message').html('操作失败！');
    $(".text-message").show().delay(3000).hide(0);
}