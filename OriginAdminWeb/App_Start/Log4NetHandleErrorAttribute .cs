using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OriginAdminWeb
{
    public class Log4NetHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            /* 调用基类的OnException方法，实现基础的功能。
            * 如果要完全的自定义，就不需要调用基类的方法
            */
            base.OnException(filterContext);

            /* 此处可进行记录错误日志，发送错误通知等操作
            * 通过Exception对象和HttpException对象可获取相关异常信息。
            * Exception exception = filterContext.Exception;
            * HttpException httpException = new HttpException(null, exception);
            */

            log4net.LogManager.GetLogger("loginfo").Error(filterContext.HttpContext.Request.Url.ToString(), filterContext.Exception);
        }
    }
}